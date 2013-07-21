#region

using System;
using System.Collections.Generic;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    public sealed partial class Network {
        private void LoadEntity <T> (XmlNode node, IDictionary <ulong, T> collection)
            where T : class, IXMLSerializable {
            if (node == null)
                throw new ArgumentNullException ("node");

            if (collection == null)
                throw new ArgumentNullException ("collection");

            var classes = this.LoadClasses (node ["classes"]);
            foreach (XmlNode entity in node ["list"].ChildNodes) {
                var class_id = ulong.Parse (entity.Attributes ["classid"].Value);
                var id = ulong.Parse (entity.Attributes ["id"].Value);

                if (! classes.ContainsKey (class_id)) {
                    this.FireOnLoadErrorEvent (new LoadError_WrongClassId (id, class_id));
                    continue;
                }

                var entityClass = classes [class_id];
                if (entityClass.IsSubclassOf (typeof (T))) {
                    try {
                        var instance = (typeof (T).IsSubclassOf (typeof (IBackboneView))
                                            ? entityClass.GetConstructor (new [] { typeof (IClock) }).Invoke (
                                                new object [] { this.Clock })
                                            : entityClass.GetConstructor (new Type [] { }).Invoke (new object [] { }))
                                       as T;
                        instance.Load (entity);
                        collection.Add (id, instance);
                    }
                    catch (Exception exception) {
                        this.FireOnLoadErrorEvent (new LoadError_EntityLoadFailed (id, exception));
                    }
                }
                else
                    this.FireOnLoadErrorEvent (new LoadError_WrongClassType (id, entityClass, typeof (T)));
            }
        }

        private void SaveEntity <T> (XmlNode node, string entityName, IEnumerable <KeyValuePair <ulong, T>> collection)
            where T : class, IXMLSerializable {
            if (node == null)
                throw new ArgumentNullException ("node");

            if (entityName == null)
                throw new ArgumentNullException ("entityName");

            if (collection == null)
                throw new ArgumentNullException ("collection");

            var doc = node.OwnerDocument;
            var classes = doc.CreateElement ("classes");
            var list = doc.CreateElement ("list");

            var clsid = new Dictionary <Type, ulong> ();
            foreach (var entity in collection) {
                var type = entity.Value.GetType ();
                ulong id;

                if (!clsid.ContainsKey (type)) {
                    id = (ulong) clsid.Count;
                    clsid.Add (type, id);

                    var clazz = doc.CreateElement ("class");
                    clazz.Attributes.Append (doc.CreateAttribute ("name"));
                    clazz.Attributes.Append (doc.CreateAttribute ("assembly"));
                    clazz.Attributes.Append (doc.CreateAttribute ("id"));
                    clazz.Attributes ["name"].Value = type.FullName;
                    clazz.Attributes ["assembly"].Value = type.Assembly.Location;
                    clazz.Attributes ["id"].Value = id.ToString ();
                    classes.AppendChild (clazz);
                }
                else
                    id = clsid [type];

                var entityNode = doc.CreateElement (entityName);
                entityNode.Attributes.Append (doc.CreateAttribute ("classid"));
                entityNode.Attributes.Append (doc.CreateAttribute ("id"));
                entityNode.Attributes ["classid"].Value = id.ToString ();
                entityNode.Attributes ["id"].Value = entity.Key.ToString ();
                try {
                    entity.Value.Store (entityNode);
                }
                catch {
                    continue;
                }
                list.AppendChild (entityNode);
            }

            node.AppendChild (classes);
            node.AppendChild (list);
        }
    }
}
