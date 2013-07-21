#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public sealed class PacketMock : IPacket {
        public PacketMock (IArray <byte> data) {
            this.Data = data;
        }

        public PacketMock (int length, byte b = (byte) 0) {
            this.Data = new Array <byte> (new byte[length]);

            for (var i = 0; i < length; ++i)
                this.Data [i] = b;
        }

        #region IPacket Members

        public IArray <byte> Data { get; private set; }

        public IArray <byte> InternalData {
            get { return this.Data; }
        }

        #endregion
    }
}
