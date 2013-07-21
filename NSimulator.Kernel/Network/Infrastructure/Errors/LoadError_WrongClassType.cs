#region

using System;

#endregion

namespace NSimulator.Kernel {
    internal sealed class LoadError_WrongClassType : ILoadTopologyError {
        public LoadError_WrongClassType (ulong entityId, Type entityType, Type expectedType) {
            this.EntityId = entityId;
            this.EntityType = entityType;
            this.ExpectedType = expectedType;
        }

        public ulong EntityId { get; private set; }

        public Type EntityType { get; private set; }

        public Type ExpectedType { get; private set; }

        public override string ToString () {
            return string.Format (Strings.LoadError_WrongClassType,
                                  this.EntityId,
                                  this.ExpectedType.FullName,
                                  this.EntityType.FullName);
        }
    }
}
