#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс устройства.
    ///   Предоставляется интерфейс только для чтения.
    /// </summary>
    /// <seealso cref = "IDevice" />
    [ContractClass (typeof (Contract_IDeviceView))]
    public interface IDeviceView : INamedElement, IXMLSerializable {
        /// <summary>
        ///   Получает интерфейс с указанным названием.
        /// </summary>
        /// <param name = "name">Название интерфейса, который необходимо получить.</param>
        /// <returns>
        ///   Интерфейс с указанным названием.
        /// </returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceNotFoundException">Интерфейса с именем <paramref name = "name" /> не существует в устройстве.</exception>
        IInterfaceView this [string name] { get; }

        /// <summary>
        ///   Получает количество имеющихся в устройстве интерфейсов.
        /// </summary>
        /// <value>
        ///   Количество физических интерфейсов устройства.
        /// </value>
        int InterfacesCount { get; }

        /// <summary>
        ///   Получает перечисление <see cref = "IEnumerable{T}" /> всех интерфейсов устройства.
        /// </summary>
        /// <value>
        ///   Перечисление физических интерфейсов устройства.
        /// </value>
        IEnumerable <IInterfaceView> Interfaces { get; }

        /// <summary>
        ///   Получает прошивку устройства.
        /// </summary>
        /// <value>
        ///   Прошивка устройства.
        /// </value>
        IDeviceEngine Engine { get; }

        /// <summary>
        ///   Возвращает <c>true</c>, если устройство включено.
        ///   Выключенное устройство неспособно обрабатывать данные.
        /// </summary>
        /// <value>
        ///   <c>true</c>, если устройство включено.
        /// </value>
        bool Enabled { get; }

        /// <summary>
        ///   Обрабатывает пакет, полученный с интерфейса устройства.
        /// </summary>
        /// <param name = "packet">Полученный пакет.</param>
        /// <param name = "from">Интерфейс, с которого пакет получен.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "packet" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "from" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceNotAttachedToDeviceException">Интерфейс не привязан к данному устройству.</exception>
        void ProcessPacket (IPacket packet, IInterfaceView from);

        /// <summary>
        ///   Происходит при включении устройства.
        /// </summary>
        /// <remarks>
        ///   Событие происходит до того, как устройство будет включено.
        /// </remarks>
        event DeviceStateChangedEventHandler OnBeforeEnable;

        /// <summary>
        ///   Происходит при выключении устройства.
        /// </summary>
        /// <remarks>
        ///   Событие происходит до того, как устройство будет выключено.
        /// </remarks>
        event DeviceStateChangedEventHandler OnBeforeDisable;

        /// <summary>
        ///   Происходит при добавлении нового интерфейса.
        /// </summary>
        /// <remarks>
        ///   Событие происходит до того, как интерфейс будет добавлен.
        /// </remarks>
        event DeviceStructureChangedEventHandler OnBeforeAddInterface;

        /// <summary>
        ///   Происходит при удалении интерфейса.
        /// </summary>
        /// <remarks>
        ///   Событие происходит до того, как интерфейс будет удалён.
        /// </remarks>
        event DeviceStructureChangedEventHandler OnBeforeRemoveInterface;
    }
}
