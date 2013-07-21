#region

using System;
using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    public sealed partial class Network {
        #region INetwork Members

        /// <inheritdoc />
        public IEnumerable <IInterface> Interfaces {
            get { return this.interfaces.Values; }
        }

        /// <inheritdoc />
        IEnumerable <IInterfaceView> INetworkView.Interfaces {
            get { return this.Interfaces; }
        }

        /// <inheritdoc />
        public NetworkEntity AddInterface (IInterface iface) {
            if (this.interfaces.Values.Contains (iface))
                throw new NetworkContainsInterfaceException (iface.Name);

            var id = this.id_interfaces.GetNext ();
            this.interfaces.Add (id, iface);
            return new NetworkEntity (id);
        }

        /// <summary>
        ///   Удаляет интерфейс из топологии сети.
        /// </summary>
        /// <param name = "interfaceEntity">Описатель удаляемого интерфейса.</param>
        /// <remarks>
        ///   При удалении интерфейса происходит следующее:
        ///   <list type = "number">
        ///     <item>
        ///       <description>Интерфейс отвязывается от среды передачи.</description>
        ///     </item>
        ///     <item>
        ///       <description>Интерфейс отвязывается от устройства.</description>
        ///     </item>
        ///     <item>
        ///       <description>Интерфейс удаляется из топологии.</description>
        ///     </item>
        ///   </list>
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "interfaceEntity" /> является <c>null</c>.</exception>
        /// <exception cref = "EntityHandlerNotFoundException">Описатель <paramref name = "interfaceEntity" /> некорректный.</exception>
        public void RemoveInterface (NetworkEntity interfaceEntity) {
            if (!this.interfaces.ContainsKey (interfaceEntity.Id))
                throw new EntityHandlerNotFoundException (interfaceEntity);

            var iface = this.interfaces [interfaceEntity.Id];
            if (iface.Backbone != null)
                iface.ReleaseBackbone ();
            if (iface.Device != null)
                iface.ReleaseDevice ();

            this.interfaces.Remove (interfaceEntity.Id);
        }

        #endregion
    }
}
