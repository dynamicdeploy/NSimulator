#region

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.Kernel;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class Host_Test : DeviceBaseTest <Host <InterfaceMock>> {
        public TestContext TestContext;

        [TestInitialize]
        public void Init () {
            this.device = new Host <InterfaceMock> ();
        }

        [TestMethod]
        public void CheckAfterConstructor () {
            Assert.AreEqual (1, this.device.InterfacesCount);
            Assert.AreEqual (this.device.HostInterface, this.device.Interfaces.First ());
            Assert.IsInstanceOfType (this.device.HostInterface, typeof (InterfaceMock));
            Assert.IsFalse (this.device.Enabled);
            Assert.IsFalse (this.device.HostInterface.Enabled);
        }

        [TestMethod]
        public new void CheckAttachBackbone () {
            var iface = this.device.HostInterface;

            var backbone = new UnicastBackboneMock ("1", 2, 1, new ClockMock (1));
            Assert.AreEqual (0, backbone.EndPointsCount);

            this.device.AttachBackbone (iface.Name, backbone);

            Assert.AreEqual (1, backbone.EndPointsCount);
            Assert.AreEqual (iface, backbone [0]);
            Assert.AreEqual (backbone, iface.Backbone);
        }

        [TestMethod]
        public new void CheckDetachBackbone () {
            var iface = this.device.HostInterface;

            var backbone = new UnicastBackboneMock ("1", 2, 1, new ClockMock (1));
            Assert.AreEqual (0, backbone.EndPointsCount);

            this.device.AttachBackbone (iface.Name, backbone);

            Assert.AreEqual (1, backbone.EndPointsCount);

            this.device.DetachBackbone (iface.Name);

            Assert.AreEqual (0, backbone.EndPointsCount);
            Assert.IsNull (iface.Backbone);
        }

        [TestMethod]
        public new void CheckGetInterface () {
            Assert.AreEqual (this.device.HostInterface, this.device [this.device.HostInterface.Name]);
        }

        [TestMethod]
        public new void CheckCannotDetachUnknownInterface () {}

        [TestMethod]
        public new void CheckAttachInterface_NoPrefix () {}

        [TestMethod]
        public new void CheckAttachInterface_NameIsUnique () {}

        [TestMethod]
        public new void CheckProcessPacketForNullPacket () {}

        [TestMethod]
        public new void CheckCannotAttachAlreadyAttachedInterface1 () {}

        [TestMethod]
        public new void CheckCannotAttachAlreadyAttachedInterface2 () {}

        [TestMethod]
        public new void CheckCannotAttachAlreadyAttachedInterface3 () {}

        [TestMethod]
        public new void CheckAttachNullInterfaceWithNamePrefix () {}

        [TestMethod]
        public new void CheckCannotAttachTwoInterfacesWithSomeName () {}

        [TestMethod]
        public new void CheckAttachInterfaceWithNullName () {}

        [TestMethod]
        public new void CheckAttachBackboneWithAllBusyEndpoints () {}

        [TestMethod]
        public new void CheckAttachInterfaceWithNameNullPrefix () {}

        [TestMethod]
        public new void CheckDetachBackboneFromFreeInterface () {}

        [TestMethod]
        public new void CheckDetachNullInterface () {}

        [TestMethod]
        public new void CheckAttachBackboneAlreadyAttached () {}

        [TestMethod]
        public new void CheckAttachInterfacePrefixNamed_NameIsUnique () {}

        [TestMethod]
        public new void CheckAttachNullInterfaceWithName () {}

        [TestMethod]
        public new void CheckDetachBackboneFromUnknownInterface () {}

        [TestMethod]
        public new void CheckAttachNullInterface () {}

        [TestMethod]
        public new void CheckOnBeforeAddInterface1Fired () {}

        [TestMethod]
        public new void CheckOnBeforeAddInterface2Fired () {}

        [TestMethod]
        public new void CheckOnBeforeAddInterface3Fired () {}

        [TestMethod]
        public new void CheckOnBeforeRemoveInterfaceFired () {}

        [TestMethod]
        public new void CheckOnBeforeAddInterface1Fired_Exception () {}

        [TestMethod]
        public new void CheckOnBeforeAddInterface2Fired_Exception () {}

        [TestMethod]
        public new void CheckOnBeforeAddInterface3Fired_Exception () {}

        [TestMethod]
        public new void CheckOnBeforeRemoveInterfaceFired_Exception () {}

        [TestMethod]
        [ExpectedException (typeof (FeatureNotSupportedException))]
        public new void CheckDetachInterface () {
            this.device.RemoveInterface (string.Empty);
        }

        [TestMethod]
        [ExpectedException (typeof (FeatureNotSupportedException))]
        public new void CheckAttachInterface () {
            this.device.AddInterface (new InterfaceMock ());
        }

        [TestMethod]
        [ExpectedException (typeof (FeatureNotSupportedException))]
        public new void CheckAttachInterfaceDirectlyNamed () {
            this.device.AddInterface (new InterfaceMock (), string.Empty);
        }

        [TestMethod]
        [ExpectedException (typeof (FeatureNotSupportedException))]
        public new void CheckDetachAllInterfaces () {
            this.device.RemoveInterfaces ();
        }

        [TestMethod]
        [ExpectedException (typeof (FeatureNotSupportedException))]
        public new void CheckAttachInterfacePrefixNamed () {
            this.device.AddInterface_PrefixNamed (new InterfaceMock (), string.Empty);
        }

        [TestMethod]
        public new void CheckSetNullEngine () {
            // todo
        }

        [TestMethod]
        public new void CheckSetEngine () {
            // todo
        }

        [TestCleanup]
        public void Done () {}
    }
}
