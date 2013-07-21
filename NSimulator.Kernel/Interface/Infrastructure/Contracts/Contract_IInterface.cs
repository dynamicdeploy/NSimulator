#region

using System;
using System.Diagnostics.Contracts;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (IInterface))]
    internal abstract class Contract_IInterface : IInterface {
        #region IInterface Members

        public abstract string Name { get; }
        public abstract event NamedElementChangedNameEventHandler OnBeforeChangeName;
        public abstract IBackboneView Backbone { get; }

        IBackbone IInterface.Backbone {
            get { return default (IBackbone); }
        }

        public abstract IDeviceView Device { get; }
        public abstract bool Enabled { get; }
        public abstract void SendPacket (IPacket packet);
        public abstract void ReceivePacket (IPacket packet);
        public abstract event InterfaceSendPacketEventHandler OnBeforeSend;
        public abstract event InterfaceReceivePacketEventHandler OnBeforeReceive;
        public abstract event InterfaceStateChangedEventHandler OnBeforeEnable;
        public abstract event InterfaceStateChangedEventHandler OnBeforeDisable;
        public abstract event BackboneAttachedEventHandler OnBeforeAttachBackbone;
        public abstract event BackboneDetachedEventHandler OnBeforeDetachBackbone;

        public void SetName (string name) {
            Contract.Requires <ArgumentNullException> (name != null);
            Contract.Ensures (this.Name == name);
        }

        public void SetBackbone (IBackbone backbone) {
            Contract.Requires <ArgumentNullException> (backbone != null);
            Contract.Ensures (this.Backbone == backbone);
            Contract.Ensures (Contract.Exists (this.Backbone.EndPoints, i => i == this));
            Contract.EnsuresOnThrow <InterfaceAlreadyAttachedToBackboneException> (this.Backbone != null);
        }

        public void ReleaseBackbone () {
            Contract.Ensures (this.Backbone == null);
            Contract.Ensures (Contract.ForAll (Contract.OldValue <IBackboneView> (this.Backbone).EndPoints,
                                               i => i != this));
            Contract.EnsuresOnThrow <InterfaceNotAttachedToBackboneException> (this.Backbone == null);
        }

        public void SetDevice (IDeviceView device) {
            Contract.Requires <ArgumentNullException> (device != null);
            Contract.Ensures (this.Device == device);
            Contract.EnsuresOnThrow <InterfaceAlreadyAttachedToDeviceException> (this.Device != null);
            Contract.EnsuresOnThrow <InterfaceNotAttachedToDeviceException> (Contract.ForAll (device.Interfaces,
                                                                                              i => i != this));
        }

        public void ReleaseDevice () {
            Contract.Ensures (this.Device == null);
            Contract.EnsuresOnThrow <InterfaceNotAttachedToDeviceException> (this.Device == null);
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
