#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс массива.
    ///   Предоставляется расширение интерфейса только для чтения до полного интерфейса.
    /// </summary>
    /// <seealso cref = "IArrayView{T}" />
    /// <seealso cref = "Array{T}" />
    /// <typeparam name = "T">Тип элементов массива.</typeparam>
    public interface IArray <T> : IArrayView <T> {
        /// <summary>
        ///   Получает или устанавливает элемент с указанным индексом.
        /// </summary>
        /// <param name = "index">Индекс элемента.</param>
        /// <exception cref = "IndexOutOfRangeException">Индекс находится вне массива.</exception>
        /// <returns>Элемент с индексом <paramref name = "index" />.</returns>
        new T this [int index] { get; set; }

        /// <summary>
        ///   Получает срез массива - полуинтервал [<paramref name = "from" />..<paramref name = "to" />).
        /// </summary>
        /// <param name = "from">Индекс начала среза.</param>
        /// <param name = "to">Индекс конца среза.</param>
        /// <exception cref = "ArgumentOutOfRangeException">Индекс <paramref name = "from" /> находится вне массива.</exception>
        /// <exception cref = "ArgumentOutOfRangeException">Индекс <paramref name = "to" /> находится вне массива.</exception>
        /// <returns>Срез массива.</returns>
        new IArray <T> Slice (int from, int to);
    }
}
