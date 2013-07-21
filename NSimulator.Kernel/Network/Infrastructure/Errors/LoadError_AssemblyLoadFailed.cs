#region

using System;

#endregion

namespace NSimulator.Kernel {
    internal sealed class LoadError_AssemblyLoadFailed : ILoadTopologyError {
        public LoadError_AssemblyLoadFailed (string name, Exception exception) {
            this.AssemblyName = name;
            this.InternalException = exception;
        }

        public string AssemblyName { get; private set; }

        public Exception InternalException { get; private set; }

        public override string ToString () {
            return string.Format (Strings.LoadError_AssemblyLoadFailed,
                                  this.AssemblyName,
                                  this.InternalException.Message);
        }
    }
}
