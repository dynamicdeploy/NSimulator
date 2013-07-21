#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс физического интерфейса.
    ///   Предоставляется интерфейс только для чтения.
    /// </summary>
    /// <seealso cref = "InterfacePrefixAttribute" />
    /// <seealso cref = "IInterface" />
    /// <remarks>
    ///   У интерфейса может быть задан атрибут <see cref = "InterfacePrefixAttribute" />, который
    ///   задаёт префикс имени интерфейса.
    /// </remarks>
    [ContractClass (typeof (Contract_IInterfaceView))]
    public interface IInterfaceView : INamedElement, IXMLSerializable {
        /// <summary>
        ///   Получает среду передачи данных, привязанную к интерфейсу.
        /// </summary>
        /// <value>
        ///   Среда передачи данных, подключённая к интерфейсу или <c>null</c>, если таковой нет.
        /// </value>
        IBackboneView Backbone { get; }

        /// <summary>
        ///   Получает устройство, которое содержит данный интерфейс.
        /// </summary>
        /// <value>
        ///   Устройства с данным физическим интерфейсом или <c>null</c>, если такового нет.
        /// </value>
        IDeviceView Device { get; }

        /// <summary>
        ///   Возвращает <c>true</c>, если интерфейс "поднят".
        ///   Опущенный интерфейс неспособен принимать и отправлять пакеты.
        /// </summary>
        /// <value>
        ///   <c>true</c>, если интерфейс "поднят".
        /// </value>
        bool Enabled { get; }

        /// <summary>
        ///   Отправляет пакет с интерфейса в среду передачи данных.
        /// </summary>
        /// <remarks>
        ///   Отправка пакета должна происходить только в том случае, если интерфейс
        ///   "поднят", т.е. свойство <see cref = "Enabled" /> установлено в <c>true</c>.
        /// </remarks>
        /// <param name = "packet">Пакет для отправки в среду передачи.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "packet" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceNotAttachedToBackboneException">Интерфейс не привязан к среде передачи данных.</exception>
        void SendPacket (IPacket packet);

        /// <summary>
        ///   Принимает пакет в интерфейс из среды передачи данных в устройство.
        /// </summary>
        /// <remarks>
        ///   Приём пакета должен происходить только в том случае, если интерфейс
        ///   "поднят", т.е. свойство <see cref = "Enabled" /> установлено в <c>true</c>.
        /// </remarks>
        /// <param name = "packet">Полученный пакет из среды передачи.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "packet" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceNotAttachedToDeviceException">Интерфейс не привязан к устройству.</exception>
        void ReceivePacket (IPacket packet);

        /// <summary>
        ///   Происходит при отправке пакета с интерфейса.
        /// </summary>
        /// <remarks>
        ///   Событие происходит до того, как пакет будет отправлен с интерфейса.
        /// </remarks>
        event InterfaceSendPacketEventHandler OnBeforeSend;

        /// <summary>
        ///   Происходит при получении интерфейсом пакета.
        /// </summary>
        /// <remarks>
        ///   Событие происходит до того, как пакет будет получен на интерфейс.
        /// </remarks>
        event InterfaceReceivePacketEventHandler OnBeforeReceive;

        /// <summary>
        ///   Происходит при "поднятии" интерфейса.
        /// </summary>
        /// <remarks>
        ///   Событие происходит до того, как интерфейс будет "поднят".
        /// </remarks>
        event InterfaceStateChangedEventHandler OnBeforeEnable;

        /// <summary>
        ///   Происходит при "опускании" интерфейса.
        /// </summary>
        /// <remarks>
        ///   Событие происходит до того, как интерфейс будет "опущен".
        /// </remarks>
        event InterfaceStateChangedEventHandler OnBeforeDisable;

        /// <summary>
        ///   Происходит при привязывании интерфейса к среде передачи данных.
        /// </summary>
        /// <remarks>
        ///   Событие происходит до того, как интерфейс будет привязан к среде передачи данных.
        /// </remarks>
        event BackboneAttachedEventHandler OnBeforeAttachBackbone;

        /// <summary>
        ///   Происходит при отвязывании интерфейса от среды передачи данных.
        /// </summary>
        /// <remarks>
        ///   Событие происходит до того, как интерфейс будет отвязан от среды передачи данных.
        /// </remarks>
        event BackboneDetachedEventHandler OnBeforeDetachBackbone;
    }
}
