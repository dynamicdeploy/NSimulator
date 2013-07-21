#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс топологии сети.
    ///   Предоставляется расширение интерфейса только для чтения до полного интерфейса.
    /// </summary>
    /// <seealso cref = "INetworkView" />
    [ContractClass (typeof (Contract_INetwork))]
    public interface INetwork : INetworkView {
        /// <summary>
        ///   Получает перечисление <see cref = "IEnumerable{T}" /> сред передачи данных,
        ///   имеющихся в топологии.
        /// </summary>
        /// <value>
        ///   Список сред передачи данных. Если сред нет, возвращается пустое перечисление.
        /// </value>
        new IEnumerable <IBackbone> Backbones { get; }

        /// <summary>
        ///   Получает перечисление <see cref = "IEnumerable{T}" /> интерфейсов,
        ///   имеющихся в топологии.
        /// </summary>
        /// <value>
        ///   Список интерфейсов. Если интерфейсов нет, возвращается пустое перечисление.
        /// </value>
        new IEnumerable <IInterface> Interfaces { get; }

        /// <summary>
        ///   Возвращает перечисление <see cref = "IEnumerable{T}" /> устройств,
        ///   имеющихся в топологии.
        /// </summary>
        /// <value>
        ///   Список устройств. Если устройств нет, возвращается пустое перечисление.
        /// </value>
        new IEnumerable <IDevice> Devices { get; }

        /// <summary>
        ///   Получает часы, используемые для инициализации среды передачи данных,
        ///   загружаемых извне.
        /// </summary>
        /// <value>
        ///   Часы, используемые для инициализации сред передач данных.
        /// </value>
        new IClock Clock { get; }

        /// <summary>
        ///   Добавляет среду передачи данных в топологию сети.
        /// </summary>
        /// <param name = "backbone">Добавляемая среда передачи.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> является <c>null</c>.</exception>
        /// <exception cref = "NetworkContainsInterfaceException">Среда передачи <paramref name = "backbone" /> уже имеется в топологии.</exception>
        /// <returns>Описатель добавленной сущности.</returns>
        NetworkEntity AddBackbone (IBackbone backbone);

        /// <summary>
        ///   Удаляет среду передачи данных из топологоии сети.
        /// </summary>
        /// <param name = "backboneEntity">Описатель удаляемой среды передачи.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "backboneEntity" /> является <c>null</c>.</exception>
        /// <exception cref = "EntityHandlerNotFoundException">Описатель <paramref name = "backboneEntity" /> некорректный.</exception>
        void RemoveBackbone (NetworkEntity backboneEntity);

        /// <summary>
        ///   Добавляет интерфейс в топологию сети.
        /// </summary>
        /// <param name = "iface">Добавляемый интерфейс.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> является <c>null</c>.</exception>
        /// <exception cref = "NetworkContainsInterfaceException">Интерфейс <paramref name = "iface" /> уже имеется в топологии.</exception>
        /// <returns>Описатель добавленной сущности.</returns>
        NetworkEntity AddInterface (IInterface iface);

        /// <summary>
        ///   Удаляет интерфейс из топологии сети.
        /// </summary>
        /// <param name = "interfaceEntity">Описатель удаляемого интерфейса.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "interfaceEntity" /> является <c>null</c>.</exception>
        /// <exception cref = "EntityHandlerNotFoundException">Описатель <paramref name = "interfaceEntity" /> некорректный.</exception>
        void RemoveInterface (NetworkEntity interfaceEntity);

        /// <summary>
        ///   Добавляет устройство в топологию сети.
        /// </summary>
        /// <param name = "device">Добавляемое устройство.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "device" /> является <c>null</c>.</exception>
        /// <exception cref = "NetworkContainsInterfaceException">Устройство <paramref name = "device" /> уже имеется в топологии.</exception>
        /// <returns>Описатель добавленной сущности.</returns>
        NetworkEntity AddDevice (IDevice device);

        /// <summary>
        ///   Удаляет устройство из топологии сети.
        /// </summary>
        /// <param name = "deviceEntity">Описатель удаляемого устройства.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "deviceEntity" /> является <c>null</c>.</exception>
        /// <exception cref = "EntityHandlerNotFoundException">Описатель <paramref name = "deviceEntity" /> некорректный.</exception>
        void RemoveDevice (NetworkEntity deviceEntity);

        /// <summary>
        ///   Загружает топологию сети из файла <paramref name = "filename" />.
        /// </summary>
        /// <param name = "filename">Имя файла, из которого нужно загрузить топологию.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "filename" /> является <c>null</c>.</exception>
        /// <exception cref = "SchemaValidationException">Файл <paramref name = "filename" /> имеет неверный формат.</exception>
        void LoadFromXml (string filename);

        /// <summary>
        ///   Сохраняет топологию сети в файл.
        /// </summary>
        /// <param name = "filename">Имя файла, в который нужно сохранить топологию.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "filename" /> является <c>null</c>.</exception>
        void SaveToXml (string filename);
    }
}
