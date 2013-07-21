namespace NSimulator.Library {
    /// <summary>
    ///   Типы протоколов в Ethernet-фрейме.
    /// </summary>
    public enum EtherType : ushort {
        /// <summary>
        ///   IPv4
        /// </summary>
        IPv4 = 0x0800,

        /// <summary>
        ///   ARP
        /// </summary>
        ARP = 0x0806,

        /// <summary>
        ///   Tagged (VLAN)
        /// </summary>
        Tagged = 0x8100,

        /// <summary>
        ///   IPv6
        /// </summary>
        IPv6 = 0x86DD
    }
}
