#region

using System;

#endregion

namespace NSimulator.Kernel {
    internal sealed class LoadError_AddEngineFailed : ILoadTopologyError {
        public LoadError_AddEngineFailed (ulong deviceId, ulong engineId, Exception internalException) {
            this.DeviceId = deviceId;
            this.EngineId = engineId;
            this.InternalException = internalException;
        }

        public ulong DeviceId { get; private set; }

        public ulong EngineId { get; private set; }

        public Exception InternalException { get; private set; }

        public override string ToString () {
            return string.Format (Strings.LoadError_AddEngineFailed,
                                  this.EngineId,
                                  this.DeviceId,
                                  this.InternalException.Message);
        }
    }
}
