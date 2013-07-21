#region

using System.IO;
using System.IO.Compression;
using CmdlineParser;

#endregion

namespace NetConv {
    public class Program {
        public static void Main (string [] args) {
            var arguments = new ProgramArguments ();
            if (!Parser.ParseArgumentsWithUsage (args, arguments))
                return;

            if (arguments.unpack) {
                using (var streamReader = File.OpenRead (arguments.from))
                using (var zipStream = new GZipStream (streamReader, CompressionMode.Decompress))
                using (var streamWriter = File.OpenWrite (arguments.to))
                    zipStream.CopyTo (streamWriter);
            }
            else {
                using (var streamReader = File.OpenRead (arguments.from))
                using (var streamWriter = File.OpenWrite (arguments.to))
                using (var zipStream = new GZipStream (streamWriter, CompressionMode.Compress))
                    streamReader.CopyTo (zipStream);
            }
        }
    }
}
