#region

using System;

#endregion

namespace NSimulator.Kernel {
    internal sealed class LoadError_EntityLoadFailed : ILoadTopologyError {
        public LoadError_EntityLoadFailed (ulong id, Exception exception) {
            this.EntityId = id;
            this.InternalException = exception;
        }

        public ulong EntityId { get; private set; }

        public Exception InternalException { get; private set; }

        public override string ToString () {
            return string.Format (Strings.LoadError_EntityLoadFailed, this.EntityId, this.InternalException.Message);
        }
    }
}
