#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class GigabitEthernetInterface_Test : InterfaceBaseTest <GigabitEthernetInterface, GigabitEthernet> {
        public TestContext TestContext;

        [TestInitialize]
        public void Init () {
            this.iface = new GigabitEthernetInterface ();
            this.second_iface = new GigabitEthernetInterface ();

            this.clock = new ClockMock (ulong.MaxValue);
            this.compatible_backbone = new GigabitEthernet (this.clock);

            this.iface_in_backbone = new GigabitEthernetInterface ();
            this.iface_in_backbone.Enable ();
            this.iface_in_backbone.SetBackbone (this.compatible_backbone);

            this.noncompatible_backbone = new BroadcastBackboneMock (string.Empty, 2, ulong.MaxValue, this.clock);
        }

        [TestCleanup]
        public void Done () {}
    }
}
