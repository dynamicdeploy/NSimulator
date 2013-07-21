namespace NSimulator.Kernel {
    internal sealed class LoadError_TwoEngines : ILoadTopologyError {
        public LoadError_TwoEngines (ulong id) {
            this.DeviceId = id;
        }

        public ulong DeviceId { get; private set; }

        public override string ToString () {
            return string.Format (Strings.LoadError_TwoEngines, this.DeviceId);
        }
    }
}
