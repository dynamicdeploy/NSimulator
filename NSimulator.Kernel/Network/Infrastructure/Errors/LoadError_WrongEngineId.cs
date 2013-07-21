namespace NSimulator.Kernel {
    internal sealed class LoadError_WrongEngineId : LoadError_WrongId {
        public LoadError_WrongEngineId (ulong id)
            : base (id) {}

        public override string ToString () {
            return string.Format (Strings.LoadError_WrongEngineId, this.Id);
        }
    }
}
