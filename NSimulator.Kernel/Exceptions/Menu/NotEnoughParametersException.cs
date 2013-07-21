#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при нехватке параметров контекста.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке задать параметры контекста, при этом
    ///   переданных параметров меньше, чем требует контекст.
    /// </remarks>
    [Serializable]
    public sealed class NotEnoughParametersException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "context">Название контекста.</param>
        /// <param name = "actual">Переданное количество параметров.</param>
        /// <param name = "expected">Необходимое количество параметров.</param>
        public NotEnoughParametersException (string context, int actual, int expected)
            : base (string.Format (Strings.NotEnoughParametersException, context, expected, actual)) {
            this.Context = context;
            this.ActualParameters = actual;
            this.ExpectedParameters = expected;
        }

        /// <summary>
        ///   Получает название контекста.
        /// </summary>
        /// <value>
        ///   Название контекста, которому передано неверное число аргументов.
        /// </value>
        public string Context { get; private set; }

        /// <summary>
        ///   Получает количество переданных аргументов.
        /// </summary>
        /// <value>
        ///   Количество переданых аргументов контексту <see cref = "Context" />.
        /// </value>
        public int ActualParameters { get; private set; }

        /// <summary>
        ///   Получает ожидаемое количество аргументов.
        /// </summary>
        /// <value>
        ///   Ожидаемое контекстом <see cref = "Context" /> количество аргументов.
        /// </value>
        public int ExpectedParameters { get; private set; }
    }
}
