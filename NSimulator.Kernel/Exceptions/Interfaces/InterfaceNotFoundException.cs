#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при отсутствии интерфейса в устройстве.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке получения доступа к
    ///   физическому интерфейсу на устройстве по имени, но при этом
    ///   устройство не содержит интерфейса с таким именем.
    /// </remarks>
    [Serializable]
    public sealed class InterfaceNotFoundException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "iface_name">Имя отсутствующего интерфейса.</param>
        /// <param name = "device_name">Имя устройства, в котором интерфейс отсутствует.</param>
        public InterfaceNotFoundException (string iface_name, string device_name)
            : base (string.Format (Strings.InterfaceNotFoundException, iface_name, device_name)) {
            this.Interface = iface_name;
            this.Device = device_name;
        }

        /// <summary>
        ///   Получает название интерфейса.
        /// </summary>
        /// <value>
        ///   Название отсутствующего в устройстве интерфейса.
        /// </value>
        public string Interface { get; private set; }

        /// <summary>
        ///   Получает название устройства.
        /// </summary>
        /// <value>
        ///   Название устройства, в котором отсутствует интерфейс с именем <see cref = "Interface" />.
        /// </value>
        public string Device { get; private set; }
    }
}
