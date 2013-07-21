#region

using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Пара элементов.
    /// </summary>
    /// <typeparam name = "T1">Тип первого элемента.</typeparam>
    /// <typeparam name = "T2">Тип второго элемента.</typeparam>
    public sealed class Pair <T1, T2> {
        /// <summary>
        ///   Инициализирует пару элементов конкретными элементами.
        /// </summary>
        /// <param name = "first">Первый элемент.</param>
        /// <param name = "second">Второй элемент.</param>
        public Pair (T1 first, T2 second) {
            this.First = first;
            this.Second = second;
        }

        /// <summary>
        ///   Инициализирует пару элементов парой "ключ-значение" словаря.
        /// </summary>
        /// <param name = "pair">Пара "ключ-значение".</param>
        public Pair (KeyValuePair <T1, T2> pair) {
            this.First = pair.Key;
            this.Second = pair.Value;
        }

        /// <summary>
        ///   Получает или устанавливает первый элемент пары.
        /// </summary>
        /// <value>
        ///   Первый элемент пары.
        /// </value>
        public T1 First { get; set; }

        /// <summary>
        ///   Получает или устанавливает второй элемент пары.
        /// </summary>
        /// <value>
        ///   Второй элемент пары.
        /// </value>
        public T2 Second { get; set; }
    }
}
