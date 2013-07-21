namespace NSimulator.Kernel {
    internal sealed class LoadError_WrongDeviceId : LoadError_WrongId {
        public LoadError_WrongDeviceId (ulong id)
            : base (id) {}

        public override string ToString () {
            return string.Format (Strings.LoadError_WrongDeviceId, this.Id);
        }
    }
}
