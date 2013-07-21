namespace NSimulator.Kernel {
    internal sealed class LoadError_WrongInterfaceId : LoadError_WrongId {
        public LoadError_WrongInterfaceId (ulong id)
            : base (id) {}

        public override string ToString () {
            return string.Format (Strings.LoadError_WrongInterfaceId, this.Id);
        }
    }
}
