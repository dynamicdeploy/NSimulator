namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при отвязывании интерфейса от среды передачи данных.
    /// </summary>
    /// <param name = "iface">Интерфейс, от которого отвязывается среда.</param>
    public delegate void BackboneDetachedEventHandler (IInterfaceView iface);
}
