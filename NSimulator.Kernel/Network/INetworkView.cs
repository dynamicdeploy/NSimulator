#region

using System.Collections.Generic;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс топологии сети.
    ///   Предоставляется интерфейс только для чтения.
    /// </summary>
    /// <seealso cref = "INetwork" />
    [ContractClass (typeof (Contract_INetworkView))]
    public interface INetworkView {
        /// <summary>
        ///   Получает перечисление <see cref = "IEnumerable{T}" /> сред передачи данных,
        ///   имеющихся в топологии.
        /// </summary>
        /// <value>
        ///   Список сред передачи данных. Если сред нет, возвращается пустое перечисление.
        /// </value>
        IEnumerable <IBackboneView> Backbones { get; }

        /// <summary>
        ///   Получает перечисление <see cref = "IEnumerable{T}" /> интерфейсов,
        ///   имеющихся в топологии.
        /// </summary>
        /// <value>
        ///   Список интерфейсов. Если интерфейсов нет, возвращается пустое перечисление.
        /// </value>
        IEnumerable <IInterfaceView> Interfaces { get; }

        /// <summary>
        ///   Возвращает перечисление <see cref = "IEnumerable{T}" /> устройств,
        ///   имеющихся в топологии.
        /// </summary>
        /// <value>
        ///   Список устройств. Если устройств нет, возвращается пустое перечисление.
        /// </value>
        IEnumerable <IDeviceView> Devices { get; }

        /// <summary>
        ///   Получает часы, используемые для инициализации среды передачи данных,
        ///   загружаемых извне.
        /// </summary>
        /// <value>
        ///   Часы, используемые для инициализации сред передач данных.
        /// </value>
        IClockView Clock { get; }

        /// <summary>
        ///   Происходит, когда возникает ошибка (устранимая) при загрузке топологии.
        /// </summary>
        event LoadTopologyErrorEventHandler OnLoadError;
    }
}
