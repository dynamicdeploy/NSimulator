#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при нарушении уникальности имени интерфейса.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавления в устройство
    ///   физического интерфейса, имя которого совпадает с именем некоторого
    ///   другого интерфейса в том же устройстве.
    /// </remarks>
    [Serializable]
    public class InterfaceNameMustBeUniqueException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "iface_name">Имя интерфейса.</param>
        /// <param name = "device_name">Имя устройства.</param>
        public InterfaceNameMustBeUniqueException (string iface_name, string device_name)
            : base (string.Format (Strings.InterfaceNameMustBeUniqueException, iface_name, device_name)) {
            this.Interface = iface_name;
            this.Device = device_name;
        }

        /// <summary>
        ///   Получает название интерфейса.
        /// </summary>
        /// <value>
        ///   Название интерфейса, которое не является уникальным.
        /// </value>
        public string Interface { get; private set; }

        /// <summary>
        ///   Получает название устройства.
        /// </summary>
        /// <value>
        ///   Название устройства, уже содержащего интерфейс с названием <see cref = "Interface" />.
        /// </value>
        public string Device { get; private set; }
    }
}
