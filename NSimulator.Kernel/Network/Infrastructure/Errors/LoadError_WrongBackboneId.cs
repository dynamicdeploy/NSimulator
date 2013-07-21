namespace NSimulator.Kernel {
    internal sealed class LoadError_WrongBackboneId : LoadError_WrongId {
        public LoadError_WrongBackboneId (ulong id)
            : base (id) {}

        public override string ToString () {
            return string.Format (Strings.LoadError_WrongBackboneId, this.Id);
        }
    }
}
