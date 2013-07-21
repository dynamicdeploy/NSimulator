#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace NSimulator.Kernel {
    public sealed partial class Network {
        #region INetwork Members

        /// <inheritdoc />
        IEnumerable <IBackboneView> INetworkView.Backbones {
            get { return this.Backbones; }
        }

        /// <inheritdoc />
        public IEnumerable <IBackbone> Backbones {
            get { return this.backbones.Values; }
        }

        /// <inheritdoc />
        public NetworkEntity AddBackbone (IBackbone backbone) {
            if (this.backbones.Values.Contains (backbone))
                throw new NetworkContainsBackboneException (backbone.Name);

            var id = this.id_backbones.GetNext ();
            this.backbones.Add (id, backbone);
            return new NetworkEntity (id);
        }

        /// <summary>
        ///   Удаляет среду передачи данных из топологоии сети.
        /// </summary>
        /// <param name = "backboneEntity">Описатель удаляемой среды передачи.</param>
        /// <remarks>
        ///   При удалении среды передачи происходит следующее:
        ///   <list type = "number">
        ///     <item>
        ///       <description>Интерфейсы всех конечных точек отвязываются от среды передачи.</description>
        ///     </item>
        ///     <item>
        ///       <description>Среда передачи удаляется из топологии.</description>
        ///     </item>
        ///   </list>
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "backboneEntity" /> является <c>null</c>.</exception>
        /// <exception cref = "EntityHandlerNotFoundException">Описатель <paramref name = "backboneEntity" /> некорректный.</exception>
        public void RemoveBackbone (NetworkEntity backboneEntity) {
            if (!this.interfaces.ContainsKey (backboneEntity.Id))
                throw new EntityHandlerNotFoundException (backboneEntity);

            var backbone = this.backbones [backboneEntity.Id];
            foreach (var iface in this.interfaces.Where (iface => iface.Value.Backbone == backbone))
                iface.Value.ReleaseBackbone ();

            this.backbones.Remove (backboneEntity.Id);
        }

        #endregion
    }
}
