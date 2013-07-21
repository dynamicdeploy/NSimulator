#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   FastEthernet-интерфейс.
    /// </summary>
    [InterfacePrefix ("fa")]
    public class FastEthernetInterface : EthernetInterface {
        /// <summary>
        ///   Инициализирует интерфейс.
        /// </summary>
        /// <remarks>
        ///   Интерфейсу назначается <see cref = "MACAddress.NullAddress">нулевой</see> MAC-адрес.
        /// </remarks>
        public FastEthernetInterface ()
            : base (MACAddress.NullAddress) {}

        /// <summary>
        ///   Инициализирует интерфейс MAC-адресом.
        /// </summary>
        /// <param name = "macAddress">MAC-адрес интерфейса.</param>
        public FastEthernetInterface (MACAddress macAddress)
            : base (macAddress) {}

        /// <inheritdoc />
        protected override bool IsCompatibleWith (IBackboneView backbone) {
            return (backbone is FastEthernet) || (backbone is Ethernet);
        }
    }
}
