#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс среды передачи данных.
    ///   Предоставляется интерфейс только для чтения.
    /// </summary>
    /// <seealso cref = "IBackbone" />
    [ContractClass (typeof (Contract_IBackboneView))]
    public interface IBackboneView : INamedElement, IXMLSerializable {
        /// <summary>
        ///   Получает количество конечных точек среды передачи данных.
        /// </summary>
        /// <value>
        ///   Количество конечных точек среды передачи данных.
        /// </value>
        int EndPointsCount { get; }

        /// <summary>
        ///   Получает максимальное количество точек среды передачи данных.
        /// </summary>
        /// <value>
        ///   Максимальное количество конечных точек среды передачи данных.
        /// </value>
        int EndPointsCapacity { get; }

        /// <summary>
        ///   Получает перечисление <see cref = "IEnumerable{T}" /> конечных точек среды передачи данных.
        /// </summary>
        /// <value>
        ///   Список конечных точек. Если конечных точек нет, возвращается пустое перечисление.
        /// </value>
        IEnumerable <IInterfaceView> EndPoints { get; }

        /// <summary>
        ///   Получает pcap-тип среды передачи.
        /// </summary>
        /// <value>
        ///   Pcap-тип среды передачи.
        /// </value>
        /// <remarks>
        ///   Данное свойство является характеристикой среды передачи данных.
        /// </remarks>
        [BackboneCharacteristic]
        PCAPNetworkTypes Type { get; }

        /// <summary>
        ///   Получает состояние среды передачи.
        /// </summary>
        /// <value>
        ///   Состояние среды передачи.
        /// </value>
        /// <remarks>
        ///   Данное свойство является характеристикой среды передачи данных.
        /// </remarks>
        [BackboneCharacteristic]
        Enum State { get; }

        /// <summary>
        ///   Получает скорость передачи данных в среде (бит/с).
        /// </summary>
        /// <value>
        ///   Скорость передачи данных.
        /// </value>
        /// <remarks>
        ///   Данное свойство является характеристикой среды передачи данных.
        /// </remarks>
        [BackboneCharacteristic]
        ulong Speed { get; }

        /// <summary>
        ///   Получает долю потерь пакетов в среде.
        /// </summary>
        /// <value>
        ///   Доля потерь пакетов. 0, если потерь нет и 1, если теряется всё.
        /// </value>
        /// <remarks>
        ///   Данное свойство является характеристикой среды передачи данных.
        /// </remarks>
        [BackboneCharacteristic]
        double LossPercent { get; }

        /// <summary>
        ///   Выполняет передачу пакета с указанного интерфейса.
        /// </summary>
        /// <param name = "packet">Пакет, который необходимо передать.</param>
        /// <param name = "from">Интерфейс, с которого осуществляется передача.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "packet" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "from" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceNotAttachedToBackboneException">Физический интерфейс <paramref name = "from" /> не привязан к данной среде передачи данных.</exception>
        void SendPacket (IPacket packet, IInterfaceView from);

        /// <summary>
        ///   Происходит, когда по среде передачи данных передаётся пакет.
        /// </summary>
        event TransmitEventHandler OnTransmit;
    }
}
