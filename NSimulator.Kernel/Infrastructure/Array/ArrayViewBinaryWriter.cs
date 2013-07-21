#region

using System;
using System.IO;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Расширение бинарного потока для вывода массивов <see cref = "IArrayView{T}" />.
    /// </summary>
    public static class ArrayViewBinaryWriter {
        /// <summary>
        ///   Записывает массив в поток.
        /// </summary>
        /// <param name = "writer">Поток.</param>
        /// <param name = "array">Массив.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "writer" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "array" /> является <c>null</c>.</exception>
        public static void Write (this BinaryWriter writer, IArrayView <byte> array) {
            if (writer == null)
                throw new ArgumentNullException ("writer");

            if (array == null)
                throw new ArgumentNullException ("array");

            writer.Write (array.Base, array.Offset, array.Length);
        }
    }
}
