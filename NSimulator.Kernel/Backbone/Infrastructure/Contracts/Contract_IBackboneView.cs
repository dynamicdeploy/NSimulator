#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (IBackboneView))]
    internal abstract class Contract_IBackboneView : IBackboneView {
        #region IBackboneView Members

        public abstract string Name { get; }
        public abstract event NamedElementChangedNameEventHandler OnBeforeChangeName;

        public int EndPointsCount {
            get {
                Contract.Ensures (Contract.Result <int> () >= 0);
                Contract.Ensures (Contract.Result <int> () <= this.EndPointsCapacity);
                return default (int);
            }
        }

        public int EndPointsCapacity {
            get {
                Contract.Ensures (Contract.Result <int> () >= 0);
                Contract.Ensures (Contract.Result <int> () >= this.EndPointsCount);
                return default (int);
            }
        }

        public IEnumerable <IInterfaceView> EndPoints {
            get {
                Contract.Ensures (Contract.Result <IEnumerable <IInterfaceView>> () != null);
                return default (IEnumerable <IInterfaceView>);
            }
        }

        public PCAPNetworkTypes Type {
            get { return default (PCAPNetworkTypes); }
        }

        public Enum State {
            get { return default (Enum); }
        }

        public ulong Speed {
            get { return default (ulong); }
        }

        public double LossPercent {
            get {
                Contract.Ensures (Contract.Result <double> () >= 0.0 && Contract.Result <double> () <= 1.0);
                return default (double);
            }
        }

        public void SendPacket (IPacket packet, IInterfaceView from) {
            Contract.Requires <ArgumentNullException> (packet != null);
            Contract.Requires <ArgumentNullException> (from != null);
        }

        public abstract event TransmitEventHandler OnTransmit;

        public abstract void Load (XmlNode data);

        public abstract void Store (XmlNode node);

        #endregion
    }
}
