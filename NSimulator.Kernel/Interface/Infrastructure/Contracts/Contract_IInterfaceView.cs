#region

using System;
using System.Diagnostics.Contracts;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (IInterfaceView))]
    internal abstract class Contract_IInterfaceView : IInterfaceView {
        #region IInterfaceView Members

        public IBackboneView Backbone {
            get { return default (IBackboneView); }
        }

        public IDeviceView Device {
            get { return default (IDeviceView); }
        }

        public bool Enabled {
            get { return default (bool); }
        }

        public void SendPacket (IPacket packet) {
            Contract.Requires <ArgumentNullException> (packet != null);
            Contract.EnsuresOnThrow <InterfaceNotAttachedToBackboneException> (this.Backbone == null);
        }

        public void ReceivePacket (IPacket packet) {
            Contract.Requires <ArgumentNullException> (packet != null);
            Contract.EnsuresOnThrow <InterfaceNotAttachedToDeviceException> (this.Device == null);
        }

        public abstract event InterfaceSendPacketEventHandler OnBeforeSend;
        public abstract event InterfaceReceivePacketEventHandler OnBeforeReceive;
        public abstract event InterfaceStateChangedEventHandler OnBeforeEnable;
        public abstract event InterfaceStateChangedEventHandler OnBeforeDisable;
        public abstract event BackboneAttachedEventHandler OnBeforeAttachBackbone;
        public abstract event BackboneDetachedEventHandler OnBeforeDetachBackbone;

        public abstract string Name { get; }
        public abstract event NamedElementChangedNameEventHandler OnBeforeChangeName;

        public abstract void Load (XmlNode data);
        public abstract void Store (XmlNode node);

        #endregion
    }
}
