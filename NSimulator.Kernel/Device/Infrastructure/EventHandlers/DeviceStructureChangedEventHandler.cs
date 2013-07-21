namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при изменении физической структуры устройства.
    /// </summary>
    /// <param name = "device">Устройство, структура которого изменена.</param>
    /// <param name = "iface">Изменённый интерфейс устройства (добавленный или удалённый).</param>
    public delegate void DeviceStructureChangedEventHandler (IDeviceView device, IInterfaceView iface);
}
