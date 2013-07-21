namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при изменении состояния интерфейса.
    /// </summary>
    /// <param name = "iface">Интерфейс, состояние которого было изменено.</param>
    /// <param name = "state">Новое состояние интерфейса.</param>
    public delegate void InterfaceStateChangedEventHandler (IInterfaceView iface, bool state);
}
