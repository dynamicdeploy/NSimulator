namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс параметра элемента меню.
    ///   Предоставляется расширение интерфейса только для чтения до полного интерефейса.
    /// </summary>
    public interface IMenuItemParameter : IMenuItemParameterView {
        /// <summary>
        ///   Устанавливает значение параметра.
        /// </summary>
        /// <param name = "value">Новое значение параметра.</param>
        void SetValue (string value);
    }
}
