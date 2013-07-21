#region

using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (INamedElement))]
    internal abstract class Contract_INamedElement : INamedElement {
        #region INamedElement Members

        public string Name {
            get { return default (string); }
        }

        public abstract event NamedElementChangedNameEventHandler OnBeforeChangeName;

        #endregion
    }
}
