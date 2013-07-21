#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при использовании некорректного описателя действия.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке отменить действие, зарегистрированное
    ///   в часах по неверному описателю.
    /// </remarks>
    [Serializable]
    public sealed class InvalidClockHandlerException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "handler">Описатель действия.</param>
        public InvalidClockHandlerException (ClockHandler handler)
            : base (string.Format (Strings.InvalidClockHandlerException, handler)) {
            this.Handler = handler;
        }

        /// <summary>
        ///   Получает описатель действия.
        /// </summary>
        /// <value>
        ///   Описатель неверного действия.
        /// </value>
        public ClockHandler Handler { get; private set; }
    }
}
