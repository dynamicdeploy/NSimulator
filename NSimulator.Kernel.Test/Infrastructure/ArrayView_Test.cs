#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public sealed class ArrayView_Test : ArrayBaseTest <IArrayView <int>, int> {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {
            this.base_array = new [] { 1, 6, 2, 8, 3, 9, 7, 4, 5 };
            this.array = new ArrayView <int> (new Array <int> (this.base_array));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckArgumentNull () {
            this.array = new ArrayView <int> (null);
        }

        [TestMethod]
        public void CheckArrayViewSlice () {
            var slice = this.array.Slice (2, 5);
            Assert.IsNotInstanceOfType (slice, typeof (IArray <int>));
            Assert.IsInstanceOfType (slice, typeof (IArrayView <int>));

            var slice2 = slice.Slice (1, 2);
            Assert.IsNotInstanceOfType (slice2, typeof (IArray <int>));
            Assert.IsInstanceOfType (slice2, typeof (IArrayView <int>));
        }

        [TestMethod]
        public void CheckEqualsExplicit () {
            Assert.IsTrue (
                new ArrayView <byte> (new Array <byte> (new byte [] { 1, 3, 5, 7, 4, 2 })).Equals (
                    new Array <byte> (new byte [] { 1, 3, 5, 7, 4, 2 })));
        }

        [TestMethod]
        public void CheckEqualsImplicit () {
            Assert.AreEqual (new ArrayView <byte> (new Array <byte> (new byte [] { 1, 3, 5, 7, 4, 2 })),
                             new Array <byte> (new byte [] { 1, 3, 5, 7, 4, 2 }));
        }

        [TestCleanup]
        public void Done () {}
    }
}
