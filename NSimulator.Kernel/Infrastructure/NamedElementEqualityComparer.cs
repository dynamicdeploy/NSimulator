#region

using System;
using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Сравниватель именованных элементов.
    /// </summary>
    public sealed class NamedElementEqualityComparer : IEqualityComparer <INamedElement> {
        #region IEqualityComparer<INamedElement> Members

        /// <inheritdoc />
        public bool Equals (INamedElement x, INamedElement y) {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            return x.Name.Equals (y.Name);
        }

        /// <inheritdoc />
        public int GetHashCode (INamedElement obj) {
            if (obj == null)
                throw new ArgumentNullException ("obj");

            return obj.Name.GetHashCode ();
        }

        #endregion
    }
}
