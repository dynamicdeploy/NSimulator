#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Массив.
    /// </summary>
    /// <remarks>
    ///   Данный массив, в отличие от <see cref = "Array" />, поддерживает интерфейс
    ///   только для чтения и взятие shared-срезов.
    /// </remarks>
    /// <seealso cref = "Array" />
    /// <seealso cref = "ArrayView{T}" />
    /// <typeparam name = "T">Тип элементов массива.</typeparam>
    public sealed partial class Array <T> : IArray <T> {
        private readonly int _from;
        private readonly int _to;
        private readonly T [] data;

        /// <summary>
        ///   Инициализирует массив "обычным" массивом.
        /// </summary>
        /// <param name = "data">Массив.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "data" /> является <c>null</c>.</exception>
        public Array (T [] data)
            : this (data, 0, data == null ? 0 : data.Length) {}

        private Array (T [] data, int from, int to) {
            if (data == null)
                throw new ArgumentNullException ("data");

            if (from < 0 || from > data.Length)
                throw new ArgumentOutOfRangeException ("from");

            if (to < 0 || to > data.Length || to < from)
                throw new ArgumentOutOfRangeException ("to");

            this.data = data;
            this._from = from;
            this._to = to;
        }

        /// <summary>
        ///   Создаёт пустой массив.
        /// </summary>
        /// <value>
        ///   Пустой массив.
        /// </value>
        public static Array <T> Empty {
            get { return new Array <T> (new T [] { }); }
        }

        #region IArray<T> Members

        /// <inheritdoc />
        public IEnumerator <T> GetEnumerator () {
            return new ArrayEnumerator (this);
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator () {
            return this.GetEnumerator ();
        }

        /// <inheritdoc />
        public T this [int index] {
            get {
                if (index < 0)
                    throw new IndexOutOfRangeException ("index");

                if (index >= this.Length)
                    throw new IndexOutOfRangeException ("index");

                return this.data [this._from + index];
            }
            set {
                if (index < 0)
                    throw new IndexOutOfRangeException ("index");

                if (index >= this.Length)
                    throw new IndexOutOfRangeException ("index");

                this.data [this._from + index] = value;
            }
        }

        /// <inheritdoc />
        public int Offset {
            get { return this._from; }
        }

        /// <inheritdoc />
        public int Length {
            get { return this._to - this._from; }
        }

        /// <inheritdoc />
        public T [] Base {
            get { return this.data; }
        }

        /// <inheritdoc />
        public IArray <T> Slice (int from, int to) {
            return new Array <T> (this.data, this._from + from, this._from + to);
        }

        /// <inheritdoc />
        IArrayView <T> IArrayView <T>.Slice (int from, int to) {
            return this.Slice (from, to);
        }

        /// <inheritdoc />
        public bool Equals (IArrayView <T> other) {
            if (other == null)
                return false;

            if (ReferenceEquals (this, other))
                return true;

            if (this.Length != other.Length)
                return false;

            for (var i = 0; i < this.Length; ++ i) {
                if (! this [i].Equals (other [i]))
                    return false;
            }

            return true;
        }

        #endregion

        /// <inheritdoc />
        public override bool Equals (object obj) {
            return this.Equals (obj as IArrayView <T>);
        }

        /// <inheritdoc />
        public override int GetHashCode () {
            return this.Aggregate (0, (current, value) => current ^ value.GetHashCode ());
        }

        /// <summary>
        ///   Конвертирует элементы массива из одного типа в другой.
        /// </summary>
        /// <typeparam name = "O">Новый тип.</typeparam>
        /// <param name = "input">Исходный массив.</param>
        /// <param name = "converter">Конвертер типов.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "input" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "converter" /> является <c>null</c>.</exception>
        /// <returns>Массив, элементы которого получены конвертированием элементов исходного массива.</returns>
        public static IArray <O> ConvertAll <O> (IArrayView <T> input, Converter <T, O> converter) {
            if (input == null)
                throw new ArgumentNullException ("input");

            if (converter == null)
                throw new ArgumentNullException ("converter");

            var result = new Array <O> (new O[input.Length]);

            for (var i = 0; i < input.Length; ++i)
                result [i] = converter (input [i]);

            return result;
        }
    }
}
