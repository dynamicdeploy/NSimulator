#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при неверном использовании API ядерных сущностей симулятора.
    /// </summary>
    [Serializable]
    public abstract class NSimulatorException : ApplicationException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения с указанной строкой сообщения.
        /// </summary>
        /// <param name = "message">Сообщение ошибки.</param>
        protected NSimulatorException (string message)
            : base (message) {}
    }
}
