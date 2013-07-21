#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   GigabitEthernet-интерфейс.
    /// </summary>
    [InterfacePrefix ("gi")]
    public class GigabitEthernetInterface : FastEthernetInterface {
        /// <summary>
        ///   Инициализирует интерфейс.
        /// </summary>
        /// <remarks>
        ///   Интерфейсу назначается <see cref = "MACAddress.NullAddress">нулевой</see> MAC-адрес.
        /// </remarks>
        public GigabitEthernetInterface ()
            : base (MACAddress.NullAddress) {}

        /// <summary>
        ///   Инициализирует интерфейс MAC-адресом.
        /// </summary>
        /// <param name = "macAddress">MAC-адрес интерфейса.</param>
        public GigabitEthernetInterface (MACAddress macAddress)
            : base (macAddress) {}

        /// <inheritdoc />
        protected override bool IsCompatibleWith (IBackboneView backbone) {
            return (backbone is GigabitEthernet) || (backbone is FastEthernet) || (backbone is Ethernet);
        }
    }
}
