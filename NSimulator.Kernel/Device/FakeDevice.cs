#region

using System.Collections.Generic;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    internal sealed class FakeDevice : IDevice {
        public FakeDevice (string name) {
            this.Name = name;
        }

        #region IDevice Members

        IInterfaceView IDeviceView.this [string name] {
            get { return this [name]; }
        }

        public IEnumerable <IInterface> Interfaces {
            get { return default (IEnumerable <IInterface>); }
        }

        public string Name { get; private set; }

        public event NamedElementChangedNameEventHandler OnBeforeChangeName {
            add { }
            remove { }
        }

        public IInterface this [string name] {
            get { return default (IInterface); }
        }

        public int InterfacesCount {
            get { return default (int); }
        }

        IEnumerable <IInterfaceView> IDeviceView.Interfaces {
            get { return this.Interfaces; }
        }

        public IDeviceEngine Engine {
            get { return default (IDeviceEngine); }
        }

        public bool Enabled {
            get { return default (bool); }
        }

        public void ProcessPacket (IPacket packet, IInterfaceView from) {}

        public event DeviceStateChangedEventHandler OnBeforeEnable {
            add { }
            remove { }
        }

        public event DeviceStateChangedEventHandler OnBeforeDisable {
            add { }
            remove { }
        }

        public event DeviceStructureChangedEventHandler OnBeforeAddInterface {
            add { }
            remove { }
        }

        public event DeviceStructureChangedEventHandler OnBeforeRemoveInterface {
            add { }
            remove { }
        }

        public void SetEngine (IDeviceEngine engine) {}

        public void SetName (string name) {}

        public void AddInterface (IInterface iface) {}

        public void AddInterface (IInterface iface, string name) {}

        public void AddInterface_PrefixNamed (IInterface iface, string prefix) {}

        public void RemoveInterface (string name) {}

        public void RemoveInterfaces () {}

        public void AttachBackbone (string iface_name, IBackbone backbone) {}

        public void DetachBackbone (string iface_name) {}

        public void Enable () {}

        public void Disable () {}

        public void Load (XmlNode data) {}

        public void Store (XmlNode node) {}

        #endregion
    }
}