namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при возникновении ошибки при загрузке топологии сети.
    /// </summary>
    /// <param name = "error">Возникшая ошибка.</param>
    /// <seealso cref = "ILoadTopologyError" />
    public delegate void LoadTopologyErrorEventHandler (ILoadTopologyError error);
}
