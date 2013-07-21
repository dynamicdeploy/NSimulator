namespace NSimulator.Library {
    /// <summary>
    ///   Тип операции ARP.
    /// </summary>
    public enum ARPOperation : byte {
        /// <summary>
        ///   Запрос.
        /// </summary>
        REQUEST = 1,

        /// <summary>
        ///   Ответ.
        /// </summary>
        RESPONSE = 2
    }
}
