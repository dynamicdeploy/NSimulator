#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при несогласованном использовании
    ///   физического интерфейса и среды передачи данных.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке привязки к интерфейсу с
    ///   уже привязанной средой передачи данных другой среды передачи.
    /// </remarks>
    [Serializable]
    public sealed class InterfaceAlreadyAttachedToBackboneException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "iface_name">Имя интерфейса.</param>
        public InterfaceAlreadyAttachedToBackboneException (string iface_name)
            : base (string.Format (Strings.InterfaceAlreadyAttachedToBackboneException, iface_name)) {
            this.Interface = iface_name;
        }

        /// <summary>
        ///   Получает название интерфейса.
        /// </summary>
        /// <value>
        ///   Название интерфейса, уже привязанного к среде передачи.
        /// </value>
        public string Interface { get; private set; }
    }
}
