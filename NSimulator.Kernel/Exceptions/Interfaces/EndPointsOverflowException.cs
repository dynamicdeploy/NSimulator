#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при переполнении количества
    ///   конечных точек в среде передачи данных.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавления интерфейса
    ///   в среду передачи, при этом среда передачи не может иметь количество
    ///   интерфейсов больше, чем уже есть.
    /// </remarks>
    [Serializable]
    public sealed class EndPointsOverflowException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "backbone">Среда передачи данных.</param>
        /// <param name = "capacity">Максимальное количество конечных точек.</param>
        public EndPointsOverflowException (string backbone, int capacity)
            : base (string.Format (Strings.EndPointsOverflowException, backbone, capacity)) {
            this.Backbone = backbone;
            this.Capacity = capacity;
        }

        /// <summary>
        ///   Получает среду передачи данных.
        /// </summary>
        /// <value>
        ///   Среда передачи данных, у которой достигнуто максимальное количество
        ///   конечных точек.
        /// </value>
        public string Backbone { get; private set; }

        /// <summary>
        ///   Получает максимальное число конечных точек.
        /// </summary>
        /// <value>
        ///   Имаксимальное количество конечных точек на среде передачи <see cref = "Backbone" />.
        /// </value>
        public int Capacity { get; private set; }
    }
}
