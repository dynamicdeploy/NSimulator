#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.Kernel;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class TaggedEthernetFrame_Test : EthernetFrameBaseTest <TaggedEthernetFrame> {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {
            var arr =
                new Array <byte> (new byte [] {
                                                  1, 2, 4, 5, 6, 8, 9, 3, 7, 10, 13, 24, 26, 47, 41, 23, 34, 28, 19, 33,
                                                  44, 41, 4, 3,
                                                  2, 7, 4
                                              });

            arr [12] = (ushort) EtherType.Tagged >> 8;
            arr [13] = (ushort) EtherType.Tagged & 0xFF;
            arr [14] = 0x0F;
            arr [15] = 0xFF;
            arr [16] = (ushort) EtherType.IPv4 >> 8;
            arr [17] = (ushort) EtherType.IPv4 & 0xFF;

            this.data = new ArrayView <byte> (arr);

            this.frame = new TaggedEthernetFrame (new PacketMock (arr.Slice (18, this.data.Length)),
                                                  EtherType.IPv4,
                                                  (short) ((arr [14] << 8) | arr [15]),
                                                  new MACAddress (arr.Slice (6, 12)),
                                                  new MACAddress (arr.Slice (0, 6)));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullData () {
            new TaggedEthernetFrame (null, EtherType.IPv4, 1, MACAddress.NullAddress, MACAddress.NullAddress);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullSource () {
            new TaggedEthernetFrame (new PacketMock (0, 0), EtherType.IPv4, 1, null, MACAddress.NullAddress);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullDestination () {
            new TaggedEthernetFrame (new PacketMock (0, 0), EtherType.IPv4, 1, MACAddress.NullAddress, null);
        }

        [TestMethod]
        public void CheckInternalData () {
            Assert.AreEqual (this.data.Slice (18, this.data.Length), this.frame.InternalData);
        }

        [TestMethod]
        public new void CheckType () {
            Assert.AreEqual (this.data [16], (ushort) this.frame.Type >> 8);
            Assert.AreEqual (this.data [17], (ushort) this.frame.Type & 0xFF);
        }

        [TestMethod]
        public void CheckPriorityCodePoint () {
            Assert.AreEqual (0, this.frame.PriorityCodePoint);
        }

        [TestMethod]
        public void CheckCanonicalFormatIndicator () {
            Assert.AreEqual (false, this.frame.CanonicalFormatIndicator);
        }

        [TestMethod]
        public void CheckVLANId () {
            Assert.AreEqual (0x0FFF, this.frame.VlanID);
        }

        [TestMethod]
        public void CheckParse () {
            var packet = TaggedEthernetFrame.Parse (this.frame.Data);
            Assert.AreEqual (this.frame.Source, packet.Source);
            Assert.AreEqual (this.frame.Destination, packet.Destination);
            Assert.AreEqual (this.frame.Type, packet.Type);
            Assert.AreEqual (this.frame.PriorityCodePoint, packet.PriorityCodePoint);
            Assert.AreEqual (this.frame.CanonicalFormatIndicator, packet.CanonicalFormatIndicator);
            Assert.AreEqual (this.frame.VlanID, packet.VlanID);
            Assert.AreEqual (this.frame.InternalData, packet.InternalData);
        }

        [TestCleanup]
        public void Done () {}
    }
}
