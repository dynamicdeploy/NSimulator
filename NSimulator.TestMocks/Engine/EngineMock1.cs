#region

using System;
using System.Collections.Generic;
using System.Xml;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public abstract class EngineMockBase : IDeviceEngine {
        protected readonly ISet <IModule> modules;

        protected EngineMockBase () {
            this.modules = new HashSet <IModule> ();
        }

        #region IDeviceEngine Members

        public abstract void Load (XmlNode data);

        public abstract void Store (XmlNode node);

        public IEnumerable <IModule> Modules {
            get { return this.modules; }
        }

        public virtual IMenuContext EngineMenu {
            get { return null; }
        }

        public virtual void DispatchPacket (IPacket packet, IInterfaceView iface) {}

        public void LoadModule (IModule module) {
            if (this.modules.Contains (module))
                throw new ArgumentException ();

            this.modules.Add (module);
        }

        public void UnloadModule (IModule module) {
            if (! this.modules.Contains (module))
                throw new ArgumentException ();

            this.modules.Remove (module);
        }

        #endregion
    }

    public sealed class EngineMock1 : EngineMockBase {
        public override void Load (XmlNode data) {
            if (data.ChildNodes.Count != 0)
                throw new ArgumentException ();
        }

        public override void Store (XmlNode node) {}
    }

    public sealed class EngineMock2 : EngineMockBase {
        public override void Load (XmlNode data) {}

        public override void Store (XmlNode node) {}
    }
}
