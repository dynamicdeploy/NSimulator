#region

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public abstract class ArrayBaseTest <T, X>
        where T : IArrayView <X> {
        protected T array;
        protected X [] base_array;

        [TestMethod]
        public void CheckIndexer () {
            for (var i = 0; i < this.base_array.Length; ++i)
                Assert.AreEqual (this.base_array [i], this.array [i]);
        }

        [TestMethod]
        [ExpectedException (typeof (IndexOutOfRangeException))]
        public void CheckIndexer_Get_OutOfRange1 () {
            this.CheckExists (this.array [-1]);
        }

        [TestMethod]
        [ExpectedException (typeof (IndexOutOfRangeException))]
        public void CheckIndexer_Get_OutOfRange2 () {
            this.CheckExists (this.array [this.array.Length]);
        }

        [TestMethod]
        public void CheckLength () {
            Assert.AreEqual (this.base_array.Length, this.array.Length);
        }

        [TestMethod]
        public void CheckOffset () {
            Assert.AreEqual (0, this.array.Offset);
        }

        [TestMethod]
        public void CheckBase () {
            Assert.AreEqual (this.base_array, this.array.Base);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckSliceLeftOutOfRange () {
            this.array.Slice (-1, 2);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckSliceRightOutOfRange () {
            this.array.Slice (1, 20);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckSliceLeftGreaterThanRight () {
            this.array.Slice (5, 2);
        }

        [TestMethod]
        public void CheckSlice () {
            var x = this.array.Slice (2, 5);
            Assert.AreEqual (3, x.Length);
            Assert.AreEqual (2, x.Offset);
        }

        [TestMethod]
        public void CheckSliceLeftBound () {
            var x = this.array.Slice (0, 4);
            Assert.AreEqual (4, x.Length);
            Assert.AreEqual (0, x.Offset);
        }

        [TestMethod]
        public void CheckSliceRightBound () {
            var x = this.array.Slice (4, this.base_array.Length);
            Assert.AreEqual (this.base_array.Length - 4, x.Length);
            Assert.AreEqual (4, x.Offset);
        }

        [TestMethod]
        public void CheckSlice_Indexer () {
            var x = this.array.Slice (2, 5);
            for (var i = 0; i < x.Length; ++i)
                Assert.AreEqual (this.base_array [2 + i], x [i]);
        }

        [TestMethod]
        [ExpectedException (typeof (IndexOutOfRangeException))]
        public void CheckSlice_OutOfRange () {
            var x = this.array.Slice (2, 5);
            this.CheckExists (x [4]);
        }

        [TestMethod]
        public void CheckDoubleSlice () {
            var x = this.array.Slice (2, 5).Slice (1, 2);
            Assert.AreEqual (1, x.Length);
            Assert.AreEqual (3, x.Offset);
            for (var i = 0; i < x.Length; ++i)
                Assert.AreEqual (this.base_array [3 + i], x [i]);
        }

        [TestMethod]
        public void CheckEnumerable () {
            var list = new List <X> (this.array);
            Assert.AreEqual (this.base_array.Length, list.Count);

            for (var i = 0; i < this.base_array.Length; ++i)
                Assert.AreEqual (this.base_array [i], list [i]);
        }

        [TestMethod]
        public void CheckEnumerator_Slice () {
            var slice = this.array.Slice (1, 6);
            var list = new List <X> (slice);
            Assert.AreEqual (slice.Length, list.Count);

            for (var i = 0; i < slice.Length; ++i)
                Assert.AreEqual (slice [i], list [i]);
        }

        [TestMethod]
        public void CheckEquals1 () {
            Assert.IsTrue (this.array.Equals (this.array));
        }

        [TestMethod]
        public void CheckEquals2 () {
            Assert.IsTrue (this.array.Equals (new ArrayView <X> (new Array <X> (this.base_array))));
        }

        [TestMethod]
        public void CheckNotEquals1 () {
            Assert.IsFalse (this.array.Equals (null));
        }

        [TestMethod]
        public void CheckNotEquals2 () {
            var arr = new X[this.base_array.Length];

            Assert.IsFalse (this.array.Equals (new ArrayView <X> (new Array <X> (arr))));
        }

        [TestMethod]
        public void CheckNotEquals3 () {
            Assert.IsFalse (this.array.Equals (Array <X>.Empty));
        }

        protected void CheckExists (X x) {}
        }
}
