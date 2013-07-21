#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (IDevice))]
    internal abstract class Contract_IDevice : IDevice {
        #region IDevice Members

        public abstract string Name { get; }
        public abstract event NamedElementChangedNameEventHandler OnBeforeChangeName;
        public abstract IInterfaceView this [string name] { get; }

        IEnumerable <IInterface> IDevice.Interfaces {
            get {
                Contract.Ensures (Contract.Result <IEnumerable <IInterface>> () != null);
                return default (IEnumerable <IInterface>);
            }
        }

        IInterface IDevice.this [string name] {
            get {
                Contract.Requires <ArgumentNullException> (name != null);
                Contract.Ensures (Contract.Result <IInterface> () != null);
                Contract.Ensures (Contract.Result <IInterface> ().Name == name);
                Contract.EnsuresOnThrow <InterfaceNotFoundException> (Contract.ForAll (this.Interfaces,
                                                                                       i => i.Name != name));
                return default (IInterface);
            }
        }

        public abstract int InterfacesCount { get; }
        public abstract IEnumerable <IInterfaceView> Interfaces { get; }
        public abstract IDeviceEngine Engine { get; }
        public abstract bool Enabled { get; }
        public abstract void ProcessPacket (IPacket packet, IInterfaceView from);
        public abstract event DeviceStateChangedEventHandler OnBeforeEnable;
        public abstract event DeviceStateChangedEventHandler OnBeforeDisable;
        public abstract event DeviceStructureChangedEventHandler OnBeforeAddInterface;
        public abstract event DeviceStructureChangedEventHandler OnBeforeRemoveInterface;

        public void SetEngine (IDeviceEngine engine) {
            Contract.Requires <ArgumentNullException> (engine != null);
            Contract.Ensures (this.Engine == engine);
        }

        public void SetName (string name) {
            Contract.Requires <ArgumentNullException> (name != null);
            Contract.Ensures (this.Name == name);
        }

        public void AddInterface (IInterface iface) {
            Contract.Requires <ArgumentNullException> (iface != null);
            Contract.Ensures (Contract.Exists (this.Interfaces, i => i == iface));
            Contract.EnsuresOnThrow <InterfaceAlreadyAttachedToDeviceException> (iface.Device != null);
        }

        public void AddInterface (IInterface iface, string name) {
            Contract.Requires <ArgumentNullException> (iface != null && name != null);
            Contract.Ensures (Contract.Exists (this.Interfaces, i => i == iface));
            Contract.EnsuresOnThrow <InterfaceAlreadyAttachedToDeviceException> (iface.Device != null);
            Contract.EnsuresOnThrow <InterfaceNameMustBeUniqueException> (Contract.Exists (this.Interfaces,
                                                                                           i => i.Name == name));
        }

        public void AddInterface_PrefixNamed (IInterface iface, string prefix) {
            Contract.Requires <ArgumentNullException> (iface != null && prefix != null);
            Contract.Ensures (Contract.Exists (this.Interfaces, i => i == iface));
            Contract.EnsuresOnThrow <InterfaceAlreadyAttachedToDeviceException> (iface.Device != null);
        }

        public void RemoveInterface (string name) {
            Contract.Requires <ArgumentNullException> (name != null);
            Contract.Ensures (Contract.ForAll (this.Interfaces, i => i.Name != name));
        }

        public void RemoveInterfaces () {
            Contract.Ensures (this.InterfacesCount == 0);
        }

        public void AttachBackbone (string iface_name, IBackbone backbone) {
            Contract.Requires <ArgumentNullException> (iface_name != null && backbone != null);
            Contract.EnsuresOnThrow <InterfaceNotFoundException> (Contract.ForAll (this.Interfaces,
                                                                                   i => i.Name != iface_name));
        }

        public void DetachBackbone (string iface_name) {
            Contract.Requires <ArgumentNullException> (iface_name != null);
            Contract.EnsuresOnThrow <InterfaceNotFoundException> (Contract.ForAll (this.Interfaces,
                                                                                   i => i.Name != iface_name));
        }

        public void Enable () {
            Contract.Ensures (this.Enabled);
        }

        public void Disable () {
            Contract.Ensures (! this.Enabled);
        }

        public abstract void Load (XmlNode data);

        public abstract void Store (XmlNode node);

        #endregion
    }
}
