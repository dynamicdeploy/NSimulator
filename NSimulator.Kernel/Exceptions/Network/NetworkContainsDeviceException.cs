#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при попытке дублирования устройства в топологии.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавить уже добавленное устройство
    ///   в топологию сети.
    /// </remarks>
    [Serializable]
    public sealed class NetworkContainsDeviceException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "deviceName">Название устройства.</param>
        public NetworkContainsDeviceException (string deviceName)
            : base (string.Format (Strings.NetworkContainsDeviceException, deviceName)) {
            this.Device = deviceName;
        }

        /// <summary>
        ///   Получает название устройства.
        /// </summary>
        /// <value>
        ///   Название устройства, которое уже имеется в топологии.
        /// </value>
        public string Device { get; private set; }
    }
}
