#region

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public sealed class Array_Test : ArrayBaseTest <IArray <int>, int> {
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {
            this.base_array = new [] { 1, 6, 2, 8, 3, 9, 7, 4, 5 };
            this.array = new Array <int> (this.base_array);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckArgumentNull () {
            this.array = new Array <int> (null);
        }

        [TestMethod]
        public void CheckShare () {
            this.base_array [2] = 10;
            this.CheckIndexer ();
        }

        [TestMethod]
        [ExpectedException (typeof (IndexOutOfRangeException))]
        public void CheckIndexer_Set_OutOfRange1 () {
            this.array [-1] = 10;
        }

        [TestMethod]
        [ExpectedException (typeof (IndexOutOfRangeException))]
        public void CheckIndexer_Set_OutOfRange2 () {
            this.array [this.array.Length] = 10;
        }

        [TestMethod]
        public void CheckSliceShared () {
            var x = this.array.Slice (2, 5);
            x [1] = 10;
            Assert.AreEqual (x [1], this.array [3]);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckConvertAll_NullArray () {
            Array <int>.ConvertAll (null, _ => _);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckConvertAll_NullConverter () {
            Array <int>.ConvertAll <int> (this.array, null);
        }

        [TestMethod]
        public void CheckConvertAll () {
            const string prefix = "prefix";

            var converted = new List <int> ();

            var result = Array <int>.ConvertAll (this.array,
                                                 _ => {
                                                     converted.Add (_);
                                                     return prefix + _.ToString ();
                                                 });

            Assert.AreEqual (this.array.Length, result.Length);
            for (var i = 0; i < this.array.Length; ++ i) {
                Assert.AreEqual (prefix + this.array [i], result [i]);
                Assert.AreEqual (this.array [i], converted [i]);
            }
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

        [TestMethod]
        public void CheckEmpty () {
            var empty = Array <int>.Empty;
            Assert.AreEqual (0, empty.Length);
        }

        [TestCleanup]
        public void Done () {}
    }
}
