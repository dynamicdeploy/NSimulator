#region

using System;
using System.IO;

#endregion

namespace NSimulator.Kernel {
    internal sealed class PCAPRecord {
        public PCAPRecord (DateTime time, IArray <byte> data) {
            this.Header = new PCAPRecordHeader (time, (uint) data.Length);
            this.Data = data;
        }

        public PCAPRecordHeader Header { get; private set; }

        public IArray <byte> Data { get; private set; }

        public static int GetDumpLength (IPacket packet) {
            return packet.Data.Length + PCAPRecordHeader.GetDumpLength ();
        }

        public void Dump (BinaryWriter writer) {
            this.Header.Dump (writer);
            writer.Write (this.Data);
        }
    }
}
