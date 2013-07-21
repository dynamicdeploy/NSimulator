#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public abstract class BackboneMock : IBackbone, IPCAPTypeModifyable {
        protected readonly IList <IInterfaceView> endpoints;

        protected BackboneMock (string name = "", int capacity = 2, ulong speed = (ulong) 0) {
            this.Name = name;
            this.Speed = speed;
            this.Type = PCAPNetworkTypes.LINKTYPE_RAW;
            this.endpoints = new List <IInterfaceView> ();

            for (var i = 0; i < capacity; ++ i)
                this.endpoints.Add (null);
        }

        public IInterfaceView this [int index] {
            get { return this.endpoints [index]; }
        }

        #region IBackbone Members

        public string Name { get; private set; }

        public event NamedElementChangedNameEventHandler OnBeforeChangeName {
            add { }
            remove { }
        }

        public int EndPointsCount {
            get { return this.endpoints.Count (_ => _ != null); }
        }

        public int EndPointsCapacity {
            get { return this.endpoints.Count; }
        }

        public IEnumerable <IInterfaceView> EndPoints {
            get { return this.endpoints; }
        }

        public PCAPNetworkTypes Type { get; private set; }

        public Enum State { get; protected set; }

        public ulong Speed { get; private set; }

        public double LossPercent { get; private set; }

        public abstract void SendPacket (IPacket packet, IInterfaceView from);

        public abstract event TransmitEventHandler OnTransmit;

        public void AttachEndPoint (IInterfaceView iface) {
            if (iface == null)
                throw new ArgumentNullException ();

            if (iface.Backbone != null)
                throw new ArgumentException ();

            for (var i = 0; i < this.endpoints.Count; ++ i) {
                if (this.endpoints [i] == null) {
                    this.endpoints [i] = iface;
                    return;
                }
            }

            throw new EndPointsOverflowException (this.Name, this.EndPointsCapacity);
        }

        public void DetachEndPoint (IInterfaceView iface) {
            if (iface == null)
                throw new ArgumentNullException ();

            if (iface.Backbone != this || ! this.endpoints.Contains (iface))
                throw new ArgumentException ();

            for (var i = 0; i < this.endpoints.Count; ++ i) {
                if (this.endpoints [i] == iface) {
                    this.endpoints [i] = null;
                    break;
                }
            }
        }

        public void ChangeSpeed (ulong speed) {
            this.Speed = speed;
        }

        public void ChangeLossPercent (double percent) {
            if (percent < 0.0 || percent > 1.0)
                throw new ArgumentOutOfRangeException ();

            this.LossPercent = percent;
        }

        public void SetName (string name) {
            this.Name = name;
        }

        public virtual void Load (XmlNode data) {}

        public virtual void Store (XmlNode node) {}

        #endregion

        #region IPCAPTypeModifyable Members

        public void SetPCAPType (PCAPNetworkTypes type) {
            this.Type = type;
        }

        #endregion
    }
}
