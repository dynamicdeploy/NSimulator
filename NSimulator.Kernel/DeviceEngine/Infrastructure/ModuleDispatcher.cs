using System;
using System.Linq;
using System.Collections.Generic;

namespace NSimulator.Kernel {
    internal sealed class ModuleDispatcher {
        private readonly HashSet <IModule> [] modules;

        public ModuleDispatcher () {
            this.modules = new HashSet <IModule> [256];
            for (var i = 0; i < 256; ++ i)
                this.modules [i] = new HashSet <IModule> ();
        }

        public void LoadModule (IModule module) {
            if (module == null)
                throw new ArgumentNullException ("module");

            var info = new ModuleInformation (module);

            if (this.modules [info.Level].Contains (module, ModulesNameEqualityComparer.Instance))
                throw new ModuleAlreadyLoadedException (module.GetType ().FullName);

            // todo
        }

        public void UnloadModule (string moduleName) {
            if (moduleName == null)
                throw new ArgumentNullException ("moduleName");
        }

        public void DispatchPacket_ToInterface (IPacket packet, IInterfaceView iface) {
            if (packet == null)
                throw new ArgumentNullException ("packet");

            if (iface == null)
                throw new ArgumentNullException ("iface");


        }

        public void DispatchPacket_FromInterface (IPacket packet, IInterfaceView iface) {
            if (packet == null)
                throw new ArgumentNullException ("packet");

            if (iface == null)
                throw new ArgumentNullException ("iface");

        }
    }
}
