#region

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NSimulator.Kernel.Test {
    public static class AssertHelper {
        public static void ArrayIsConstant <T> (T expected, T [] array) {
            SubarrayIsConstant (expected, array, 0, array.Length);
        }

        public static void SubarrayIsConstant <T> (T expected, T [] array, int from, int to) {
            for (var i = from; i < to; ++ i)
                Assert.AreEqual (expected, array [i]);
        }

        public static void ArrayStartWith <T> (T [] expected, T [] array) {
            SubarraysEquals (expected, array, 0);
        }

        public static void SubarraysEquals <T> (T [] expected, T [] array, int position) {
            Assert.IsTrue (array.Length >= position + expected.Length);

            for (var i = 0; i < expected.Length; ++ i)
                Assert.AreEqual (expected [i], array [position + i]);
        }

        public static void EnumerableContains <T> (IEnumerable <T> enumerable, T element) {
            Assert.IsTrue (enumerable.Contains (element));
        }

        public static void InnerExceptionThrown <T> (Action action)
            where T : Exception {
            try {
                action.Invoke ();
            }
            catch (Exception exception) {
                Assert.IsInstanceOfType (exception.InnerException, typeof (T));
            }
        }
    }
}
