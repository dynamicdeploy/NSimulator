#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public sealed class DeviceMock : IDevice {
        private readonly ISet <IInterface> interfaces;

        public DeviceMock ()
            : this (string.Empty) {}

        public DeviceMock (string name) {
            this.Name = name;
            this.interfaces = new HashSet <IInterface> ();
        }

        #region IDevice Members

        public string Name { get; private set; }

        public event NamedElementChangedNameEventHandler OnBeforeChangeName;

        public IInterface this [string name] {
            get {
                if (this.interfaces.Count (_ => _.Name == name) == 0)
                    throw new ArgumentException ();

                return this.interfaces.First (_ => _.Name == name);
            }
        }

        IEnumerable <IInterfaceView> IDeviceView.Interfaces {
            get { return this.Interfaces; }
        }

        IInterfaceView IDeviceView.this [string name] {
            get { return this [name]; }
        }

        public int InterfacesCount {
            get { return this.Interfaces.Count (); }
        }

        public IEnumerable <IInterface> Interfaces {
            get { return this.interfaces; }
        }

        public IDeviceEngine Engine { get; private set; }

        public bool Enabled { get; private set; }

        public void ProcessPacket (IPacket packet, IInterfaceView from) {
            if (packet == null || from == null)
                throw new ArgumentNullException ();

            if (! this.interfaces.Contains (from))
                throw new ArgumentException ();

            if (this.OnBeforeProcessPacket != null)
                this.OnBeforeProcessPacket.Invoke (this);

            if (this.OnProcessPacket != null)
                this.OnProcessPacket.Invoke (packet, from);
        }

        public event DeviceStateChangedEventHandler OnBeforeEnable;

        public event DeviceStateChangedEventHandler OnBeforeDisable;

        public event DeviceStructureChangedEventHandler OnBeforeAddInterface;

        public event DeviceStructureChangedEventHandler OnBeforeRemoveInterface;

        public void SetEngine (IDeviceEngine engine) {
            this.Engine = engine;
        }

        public void SetName (string name) {
            if (name == null)
                throw new ArgumentNullException ();

            if (this.OnBeforeChangeName != null)
                this.OnBeforeChangeName.Invoke (this, name);

            this.Name = name;
        }

        public void AddInterface (IInterface iface) {
            if (iface == null)
                throw new ArgumentNullException ();

            if (this.interfaces.Contains (iface))
                throw new ArgumentException ();

            for (ulong i = 0;; ++ i) {
                var i1 = i;

                if (this.interfaces.Count (_ => _.Name == i1.ToString ()) != 0)
                    continue;

                this.AddInterface (iface, i.ToString ());
                return;
            }
        }

        public void AddInterface (IInterface iface, string name) {
            if (iface == null || name == null)
                throw new ArgumentNullException ();

            if (this.interfaces.Contains (iface))
                throw new ArgumentException ();

            if (this.interfaces.Count (_ => _.Name == name) != 0)
                throw new ArgumentException ();

            if (this.OnBeforeAddInterface != null)
                this.OnBeforeAddInterface.Invoke (this, iface);

            this.interfaces.Add (iface);
            iface.SetName (name);
            // iface.SetDevice (this);
        }

        public void AddInterface_PrefixNamed (IInterface iface, string prefix) {
            if (iface == null || prefix == null)
                throw new ArgumentNullException ();

            if (this.interfaces.Contains (iface))
                throw new ArgumentException ();

            for (ulong i = 0;; ++i) {
                var name = string.Format ("{0}{1}", prefix, i);

                if (this.interfaces.Count (_ => _.Name == name) != 0)
                    continue;

                this.AddInterface (iface, name);
                return;
            }
        }

        public void RemoveInterface (string name) {
            if (name == null)
                throw new ArgumentNullException ();

            if (this.interfaces.Count (_ => _.Name == name) == 0)
                throw new ArgumentException ();

            var iface = this.interfaces.First (_ => _.Name == name);

            if (this.OnBeforeRemoveInterface != null)
                this.OnBeforeRemoveInterface.Invoke (this, iface);

            iface.ReleaseDevice ();
            this.interfaces.Remove (iface);
        }

        public void RemoveInterfaces () {
            foreach (var iface in this.interfaces)
                iface.ReleaseDevice ();

            this.interfaces.Clear ();
        }

        public void AttachBackbone (string iface_name, IBackbone backbone) {
            if (iface_name == null || backbone == null)
                throw new ArgumentNullException ();

            // todo
        }

        public void DetachBackbone (string iface_name) {
            if (iface_name == null)
                throw new ArgumentNullException ();

            // todo
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

        public event DeviceProcessPacketEventHandler OnBeforeProcessPacket;

        public event Action <IPacket, IInterfaceView> OnProcessPacket;
    }
}
