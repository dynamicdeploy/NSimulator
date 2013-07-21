#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при невозможности автодополнения элемента.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке выполнения автодополнения элемента,
    ///   при котором требуется получить не более одного возможного результата дополнения
    ///   либо более одного, но имеющего точный результат.
    /// </remarks>
    [Serializable]
    public sealed class MenuElementNameIsAmbiguousException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "element">Название элемента.</param>
        /// <param name = "context">Название контекста.</param>
        public MenuElementNameIsAmbiguousException (string element, string context)
            : base (string.Format (Strings.MenuElementNameIsAmbigiousException, element, context)) {
            this.Element = element;
            this.Context = context;
        }

        /// <summary>
        ///   Получает название элемента.
        /// </summary>
        /// <value>
        ///   Название элемента, которое невозможно автодополнить.
        /// </value>
        public string Element { get; private set; }

        /// <summary>
        ///   Получает название контекста.
        /// </summary>
        /// <value>
        ///   Название контекста, в котором невозможно выполнить автодополнение элемента <see cref = "Element" />.
        /// </value>
        public string Context { get; private set; }
    }
}
