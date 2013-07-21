#region

using System.Collections.Generic;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (INetworkView))]
    internal abstract class Contract_INetworkView : INetworkView {
        #region INetworkView Members

        public IEnumerable <IBackboneView> Backbones {
            get {
                Contract.Ensures (Contract.Result <IEnumerable <IBackboneView>> () != null);
                return default (IEnumerable <IBackboneView>);
            }
        }

        public IEnumerable <IInterfaceView> Interfaces {
            get {
                Contract.Ensures (Contract.Result <IEnumerable <IInterfaceView>> () != null);
                return default (IEnumerable <IInterfaceView>);
            }
        }

        public IEnumerable <IDeviceView> Devices {
            get {
                Contract.Ensures (Contract.Result <IEnumerable <IDeviceView>> () != null);
                return default (IEnumerable <IDeviceView>);
            }
        }

        public abstract IClockView Clock { get; }
        public abstract event LoadTopologyErrorEventHandler OnLoadError;

        #endregion
    }
}
