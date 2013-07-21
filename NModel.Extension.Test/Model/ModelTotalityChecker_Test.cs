#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NModel.Extension.Test {
    [TestClass]
    public class ModelTotalityChecker_Test {
        private ExplicitModel model;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {
            this.model = new ExplicitModel ();
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullModel () {
            ModelTotalityChecker.Check (null);
        }

        [TestMethod]
        public void CheckNotTotalModel () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddTransition (0, 1);
            this.model.AddTransition (2, 1);
            this.model.AddTransition (2, 0);

            Assert.IsFalse (ModelTotalityChecker.Check (this.model));
        }

        [TestMethod]
        public void CheckTotalModel1 () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddTransition (0, 0);
            this.model.AddTransition (1, 1);
            this.model.AddTransition (2, 2);

            Assert.IsTrue (ModelTotalityChecker.Check (this.model));
        }

        [TestMethod]
        public void CheckTotalModel2 () {
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddState (string.Empty);
            this.model.AddTransition (0, 1);
            this.model.AddTransition (1, 2);
            this.model.AddTransition (2, 0);

            Assert.IsTrue (ModelTotalityChecker.Check (this.model));
        }

        [TestCleanup]
        public void Done () {}
    }
}
