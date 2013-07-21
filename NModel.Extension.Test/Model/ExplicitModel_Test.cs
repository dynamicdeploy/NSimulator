#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NModel.Extension.Test {
    [TestClass]
    public sealed class ExplicitModel_Test {
        private ExplicitModel model;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {
            this.model = new ExplicitModel ();
        }

        [TestMethod]
        public void CheckConstructor () {
            Assert.AreEqual (0, this.model.StatesCount);
            Assert.IsNotNull (this.model.States);
            Assert.AreEqual (0, this.model.States.Count);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAddStateNull () {
            this.model.AddState (null);
        }

        [TestMethod]
        public void CheckAddState () {
            const string l1 = "qwe";
            const string l2 = "asd";

            this.model.AddState (l1, l2);

            Assert.AreEqual (1, this.model.StatesCount);
            Assert.AreEqual (0, this.model.States [0]);
            Assert.IsTrue (this.model [0].Contains (l1));
            Assert.IsTrue (this.model [0].Contains (l2));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckAddTransitionNegative () {
            this.model.AddTransition (-1, -1);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckAddIncorrectTransition_Start () {
            this.model.AddState (string.Empty);
            this.model.AddTransition (0, 1);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckAddIncorrectTransitionTo () {
            this.model.AddState (string.Empty);
            this.model.AddTransition (1, 0);
        }

        [TestMethod]
        public void CheckAddTransition () {
            const string l1 = "qwe";
            const string l2 = "asd";
            const string l3 = "zxc";

            this.model.AddState (l1, l2);
            this.model.AddState (l3);
            this.model.AddTransition (0, 1);

            Assert.AreEqual (2, this.model.StatesCount);
            AssertHelper.ListsEquals (new [] { 1 }, this.model.Transitions (0));
            AssertHelper.ListsEquals (new int [] { }, this.model.Transitions (1));
            Assert.IsTrue (this.model.HasTransition (0, 1));
            Assert.IsFalse (this.model.HasTransition (0, 0));
            Assert.IsFalse (this.model.HasTransition (1, 1));
            Assert.IsFalse (this.model.HasTransition (1, 0));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckTransitionsNegative () {
            this.model.Transitions (-1);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckTransitionsBigNumber () {
            this.model.AddState (string.Empty);

            Assert.IsNotNull (this.model.Transitions (0));
            this.model.Transitions (10);
        }

        [TestMethod]
        public void CheckTransitions () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddTransition (0, 1);
            this.model.AddTransition (1, 1);

            AssertHelper.ListsEquals (new [] { 1 }, this.model.Transitions (0));
            AssertHelper.ListsEquals (new [] { 1 }, this.model.Transitions (1));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckHasTransitionsAllNegative () {
            this.model.HasTransition (-1, -2);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckHasTransitionFirstNegative () {
            this.model.AddState (string.Empty);

            this.model.HasTransition (-1, 0);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckHasTransitionSecondNegative () {
            this.model.AddState (string.Empty);

            this.model.HasTransition (0, -1);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckHasTransitionFirstBig () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);

            this.model.HasTransition (10, 1);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckHasTransitionSecondBig () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);

            this.model.HasTransition (1, 10);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckHasTransitonAllBig () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);

            this.model.HasTransition (10, 10);
        }

        [TestMethod]
        public void CheckHasTransition () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddTransition (0, 1);
            this.model.AddTransition (1, 1);

            Assert.IsTrue (this.model.HasTransition (0, 1));
            Assert.IsTrue (this.model.HasTransition (1, 1));
            Assert.IsFalse (this.model.HasTransition (0, 0));
            Assert.IsFalse (this.model.HasTransition (1, 0));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckIndexerNegative () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);

            Check (this.model [-1]);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckIndexerBig () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);

            Check (this.model [10]);
        }

        [TestMethod]
        public void CheckIndexer () {
            const string s1 = "qwe";
            const string s2 = "rty";
            const string s3 = "asd";

            this.model.AddState (s1, s3);
            this.model.AddState ();
            this.model.AddState (s2);

            AssertHelper.ListsEquals (new [] { s1, s3 }, this.model [0]);
            AssertHelper.ListsEquals (new string [] { }, this.model [1]);
            AssertHelper.ListsEquals (new [] { s2 }, this.model [2]);
        }

        [TestMethod]
        public void CheckStatesCount () {
            Assert.AreEqual (0, this.model.StatesCount);

            this.model.AddState ();
            this.model.AddState ();
            Assert.AreEqual (2, this.model.StatesCount);

            this.model.AddState ();
            Assert.AreEqual (3, this.model.StatesCount);
        }

        [TestMethod]
        public void CheckStates () {
            AssertHelper.ListsEquals (new int [] { }, this.model.States);

            this.model.AddState ();
            this.model.AddState ();
            AssertHelper.ListsEquals (new [] { 0, 1 }, this.model.States);

            this.model.AddState ();
            AssertHelper.ListsEquals (new [] { 0, 1, 2 }, this.model.States);
        }

        private static void Check (object o) {}

        [TestCleanup]
        public void Done () {}
    }
}
