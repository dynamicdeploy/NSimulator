#region

using System;

#endregion

namespace NSimulator.Kernel {
    internal sealed class LoadError_SetBackboneFailed : ILoadTopologyError {
        public LoadError_SetBackboneFailed (ulong interfaceId, ulong backboneId, Exception internalException) {
            this.InterfaceId = interfaceId;
            this.BackboneId = backboneId;
            this.InternalException = internalException;
        }

        public ulong InterfaceId { get; private set; }

        public ulong BackboneId { get; private set; }

        public Exception InternalException { get; private set; }

        public override string ToString () {
            return string.Format (Strings.LoadError_SetBackboneFailed,
                                  this.InterfaceId,
                                  this.BackboneId,
                                  this.InternalException.Message);
        }
    }
}
