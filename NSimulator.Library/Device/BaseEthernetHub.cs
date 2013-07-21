namespace NSimulator.Library {
    /// <summary>
    ///   Абстрактный хаб с Ethernet-портами.
    /// </summary>
    /// <typeparam name = "T">Тип Ethernet-интерфейса.</typeparam>
    public abstract class BaseEthernetHub <T> : BaseHub <T>
        where T : EthernetInterfaceBase, new ( ) {
        /// <summary>
        ///   Инициализирует хаб определённым число интерфейсов.
        /// </summary>
        /// <param name = "interfaces">Количество интерфейсов.</param>
        protected BaseEthernetHub (int interfaces)
            : base (interfaces) {}
        }
}
