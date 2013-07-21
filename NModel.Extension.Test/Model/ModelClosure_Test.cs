#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NModel.Extension.Test {
    [TestClass]
    public sealed class ModelClosure_Test {
        private ExplicitModel model;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {
            this.model = new ExplicitModel ();
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullConstructor () {
            new ModelClosure (null);
        }

        [TestMethod]
        public void Check1 () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddTransition (0, 1);
            this.model.AddTransition (2, 1);

            var closure = new ModelClosure (this.model);
            Assert.AreEqual (this.model.StatesCount, closure.StatesCount);
            Assert.IsTrue (ModelTotalityChecker.Check (closure));
            AssertHelper.IsSubmodel (this.model, closure);
            Assert.IsTrue (closure.Transitions (1).Count > 0);
        }

        [TestMethod]
        public void Check2 () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddTransition (0, 1);
            this.model.AddTransition (1, 2);
            this.model.AddTransition (0, 2);

            var closure = new ModelClosure (this.model);
            Assert.AreEqual (this.model.StatesCount, closure.StatesCount);
            Assert.IsTrue (ModelTotalityChecker.Check (closure));
            AssertHelper.IsSubmodel (this.model, closure);
            Assert.IsTrue (closure.Transitions (2).Count > 0);
        }

        [TestMethod]
        public void Check3 () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddTransition (0, 1);
            this.model.AddTransition (1, 2);
            this.model.AddTransition (2, 0);

            var closure = new ModelClosure (this.model);
            AssertHelper.ModelEquals (this.model, closure);
        }

        [TestMethod]
        public void Check4 () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddTransition (0, 1);
            this.model.AddTransition (1, 2);
            this.model.AddTransition (0, 2);

            var closure1 = new ModelClosure (this.model);
            var closure2 = new ModelClosure (closure1);
            AssertHelper.ModelEquals (closure1, closure2);
        }

        [TestCleanup]
        public void Done () {}
    }
}
