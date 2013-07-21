#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class FastEthernet_Test : WireTest <FastEthernet> {
        public TestContext TestContext;

        [TestInitialize]
        public void Init () {
            this.clock = new ClockMock (ulong.MaxValue);
            this.backbone = new FastEthernet (this.clock);
        }

        [TestCleanup]
        public void Done () {}
    }
}
