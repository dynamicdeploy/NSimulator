#region

using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NModel.Extension.Test {
    [TestClass]
    public sealed class Model_Test {
        private IModel model;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {}

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullConstructor () {
            new Model (null);
        }

        [TestMethod]
        public void CheckModelSimple1 () {
            this.model = new Model (Model1.Make ());

            Assert.AreEqual (5, this.model.StatesCount);

            for (var i = 0; i < 5; ++ i)
                AssertHelper.ListsEquals (new [] { string.Format ("x={0}", i) }, this.model [i]);

            for (var i = 0; i < 5 - 1; ++i)
                AssertHelper.ListsEquals (new [] { i + 1 }, this.model.Transitions (i));

            AssertHelper.ListsEquals (new int [] { }, this.model.Transitions (4));
        }

        [TestMethod]
        public void CheckModelSimple2 () {
            this.model = new Model (Model2.Make ());

            Assert.AreEqual (100, this.model.StatesCount);

            var statesIndex = new int[100];
            for (var i = 0; i < 100; ++i)
                statesIndex [i] = -1;

            var regexp = new Regex (@"^x=(\d+)$");

            for (var i = 0; i < 100; ++ i) {
                Assert.AreEqual (1, this.model [i].Count);
                Assert.IsTrue (regexp.IsMatch (this.model [i] [0]));

                var index = int.Parse (regexp.Match (this.model [i] [0]).Groups [1].Value) - 1;
                Assert.IsTrue (index >= 0);
                Assert.AreEqual (-1, statesIndex [index]);
                statesIndex [index] = i;
            }

            for (var i = 1; i <= 100; ++i) {
                for (var j = 1; j <= 100; ++j)
                    Assert.AreEqual (j % i == 0, this.model.HasTransition (statesIndex [i - 1], statesIndex [j - 1]));
            }
        }

        [TestCleanup]
        public void Done () {}
    }
}
