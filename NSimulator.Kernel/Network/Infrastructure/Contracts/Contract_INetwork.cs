#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (INetwork))]
    internal abstract class Contract_INetwork : INetwork {
        #region INetwork Members

        public abstract IEnumerable <IBackboneView> Backbones { get; }

        IEnumerable <IInterface> INetwork.Interfaces {
            get {
                Contract.Ensures (Contract.Result <IEnumerable <IInterface>> () != null);
                return default (IEnumerable <IInterface>);
            }
        }

        IEnumerable <IDevice> INetwork.Devices {
            get {
                Contract.Ensures (Contract.Result <IEnumerable <IDevice>> () != null);
                return default (IEnumerable <IDevice>);
            }
        }

        IClock INetwork.Clock {
            get { return default (IClock); }
        }

        public NetworkEntity AddBackbone (IBackbone backbone) {
            Contract.Requires <ArgumentNullException> (backbone != null);
            Contract.Ensures (Contract.Result <NetworkEntity> () != null);
            Contract.Ensures (Contract.Exists (this.Backbones, b => b == backbone));
            Contract.EnsuresOnThrow <NetworkContainsBackboneException> (Contract.Exists (this.Backbones,
                                                                                         b => b == backbone));
            return default (NetworkEntity);
        }

        public void RemoveBackbone (NetworkEntity backboneEntity) {
            Contract.Requires <ArgumentNullException> (backboneEntity != null);
        }

        public NetworkEntity AddInterface (IInterface iface) {
            Contract.Requires <ArgumentNullException> (iface != null);
            Contract.Ensures (Contract.Result <NetworkEntity> () != null);
            Contract.Ensures (Contract.Exists (this.Interfaces, i => i == iface));
            Contract.EnsuresOnThrow <NetworkContainsInterfaceException> (Contract.Exists (this.Interfaces,
                                                                                          i => i == iface));
            return default (NetworkEntity);
        }

        public void RemoveInterface (NetworkEntity interfaceEntity) {
            Contract.Requires <ArgumentNullException> (interfaceEntity != null);
        }

        public NetworkEntity AddDevice (IDevice device) {
            Contract.Requires <ArgumentNullException> (device != null);
            Contract.Ensures (Contract.Result <NetworkEntity> () != null);
            Contract.Ensures (Contract.Exists (this.Devices, d => d == device));
            Contract.EnsuresOnThrow <NetworkContainsDeviceException> (Contract.Exists (this.Devices, d => d == device));
            return default (NetworkEntity);
        }

        public void RemoveDevice (NetworkEntity deviceEntity) {
            Contract.Requires <ArgumentNullException> (deviceEntity != null);
        }

        public void LoadFromXml (string filename) {
            Contract.Requires <ArgumentNullException> (filename != null);
        }

        public void SaveToXml (string filename) {
            Contract.Requires <ArgumentNullException> (filename != null);
        }

        IEnumerable <IBackbone> INetwork.Backbones {
            get {
                Contract.Ensures (Contract.Result <IEnumerable <IBackbone>> () != null);
                return default (IEnumerable <IBackbone>);
            }
        }

        public abstract IEnumerable <IInterfaceView> Interfaces { get; }
        public abstract IEnumerable <IDeviceView> Devices { get; }
        public abstract IClockView Clock { get; }
        public abstract event LoadTopologyErrorEventHandler OnLoadError;

        #endregion
    }
}
