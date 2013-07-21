#region

using System;
using System.IO;

#endregion

namespace NSimulator.Kernel {
    internal sealed class PCAPFileHeader {
        public PCAPFileHeader (PCAPNetworkTypes networkType) {
            this.magic_number = 0xA1B2C3D4;
            this.version_major = 2;
            this.version_minor = 4;
            this.thiszone = 0;
            this.sigfigs = 0;
            this.snaplen = 65535;
            this.network = networkType;
        }

        public UInt32 magic_number { get; private set; }

        public UInt16 version_major { get; private set; }

        public UInt16 version_minor { get; private set; }

        public Int32 thiszone { get; private set; }

        public UInt32 sigfigs { get; private set; }

        public UInt32 snaplen { get; private set; }

        public PCAPNetworkTypes network { get; private set; }

        public void Dump (BinaryWriter writer) {
            writer.Write (this.magic_number);
            writer.Write (this.version_major);
            writer.Write (this.version_minor);
            writer.Write (this.thiszone);
            writer.Write (this.sigfigs);
            writer.Write (this.snaplen);
            writer.Write ((uint) this.network);
        }
    }
}
