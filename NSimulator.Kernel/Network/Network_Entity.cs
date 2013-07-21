namespace NSimulator.Kernel {
    public sealed partial class Network {
        #region Nested type: Entity

        /// <summary>
        ///   Описатель сущности в сети.
        /// </summary>
        /// <seealso cref = "Network" />
        public sealed class Entity {
            internal Entity (ulong id) {
                this.Id = id;
            }

            internal ulong Id { get; private set; }

            /// <inheritdoc />
            public override string ToString () {
                return string.Format ("HANDLE(NetworkEntity:0x{0:X16})", this.Id);
            }
        }

        #endregion
    }
}