#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс устройства.
    ///   Предоставляется расширение интерфейса только для чтения до полного интерфейса.
    /// </summary>
    /// <seealso cref = "IDeviceView" />
    [ContractClass (typeof (Contract_IDevice))]
    public interface IDevice : IDeviceView {
        /// <summary>
        ///   Получает интерфейс с указанным названием.
        /// </summary>
        /// <param name = "name">Название интерфейса, который необходимо получить.</param>
        /// <returns>
        ///   Интерфейс с указанным названием.
        /// </returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceNotFoundException">Интерфейса с именем <paramref name = "name" /> не существует в устройстве.</exception>
        new IInterface this [string name] { get; }

        /// <summary>
        ///   Получает перечисление <see cref = "IEnumerable{T}" /> всех интерфейсов устройства.
        /// </summary>
        /// <value>
        ///   Перечисление физических интерфейсов устройства.
        /// </value>
        new IEnumerable <IInterface> Interfaces { get; }

        /// <summary>
        ///   Устанавливает новую прошивку устройства.
        /// </summary>
        /// <param name = "engine">Прошивка устройства.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "engine" /> является <c>null</c>.</exception>
        void SetEngine (IDeviceEngine engine);

        /// <summary>
        ///   Устанавливает имя устройства.
        /// </summary>
        /// <param name = "name">Имя устройства.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        void SetName (string name);

        /// <summary>
        ///   Добавляет физический интерфейс в устройство.
        /// </summary>
        /// <param name = "iface">Физический интерфейс, который нужно добавить.</param>
        /// <remarks>
        ///   Название интерфейса будет получено автоматически.
        ///   Более конкретный способ получения зависит от реализации.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToDeviceException">Интерфейс уже привязан к устройству.</exception>
        void AddInterface (IInterface iface);

        /// <summary>
        ///   Добавляет физический интерфейс в устройство.
        /// </summary>
        /// <param name = "iface">Физический интерфейс, который нужно добавить.</param>
        /// <param name = "name">Название интерфейса.</param>
        /// <remarks>
        ///   В качестве названия интерфейса используется заданное в <paramref name = "name" /> имя.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToDeviceException">Интерфейс уже привязан к устройству.</exception>
        /// <exception cref = "InterfaceNameMustBeUniqueException">Имя интерфейса не уникально в пределах устройства.</exception>
        void AddInterface (IInterface iface, string name);

        /// <summary>
        ///   Добавляет физический интерфейс в устройство.
        /// </summary>
        /// <param name = "iface">Физический интерфейс, который нужно добавить.</param>
        /// <param name = "prefix">Префикс названия интерфейса.</param>
        /// <remarks>
        ///   Название интерфейса будет получено автоматически, но оно будет иметь указанный
        ///   в <paramref name = "prefix" /> префикс.
        ///   Более конкретный способ получения зависит от реализации.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "prefix" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToDeviceException">Интерфейс уже привязан к устройству.</exception>
        void AddInterface_PrefixNamed (IInterface iface, string prefix);

        /// <summary>
        ///   Удаляет физический интерфейс из устройства.
        /// </summary>
        /// <param name = "name">Название интерфейса, которй необходимо удалить.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceNotFoundException">Интерфейса с именем <paramref name = "name" /> не существует в устройстве.</exception>
        void RemoveInterface (string name);

        /// <summary>
        ///   Удаляет все физические интерфейсы из устройства.
        /// </summary>
        void RemoveInterfaces ();

        /// <summary>
        ///   Присоединяет среду передачи к интерфейсу.
        /// </summary>
        /// <param name = "iface_name">Имя интерфейса, к которому присоединять.</param>
        /// <param name = "backbone">Среда передачи данных, которую присоединять.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface_name" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceNotFoundException">Интерфейса с именем <paramref name = "iface_name" /> не существует в устройстве.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToBackboneException">Интерфейс уже привязан к среде передачи данных.</exception>
        /// <exception cref = "InterfaceNotCompatibleWithBackboneException">Интерфейс несовместим со средой передачи данных.</exception>
        /// <exception cref = "EndPointsOverflowException">Среда передачи данных уже имеет максимально возможное число конечных точек.</exception>
        void AttachBackbone (string iface_name, IBackbone backbone);

        /// <summary>
        ///   Отсоединяет среду передачи от интерфейса.
        /// </summary>
        /// <param name = "iface_name">Имя интерфейса, который присоединять.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface_name" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceNotFoundException">Интерфейса с именем <paramref name = "iface_name" /> не существует в устройстве.</exception>
        /// <exception cref = "InterfaceNotAttachedToBackboneException">Интерфейс с именем <paramref name = "iface_name" /> не привязан к среде передачи данных.</exception>
        void DetachBackbone (string iface_name);

        /// <summary>
        ///   Включает устройство.
        ///   После включения устройство способно обрабатывать данные.
        /// </summary>
        /// <remarks>
        ///   После включения устройство должно "поднять" все интерфейсы.
        /// </remarks>
        void Enable ();

        /// <summary>
        ///   Выключает устройство.
        ///   После выключения устройство неспособно обрабатывать данные.
        /// </summary>
        /// <remarks>
        ///   После выключения устройство должно "опустить" все интерфейсы.
        /// </remarks>
        void Disable ();
    }
}
