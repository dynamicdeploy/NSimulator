#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (IDeviceView))]
    internal abstract class Contract_IDeviceView : IDeviceView {
        #region IDeviceView Members

        public abstract string Name { get; }
        public abstract event NamedElementChangedNameEventHandler OnBeforeChangeName;

        public IInterfaceView this [string name] {
            get {
                Contract.Requires <ArgumentNullException> (name != null);
                Contract.Ensures (Contract.Result <IInterfaceView> () != null);
                Contract.Ensures (Contract.Result <IInterfaceView> ().Name == name);
                Contract.EnsuresOnThrow <InterfaceNotFoundException> (Contract.ForAll (this.Interfaces,
                                                                                       i => i.Name != name));
                return default (IInterfaceView);
            }
        }

        public int InterfacesCount {
            get {
                Contract.Ensures (Contract.Result <int> () >= 0);
                Contract.Ensures (Contract.Result <int> () == this.Interfaces.Count ());
                return default (int);
            }
        }

        public IEnumerable <IInterfaceView> Interfaces {
            get {
                Contract.Ensures (Contract.Result <IEnumerable <IInterfaceView>> () != null);
                return default (IEnumerable <IInterfaceView>);
            }
        }

        public IDeviceEngine Engine {
            get { return default (IDeviceEngine); }
        }

        public bool Enabled {
            get { return default (bool); }
        }

        public void ProcessPacket (IPacket packet, IInterfaceView from) {
            Contract.Requires <ArgumentNullException> (packet != null);
            Contract.Requires <ArgumentNullException> (from != null);
            Contract.Requires <InterfaceNotAttachedToDeviceException> (Contract.Exists (this.Interfaces, i => i == from));
        }

        public abstract event DeviceStateChangedEventHandler OnBeforeEnable;
        public abstract event DeviceStateChangedEventHandler OnBeforeDisable;
        public abstract event DeviceStructureChangedEventHandler OnBeforeAddInterface;
        public abstract event DeviceStructureChangedEventHandler OnBeforeRemoveInterface;

        public abstract void Load (XmlNode data);

        public abstract void Store (XmlNode node);

        #endregion
    }
}
