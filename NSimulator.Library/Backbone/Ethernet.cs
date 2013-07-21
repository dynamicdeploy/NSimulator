#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Среда передачи "Ethernet" со скоростью 10Mb/sec.
    /// </summary>
    /// <remarks>
    ///   Данная среда передачи совместима со следующими интерфейсами:
    ///   <list type = "bullet">
    ///     <item><see cref = "EthernetInterface" /></item>
    ///     <item><see cref = "FastEthernetInterface" /></item>
    ///     <item><see cref = "GigabitEthernetInterface" /></item>
    ///   </list>
    /// </remarks>
    // todo Ограничить скорость среды передачи.
    public class Ethernet : EthernetBase {
        /// <summary>
        ///   Инициализация среды передачи.
        /// </summary>
        /// <param name = "clock">Часы, используемые при передаче.</param>
        public Ethernet (IClock clock)
            : base (clock) {}
    }
}
