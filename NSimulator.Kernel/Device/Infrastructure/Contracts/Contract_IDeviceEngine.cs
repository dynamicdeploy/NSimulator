#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (IDeviceEngine))]
    internal abstract class Contract_IDeviceEngine : IDeviceEngine {
        #region IDeviceEngine Members

        public IEnumerable <IModule> Modules {
            get {
                Contract.Ensures (Contract.Result <IEnumerable <IModule>> () != null);
                return default (IEnumerable <IModule>);
            }
        }

        public abstract IMenuContext EngineMenu { get; }

        public void DispatchPacket (IPacket packet, IInterfaceView iface) {
            Contract.Requires <ArgumentNullException> (packet != null);
            Contract.Requires <ArgumentNullException> (iface != null);
        }

        public void LoadModule (IModule module) {
            Contract.Requires <ArgumentNullException> (module != null);
        }

        public void UnloadModule (IModule module) {
            Contract.Requires <ArgumentNullException> (module != null);
        }

        public abstract void Load (XmlNode data);
        public abstract void Store (XmlNode node);

        #endregion
    }
}
