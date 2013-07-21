#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Базовая среда передачи "Ethernet".
    /// </summary>
    public abstract class EthernetBase : Wire {
        /// <summary>
        ///   Инициализация среды передачи.
        /// </summary>
        /// <param name = "clock">Часы, используемые при передаче.</param>
        protected EthernetBase (IClock clock)
            : base (clock) {
            this.Type = PCAPNetworkTypes.LINKTYPE_ETHERNET;
        }
    }
}
