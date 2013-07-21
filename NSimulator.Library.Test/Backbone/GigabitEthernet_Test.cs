#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class GigabitEthernet_Test : WireTest <GigabitEthernet> {
        public TestContext TestContext;

        [TestInitialize]
        public void Init () {
            this.clock = new ClockMock (ulong.MaxValue);
            this.backbone = new GigabitEthernet (this.clock);
        }

        [TestCleanup]
        public void Done () {}
    }
}
