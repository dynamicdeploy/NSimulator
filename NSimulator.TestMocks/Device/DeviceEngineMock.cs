#region

using System.Collections.Generic;
using System.Xml;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public sealed class DeviceEngineMock : IDeviceEngine {
        private readonly ISet <IModule> modules;

        public DeviceEngineMock () {
            this.modules = new HashSet <IModule> ();
        }

        #region IDeviceEngine Members

        public IEnumerable <IModule> Modules {
            get { return this.modules; }
        }

        public IMenuContext EngineMenu {
            get { return null; }
        }

        public void DispatchPacket (IPacket packet, IInterfaceView iface) {
            // todo
        }

        public void LoadModule (IModule module) {
            this.modules.Add (module);
        }

        public void UnloadModule (IModule module) {
            this.modules.Remove (module);
        }

        public void Load (XmlNode data) {}

        public void Store (XmlNode node) {}

        #endregion
    }
}