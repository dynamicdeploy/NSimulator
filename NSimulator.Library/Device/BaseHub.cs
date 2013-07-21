#region

using System;
using System.Linq;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Абстрактный хаб с однотипными интерфейсами.
    /// </summary>
    /// <typeparam name = "T">Тип интерфейсов.</typeparam>
    /// <remarks>
    ///   Класс интерфейса <typeparamref name = "T" /> должен иметь конструктор по умолчанию.
    /// </remarks>
    public abstract class BaseHub <T> : DeviceBase
        where T : IInterface, new ( ) {
        /// <summary>
        ///   Инициализирует хаб заданным числом интерфейсов.
        /// </summary>
        /// <param name = "interfaces">Количество интерфейсов.</param>
        /// <exception cref = "ArgumentOutOfRangeException"><paramref name = "interfaces" /> меньше 0.</exception>
        protected BaseHub (int interfaces) {
            if (interfaces < 0)
                throw new ArgumentOutOfRangeException ("interfaces");

            for (var i = 0; i < interfaces; ++ i)
                base.AddInterface (new T ());

            base.Enable ();
        }

        /// <summary>
        ///   Метод пробрасывает исключение <see cref = "FeatureNotSupportedException" />, потому что
        ///   хаб нельзя отключить.
        /// </summary>
        /// <exception cref = "FeatureNotSupportedException">Хаб нельзя отключить.</exception>
        public override sealed void Disable () {
            throw new FeatureNotSupportedException (this.Name, Features.Disable);
        }

        /// <summary>
        ///   Метод пробрасывает исключение <see cref = "FeatureNotSupportedException" />, потому что
        ///   хаб является спайкой проводов и не имеет прошивки.
        /// </summary>
        /// <param name = "engine">Устанавливаемая прошивка.</param>
        /// <exception cref = "FeatureNotSupportedException">Хаб нельзя "прошить".</exception>
        public override sealed void SetEngine (IDeviceEngine engine) {
            throw new FeatureNotSupportedException (this.Name, Features.SetEngine);
        }

        /// <summary>
        ///   Метод обрабатывает пакет.
        /// </summary>
        /// <param name = "packet">Пакет, который нужно обработать.</param>
        /// <param name = "from">Интерфейс, с которого получен пакет.</param>
        /// <remarks>
        ///   При обработке пакета пакет отсылается во все интерфейсы, кроме
        ///   того, с которого он был получен, т.е. <paramref name = "from" />.
        /// </remarks>
        public override void ProcessPacket (IPacket packet, IInterfaceView from) {
            if (from.Device != this)
                throw new InterfaceNotAttachedToDeviceException (from.Name);

            foreach (var iface in this.Interfaces.Where (iface => iface != from)) {
                try {
                    iface.SendPacket (packet);
                }
                catch {}
            }
        }
        }
}
