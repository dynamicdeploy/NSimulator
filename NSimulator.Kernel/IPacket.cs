namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс пакета данных.
    /// </summary>
    public interface IPacket {
        /// <summary>
        ///   Получает байтовое представление пакета.
        /// </summary>
        /// <value>
        ///   Байтовое представление пакета.
        /// </value>
        IArray <byte> Data { get; }

        /// <summary>
        ///   Получает байтовое представление данных пакета.
        /// </summary>
        /// <remarks>
        ///   Свойство возвращает только данные пакета, в отличие от
        ///   <see cref = "Data" />, которое возвращает ещё и представление
        ///   служебной информации.
        /// </remarks>
        /// <value>
        ///   Байтовое представление данных пакета.
        /// </value>
        IArray <byte> InternalData { get; }
    }
}
