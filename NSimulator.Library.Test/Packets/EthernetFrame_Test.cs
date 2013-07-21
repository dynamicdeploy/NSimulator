#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.Kernel;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class EthernetFrame_Test : EthernetFrameBaseTest <EthernetFrame> {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {
            var arr =
                new Array <byte> (new byte [] { 1, 2, 4, 5, 6, 8, 9, 3, 7, 10, 13, 24, 26, 47, 41, 23, 34, 28, 19, 33 });

            arr [12] = (ushort) EtherType.IPv4 >> 8;
            arr [13] = (ushort) EtherType.IPv4 & 0xFF;

            this.data = new ArrayView <byte> (arr);

            this.frame = new EthernetFrame (new PacketMock (arr.Slice (14, this.data.Length)),
                                            EtherType.IPv4,
                                            new MACAddress (arr.Slice (6, 12)),
                                            new MACAddress (arr.Slice (0, 6)));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckDataNull () {
            new EthernetFrame (null, EtherType.IPv4, MACAddress.NullAddress, MACAddress.NullAddress);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSourceNull () {
            new EthernetFrame (new PacketMock (0, 0), EtherType.IPv4, null, MACAddress.NullAddress);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckDestinationNull () {
            new EthernetFrame (new PacketMock (0, 0), EtherType.IPv4, MACAddress.NullAddress, null);
        }

        [TestMethod]
        public void CheckInternalData () {
            Assert.AreEqual (this.data.Slice (14, this.data.Length), this.frame.InternalData);
        }

        [TestMethod]
        public void CheckParse () {
            var packet = EthernetFrame.Parse (this.frame.Data);
            Assert.AreEqual (this.frame.Source, packet.Source);
            Assert.AreEqual (this.frame.Destination, packet.Destination);
            Assert.AreEqual (this.frame.Type, packet.Type);
            Assert.AreEqual (this.frame.InternalData, packet.InternalData);
        }

        [TestCleanup]
        public void Done () {}
    }
}
