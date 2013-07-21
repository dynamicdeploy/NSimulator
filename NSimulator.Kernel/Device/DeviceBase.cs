#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������� ����������.
    /// </summary>
    /// <remarks>
    ///   ����������� ���������� �������� ���������� ������� ���������� � �������� �����������,
    ///   � ����� ������ ���������-���������� ���������� � ��������� ��������.
    ///   ������������ ��������������� ����� - ����� ��������� ������.
    /// </remarks>
    public abstract class DeviceBase : IDevice {
        /// <summary>
        ///   �������� xml-�������� "���".
        /// </summary>
        protected const string XML_NAME_NODE = "name";

        /// <summary>
        ///   �������� xml-�������� "���������� ��������".
        /// </summary>
        protected const string XML_ENABLED_NODE = "enabled";

        /// <summary>
        ///   ��������� �����������.
        /// </summary>
        /// <remarks>
        ///   ������� ��������� �������� �������� �����������, ���������� - ������ �� ����������.
        /// </remarks>
        protected readonly IDictionary <string, IInterface> interfaces;

        /// <summary>
        ///   �������������� ����������� ����������.
        /// </summary>
        /// <remarks>
        ///   ����� ������������� ���������� �� �������� �������� � �� �������� �����������.
        /// </remarks>
        protected DeviceBase () {
            this.interfaces = new Dictionary <string, IInterface> ();
            this.Engine = null;
            this.Name = string.Empty;
        }

        #region IDevice Members

        /// <inheritdoc />
        IInterfaceView IDeviceView.this [string name] {
            get { return this [name]; }
        }

        /// <inheritdoc />
        public IEnumerable <IInterface> Interfaces {
            get { return this.interfaces.Values; }
        }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        public event NamedElementChangedNameEventHandler OnBeforeChangeName;

        /// <inheritdoc />
        public IInterface this [string name] {
            get {
                if (!this.interfaces.ContainsKey (name))
                    throw new InterfaceNotFoundException (name, this.Name);

                return this.interfaces [name];
            }
        }

        /// <inheritdoc />
        public int InterfacesCount {
            get { return this.interfaces.Count; }
        }

        /// <inheritdoc />
        IEnumerable <IInterfaceView> IDeviceView.Interfaces {
            get { return this.Interfaces; }
        }

        /// <inheritdoc />
        public IDeviceEngine Engine { get; private set; }

        /// <inheritdoc />
        public bool Enabled { get; private set; }

        /// <inheritdoc />
        public abstract void ProcessPacket (IPacket packet, IInterfaceView from);

        /// <inheritdoc />
        public event DeviceStateChangedEventHandler OnBeforeEnable;

        /// <inheritdoc />
        public event DeviceStateChangedEventHandler OnBeforeDisable;

        /// <inheritdoc />
        public event DeviceStructureChangedEventHandler OnBeforeAddInterface;

        /// <inheritdoc />
        public event DeviceStructureChangedEventHandler OnBeforeRemoveInterface;

        /// <inheritdoc />
        public virtual void SetEngine (IDeviceEngine engine) {
            this.Engine = engine;
        }

        /// <inheritdoc />
        public void SetName (string name) {
            if (this.OnBeforeChangeName != null) {
                try {
                    this.OnBeforeChangeName.Invoke (this, name);
                }
                catch {}
            }

            this.Name = name;
        }

        /// <summary>
        ///   ��������� ���������� ��������� � ����������.
        /// </summary>
        /// <param name = "iface">���������� ���������, ������� ����� ��������.</param>
        /// <remarks>
        ///   �������� ���������� ���������� �������������. ��� ����� ������������ �������,
        ///   ��������� � �������� <see cref = "InterfacePrefixAttribute" /> ������ ����������.
        ///   ���� ������� �� ������, �� � �������� �������� ����� ������������ ������ <i>"iface"</i>.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> �������� <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToDeviceException">��������� ��� �������� � ����������.</exception>
        public virtual void AddInterface (IInterface iface) {
            if (iface.Device != null)
                throw new InterfaceAlreadyAttachedToDeviceException (iface.Name);

            var prefix = InterfacePrefixAttribute.GetPrefix (iface) ?? "iface";
            this.AddInterface_PrefixNamed (iface, prefix);
        }

        /// <summary>
        ///   ��������� ���������� ��������� � ����������.
        /// </summary>
        /// <param name = "iface">���������� ���������, ������� ����� ��������.</param>
        /// <param name = "name">�������� ����������.</param>
        /// <remarks>
        ///   � �������� �������� ���������� ������������ �������� � <paramref name = "name" /> ���.
        ///   ����� ���������� ���������� � ���������� ��� ������������� ��������� ���.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> �������� <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToDeviceException">��������� ��� �������� � ����������.</exception>
        /// <exception cref = "InterfaceNameMustBeUniqueException">��� ���������� �� ��������� � �������� ����������.</exception>
        public virtual void AddInterface (IInterface iface, string name) {
            if (iface.Device != null)
                throw new InterfaceAlreadyAttachedToDeviceException (iface.Name);

            if (this.interfaces.ContainsKey (name))
                throw new InterfaceNameMustBeUniqueException (name, this.Name);

            if (this.OnBeforeAddInterface != null) {
                try {
                    this.OnBeforeAddInterface.Invoke (this, iface);
                }
                catch {}
            }

            this.interfaces.Add (name, iface);
            iface.SetName (name);
            iface.SetDevice (this);
        }

        /// <summary>
        ///   ��������� ���������� ��������� � ����������.
        /// </summary>
        /// <param name = "iface">���������� ���������, ������� ����� ��������.</param>
        /// <param name = "prefix">������� �������� ����������.</param>
        /// <remarks>
        ///   �������� ���������� ���������� ������������� �� ���������� � <paramref name = "prefix" /> ��������.
        ///   �� ���������� ���������� ��� ���������� �������� ������� ������� �� �������� � �����.
        ///   � �������� ����� ������ ������ �� �������� � ������, ������� �� 1 ������, ��� ������������
        ///   � ��������� �����������, ��� 0, ���� �� ���� ��������� �� ������.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "prefix" /> �������� <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToDeviceException">��������� ��� �������� � ����������.</exception>
        public virtual void AddInterface_PrefixNamed (IInterface iface, string prefix) {
            if (iface.Device != null)
                throw new InterfaceAlreadyAttachedToDeviceException (iface.Name);

            var result = 0;
            this.AddInterface (iface,
                               string.Format ("{0}{1}",
                                              prefix,
                                              this.interfaces.Keys.Where (_iface => _iface.StartsWith (prefix)).Where (
                                                  _iface => int.TryParse (_iface.Substring (prefix.Length), out result))
                                                  .
                                                  Aggregate (-1, (current, _iface) => Math.Max (current, result)) + 1));
        }

        /// <inheritdoc />
        public virtual void RemoveInterface (string name) {
            if (!this.interfaces.ContainsKey (name))
                throw new InterfaceNotFoundException (name, this.Name);

            var iface = this.interfaces [name];

            if (this.OnBeforeRemoveInterface != null) {
                try {
                    this.OnBeforeRemoveInterface.Invoke (this, iface);
                }
                catch {}
            }

            iface.ReleaseDevice ();
            this.interfaces.Remove (name);
        }

        /// <inheritdoc />
        public void AttachBackbone (string iface_name, IBackbone backbone) {
            if (!this.interfaces.ContainsKey (iface_name))
                throw new InterfaceNotFoundException (iface_name, this.Name);

            var iface = this.interfaces [iface_name];
            if (iface.Backbone != null)
                throw new InterfaceAlreadyAttachedToBackboneException (iface_name);

            iface.SetBackbone (backbone);
        }

        /// <inheritdoc />
        public void DetachBackbone (string iface_name) {
            if (!this.interfaces.ContainsKey (iface_name))
                throw new InterfaceNotFoundException (iface_name, this.Name);

            var iface = this.interfaces [iface_name];
            if (iface.Backbone == null)
                throw new InterfaceNotAttachedToBackboneException (iface_name);

            iface.ReleaseBackbone ();
        }

        /// <inheritdoc />
        public virtual void RemoveInterfaces () {
            try {
                while (true)
                    this.RemoveInterface (this.interfaces.First ().Key);
            }
            catch {}
        }

        /// <inheritdoc />
        public virtual void Enable () {
            if (this.OnBeforeEnable != null) {
                try {
                    this.OnBeforeEnable.Invoke (this, true);
                }
                catch {}
            }

            this.Enabled = true;

            foreach (var iface in this.interfaces)
                iface.Value.Enable ();
        }

        /// <inheritdoc />
        public virtual void Disable () {
            if (this.OnBeforeDisable != null) {
                try {
                    this.OnBeforeDisable.Invoke (this, false);
                }
                catch {}
            }

            this.Enabled = false;

            foreach (var iface in this.interfaces)
                iface.Value.Disable ();
        }

        /// <summary>
        ///   ��������� �������� ���������� �� ��������� xml-����.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     ����������� �������� �������� ���������� �� ���������� �������� ��������� �������:
        ///     <list type = "number">
        ///       <item>
        ///         <description>
        ///           ��������� ��� ���������� - �����, ������������ ������ ���� &lt;name&gt;.
        ///         </description>
        ///       </item>
        ///       <item>
        ///         <description>
        ///           ��������� �������� ��������� "���������� ��������", ������������ ������
        ///           ���� &lt;enabled&gt;.
        ///         </description>
        ///       </item>
        ///     </list>
        ///   </para>
        /// </remarks>
        /// <param name = "data">xml-����, �� �������� ��������� ����������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "data" /> �������� <c>null</c>.</exception>
        public virtual void Load (XmlNode data) {
            this.Name = data [XML_NAME_NODE].InnerText;
            this.Enabled = bool.Parse (data [XML_ENABLED_NODE].InnerText);
        }

        /// <summary>
        ///   ��������� ���������� ���������� � �������� xml-����.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     ����������� �������� ���������� ���������� �� ����������
        ///     ��������� ��������� ����������:
        ///     <list type = "bullet">
        ///       <item>
        ///         <description>�������� ����������.</description>
        ///       </item>
        ///       <item>
        ///         <description>�������� ��������� "���������� ��������".</description>
        ///       </item>
        ///     </list>
        ///   </para>
        ///   <para>
        ///     ���������� ���������� ����� ����� ��������� ������:
        ///     <code>
        ///       &lt;name&gt;��� ����������&lt;/name&gt;
        ///       &lt;enabled&gt;���������� ��������?&lt;/enabled&gt;
        ///     </code>
        ///   </para>
        /// </remarks>
        /// <param name = "node">xml-����, � ������� ����� ��������� ����������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "node" /> �������� <c>null</c>.</exception>
        public virtual void Store (XmlNode node) {
            var doc = node.OwnerDocument;

            var nameNode = doc.CreateElement (XML_NAME_NODE);
            nameNode.InnerText = this.Name;
            node.AppendChild (nameNode);

            var enabledNode = doc.CreateElement (XML_ENABLED_NODE);
            enabledNode.InnerText = this.Enabled.ToString ();
            node.AppendChild (enabledNode);
        }

        #endregion
    }
}
