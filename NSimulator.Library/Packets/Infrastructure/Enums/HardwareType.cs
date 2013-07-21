namespace NSimulator.Library {
    /// <summary>
    ///   Тип канального уровня в ARP-пакете.
    /// </summary>
    public enum HardwareType : ushort {
        /// <summary>
        ///   Ethernet.
        /// </summary>
        [FieldLength (6)]
        ETHERNET = 1
    }
}
