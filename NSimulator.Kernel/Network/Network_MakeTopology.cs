#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    public sealed partial class Network {
        private void MakeTopology (XmlNode node) {
            foreach (XmlNode child in node.ChildNodes) {
                if (child.Name == "device") {
                    var device_id = ulong.Parse (child.Attributes ["id"].Value);

                    if (!this.devices.ContainsKey (device_id)) {
                        this.FireOnLoadErrorEvent (new LoadError_WrongDeviceId (device_id));

                        continue;
                    }

                    var device = this.devices [device_id];
                    IDeviceEngine device_engine = null;
                    var device_modules = new HashSet <ulong> ();

                    foreach (XmlNode deviceEntity in child.ChildNodes) {
                        var id = ulong.Parse (deviceEntity.Attributes ["id"].Value);

                        if (deviceEntity.Name == "interface") {
                            if (this.interfaces.ContainsKey (id)) {
                                try {
                                    device.AddInterface (this.interfaces [id]);
                                }
                                catch (Exception exception) {
                                    this.FireOnLoadErrorEvent (new LoadError_AddInterfaceFailed (device_id,
                                                                                                 id,
                                                                                                 exception));
                                }
                            }
                            else
                                this.FireOnLoadErrorEvent (new LoadError_WrongInterfaceId (id));
                        }

                        if (deviceEntity.Name == "engine") {
                            if (this.engines.ContainsKey (id) && device_engine == null) {
                                try {
                                    device.SetEngine (device_engine = this.engines [id]);
                                }
                                catch (Exception exception) {
                                    this.FireOnLoadErrorEvent (new LoadError_AddEngineFailed (device_id, id, exception));
                                }
                            }
                            else {
                                if (!this.engines.ContainsKey (id))
                                    this.FireOnLoadErrorEvent (new LoadError_WrongEngineId (id));

                                if (device_engine != null)
                                    this.FireOnLoadErrorEvent (new LoadError_TwoEngines (device_id));
                            }
                        }

                        if (deviceEntity.Name == "module") {
                            if (this.modules.ContainsKey (id))
                                device_modules.Add (id);
                            else
                                this.FireOnLoadErrorEvent (new LoadError_WrongModuleId (id));
                        }
                    }

                    foreach (var moduleId in device_modules)
                        device.Engine.LoadModule (this.modules [moduleId]);
                }

                if (child.Name == "backbone") {
                    var backbone_id = ulong.Parse (child.Attributes ["id"].Value);
                    if (!this.backbones.ContainsKey (backbone_id)) {
                        this.FireOnLoadErrorEvent (new LoadError_WrongBackboneId (backbone_id));

                        continue;
                    }

                    var backbone = this.backbones [backbone_id];

                    foreach (var id in from XmlNode iface in child.ChildNodes
                                       select ulong.Parse (iface.Attributes ["id"].Value)) {
                        if (this.interfaces.ContainsKey (id)) {
                            try {
                                this.interfaces [id].SetBackbone (backbone);
                            }
                            catch (Exception exception) {
                                this.FireOnLoadErrorEvent (new LoadError_SetBackboneFailed (id, backbone_id, exception));
                            }
                        }
                        else {
                            this.FireOnLoadErrorEvent (new LoadError_WrongInterfaceId (id));

                            continue;
                        }
                    }
                }
            }
        }
    }
}
