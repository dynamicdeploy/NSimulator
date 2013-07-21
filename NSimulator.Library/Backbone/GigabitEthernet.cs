#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Среда передачи "GigabitEthernet" со скоростью 1Gb/sec.
    /// </summary>
    /// <remarks>
    ///   Данная среда передачи совместима со следующими интерфейсами:
    ///   <list type = "bullet">
    ///     <item><see cref = "GigabitEthernetInterface" /></item>
    ///   </list>
    /// </remarks>
    // todo Ограничить скорость среды передачи.
    public class GigabitEthernet : FastEthernet {
        /// <summary>
        ///   Инициализация среды передачи.
        /// </summary>
        /// <param name = "clock">Часы, используемые при передаче.</param>
        public GigabitEthernet (IClock clock)
            : base (clock) {}
    }
}
