namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при обработке пакета устройством.
    /// </summary>
    /// <param name = "device">Устройство, обрабатывающее пакет.</param>
    public delegate void DeviceProcessPacketEventHandler (IDeviceView device);
}
