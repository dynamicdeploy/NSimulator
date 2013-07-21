#region

using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public class PCAPPacketDumper_Test {
        private const int BACKBONE_SIZE = 4;
        private const string DIRECTORY = "dir";
        public TestContext TestContext;

        private BroadcastBackboneMock broadcast_backbone;
        private IClock clock;
        private PCAPPacketDumper dumper;
        private UnicastBackboneMock unicast_backbone;

        [TestInitialize]
        public void Init () {
            this.clock = new ClockMock (100);
            this.dumper = new PCAPPacketDumper (this.clock);

            this.broadcast_backbone = new BroadcastBackboneMock ("1", BACKBONE_SIZE, ulong.MaxValue, this.clock);
            for (var i = 0; i < BACKBONE_SIZE; ++i)
                new InterfaceMock ().SetBackbone (this.broadcast_backbone);

            this.unicast_backbone = new UnicastBackboneMock ("2", BACKBONE_SIZE, ulong.MaxValue, this.clock);
            for (var i = 0; i < BACKBONE_SIZE; ++i)
                new InterfaceMock ().SetBackbone (this.unicast_backbone);
            this.unicast_backbone.SetPCAPType (PCAPNetworkTypes.LINKTYPE_C_HDLC);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckArgumentNullInConstructor () {
            new PCAPPacketDumper (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectArgumentInConstructor () {
            new PCAPPacketDumper (this.clock, "*");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullBackbone1 () {
            this.dumper.AttachToBackbone (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullBackbone2 () {
            this.dumper.AttachToBackbone (null, new MemoryStream (new byte[1024]));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullStream () {
            this.dumper.AttachToBackbone (this.broadcast_backbone, null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullInDetach () {
            this.dumper.DetachFromBackbone (null);
        }

        private void CheckFileHeader (BinaryReader reader, PCAPNetworkTypes type) {
            Assert.AreEqual (0xA1B2C3D4, reader.ReadUInt32 ());
            Assert.AreEqual <UInt16> (2, reader.ReadUInt16 ());
            Assert.AreEqual <UInt16> (4, reader.ReadUInt16 ());
            Assert.AreEqual (0, reader.ReadInt32 ());
            Assert.AreEqual <UInt32> (0, reader.ReadUInt32 ());
            Assert.AreEqual <UInt32> (65535, reader.ReadUInt32 ());
            Assert.AreEqual ((UInt32) type, reader.ReadUInt32 ());
        }

        private void CheckPacketHeader (BinaryReader reader, int tick, UInt32 length) {
            Assert.AreEqual ((UInt32) (tick * (int) this.clock.TickLength) / 1000, reader.ReadUInt32 ());
            Assert.AreEqual ((UInt32) (tick * (int) this.clock.TickLength) % 1000, reader.ReadUInt32 ());
            Assert.AreEqual (length, reader.ReadUInt32 ());
            Assert.AreEqual (length, reader.ReadUInt32 ());
        }

        private void CheckTransmitPacketAtBroadcast_Process () {
            this.clock.RegisterAction (1,
                                       () =>
                                       this.broadcast_backbone.SendPacket (new PacketMock (100, 1),
                                                                           this.broadcast_backbone [0]));
            this.clock.RegisterAction (3,
                                       () =>
                                       this.broadcast_backbone.SendPacket (new PacketMock (64, 7),
                                                                           this.broadcast_backbone [2]));
            this.clock.RegisterAction (4, () => this.clock.Dispose ());
            this.clock.Start ();
        }

        private void CheckTransmitPacketAtBroadcast_Check (byte [] buffer) {
            using (var memoryStream = new MemoryStream (buffer, false))
            using (var binaryReader = new BinaryReader (memoryStream)) {
                this.CheckFileHeader (binaryReader, this.broadcast_backbone.Type);

                for (var i = 0; i < 3; ++i) {
                    this.CheckPacketHeader (binaryReader, 2, 100);
                    AssertHelper.ArrayIsConstant <byte> (1, binaryReader.ReadBytes (100));
                }

                for (var i = 0; i < 3; ++i) {
                    this.CheckPacketHeader (binaryReader, 4, 64);
                    AssertHelper.ArrayIsConstant <byte> (7, binaryReader.ReadBytes (64));
                }

                AssertHelper.ArrayIsConstant <byte> (0,
                                                     binaryReader.ReadBytes (
                                                         (int)
                                                         (binaryReader.BaseStream.Length -
                                                          binaryReader.BaseStream.Position + 1)));
            }
        }

        [TestMethod]
        public void CheckTransmitPacketAtBroadcast_Stream () {
            var buffer = new byte[65536];

            using (var memoryStream = new MemoryStream (buffer)) {
                this.dumper.AttachToBackbone (this.broadcast_backbone, memoryStream);
                this.CheckTransmitPacketAtBroadcast_Process ();
                memoryStream.Flush ();
            }

            this.CheckTransmitPacketAtBroadcast_Check (buffer);
        }

        [TestMethod]
        public void CheckTransmitPacketAtBroadcast_File () {
            var buffer = new byte[65536];

            this.dumper.AttachToBackbone (this.broadcast_backbone);
            using (this.dumper)
                this.CheckTransmitPacketAtBroadcast_Process ();

            var filename = this.broadcast_backbone.Name;
            Assert.IsTrue (File.Exists (filename));

            using (var fileStream = new FileStream (filename, FileMode.Open))
            using (var binaryReader = new BinaryReader (fileStream))
                binaryReader.Read (buffer, 0, (int) binaryReader.BaseStream.Length);

            this.CheckTransmitPacketAtBroadcast_Check (buffer);
        }

        [TestMethod]
        public void CheckTransmitPacketAtBroadcast_FileInDirectory () {
            var buffer = new byte[65536];

            this.dumper.Dispose ();
            this.dumper = new PCAPPacketDumper (this.clock, DIRECTORY);
            Assert.IsTrue (Directory.Exists (DIRECTORY));

            this.dumper.AttachToBackbone (this.broadcast_backbone);
            using (this.dumper)
                this.CheckTransmitPacketAtBroadcast_Process ();

            var filename = string.Format (@"{0}\{1}", DIRECTORY, this.broadcast_backbone.Name);
            Assert.IsTrue (File.Exists (filename));

            using (var fileStream = new FileStream (filename, FileMode.Open))
            using (var binaryReader = new BinaryReader (fileStream))
                binaryReader.Read (buffer, 0, (int) binaryReader.BaseStream.Length);

            this.CheckTransmitPacketAtBroadcast_Check (buffer);
        }

        private void CheckTransmitPacketAtUnicast_Process () {
            this.clock.RegisterAction (1,
                                       () =>
                                       this.unicast_backbone.SendPacket (new PacketMock (100, 1),
                                                                         this.unicast_backbone [0]));
            this.clock.RegisterAction (3,
                                       () =>
                                       this.unicast_backbone.SendPacket (new PacketMock (64, 3),
                                                                         this.unicast_backbone [2]));
            this.clock.RegisterAction (4, () => this.clock.Dispose ());
            this.clock.Start ();
        }

        private void CheckTransmitPacketAtUnicast_Check (byte [] buffer) {
            using (var memoryStream = new MemoryStream (buffer, false))
            using (var binaryReader = new BinaryReader (memoryStream)) {
                this.CheckFileHeader (binaryReader, this.unicast_backbone.Type);
                this.CheckPacketHeader (binaryReader, 2, 100);
                AssertHelper.ArrayIsConstant <byte> (1, binaryReader.ReadBytes (100));
                this.CheckPacketHeader (binaryReader, 4, 64);
                AssertHelper.ArrayIsConstant <byte> (3, binaryReader.ReadBytes (64));
                AssertHelper.ArrayIsConstant <byte> (0,
                                                     binaryReader.ReadBytes (
                                                         (int)
                                                         (binaryReader.BaseStream.Length -
                                                          binaryReader.BaseStream.Position + 1)));
            }
        }

        [TestMethod]
        public void CheckTransmitPacketAtUnicast_Stream () {
            var buffer = new byte[65536];

            using (var memoryStream = new MemoryStream (buffer)) {
                this.dumper.AttachToBackbone (this.unicast_backbone, memoryStream);
                this.CheckTransmitPacketAtUnicast_Process ();
                memoryStream.Flush ();
            }

            this.CheckTransmitPacketAtUnicast_Check (buffer);
        }

        [TestMethod]
        public void CheckTransmitPacketAtUnicast_File () {
            var buffer = new byte[65536];

            this.dumper.AttachToBackbone (this.unicast_backbone);
            using (this.dumper)
                this.CheckTransmitPacketAtUnicast_Process ();

            var filename = this.unicast_backbone.Name;
            Assert.IsTrue (File.Exists (filename));

            using (var fileStream = new FileStream (filename, FileMode.Open))
            using (var binaryReader = new BinaryReader (fileStream))
                binaryReader.Read (buffer, 0, (int) binaryReader.BaseStream.Length);

            this.CheckTransmitPacketAtUnicast_Check (buffer);
        }

        [TestMethod]
        public void CheckTransmitPacketAtUnicast_FileInDirectory () {
            var buffer = new byte[65536];

            this.dumper.Dispose ();
            this.dumper = new PCAPPacketDumper (this.clock, DIRECTORY);
            Assert.IsTrue (Directory.Exists (DIRECTORY));

            this.dumper.AttachToBackbone (this.unicast_backbone);
            using (this.dumper)
                this.CheckTransmitPacketAtUnicast_Process ();

            var filename = string.Format (@"{0}\{1}", DIRECTORY, this.unicast_backbone.Name);
            Assert.IsTrue (File.Exists (filename));

            using (var fileStream = new FileStream (filename, FileMode.Open))
            using (var binaryReader = new BinaryReader (fileStream))
                binaryReader.Read (buffer, 0, (int) binaryReader.BaseStream.Length);

            this.CheckTransmitPacketAtUnicast_Check (buffer);
        }

        private void CheckDetachAndTransmit_Process () {
            this.clock.RegisterAction (1,
                                       () =>
                                       this.unicast_backbone.SendPacket (new PacketMock (100, 1),
                                                                         this.unicast_backbone [0]));
            this.clock.RegisterAction (3, () => this.dumper.DetachFromBackbone (this.unicast_backbone));
            this.clock.RegisterAction (4,
                                       () =>
                                       this.unicast_backbone.SendPacket (new PacketMock (64, 3),
                                                                         this.unicast_backbone [2]));
            this.clock.RegisterAction (6, () => this.clock.Dispose ());
            this.clock.Start ();
        }

        private void CheckDetachAndTransmit_Check (byte [] buffer) {
            using (var memoryStream = new MemoryStream (buffer, false))
            using (var binaryReader = new BinaryReader (memoryStream)) {
                this.CheckFileHeader (binaryReader, this.unicast_backbone.Type);
                this.CheckPacketHeader (binaryReader, 2, 100);
                AssertHelper.ArrayIsConstant <byte> (1, binaryReader.ReadBytes (100));
                AssertHelper.ArrayIsConstant <byte> (0,
                                                     binaryReader.ReadBytes (
                                                         (int)
                                                         (binaryReader.BaseStream.Length -
                                                          binaryReader.BaseStream.Position + 1)));
            }
        }

        [TestMethod]
        public void CheckDetachAndTransmit_Stream () {
            var buffer = new byte[65536];

            using (var memoryStream = new MemoryStream (buffer)) {
                this.dumper.AttachToBackbone (this.unicast_backbone, memoryStream);
                this.CheckDetachAndTransmit_Process ();
                memoryStream.Flush ();
            }

            this.CheckDetachAndTransmit_Check (buffer);
        }

        [TestMethod]
        public void CheckDetachAndTransmit_File () {
            var buffer = new byte[65536];

            this.dumper.AttachToBackbone (this.unicast_backbone);
            using (this.dumper)
                this.CheckDetachAndTransmit_Process ();

            var filename = this.unicast_backbone.Name;
            Assert.IsTrue (File.Exists (filename));

            using (var fileStream = new FileStream (filename, FileMode.Open))
            using (var binaryReader = new BinaryReader (fileStream))
                binaryReader.Read (buffer, 0, (int) binaryReader.BaseStream.Length);

            this.CheckDetachAndTransmit_Check (buffer);
        }

        [TestMethod]
        public void CheckDetachAndTransmit_FileInDirectory () {
            var buffer = new byte[65536];

            this.dumper.Dispose ();
            this.dumper = new PCAPPacketDumper (this.clock, DIRECTORY);
            Assert.IsTrue (Directory.Exists (DIRECTORY));

            this.dumper.AttachToBackbone (this.unicast_backbone);
            using (this.dumper)
                this.CheckDetachAndTransmit_Process ();

            var filename = string.Format (@"{0}\{1}", DIRECTORY, this.unicast_backbone.Name);
            Assert.IsTrue (File.Exists (filename));

            using (var fileStream = new FileStream (filename, FileMode.Open))
            using (var binaryReader = new BinaryReader (fileStream))
                binaryReader.Read (buffer, 0, (int) binaryReader.BaseStream.Length);

            this.CheckDetachAndTransmit_Check (buffer);
        }

        [TestCleanup]
        public void Done () {
            this.CleanFile (this.broadcast_backbone.Name);
            this.CleanFile (this.unicast_backbone.Name);

            this.CleanFile (string.Format (@"{0}\{1}", DIRECTORY, this.broadcast_backbone.Name));
            this.CleanFile (string.Format (@"{0}\{1}", DIRECTORY, this.unicast_backbone.Name));

            this.dumper.Dispose ();
            this.clock.Dispose ();
        }

        private void CleanFile (string name) {
            if (File.Exists (name)) {
                try {
                    File.Delete (name);
                }
                catch {}
            }
        }
    }
}
