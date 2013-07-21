#region

using System;

#endregion

namespace NSimulator.Kernel {
    internal sealed class LoadError_AddInterfaceFailed : ILoadTopologyError {
        public LoadError_AddInterfaceFailed (ulong deviceId, ulong interfaceId, Exception internalException) {
            this.DeviceId = deviceId;
            this.InterfaceId = interfaceId;
            this.InternalException = internalException;
        }

        public ulong DeviceId { get; private set; }

        public ulong InterfaceId { get; private set; }

        public Exception InternalException { get; private set; }

        public override string ToString () {
            return string.Format (Strings.LoadError_AddInterfaceFailed,
                                  this.InterfaceId,
                                  this.DeviceId,
                                  this.InternalException.Message);
        }
    }
}
