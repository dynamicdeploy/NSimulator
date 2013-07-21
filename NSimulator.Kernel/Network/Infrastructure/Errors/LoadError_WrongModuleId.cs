namespace NSimulator.Kernel {
    internal sealed class LoadError_WrongModuleId : LoadError_WrongId {
        public LoadError_WrongModuleId (ulong id)
            : base (id) {}

        public override string ToString () {
            return string.Format (Strings.LoadError_WrongModuleId, this.Id);
        }
    }
}
