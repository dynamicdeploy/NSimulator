#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при несовместимости интерфейса
    ///   со средой передачи данных.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке выполнения привязки
    ///   интерфейса к среде передачи, с которой интерфейс несовместим.
    /// </remarks>
    [Serializable]
    public sealed class InterfaceNotCompatibleWithBackboneException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "iface_name">Имя интерфейса.</param>
        /// <param name = "backbone">Имя среды передачи.</param>
        public InterfaceNotCompatibleWithBackboneException (string iface_name, string backbone)
            : base (string.Format (Strings.InterfaceNotCompatibleWithBackboneException, iface_name, backbone)) {
            this.Interface = iface_name;
            this.Backbone = backbone;
        }

        /// <summary>
        ///   Получает название интерфейса.
        /// </summary>
        /// <value>
        ///   Название интерфейса, несовместимого со средой передачи данных.
        /// </value>
        public string Interface { get; private set; }

        /// <summary>
        ///   Получает среду передачи данных.
        /// </summary>
        /// <value>
        ///   Среда передачи данных, несовместимая с интерфейсом <see cref = "Interface" />.
        /// </value>
        public string Backbone { get; private set; }
    }
}
