#region

using System;
using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    internal sealed class ModulesNameEqualityComparer : IEqualityComparer <IModule> {
        private static ModulesNameEqualityComparer instance;

        public static ModulesNameEqualityComparer Instance {
            get {
                if (instance == null)
                    Instance = new ModulesNameEqualityComparer ();

                return instance;
            }

            private set { instance = value; }
        }

        #region IEqualityComparer<IModule> Members

        public bool Equals (IModule x, IModule y) {
            if (x == null)
                throw new ArgumentNullException ("x");

            if (y == null)
                throw new ArgumentNullException ("y");

            if (x == y)
                return true;

            return x.Name == y.Name;
        }

        public int GetHashCode (IModule obj) {
            if (obj == null)
                throw new ArgumentNullException ("obj");

            return obj.GetHashCode ();
        }

        #endregion
    }
}
