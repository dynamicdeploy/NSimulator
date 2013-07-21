#region

using System;
using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс массива.
    ///   Предоставляется интерфейс только для чтения.
    /// </summary>
    /// <seealso cref = "IArray{T}" />
    /// <seealso cref = "ArrayView{T}" />
    /// <typeparam name = "T">Тип элементов массива.</typeparam>
    public interface IArrayView <T> : IEnumerable <T>, IEquatable <IArrayView <T>> {
        /// <summary>
        ///   Получает элемент с указанным индексом.
        /// </summary>
        /// <param name = "index">Индекс элемента.</param>
        /// <exception cref = "IndexOutOfRangeException">Индекс находится вне массива.</exception>
        /// <returns>Элемент с индексом <paramref name = "index" />.</returns>
        T this [int index] { get; }

        /// <summary>
        ///   Получает смещение массива в базовом массиве.
        /// </summary>
        /// <value>
        ///   Смещение массива в базовом массиве.
        /// </value>
        int Offset { get; }

        /// <summary>
        ///   Получает размер массива.
        /// </summary>
        /// <value>
        ///   Размер массива.
        /// </value>
        int Length { get; }

        /// <summary>
        ///   Получает базовый массив.
        /// </summary>
        /// <value>
        ///   Базовый массив.
        /// </value>
        T [] Base { get; }

        /// <summary>
        ///   Получает срез массива - полуинтервал [<paramref name = "from" />..<paramref name = "to" />).
        /// </summary>
        /// <param name = "from">Индекс начала среза.</param>
        /// <param name = "to">Индекс конца среза.</param>
        /// <exception cref = "ArgumentOutOfRangeException">Индекс <paramref name = "from" /> находится вне массива.</exception>
        /// <exception cref = "ArgumentOutOfRangeException">Индекс <paramref name = "to" /> находится вне массива.</exception>
        /// <returns>Срез массива.</returns>
        IArrayView <T> Slice (int from, int to);
    }
}
