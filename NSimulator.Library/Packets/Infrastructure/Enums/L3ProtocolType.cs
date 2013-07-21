namespace NSimulator.Library {
    /// <summary>
    ///   Тип сетевого уровня в ARP-пакете.
    /// </summary>
    public enum L3ProtocolType : ushort {
        /// <summary>
        ///   IPv4.
        /// </summary>
        [FieldLength (4)]
        IPv4 = 0x0800
    }
}
