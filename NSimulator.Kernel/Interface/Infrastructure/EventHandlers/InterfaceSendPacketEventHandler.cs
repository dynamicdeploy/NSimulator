namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при отправке пакета с интерфейса.
    /// </summary>
    /// <param name = "iface">Интерфейс, с которого отправлен пакет.</param>
    public delegate void InterfaceSendPacketEventHandler (IInterfaceView iface);
}
