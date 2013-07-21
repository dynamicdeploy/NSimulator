#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    internal sealed class SimplePacket : IPacket {
        public SimplePacket (IArray <byte> data) {
            this.Data = data;
        }

        #region IPacket Members

        public IArray <byte> Data { get; private set; }

        public IArray <byte> InternalData {
            get { return Array <byte>.Empty; }
        }

        #endregion
    }
}
