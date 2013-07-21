namespace NSimulator.Library {
    /// <summary>
    ///   Ethernet-хаб.
    /// </summary>
    /// <remarks>
    ///   Хаб содержит гигабитные Ethernet-порты.
    /// </remarks>
    public class EthernetHub : BaseEthernetHub <GigabitEthernetInterface> {
        private EthernetHub ()
            : base (0) {}

        /// <summary>
        ///   Инициализирует хаб определённым числом интерфейсов.
        /// </summary>
        /// <param name = "interfaces">Количество интерфейсов в хабе.</param>
        public EthernetHub (int interfaces)
            : base (interfaces) {}
    }
}
