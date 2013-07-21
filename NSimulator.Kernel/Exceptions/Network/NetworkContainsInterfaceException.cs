#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при попытке дублирования интерфейса в топологии.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавить уже добавленный интерфейс
    ///   в топологию сети.
    /// </remarks>
    [Serializable]
    public sealed class NetworkContainsInterfaceException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "interfaceName">Название интерфейса.</param>
        public NetworkContainsInterfaceException (string interfaceName)
            : base (string.Format (Strings.NetworkContainsInterfaceException, interfaceName)) {
            this.Interface = interfaceName;
        }

        /// <summary>
        ///   Получает название интерфейса.
        /// </summary>
        /// <value>
        ///   Название интерфейса, который уже имеется в топологии.
        /// </value>
        public string Interface { get; private set; }
    }
}
