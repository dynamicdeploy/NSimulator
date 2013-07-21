#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при несогласованном использовании
    ///   физического интерфейса и устройства.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     Исключение пробрасывается при попытке выполнения привязки
    ///     физического интерфейса к устройству, которое не содержит
    ///     привязываемого интерфейса.
    ///   </para>
    /// 
    ///   <para>
    ///     Исключение также может быть проброшено при попытке получения с
    ///     "поднятого" физического интерфейса пакета из устройства, но при
    ///     этом никакое устройство не привязано к интерфейсу.
    ///   </para>
    /// </remarks>
    [Serializable]
    public class InterfaceNotAttachedToDeviceException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "iface_name">Имя отсутствующего интерфейса.</param>
        /// <param name = "device_name">Имя устройства, в котором интерфейс отсутствует.</param>
        public InterfaceNotAttachedToDeviceException (string iface_name, string device_name)
            : base (string.Format (Strings.InterfaceNotAttachedToDeviceException2, iface_name, device_name)) {
            this.Interface = iface_name;
            this.Device = device_name;
        }

        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "iface_name">Имя отсутствующего интерфейса.</param>
        public InterfaceNotAttachedToDeviceException (string iface_name)
            : base (string.Format (Strings.InterfaceNotAttachedToDeviceException1, iface_name)) {
            this.Interface = iface_name;
            this.Device = null;
        }

        /// <summary>
        ///   Получает название интерфейса.
        /// </summary>
        /// <value>
        ///   Название интерфейса, который отсутствует в устройстве.
        /// </value>
        public string Interface { get; private set; }

        /// <summary>
        ///   Получает название устройства.
        /// </summary>
        /// <value>
        ///   Название устройства, в котором отсутствует интерфейс <see cref = "Interface" />
        ///   или <c>null</c>, если интерфейс не содержится ни в одном устройстве.
        /// </value>
        public string Device { get; private set; }
    }
}
