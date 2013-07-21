namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при получении интерфейсом пакета.
    /// </summary>
    /// <param name = "iface">Интерфейс, на которй получен пакет.</param>
    public delegate void InterfaceReceivePacketEventHandler (IInterfaceView iface);
}
