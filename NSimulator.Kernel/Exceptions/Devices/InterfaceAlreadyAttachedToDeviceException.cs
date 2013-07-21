#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при несогласованном использовании
    ///   физического интерфейса и устройства.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке привязки к интерфейсу с
    ///   уже привязанным устройством другого устройства.
    /// </remarks>
    [Serializable]
    public sealed class InterfaceAlreadyAttachedToDeviceException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "iface_name">Имя интерфейса.</param>
        public InterfaceAlreadyAttachedToDeviceException (string iface_name)
            : base (string.Format (Strings.InterfaceAlreadyAttachedToDeviceException, iface_name)) {
            this.Interface = iface_name;
        }

        /// <summary>
        ///   Получает название интерфейса.
        /// </summary>
        /// <value>
        ///   Название интерфейса, уже привязанного к устройству.
        /// </value>
        public string Interface { get; private set; }
    }
}
