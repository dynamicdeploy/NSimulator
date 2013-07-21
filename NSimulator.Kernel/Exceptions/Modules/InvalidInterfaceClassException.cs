#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при инициализации атрибута модуля неверным типом.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке инициализировать атрибут модуля
    ///   информацией о типе, не являющимся реализацией интерфейса <see cref = "IInterfaceView" />.
    /// </remarks>
    [Serializable]
    public sealed class InvalidInterfaceClassException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "className">Название класса.</param>
        public InvalidInterfaceClassException (string className)
            : base (string.Format (Strings.InvalidInterfaceClassException, className)) {
            this.ClassName = className;
        }

        /// <summary>
        ///   Получает название класса.
        /// </summary>
        /// <value>
        ///   Название класса, не реализующего интерфейс <see cref = "IInterfaceView" />.
        /// </value>
        public string ClassName { get; private set; }
    }
}
