#region

using System;
using System.Collections.Generic;
using System.Xml;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public sealed class FakeBackbone : IBackbone {
        public FakeBackbone ()
            : this (string.Empty) {}

        public FakeBackbone (string name) {
            this.Name = name;
        }

        #region IBackbone Members

        public string Name { get; private set; }

        public event NamedElementChangedNameEventHandler OnBeforeChangeName {
            add { }
            remove { }
        }

        public int EndPointsCount {
            get { return default (int); }
        }

        public int EndPointsCapacity {
            get { return default (int); }
        }

        public IEnumerable <IInterfaceView> EndPoints {
            get { return new IInterfaceView [] { }; }
        }

        public PCAPNetworkTypes Type {
            get { return PCAPNetworkTypes.LINKTYPE_RAW; }
        }

        public Enum State {
            get { return default (Enum); }
        }

        public ulong Speed {
            get { return default (ulong); }
        }

        public double LossPercent {
            get { return default (double); }
        }

        public void SendPacket (IPacket packet, IInterfaceView from) {}

        public event TransmitEventHandler OnTransmit {
            add { }
            remove { }
        }

        public void AttachEndPoint (IInterfaceView iface) {}

        public void DetachEndPoint (IInterfaceView iface) {}

        public void ChangeSpeed (ulong speed) {}

        public void SetName (string name) {}

        public void ChangeLossPercent (double percent) {}

        public void Load (XmlNode data) {}

        public void Store (XmlNode node) {}

        #endregion
    }
}
