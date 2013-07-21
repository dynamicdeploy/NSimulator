#region

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Обёртка для создания массива только для чтения.
    /// </summary>
    /// <remarks>
    ///   Чтобы создать представление массива как массива только для чтения,
    ///   можно использовать данную обёртку.
    /// </remarks>
    /// <example>
    ///   <code>
    ///     var array = new Array &lt;int&gt; (new [] { 1, 2, 3, 4, 5 });
    ///     var ronly = new ArrayView &lt;int&gt; (array);
    ///     array [2] = 10; // OK
    ///     // ronly [2] = 10; // Error
    ///   </code>
    /// </example>
    /// <typeparam name = "T">Тип элементов массива.</typeparam>
    public sealed class ArrayView <T> : IArrayView <T> {
        private readonly IArrayView <T> array;

        /// <summary>
        ///   Инициализирует обёртку массива только для чтения.
        /// </summary>
        /// <param name = "array">Массив.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "array" /> является <c>null</c>.</exception>
        public ArrayView (IArrayView <T> array) {
            if (array == null)
                throw new ArgumentNullException ("array");

            this.array = array;
        }

        #region IArrayView<T> Members

        /// <inheritdoc />
        public IEnumerator <T> GetEnumerator () {
            return this.array.GetEnumerator ();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator () {
            return this.GetEnumerator ();
        }

        /// <inheritdoc />
        public T this [int index] {
            get { return this.array [index]; }
        }

        /// <inheritdoc />
        public int Offset {
            get { return this.array.Offset; }
        }

        /// <inheritdoc />
        public int Length {
            get { return this.array.Length; }
        }

        /// <inheritdoc />
        public T [] Base {
            get { return this.array.Base; }
        }

        /// <inheritdoc />
        public IArrayView <T> Slice (int from, int to) {
            return new ArrayView <T> (this.array.Slice (from, to));
        }

        /// <inheritdoc />
        public bool Equals (IArrayView <T> other) {
            return this.array.Equals (other);
        }

        #endregion

        /// <inheritdoc />
        public override bool Equals (object obj) {
            return this.array.Equals (obj);
        }

        /// <inheritdoc />
        public override int GetHashCode () {
            return this.array.GetHashCode ();
        }
    }
}
