namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при передаче пакета по среде передачи данных.
    /// </summary>
    /// <seealso cref = "IBackboneView" />
    /// <param name = "backbone">Среда передачи, в которой передаётся пакет.</param>
    /// <param name = "packet">Передающийся пакет.</param>
    public delegate void TransmitEventHandler (IBackboneView backbone, IPacket packet);
}
