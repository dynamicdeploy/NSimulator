namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс изменяемости среды передачи данных.
    /// </summary>
    /// <remarks>
    ///   Если среда передачи данных может изменять во время работы
    ///   свой pcap-тип, то реализуемый класс должен реализовывать данный
    ///   интерфейс.
    /// </remarks>
    public interface IPCAPTypeModifyable {
        /// <summary>
        ///   Изменяет тип среды передачи данных.
        /// </summary>
        /// <param name = "type">Новый тип среды передачи.</param>
        void SetPCAPType (PCAPNetworkTypes type);
    }
}
