namespace NSimulator.Kernel {
    internal sealed class LoadError_WrongClassId : ILoadTopologyError {
        public LoadError_WrongClassId (ulong entityId, ulong classId) {
            this.EntityId = entityId;
            this.ClassId = classId;
        }

        public ulong EntityId { get; private set; }

        public ulong ClassId { get; private set; }

        public override string ToString () {
            return string.Format (Strings.LoadError_WrongClassId, this.EntityId, this.ClassId);
        }
    }
}
