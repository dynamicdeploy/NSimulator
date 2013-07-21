#region

using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public sealed class RealtimeClock_Test : ClockBaseTest <RealtimeClock> {
        private const ulong TICK_LENGTH = 200000;
        public TestContext TestContext;

        [TestInitialize]
        public void Init () {
            this.clock = new RealtimeClock (TICK_LENGTH);
        }

        [TestMethod]
        public void CheckStartupState () {
            Assert.AreEqual ((ulong) 0, this.clock.CurrentTick);
            Assert.AreEqual (TICK_LENGTH, this.clock.TickLength);
            Assert.IsFalse (this.clock.IsSuspended);
        }

        [TestMethod]
        public void CheckCannotRegisterAtZeroTick_AtTime () {
            Assert.IsNull (this.clock.RegisterActionAtTime (0, this.Finish));
        }

        [TestMethod]
        public void CheckCanRegisterAtTime () {
            var fired = false;
            this.clock.RegisterActionAtTime (TICK_LENGTH,
                                             () => {
                                                 fired = true;
                                                 this.clock.Dispose ();
                                             });
            this.clock.Start ();
            Assert.IsTrue (fired);
            Assert.AreEqual ((ulong) 2, this.clock.CurrentTick);
        }

        [TestMethod]
        public void CheckRealtime () {
            const ulong N = 5;

            var time = DateTime.Now;
            this.clock.RegisterActionAtTime (TICK_LENGTH * N, this.Finish);
            this.clock.Start ();
            var delta = DateTime.Now - time;

            Assert.AreEqual (N + 1, this.clock.CurrentTick);
            Assert.IsTrue (delta.TotalMilliseconds >= N * TICK_LENGTH / 1000);
        }

        [TestMethod]
        public void CheckSuspendResume () {
            var thread = new Thread (() => this.clock.Start ());
            thread.Start ();
            this.clock.Suspend ();
            Assert.IsTrue (this.clock.IsSuspended);

            var tick = this.clock.CurrentTick;
            Thread.Sleep ((int) ((this.clock.TickLength / 1000) << 1) + 200);
            Assert.AreEqual (tick, this.clock.CurrentTick);

            this.clock.Resume ();
            Assert.IsFalse (this.clock.IsSuspended);

            tick = this.clock.CurrentTick;
            Thread.Sleep ((int) ((this.clock.TickLength / 1000) << 1) + 200);
            Assert.AreNotEqual (tick, this.clock.CurrentTick);
        }

        [TestCleanup]
        public void Done () {
            this.Finish ();
        }
    }
}
