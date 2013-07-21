namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при привязывании интерфейса к среде передачи данных.
    /// </summary>
    /// <param name = "iface">Интерфейс, к которому привязывается среда.</param>
    /// <param name = "backbone">Привязываемая среда передачи данных.</param>
    public delegate void BackboneAttachedEventHandler (IInterfaceView iface, IBackboneView backbone);
}
