namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс элемента меню.
    /// </summary>
    public interface IElement : INamedElement {
        /// <summary>
        ///   Получает описание элемента.
        /// </summary>
        /// <value>
        ///   Описание элемента.
        /// </value>
        string Description { get; }
    }
}
