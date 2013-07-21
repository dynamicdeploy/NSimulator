#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    public sealed partial class Network {
        private static IDictionary <Value, Key> InvertDictionary <Key, Value> (
            IEnumerable <KeyValuePair <Key, Value>> dictionary)
            where Value : class {
            return InvertDictionary <Key, Value, Value> (dictionary);
        }

        private static IDictionary <ValueView, Key> InvertDictionary <Key, Value, ValueView> (
            IEnumerable <KeyValuePair <Key, Value>> dictionary)
            where Value : ValueView
            where ValueView : class {
            return dictionary.ToDictionary (_ => _.Value as ValueView, _ => _.Key);
        }

        private void SaveTopology (XmlNode node) {
            if (node == null)
                throw new ArgumentNullException ("node");

            var inv_devices = InvertDictionary (this.devices);
            var inv_interfaces = InvertDictionary <ulong, IInterface, IInterfaceView> (this.interfaces);
            var inv_backbones = InvertDictionary (this.backbones);
            var inv_modules = InvertDictionary (this.modules);
            var inv_engines = InvertDictionary (this.engines);

            var doc = node.OwnerDocument;

            foreach (var device in inv_devices) {
                var deviceNode = doc.CreateElement ("device");
                deviceNode.Attributes.Append (doc.CreateAttribute ("id"));
                deviceNode.Attributes ["id"].Value = device.Value.ToString ();

                foreach (var iface in device.Key.Interfaces) {
                    var interfaceNode = doc.CreateElement ("interface");
                    interfaceNode.Attributes.Append (doc.CreateAttribute ("id"));
                    interfaceNode.Attributes ["id"].Value = inv_interfaces [iface].ToString ();
                    deviceNode.AppendChild (interfaceNode);
                }

                if (device.Key.Engine != null) {
                    var engineNode = doc.CreateElement ("engine");
                    engineNode.Attributes.Append (doc.CreateAttribute ("id"));
                    engineNode.Attributes ["id"].Value = inv_engines [device.Key.Engine].ToString ();
                    deviceNode.AppendChild (engineNode);

                    foreach (var module in device.Key.Engine.Modules) {
                        var moduleNode = doc.CreateElement ("module");
                        moduleNode.Attributes.Append (doc.CreateAttribute ("id"));
                        moduleNode.Attributes ["id"].Value = inv_modules [module].ToString ();
                        deviceNode.AppendChild (moduleNode);
                    }
                }

                node.AppendChild (deviceNode);
            }

            foreach (var backbone in inv_backbones) {
                var backboneNode = doc.CreateElement ("backbone");
                backboneNode.Attributes.Append (doc.CreateAttribute ("id"));
                backboneNode.Attributes ["id"].Value = backbone.Value.ToString ();

                foreach (var iface in backbone.Key.EndPoints) {
                    var interfaceNode = doc.CreateElement ("interface");
                    interfaceNode.Attributes.Append (doc.CreateAttribute ("id"));
                    interfaceNode.Attributes ["id"].Value = inv_interfaces [iface].ToString ();
                    backboneNode.AppendChild (interfaceNode);
                }

                node.AppendChild (backboneNode);
            }
        }
    }
}
