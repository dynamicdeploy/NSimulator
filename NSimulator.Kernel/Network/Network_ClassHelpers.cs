#region

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    public sealed partial class Network {
        private IDictionary <ulong, Type> LoadClasses (XmlNode classNode) {
            var result = new Dictionary <ulong, Type> ();

            foreach (XmlNode node in classNode.ChildNodes) {
                var id = ulong.Parse (node.Attributes ["id"].Value);
                var assemblyName = node.Attributes ["name"].Value.ToLower ();

                if (!assemblies.ContainsKey (assemblyName)) {
                    try {
                        assemblies.Add (assemblyName, Assembly.LoadFrom (assemblyName));
                    }
                    catch (Exception exception) {
                        this.FireOnLoadErrorEvent (new LoadError_AssemblyLoadFailed (assemblyName, exception));

                        assemblies.Add (assemblyName, null);
                        continue;
                    }
                }

                var className = node.Attributes ["name"].Value;

                try {
                    result.Add (id, assemblies [assemblyName].GetType (className));
                }
                catch (Exception exception) {
                    this.FireOnLoadErrorEvent (new LoadError_ClassLoadFailed (className, assemblyName, exception));

                    result.Add (id, null);
                    continue;
                }
            }

            return result;
        }
    }
}
