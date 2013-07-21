#region

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NModel.Extension.Test {
    public static class AssertHelper {
        public static void ListsEquals <T> (IList <T> expected, IList <T> actual) {
            Assert.IsNotNull (expected);
            Assert.IsNotNull (actual);

            Assert.AreEqual (expected.Count, actual.Count);

            for (var i = 0; i < expected.Count; ++ i)
                Assert.AreEqual (expected [i], actual [i]);
        }

        public static void ListsEqualsAsSet <T> (IEnumerable <T> expected, IEnumerable <T> actual) {
            var expectedSorted = new List <T> (expected);
            expectedSorted.Sort ();

            var actualSorted = new List <T> (actual);
            actualSorted.Sort ();

            ListsEquals (expectedSorted, actualSorted);
        }

        public static void IsSubmodel (IModel submodel, IModel model) {
            Assert.IsNotNull (submodel);
            Assert.IsNotNull (model);

            Assert.IsTrue (submodel.StatesCount == model.StatesCount);

            foreach (var state in submodel.States) {
                foreach (var state_to in submodel.Transitions (state))
                    Assert.IsTrue (model.HasTransition (state, state_to));
            }
        }

        public static void ModelEquals (IModel expected, IModel actual) {
            IsSubmodel (expected, actual);
            IsSubmodel (actual, expected);
        }
    }
}
