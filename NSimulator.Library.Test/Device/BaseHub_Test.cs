#region

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.Kernel;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class BaseHub_Test : DeviceBaseTest <BaseHubMock> {
        private const int INTERFACES = 4;
        public TestContext TestContext;

        [TestInitialize]
        public void Init () {
            this.device = new BaseHubMock (INTERFACES);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckIncorrectArgumentInConstructor () {
            new BaseHubMock (-1);
        }

        [TestMethod]
        [ExpectedException (typeof (FeatureNotSupportedException))]
        public void CheckHubIsNotDisableable () {
            this.device.Disable ();
        }

        [TestMethod]
        [ExpectedException (typeof (FeatureNotSupportedException))]
        public void CheckHubIsNotManageable () {
            this.device.SetEngine (new EngineMock1 ());
        }

        [TestMethod]
        public void CheckHubAlwaysEnable () {
            this.device.Enable ();
            Assert.IsTrue (this.device.Enabled);
        }

        [TestMethod]
        public void CheckInitParameters () {
            Assert.IsTrue (this.device.Enabled);
            Assert.IsNull (this.device.Engine);
            Assert.AreEqual (INTERFACES, this.device.InterfacesCount);

            foreach (var iface in this.device.Interfaces)
                Assert.IsInstanceOfType (iface, typeof (InterfaceMock));

            Assert.AreEqual (string.Empty, this.device.Name);
        }

        [TestMethod]
        public void CheckTransmitPacketWithOneInterfaceHub () {
            var clock = new ClockMock (100);
            this.device = new BaseHubMock (1);

            var dev_iface = new List <IInterfaceView> (this.device.Interfaces) [0];
            var snd_iface = new InterfaceMock ();

            var transmitted = 0;

            var backbone = new BroadcastBackboneMock ("1", 2, ulong.MaxValue, clock);
            backbone.OnTransmit += (b, p) => { ++transmitted; };

            this.device.AttachBackbone (dev_iface.Name, backbone);
            snd_iface.SetBackbone (backbone);

            snd_iface.SendPacket (new PacketMock (1));

            clock.Start ();

            Assert.AreEqual (1, transmitted);
        }

        [TestMethod]
        public void CheckTransmitPacketWithOneFreeInterfaceHub () {
            var clock = new ClockMock (100);
            var transmitted = new List <string> ();

            var dev_ifaces = this.device.Interfaces.Take (INTERFACES - 1);
            var snd_iface = new InterfaceMock ();

            var last_backbone = new BroadcastBackboneMock ("1", 1, 1, clock);

            ulong id = 0;
            foreach (var iface in dev_ifaces) {
                var backbone = new BroadcastBackboneMock (id ++.ToString (), 2, ulong.MaxValue, clock);
                backbone.OnTransmit += (b, p) => transmitted.Add (b.Name.ToString ());

                this.device.AttachBackbone (iface.Name, backbone);

                last_backbone = backbone;
            }

            snd_iface.SetBackbone (last_backbone);

            snd_iface.SendPacket (new PacketMock (1));

            clock.Start ();

            Assert.AreEqual (2 + 1, transmitted.Count);
            Assert.AreEqual (2.ToString (), transmitted [0]);
            Assert.IsTrue (transmitted.Contains (0.ToString ()));
            Assert.IsTrue (transmitted.Contains (1.ToString ()));
        }

        [TestMethod]
        public new void CheckEnableDisable () {}

        [TestMethod]
        public new void CheckDisableEnable () {}

        [TestMethod]
        public new void CheckOnBeforeEnableFired () {}

        [TestMethod]
        public new void CheckOnBeforeDisableFired () {}

        [TestMethod]
        public new void CheckOnBeforeEnableFired_Exception () {}

        [TestMethod]
        public new void CheckOnBeforeDisableFired_Exception () {}

        [TestMethod]
        public new void CheckSetNullEngine () {}

        [TestMethod]
        [ExpectedException (typeof (FeatureNotSupportedException))]
        public new void CheckSetEngine () {
            this.device.SetEngine (new EngineMock1 ());
        }

        [TestCleanup]
        public void Done () {}
    }
}
