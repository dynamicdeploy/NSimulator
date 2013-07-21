#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс прошивки устройства.
    /// </summary>
    [ContractClass (typeof (Contract_IDeviceEngine))]
    public interface IDeviceEngine : IXMLSerializable {
        /// <summary>
        ///   Возвращает итератор по загруженным модулям.
        /// </summary>
        /// <value>Итератор по загруженным модулям.</value>
        IEnumerable <IModule> Modules { get; }

        /// <summary>
        ///   Возвращает контекст меню настройки прошивки.
        /// </summary>
        /// <value>Контекст меню настройки прошивки устройства.</value>
        IMenuContext EngineMenu { get; }

        /// <summary>
        ///   Начинает обработку пакета устройством.
        /// </summary>
        /// <param name = "packet">Пакет для обработки.</param>
        /// <param name = "iface">Интерфейс, с которого получен пакет.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "packet" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> является <c>null</c>.</exception>
        void DispatchPacket (IPacket packet, IInterfaceView iface);

        /// <summary>
        ///   Загружает модуль в прошивку.
        /// </summary>
        /// <param name = "module">Модуль, который нужно загрузить.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "module" /> является <c>null</c>.</exception>
        void LoadModule (IModule module);

        /// <summary>
        ///   Выгружает модуль из прошивки.
        /// </summary>
        /// <param name = "module">Модуль, который необходимо выгрузить.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "module" /> является <c>null</c>.</exception>
        void UnloadModule (IModule module);
    }
}
