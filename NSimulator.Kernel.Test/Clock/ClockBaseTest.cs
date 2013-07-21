#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public abstract class ClockBaseTest <T>
        where T : IClock {
        protected T clock;

        protected static Func <bool> True {
            get { return () => true; }
        }

        protected static ClockAction Empty {
            get { return delegate { }; }
        }

        protected ClockAction Finish {
            get { return () => this.clock.Dispose (); }
        }

        [TestMethod]
        public void CheckCannotRegisterAtZeroTick () {
            Assert.IsNull (this.clock.RegisterAction (0, Empty));
        }

        [TestMethod]
        public void CheckCanRegisterAtFirstTick () {
            var fired = false;
            this.clock.RegisterAction (1,
                                       delegate {
                                           fired = true;
                                           this.clock.Dispose ();
                                       });
            this.clock.Start ();
            Assert.IsTrue (fired);
            Assert.AreEqual ((ulong) 2, this.clock.CurrentTick);
        }

        [TestMethod]
        public void CheckCanRegisterAtNextTick () {
            var fired = false;
            this.clock.RegisterAction (delegate {
                                           fired = true;
                                           this.clock.Dispose ();
                                       });
            this.clock.Start ();
            Assert.IsTrue (fired);
            Assert.AreEqual ((ulong) 2, this.clock.CurrentTick);
        }

        [TestMethod]
        public void CheckOnErrorHandler () {
            var fired = false;

            var exception = new Exception ();
            var action = new ClockAction (delegate { throw exception; });

            this.clock.OnError += (c, a, e) => {
                                      fired = true;
                                      Assert.AreEqual (exception, e);
                                      Assert.AreEqual (action, a);
                                  };

            this.clock.RegisterAction (action);
            this.clock.RegisterAction (this.Finish);

            this.clock.Start ();
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckRegisterCondTrueAction () {
            var counter = 0;

            Assert.IsNotNull (this.clock.RegisterConditionalAction (True, delegate { counter++; }));
            Assert.IsNotNull (this.clock.RegisterAction (9, this.Finish));

            this.clock.Start ();
            Assert.AreEqual (10, counter);
        }

        [TestMethod]
        public void CheckRegisterCondFirstTrueAction () {
            var counter = 0;

            Assert.IsNotNull (this.clock.RegisterConditionalAction (() => this.clock.CurrentTick < 5,
                                                                    delegate { counter++; }));
            Assert.IsNotNull (this.clock.RegisterAction (10, this.Finish));

            this.clock.Start ();
            Assert.AreEqual (5, counter);
        }

        [TestMethod]
        [ExpectedException (typeof (InvalidClockHandlerException))]
        public void CheckRemoveUnknownAction () {
            this.clock.RemoveAction (new ClockHandler ());
        }

        protected Func <bool> AtTick (ulong tick) {
            return () => this.clock.CurrentTick == tick;
        }

        [TestMethod]
        public void CheckRemoveTrueAction1 () {
            var counter = 0;

            var action = this.clock.RegisterConditionalAction (True, delegate { counter++; });
            Assert.IsNotNull (action);
            Assert.IsNotNull (this.clock.RegisterAction (5, () => this.clock.RemoveAction (action)));
            Assert.IsNotNull (this.clock.RegisterAction (10, this.Finish));

            this.clock.Start ();
            Assert.AreEqual (5, counter);
        }

        [TestMethod]
        public void CheckRemoveTrueAction2 () {
            var counter = 0;

            var action = this.clock.RegisterConditionalAction (True, delegate { counter++; });
            Assert.IsNotNull (action);
            Assert.IsNotNull (this.clock.RegisterConditionalAction (this.AtTick (5),
                                                                    () => this.clock.RemoveAction (action)));
            Assert.IsNotNull (this.clock.RegisterAction (10, this.Finish));

            this.clock.Start ();
            Assert.AreEqual (6, counter);
        }

        [TestMethod]
        public void RemoveAction () {
            var fired = false;

            var action = this.clock.RegisterAction (5, () => fired = true);
            Assert.IsNotNull (action);
            Assert.IsNotNull (this.clock.RegisterAction (3, () => this.clock.RemoveAction (action)));
            Assert.IsNotNull (this.clock.RegisterAction (10, this.Finish));

            this.clock.Start ();
            Assert.IsFalse (fired);
        }

        [TestMethod]
        [ExpectedException (typeof (InvalidClockHandlerException))]
        public void CheckDoubleRemoveConditionalAction () {
            var action = this.clock.RegisterConditionalAction (True, Empty);
            Assert.IsNotNull (action);

            this.clock.RemoveAction (action);
            this.clock.RemoveAction (action);
        }

        [TestMethod]
        [ExpectedException (typeof (InvalidClockHandlerException))]
        public void CheckDoubleRemoveAction () {
            var action = this.clock.RegisterAction (Empty);
            Assert.IsNotNull (action);

            this.clock.RemoveAction (action);
            this.clock.RemoveAction (action);
        }

        [TestMethod]
        [ExpectedException (typeof (InvalidClockHandlerException))]
        public void CheckRemoveActionAfterFired () {
            var action = this.clock.RegisterAction (5, Empty);
            Assert.IsNotNull (action);

            Assert.IsNotNull (this.clock.RegisterAction (7, this.Finish));

            this.clock.Start ();
            this.clock.RemoveAction (action);
        }

        [TestMethod]
        public void CheckRemoveActionAfterFired_Exception () {
            var fired = false;

            var action = this.clock.RegisterAction (5, Empty);
            Assert.IsNotNull (action);

            ClockAction remove_action = () => this.clock.RemoveAction (action);

            Assert.IsNotNull (this.clock.RegisterAction (7, remove_action));
            Assert.IsNotNull (this.clock.RegisterAction (10, this.Finish));

            this.clock.OnError += (c, a, e) => {
                                      fired = true;
                                      Assert.AreEqual (remove_action, a);
                                      Assert.IsInstanceOfType (e, typeof (InvalidClockHandlerException));
                                  };

            this.clock.Start ();
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckRemoveConditionalActionInHandler () {
            var fired = false;
            ClockHandler handler = null;

            handler = this.clock.RegisterConditionalAction (this.AtTick (5), () => this.clock.RemoveAction (handler));
            Assert.IsNotNull (handler);

            this.clock.RegisterAction (10, this.Finish);

            this.clock.OnError += (c, a, e) => fired = true;

            this.clock.Start ();
            Assert.IsFalse (fired);
        }

        [TestMethod]
        public void CheckRemoveActionInHandler () {
            var fired = false;
            ClockHandler handler = null;

            handler = this.clock.RegisterAction (5, () => this.clock.RemoveAction (handler));
            Assert.IsNotNull (handler);

            this.clock.RegisterAction (10, this.Finish);

            this.clock.OnError += (c, a, e) => fired = true;

            this.clock.Start ();
            Assert.IsFalse (fired);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckActionIsNull_1 () {
            this.clock.RegisterAction (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckActionIsNull_2 () {
            this.clock.RegisterAction (1, null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckActionIsNull_3 () {
            this.clock.RegisterActionAtTime (0, null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckRegisterConditionalAction_Null1 () {
            this.clock.RegisterConditionalAction (null, () => { });
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckRegisterConditionalAction_Null2 () {
            this.clock.RegisterConditionalAction (() => true, null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckRemoveAction_Null () {
            this.clock.RemoveAction (null);
        }
        }
}
