#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (IBackbone))]
    internal abstract class Contract_IBackbone : IBackbone {
        #region IBackbone Members

        public abstract string Name { get; }

        public abstract event NamedElementChangedNameEventHandler OnBeforeChangeName;

        public abstract int EndPointsCount { get; }

        public abstract int EndPointsCapacity { get; }

        public abstract IEnumerable <IInterfaceView> EndPoints { get; }

        public abstract PCAPNetworkTypes Type { get; }

        public abstract Enum State { get; }

        public abstract ulong Speed { get; }

        public abstract double LossPercent { get; }

        public abstract void SendPacket (IPacket packet, IInterfaceView from);

        public abstract event TransmitEventHandler OnTransmit;

        public void SetName (string name) {
            Contract.Requires <ArgumentNullException> (name != null);
            Contract.Ensures (this.Name == name);
        }

        public void AttachEndPoint (IInterfaceView iface) {
            Contract.Requires <ArgumentNullException> (iface != null);
            Contract.Ensures (Contract.Exists (this.EndPoints, i => i == iface));
            Contract.EnsuresOnThrow <InterfaceAlreadyAttachedToBackboneException> (iface.Backbone != null ||
                                                                                   Contract.Exists (this.EndPoints,
                                                                                                    i => i == iface));
            Contract.EnsuresOnThrow <EndPointsOverflowException> (this.EndPointsCount >= this.EndPointsCapacity);
        }

        public void DetachEndPoint (IInterfaceView iface) {
            Contract.Requires <ArgumentNullException> (iface != null);
            Contract.Ensures (Contract.ForAll (this.EndPoints, i => i != iface));
            Contract.EnsuresOnThrow <InterfaceNotAttachedToBackboneException> (iface.Backbone == null ||
                                                                               Contract.ForAll (this.EndPoints,
                                                                                                i => i != iface));
        }

        public void ChangeSpeed (ulong speed) {
            Contract.Ensures (this.Speed == speed);
        }

        public void ChangeLossPercent (double percent) {
            Contract.Requires <ArgumentOutOfRangeException> (percent >= 0.0 && percent <= 1.0);
            Contract.Ensures (this.LossPercent == percent);
        }

        public abstract void Load (XmlNode data);

        public abstract void Store (XmlNode node);

        #endregion
    }
}
