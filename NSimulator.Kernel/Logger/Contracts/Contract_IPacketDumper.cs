#region

using System;
using System.Diagnostics.Contracts;
using System.IO;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (IPacketDumper))]
    internal abstract class Contract_IPacketDumper : IPacketDumper {
        #region IPacketDumper Members

        public void Dispose () {}

        public void AttachToBackbone (IBackboneView backbone) {
            Contract.Requires <ArgumentNullException> (backbone != null);
        }

        public void AttachToBackbone (IBackboneView backbone, Stream stream) {
            Contract.Requires <ArgumentNullException> (backbone != null);
            Contract.Requires <ArgumentNullException> (stream != null);
        }

        public void DetachFromBackbone (IBackboneView backbone) {
            Contract.Requires <ArgumentNullException> (backbone != null);
        }

        #endregion
    }
}
