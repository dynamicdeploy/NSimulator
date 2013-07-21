#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Среда передачи "FastEthernet" со скоростью 100Mb/sec.
    /// </summary>
    /// <remarks>
    ///   Данная среда передачи совместима со следующими интерфейсами:
    ///   <list type = "bullet">
    ///     <item><see cref = "FastEthernetInterface" /></item>
    ///     <item><see cref = "GigabitEthernetInterface" /></item>
    ///   </list>
    /// </remarks>
    // todo Ограничить скорость среды передачи.
    public class FastEthernet : Ethernet {
        /// <summary>
        ///   Инициализация среды передачи.
        /// </summary>
        /// <param name = "clock">Часы, используемые при передаче.</param>
        public FastEthernet (IClock clock)
            : base (clock) {}
    }
}
