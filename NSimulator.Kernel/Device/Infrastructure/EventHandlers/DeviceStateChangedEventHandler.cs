namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при изменении состояния устройства.
    /// </summary>
    /// <param name = "device">Устройство, изменившее состояние.</param>
    /// <param name = "state">Новое состояние устройства.</param>
    public delegate void DeviceStateChangedEventHandler (IDeviceView device, bool state);
}
