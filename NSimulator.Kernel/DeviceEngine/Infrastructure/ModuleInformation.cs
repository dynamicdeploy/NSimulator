using System;
using System.Collections.Generic;

namespace NSimulator.Kernel {
    internal sealed class ModuleInformation {
        public ModuleInformation (IModule module) {
            if (module == null)
                throw new ArgumentNullException ("module");

            this.Module = module;
            this.CollectInformation ();
        }

        private void CollectInformation () {
            var type = this.Module.GetType ();

            this.GetLevel (type);
            this.GetInterfaces (type);
            this.GetDepends (type);
        }

        private void GetLevel (Type type) {
            var levelAttribute = type.GetCustomAttributes (typeof (LevelAttribute), false);
            if (levelAttribute == null || levelAttribute.Length != 1)
                throw new UnknownModuleLevelException (type.FullName);

            this.Level = (levelAttribute [0] as LevelAttribute).Level;
        }

        private void GetInterfaces (Type type) {
            var interfacesAttribute = type.GetCustomAttributes (typeof (InterfaceTypeAttribute), false);
            if (this.Level != 0) {
                if (interfacesAttribute == null || interfacesAttribute.Length == 0) {
                    this.Interfaces = new Type [0];
                    return;
                }

                throw new RedundantInterfaceTypeException (type.FullName);
            }

            var interfaces = new List <Type> ();

            foreach (var attr in interfacesAttribute)
                interfaces.AddRange ((attr as InterfaceTypeAttribute).Types);

            this.Interfaces = interfaces.ToArray ();
        }

        private void GetDepends (Type type) {
            var dependsFromAttribute = type.GetCustomAttributes (typeof (DependsOn_FromAttribute), false);
            if (this.Level == 0 && (dependsFromAttribute != null && dependsFromAttribute.Length > 0))
                throw new RedundantDependsException (type.FullName);

            this.DependsOn_From = new Type [dependsFromAttribute.Length] [];
            for (var i = 0; i < dependsFromAttribute.Length; ++ i)
                this.DependsOn_From [i] = (dependsFromAttribute [i] as DependsOn_FromAttribute).Types;


            var dependsToAttribute = type.GetCustomAttributes (typeof (DependsOn_ToAttribute), false);

            this.DependsOn_To = new Type [dependsToAttribute.Length] [];
            for (var i = 0; i < dependsToAttribute.Length; ++i)
                this.DependsOn_To [i] = (dependsToAttribute [i] as DependsOn_ToAttribute).Types;
        }

        public IModule Module { get; private set; }

        public byte Level { get; private set; }

        public Type [] Interfaces { get; private set; }

        public Type [] [] DependsOn_From { get; private set; }

        public Type [] [] DependsOn_To { get; private set; }
    }
}
