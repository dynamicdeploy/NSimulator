#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при попытке дублирования среды передачи в топологии.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавить уже добавленную среду передачи
    ///   в топологию сети.
    /// </remarks>
    [Serializable]
    public sealed class NetworkContainsBackboneException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "backboneName">Название среды передачи данных.</param>
        public NetworkContainsBackboneException (string backboneName)
            : base (string.Format (Strings.NetworkContainsBackboneException, backboneName)) {
            this.Backbone = backboneName;
        }

        /// <summary>
        ///   Получает название среды передачи данных.
        /// </summary>
        /// <value>
        ///   Название среды передачи данных, которая уже имеется в топологии.
        /// </value>
        public string Backbone { get; private set; }
    }
}
