#region

using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public sealed class BaseClock_Test : ClockBaseTest <BaseClock> {
        public TestContext TestContext;

        [TestInitialize]
        public void Init () {
            this.clock = new BaseClock ();
        }

        [TestMethod]
        public void CheckStartupState () {
            Assert.AreEqual ((ulong) 0, this.clock.CurrentTick);
            Assert.AreEqual ((ulong) 0, this.clock.TickLength);
            Assert.IsFalse (this.clock.IsSuspended);
        }

        [TestMethod]
        public void CheckCannotRegisterAtZeroTick_AtTime () {
            Assert.IsNull (this.clock.RegisterActionAtTime (1, this.Finish));
        }

        [TestMethod]
        public void CheckSuspendResume () {
            var thread = new Thread (() => this.clock.Start ());
            thread.Start ();
            this.clock.Suspend ();
            Assert.IsTrue (this.clock.IsSuspended);

            var tick = this.clock.CurrentTick;
            Thread.Sleep ((int) (this.clock.TickLength << 1) + 200);
            Assert.AreEqual (tick, this.clock.CurrentTick);

            this.clock.Resume ();
            Assert.IsFalse (this.clock.IsSuspended);

            tick = this.clock.CurrentTick;
            Thread.Sleep ((int) (this.clock.TickLength << 1) + 200);
            Assert.AreNotEqual (tick, this.clock.CurrentTick);
        }

        [TestCleanup]
        public void Done () {
            this.Finish ();
        }
    }
}
