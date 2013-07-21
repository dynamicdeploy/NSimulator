#region

using System.Xml;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public sealed class FakeInterface : IInterface {
        public FakeInterface ()
            : this (string.Empty) {}

        public FakeInterface (string name) {
            this.Name = name;
        }

        #region IInterface Members

        public string Name { get; private set; }

        public event NamedElementChangedNameEventHandler OnBeforeChangeName {
            add { }
            remove { }
        }

        public void Load (XmlNode data) {}

        public void Store (XmlNode node) {}

        IBackboneView IInterfaceView.Backbone {
            get { return this.Backbone; }
        }

        public void SetName (string name) {}

        public void SetBackbone (IBackbone backbone) {}

        public void ReleaseBackbone () {}

        public void SetDevice (IDeviceView device) {}

        public void ReleaseDevice () {}

        public void Enable () {}

        public void Disable () {}

        public IBackbone Backbone {
            get { return default (IBackbone); }
        }

        public IDeviceView Device {
            get { return default (IDeviceView); }
        }

        public bool Enabled {
            get { return default (bool); }
        }

        public void SendPacket (IPacket packet) {}

        public void ReceivePacket (IPacket packet) {}

        public event InterfaceSendPacketEventHandler OnBeforeSend {
            add { }
            remove { }
        }

        public event InterfaceReceivePacketEventHandler OnBeforeReceive {
            add { }
            remove { }
        }

        public event InterfaceStateChangedEventHandler OnBeforeEnable {
            add { }
            remove { }
        }

        public event InterfaceStateChangedEventHandler OnBeforeDisable {
            add { }
            remove { }
        }

        public event BackboneAttachedEventHandler OnBeforeAttachBackbone {
            add { }
            remove { }
        }

        public event BackboneDetachedEventHandler OnBeforeDetachBackbone {
            add { }
            remove { }
        }

        #endregion
    }
}
