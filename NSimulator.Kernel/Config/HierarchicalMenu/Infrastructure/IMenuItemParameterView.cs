namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс параметра элемента меню.
    ///   Предоставляется интерфейс только для чтения.
    /// </summary>
    /// <seealso cref = "IMenuItemParameter" />
    public interface IMenuItemParameterView : IElement {
        /// <summary>
        ///   Получает значение параметра.
        /// </summary>
        /// <value>
        ///   Значение параметра.
        /// </value>
        string Value { get; }
    }
}
