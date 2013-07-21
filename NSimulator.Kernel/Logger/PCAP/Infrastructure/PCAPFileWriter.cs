#region

using System;
using System.IO;

#endregion

namespace NSimulator.Kernel {
    internal sealed class PCAPFileWriter : IDisposable {
        private readonly bool dispose;
        private readonly BinaryWriter writer;
        private bool isDisposed;

        public PCAPFileWriter (string filename, PCAPNetworkTypes networkType) {
            this.writer = new BinaryWriter (new FileStream (filename, FileMode.OpenOrCreate, FileAccess.Write));
            this.dispose = true;
            new PCAPFileHeader (networkType).Dump (this.writer);
        }

        public PCAPFileWriter (Stream stream, PCAPNetworkTypes networkType, bool dispose) {
            this.writer = new BinaryWriter (stream);
            this.dispose = dispose;
            new PCAPFileHeader (networkType).Dump (this.writer);
        }

        #region IDisposable Members

        public void Dispose () {
            this.Dispose (true);
            GC.SuppressFinalize (this);
        }

        #endregion

        public void Write (IPacket packet) {
            this.Write (packet, DateTime.Now);
        }

        public void Write (IPacket packet, DateTime time) {
            new PCAPRecord (time, packet.Data).Dump (this.writer);
            this.writer.Flush ();
        }

        private void Dispose (bool disposing) {
            if (this.isDisposed)
                return;

            if (disposing && this.dispose)
                this.writer.Dispose ();

            this.isDisposed = true;
        }

        ~PCAPFileWriter () {
            this.Dispose (false);
        }
    }
}
