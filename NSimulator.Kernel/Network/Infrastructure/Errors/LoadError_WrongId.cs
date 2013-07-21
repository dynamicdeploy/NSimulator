namespace NSimulator.Kernel {
    internal abstract class LoadError_WrongId : ILoadTopologyError {
        protected LoadError_WrongId (ulong id) {
            this.Id = id;
        }

        public ulong Id { get; private set; }
    }
}
