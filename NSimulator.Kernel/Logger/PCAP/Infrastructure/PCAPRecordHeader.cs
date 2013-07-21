#region

using System;
using System.IO;

#endregion

namespace NSimulator.Kernel {
    internal sealed class PCAPRecordHeader {
        public PCAPRecordHeader (DateTime time, uint length) {
            var delta = time - new DateTime (1970, 1, 1, 0, 0, 0);
            this.ts_sec = (uint) (delta.Ticks / 10000000);
            this.ts_usec = (uint) ((delta.Ticks / 10000) % 1000);
            this.incl_len = length;
            this.orig_len = length;
        }

        public UInt32 ts_sec { get; private set; }

        public UInt32 ts_usec { get; private set; }

        public UInt32 incl_len { get; private set; }

        public UInt32 orig_len { get; private set; }

        public static int GetDumpLength () {
            return 4 + 4 + 4 + 4;
        }

        public void Dump (BinaryWriter writer) {
            writer.Write (this.ts_sec);
            writer.Write (this.ts_usec);
            writer.Write (this.incl_len);
            writer.Write (this.orig_len);
        }
    }
}
