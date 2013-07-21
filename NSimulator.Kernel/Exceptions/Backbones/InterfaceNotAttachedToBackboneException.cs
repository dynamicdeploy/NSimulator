#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при несогласованном использовании
    ///   физического интерфейса и среды передачи данных.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     Исключение пробрасывается при попытке выполнения привязки
    ///     физического интерфейса к среде передачи данных, которая
    ///     не привязана к привязываемому интерфейсу.
    ///   </para>
    /// 
    ///   <para>
    ///     Исключение также может быть проброшено при попытке отправки с "поднятого"
    ///     физического интерфейса пакета в среду передачи данных, но при этом никакая
    ///     среда передачи не привязана к интерфейсу.
    ///   </para>
    /// </remarks>
    [Serializable]
    public sealed class InterfaceNotAttachedToBackboneException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "iface_name">Имя интерфейса.</param>
        public InterfaceNotAttachedToBackboneException (string iface_name)
            : base (string.Format (Strings.InterfaceNotAttachedToBackboneException1, iface_name)) {
            this.Interface = iface_name;
            this.Backbone = null;
        }

        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "iface_name">Имя интерфейса.</param>
        /// <param name = "backbone">Имя среды передачи данных.</param>
        public InterfaceNotAttachedToBackboneException (string iface_name, string backbone)
            : base (string.Format (Strings.InterfaceNotAttachedToBackboneException2, iface_name, backbone)) {
            this.Interface = iface_name;
            this.Backbone = backbone;
        }

        /// <summary>
        ///   Получает название интерфейса.
        /// </summary>
        /// <value>
        ///   Название интерфейса, который не является конечной точкой среды передачи.
        /// </value>
        public string Interface { get; private set; }

        /// <summary>
        ///   Получает название среды передачи данных.
        /// </summary>
        /// <value>
        ///   Название среды передачи, в которой отсутствует конечная точка <see cref = "Interface" />
        ///   или <c>null</c>, если интерфейс не является конечной точкой ни в одной среде передачи.
        /// </value>
        public string Backbone { get; private set; }
    }
}
