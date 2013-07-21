#region

using System;
using System.Linq;
using System.Xml;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    [InterfacePrefix ("int")]
    public class InterfaceMock : IInterface {
        public InterfaceMock () {
            this.Enabled = true;
        }

        #region IInterface Members

        public IBackbone Backbone { get; private set; }

        IBackboneView IInterfaceView.Backbone {
            get { return this.Backbone; }
        }

        public IDeviceView Device { get; private set; }

        public string Name { get; private set; }

        public event NamedElementChangedNameEventHandler OnBeforeChangeName;

        public bool Enabled { get; private set; }

        public void SendPacket (IPacket packet) {
            if (! this.Enabled)
                return;

            if (this.Backbone == null)
                throw new ArgumentException ();

            if (this.OnBeforeSend != null)
                this.OnBeforeSend.Invoke (this);

            this.Backbone.SendPacket (packet, this);
        }

        public void ReceivePacket (IPacket packet) {
            if (! this.Enabled)
                return;

            if (this.Device == null)
                throw new ArgumentException ();

            if (this.OnBeforeReceive != null)
                this.OnBeforeReceive.Invoke (this);

            this.Device.ProcessPacket (packet, this);
        }

        public event InterfaceSendPacketEventHandler OnBeforeSend;

        public event InterfaceReceivePacketEventHandler OnBeforeReceive;

        public event InterfaceStateChangedEventHandler OnBeforeEnable;

        public event InterfaceStateChangedEventHandler OnBeforeDisable;

        public event BackboneAttachedEventHandler OnBeforeAttachBackbone;

        public event BackboneDetachedEventHandler OnBeforeDetachBackbone;

        public void SetName (string name) {
            if (name == null)
                throw new ArgumentNullException ();

            if (this.OnBeforeChangeName != null)
                this.OnBeforeChangeName.Invoke (this, name);

            this.Name = name;
        }

        public void SetBackbone (IBackbone backbone) {
            if (backbone == null)
                throw new ArgumentNullException ();

            if (this.Backbone != null)
                throw new ArgumentException ();

            if (this.OnBeforeAttachBackbone != null)
                this.OnBeforeAttachBackbone.Invoke (this, backbone);

            backbone.AttachEndPoint (this);
            this.Backbone = backbone;
        }

        public void ReleaseBackbone () {
            if (this.Backbone == null)
                return;

            if (this.OnBeforeDetachBackbone != null)
                this.OnBeforeDetachBackbone.Invoke (this);

            this.Backbone.DetachEndPoint (this);
            this.Backbone = null;
        }

        public void SetDevice (IDeviceView device) {
            if (device == null)
                throw new ArgumentNullException ();

            if (this.Device != null)
                throw new ArgumentException ();

            if (! device.Interfaces.Contains (this))
                throw new ArgumentException ();

            this.Device = device;
        }

        public void ReleaseDevice () {
            if (this.Device == null)
                throw new ArgumentException ();

            this.Device = null;
        }

        public void Enable () {
            if (this.OnBeforeEnable != null)
                this.OnBeforeEnable.Invoke (this, true);

            this.Enabled = true;
        }

        public void Disable () {
            if (this.OnBeforeDisable != null)
                this.OnBeforeDisable.Invoke (this, false);

            this.Enabled = false;
        }

        public void Load (XmlNode data) {}

        public void Store (XmlNode node) {}

        #endregion
    }
}
