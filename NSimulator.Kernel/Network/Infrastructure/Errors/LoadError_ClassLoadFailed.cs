#region

using System;

#endregion

namespace NSimulator.Kernel {
    internal sealed class LoadError_ClassLoadFailed : ILoadTopologyError {
        public LoadError_ClassLoadFailed (string className, string assemblyName, Exception exception) {
            this.ClassName = className;
            this.AssemblyName = assemblyName;
            this.InternalException = exception;
        }

        public string ClassName { get; private set; }

        public string AssemblyName { get; private set; }

        public Exception InternalException { get; private set; }

        public override string ToString () {
            return string.Format (Strings.LoadError_ClassLoadFailed,
                                  this.ClassName,
                                  this.AssemblyName,
                                  this.InternalException.Message);
        }
    }
}
