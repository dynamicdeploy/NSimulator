#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace NSimulator.Kernel {
    public sealed partial class Network {
        #region INetwork Members

        /// <inheritdoc />
        public IEnumerable <IDevice> Devices {
            get { return this.devices.Values; }
        }

        /// <inheritdoc />
        IEnumerable <IDeviceView> INetworkView.Devices {
            get { return this.Devices; }
        }

        /// <inheritdoc />
        public NetworkEntity AddDevice (IDevice device) {
            if (this.devices.Values.Contains (device))
                throw new NetworkContainsDeviceException (device.Name);

            var id = this.id_devices.GetNext ();
            this.devices.Add (id, device);
            return new NetworkEntity (id);
        }

        /// <summary>
        ///   Удаляет устройство из топологии сети.
        /// </summary>
        /// <param name = "deviceEntity">Описатель удаляемого устройства.</param>
        /// <remarks>
        ///   При удалении устройства происходит следующее:
        ///   <list type = "number">
        ///     <item>
        ///       <description>Отключаются от всех интерфейсов среды передачи.</description>
        ///     </item>
        ///     <item>
        ///       <description>Удаляются все интерфейсы из устройства.</description>
        ///     </item>
        ///     <item>
        ///       <description>Удаляются интерфейсы из топологии.</description>
        ///     </item>
        ///     <item>
        ///       <description>Устройство удаляется из топологии.</description>
        ///     </item>
        ///   </list>
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "deviceEntity" /> является <c>null</c>.</exception>
        /// <exception cref = "EntityHandlerNotFoundException">Описатель <paramref name = "deviceEntity" /> некорректный.</exception>
        public void RemoveDevice (NetworkEntity deviceEntity) {
            if (!this.devices.ContainsKey (deviceEntity.Id))
                throw new EntityHandlerNotFoundException (deviceEntity);

            var dev = this.devices [deviceEntity.Id];

            var ifaces = new HashSet <ulong> ();
            foreach (var iface in dev.Interfaces) {
                if (iface.Backbone != null)
                    iface.ReleaseBackbone ();

                var iface1 = iface;
                ifaces.Add ((from i in this.interfaces
                             where i.Value == iface1
                             select i.Key).First ());
            }

            dev.RemoveInterfaces ();

            foreach (var id in ifaces)
                this.interfaces.Remove (id);

            this.devices.Remove (deviceEntity.Id);
        }

        #endregion
    }
}
