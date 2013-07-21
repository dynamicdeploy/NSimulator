namespace NSimulator.Kernel {
    /// <summary>
    ///   Описатель сущности в сети.
    /// </summary>
    /// <seealso cref = "Network" />
    public sealed class NetworkEntity {
        internal NetworkEntity (ulong id) {
            this.Id = id;
        }

        internal ulong Id { get; private set; }

        /// <inheritdoc />
        public override string ToString () {
            return string.Format ("HANDLE(NetworkEntity:0x{0:X16})", this.Id);
        }
    }
}
