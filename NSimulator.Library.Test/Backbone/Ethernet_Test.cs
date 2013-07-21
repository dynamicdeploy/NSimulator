#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class Ethernet_Test : WireTest <Ethernet> {
        public TestContext TestContext;

        [TestInitialize]
        public void Init () {
            this.clock = new ClockMock (ulong.MaxValue);
            this.backbone = new Ethernet (this.clock);
        }

        [TestCleanup]
        public void Done () {}
    }
}
