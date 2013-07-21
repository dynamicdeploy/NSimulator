#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс физического интерфейса.
    ///   Предоставляется расширение интерфейса только для чтения до полного интерфейса.
    /// </summary>
    /// <seealso cref = "IInterfaceView" />
    [ContractClass (typeof (Contract_IInterface))]
    public interface IInterface : IInterfaceView {
        /// <summary>
        ///   Получает среду передачи данных, привязанную к интерфейсу.
        /// </summary>
        /// <value>
        ///   Среда передачи данных, подключённая к интерфейсу или <c>null</c>, если таковой нет.
        /// </value>
        new IBackbone Backbone { get; }

        /// <summary>
        ///   Устанавливает название интерфейса.
        /// </summary>
        /// <param name = "name">Название интерфейса.</param>
        /// <remarks>
        ///   Обычно название интерфейса устанавливает устройство при добавлении в него
        ///   данного интерфейса.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        void SetName (string name);

        /// <summary>
        ///   Устанавливает среду передачи данных, привязанную к интерфейсу.
        /// </summary>
        /// <param name = "backbone">Привязанная среда передачи данных.</param>
        /// <remarks>
        ///   Метод должен выполнить проверку корректности привязывания с
        ///   последующей полной привязкой интерфейса и среды передачи данных.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToBackboneException">Интерфейс уже привязан к среде передачи данных.</exception>
        /// <exception cref = "InterfaceNotCompatibleWithBackboneException">Интерфейс несовместим со средой передачи данных.</exception>
        /// <exception cref = "EndPointsOverflowException">Среда передачи данных уже имеет максимально возможное число конечных точек.</exception>
        void SetBackbone (IBackbone backbone);

        /// <summary>
        ///   Освобождает среду передачи данных, привязанную к интерфейсу.
        /// </summary>
        /// <exception cref = "InterfaceNotAttachedToBackboneException">Интерфейс не привязан к среде передачи данных.</exception>
        void ReleaseBackbone ();

        /// <summary>
        ///   Устанавливает устройство, содержащее данный интерфейс.
        /// </summary>
        /// <param name = "device">Устройство, содержащее интерфейс.</param>
        /// <remarks>
        ///   <para>
        ///     Метод должен только выполнить проверку корректности и последующее
        ///     присвоение свойству <see cref = "IInterfaceView.Device" />
        ///     устройства, переданного в <paramref name = "device" />.
        ///   </para>
        ///   <para>
        ///     Обычно метод вызывается при добавлении интерфейса в устройство.
        ///   </para>
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "device" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToDeviceException">Интерфейс уже привязан к устройству.</exception>
        /// <exception cref = "InterfaceNotAttachedToDeviceException">Интерфейс не содержится в устройстве <paramref name = "device" />.</exception>
        void SetDevice (IDeviceView device);

        /// <summary>
        ///   Освобождает устройство, содержащее данный интерфейс.
        /// </summary>
        /// <exception cref = "InterfaceNotAttachedToDeviceException">Интерфейс не привязан к устройству.</exception>
        void ReleaseDevice ();

        /// <summary>
        ///   "Поднимает" интерфейс.
        ///   После поднятия интерфейс способен передавать и принимать пакеты.
        /// </summary>
        void Enable ();

        /// <summary>
        ///   "Опускает" интерфейс.
        ///   После опускания интерфейс неспособен передавать и принимать пакеты.
        /// </summary>
        void Disable ();
    }
}
