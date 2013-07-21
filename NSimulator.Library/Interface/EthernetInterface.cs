#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Ethernet-интерфейс.
    /// </summary>
    [InterfacePrefix ("e")]
    public class EthernetInterface : EthernetInterfaceBase {
        /// <summary>
        ///   Инициализирует интерфейс.
        /// </summary>
        /// <remarks>
        ///   Интерфейсу назначается <see cref = "MACAddress.NullAddress">нулевой</see> MAC-адрес.
        /// </remarks>
        public EthernetInterface ()
            : base (MACAddress.NullAddress) {}

        /// <summary>
        ///   Инициализирует интерфейс MAC-адресом.
        /// </summary>
        /// <param name = "macAddress">MAC-адрес интерфейса.</param>
        public EthernetInterface (MACAddress macAddress)
            : base (macAddress) {}

        /// <inheritdoc />
        protected override bool IsCompatibleWith (IBackboneView backbone) {
            return (backbone is Ethernet);
        }
    }
}
