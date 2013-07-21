#region

using System;
using System.Linq;
using System.Xml;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Устройство "хост".
    /// </summary>
    /// <typeparam name = "Interface">Тип интерфейса.</typeparam>
    /// <remarks>
    ///   Усройство "хост" имеет лишь один интерфейс указанного типа.
    /// </remarks>
    public sealed class Host <Interface> : DeviceBase
        where Interface : IInterface, new ( ) {
        private bool initialized;

        /// <summary>
        ///   Инициализация устройства "хост".
        /// </summary>
        /// <remarks>
        ///   После инициализации устройство выключено.
        /// </remarks>
        public Host () {
            base.AddInterface (new Interface ());
            this.Disable ();

            this.initialized = true;
        }

        /// <summary>
        /// </summary>
        public IInterfaceView HostInterface {
            get { return this.Interfaces.First (); }
        }

        /// <summary>
        ///   Добавляет физический интерфейс в устройство.
        /// </summary>
        /// <param name = "iface">Физический интерфейс, который нужно добавить.</param>
        /// <remarks>
        ///   В устройство "хост" можно добавлять интефейсы только во время инициализации.
        ///   После создания экземпляра объекта возможность отключается.
        /// </remarks>
        /// <exception cref = "FeatureNotSupportedException">Возможность не поддерживается.</exception>
        public override void AddInterface (IInterface iface) {
            if (this.initialized)
                throw new FeatureNotSupportedException (this.Name, Features.AddInterface);

            base.AddInterface (iface);
        }

        /// <summary>
        ///   Добавляет физический интерфейс в устройство.
        /// </summary>
        /// <param name = "iface">Физический интерфейс, который нужно добавить.</param>
        /// <param name = "name">Название интерфейса.</param>
        /// <remarks>
        ///   В устройство "хост" можно добавлять интефейсы только во время инициализации.
        ///   После создания экземпляра объекта возможность отключается.
        /// </remarks>
        /// <exception cref = "FeatureNotSupportedException">Возможность не поддерживается.</exception>
        public override void AddInterface (IInterface iface, string name) {
            if (this.initialized)
                throw new FeatureNotSupportedException (this.Name, Features.AddInterface);

            base.AddInterface (iface, name);
        }

        /// <summary>
        ///   Добавляет физический интерфейс в устройство.
        /// </summary>
        /// <param name = "iface">Физический интерфейс, который нужно добавить.</param>
        /// <param name = "prefix">Префикс названия интерфейса.</param>
        /// <remarks>
        ///   В устройство "хост" можно добавлять интефейсы только во время инициализации.
        ///   После создания экземпляра объекта возможность отключается.
        /// </remarks>
        /// <exception cref = "FeatureNotSupportedException">Возможность не поддерживается.</exception>
        public override void AddInterface_PrefixNamed (IInterface iface, string prefix) {
            if (this.initialized)
                throw new FeatureNotSupportedException (this.Name, Features.AddInterface);

            base.AddInterface_PrefixNamed (iface, prefix);
        }

        /// <summary>
        ///   Удаляет физический интерфейс из устройства.
        /// </summary>
        /// <remarks>
        ///   Из устройства "хост" можно удалять интефейсы только во время инициализации.
        ///   После создания экземпляра объекта возможность отключается.
        /// </remarks>
        /// <exception cref = "FeatureNotSupportedException">Возможность не поддерживается.</exception>
        public override void RemoveInterface (string name) {
            if (this.initialized)
                throw new FeatureNotSupportedException (this.Name, Features.RemoveInterface);

            base.RemoveInterface (name);
        }

        /// <summary>
        ///   Удаляет все физические интерфейсы из устройства.
        /// </summary>
        /// <remarks>
        ///   Из устройства "хост" можно удалять интефейсы только во время инициализации.
        ///   После создания экземпляра объекта возможность отключается.
        /// </remarks>
        /// <exception cref = "FeatureNotSupportedException">Возможность не поддерживается.</exception>
        public override void RemoveInterfaces () {
            if (this.initialized)
                throw new FeatureNotSupportedException (this.Name, Features.RemoveInterface);

            base.RemoveInterfaces ();
        }

        /// <inheritdoc />
        public override void Load (XmlNode data) {
            this.initialized = false;

            base.RemoveInterfaces ();
            base.Load (data);

            this.initialized = true;
        }

        /// <inheritdoc />
        public override void ProcessPacket (IPacket packet, IInterfaceView from) {
            if (packet == null)
                throw new ArgumentNullException ("packet");

            if (from == null)
                throw new ArgumentNullException ("packet");

            if (from != this.HostInterface)
                throw new InterfaceNotAttachedToDeviceException (from.Name, this.Name);

            if (this.Engine == null)
                return;

            this.Engine.DispatchPacket (packet, from);
        }
        }
}
