#region

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NModel.Extension.Test {
    [TestClass]
    public sealed class EmersonLieVerifier_Test {
        private EmersonLieVerifier verifier;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {}

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckConstructorFirstNull () {
            new EmersonLieVerifier (null, new CTLFormula ("1"));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckConstructorSecondNull () {
            new EmersonLieVerifier (new ExplicitModel (), null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckConstructorAllNull () {
            new EmersonLieVerifier (null, null);
        }

        [TestMethod]
        public void CheckSimpleFormula () {
            this.verifier = new EmersonLieVerifier (SimpleModel3.Make (), new CTLFormula ("b"));

            var states = new [] { 2, 5 };
            AssertHelper.ListsEqualsAsSet (states, this.verifier.States);
            foreach (var state in SimpleModel3.Make ().States)
                Assert.AreEqual (states.Contains (state), this.verifier.CheckState (state));
        }

        [TestMethod]
        public void CheckFormula_Not () {
            this.verifier = new EmersonLieVerifier (SimpleModel3.Make (), ! new CTLFormula ("a"));

            var states = new [] { 1, 2, 5 };
            AssertHelper.ListsEqualsAsSet (states, this.verifier.States);
            foreach (var state in SimpleModel3.Make ().States)
                Assert.AreEqual (states.Contains (state), this.verifier.CheckState (state));
        }

        [TestMethod]
        public void CheckFormula_Or () {
            this.verifier = new EmersonLieVerifier (SimpleModel3.Make (), new CTLFormula ("a") | new CTLFormula ("b"));

            var states = new [] { 0, 2, 3, 4, 5 };
            AssertHelper.ListsEqualsAsSet (states, this.verifier.States);
            foreach (var state in SimpleModel3.Make ().States)
                Assert.AreEqual (states.Contains (state), this.verifier.CheckState (state));
        }

        [TestMethod]
        public void CheckFormula_EX () {
            this.verifier = new EmersonLieVerifier (SimpleModel3.Make (), CTLFormula.EX (new CTLFormula ("a")));

            var states = new [] { 1, 2, 4, 5 };
            AssertHelper.ListsEqualsAsSet (states, this.verifier.States);
            foreach (var state in SimpleModel3.Make ().States)
                Assert.AreEqual (states.Contains (state), this.verifier.CheckState (state));
        }

        [TestMethod]
        public void CheckFormula_EG () {
            this.verifier = new EmersonLieVerifier (SimpleModel3.Make (), CTLFormula.EG (new CTLFormula ("a")));

            var states = new [] { 4 };
            AssertHelper.ListsEqualsAsSet (states, this.verifier.States);
            foreach (var state in SimpleModel3.Make ().States)
                Assert.AreEqual (states.Contains (state), this.verifier.CheckState (state));
        }

        [TestMethod]
        public void CheckFormula_EU () {
            this.verifier = new EmersonLieVerifier (SimpleModel3.Make (),
                                                    CTLFormula.EU (new CTLFormula ("a"), new CTLFormula ("b")));

            var states = new [] { 2, 3, 5 };
            AssertHelper.ListsEqualsAsSet (states, this.verifier.States);
            foreach (var state in SimpleModel3.Make ().States)
                Assert.AreEqual (states.Contains (state), this.verifier.CheckState (state));
        }

        [TestMethod]
        public void CheckComplexFormula () {
            this.verifier = new EmersonLieVerifier (SimpleModel3.Make (), CTLFormula.Parse ("E({a} U EG {b})"));

            var states = new [] { 3, 5 };
            AssertHelper.ListsEqualsAsSet (states, this.verifier.States);
            foreach (var state in SimpleModel3.Make ().States)
                Assert.AreEqual (states.Contains (state), this.verifier.CheckState (state));
        }

        [TestCleanup]
        public void Done () {}
    }
}
