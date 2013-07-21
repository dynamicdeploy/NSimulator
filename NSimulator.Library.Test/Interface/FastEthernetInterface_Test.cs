#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class FastEthernetInterface_Test : InterfaceBaseTest <FastEthernetInterface, FastEthernet> {
        public TestContext TestContext;

        [TestInitialize]
        public void Init () {
            this.iface = new FastEthernetInterface ();
            this.second_iface = new FastEthernetInterface ();

            this.clock = new ClockMock (ulong.MaxValue);
            this.compatible_backbone = new FastEthernet (this.clock);

            this.iface_in_backbone = new FastEthernetInterface ();
            this.iface_in_backbone.Enable ();
            this.iface_in_backbone.SetBackbone (this.compatible_backbone);

            this.noncompatible_backbone = new BroadcastBackboneMock (string.Empty, 2, ulong.MaxValue, this.clock);
        }

        [TestCleanup]
        public void Done () {}
    }
}
