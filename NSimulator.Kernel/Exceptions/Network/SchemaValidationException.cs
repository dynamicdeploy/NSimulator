#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при ошибке валидации документа xsd-схемой.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке загрузить некорректное xml-описание
    ///   топологии сети (не удовлетворяющее xsd-схеме).
    /// </remarks>
    [Serializable]
    public sealed class SchemaValidationException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "exception">Внутреннее исключение валидации.</param>
        public SchemaValidationException (Exception exception)
            : base (string.Format (Strings.SchemaValidationException, exception.Message)) {
            this.ValidationException = exception;
        }

        /// <summary>
        ///   Получает исключение, возникшее при валидации.
        /// </summary>
        /// <value>
        ///   Исключение, возникшее при валидации.
        /// </value>
        public Exception ValidationException { get; private set; }
    }
}
