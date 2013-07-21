#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class EthernetInterface_Test : InterfaceBaseTest <EthernetInterface, Ethernet> {
        public TestContext TestContext;

        [TestInitialize]
        public void Init () {
            this.iface = new EthernetInterface ();
            this.second_iface = new EthernetInterface ();

            this.clock = new ClockMock (ulong.MaxValue);
            this.compatible_backbone = new Ethernet (this.clock);

            this.iface_in_backbone = new EthernetInterface ();
            this.iface_in_backbone.Enable ();
            this.iface_in_backbone.SetBackbone (this.compatible_backbone);

            this.noncompatible_backbone = new BroadcastBackboneMock (string.Empty, 2, ulong.MaxValue, this.clock);
        }

        [TestCleanup]
        public void Done () {}
    }
}
