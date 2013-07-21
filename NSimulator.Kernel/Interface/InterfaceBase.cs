#region

using System;
using System.Linq;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������� ���������.
    /// </summary>
    /// <remarks>
    ///   ����������� ��������� �������� ���������� ���� �������, ����� �����������������
    ///   ������ <see cref = "IsCompatibleWith" />. ���������������� ����� ���������
    ///   ������������� ���������� � ����� �������� ������, ������������ � ����.
    /// </remarks>
    public abstract class InterfaceBase : IInterface {
        /// <summary>
        ///   �������� xml-�������� "���".
        /// </summary>
        protected const string XML_NAME_NODE = "name";

        /// <summary>
        ///   �������� xml-�������� "��������� �������".
        /// </summary>
        protected const string XML_ENABLED_NODE = "enabled";

        /// <summary>
        ///   ����� �������� ������, � ������� �������� ���������.
        /// </summary>
        protected IBackbone _backbone;

        #region IInterface Members

        /// <inheritdoc />
        public IBackbone Backbone {
            get { return this._backbone; }
        }

        IBackboneView IInterfaceView.Backbone {
            get { return this.Backbone; }
        }

        /// <inheritdoc />
        public IDeviceView Device { get; private set; }

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        public event NamedElementChangedNameEventHandler OnBeforeChangeName;

        /// <inheritdoc />
        public bool Enabled { get; private set; }

        /// <inheritdoc />
        public virtual void SetName (string name) {
            if (this.OnBeforeChangeName != null) {
                try {
                    this.OnBeforeChangeName.Invoke (this, name);
                }
                catch {}
            }

            this.Name = name;
        }

        /// <inheritdoc />
        public virtual void SetBackbone (IBackbone backbone) {
            if (this.Backbone != null)
                throw new InterfaceAlreadyAttachedToBackboneException (this.Name);

            if (! this.IsCompatibleWith (backbone))
                throw new InterfaceNotCompatibleWithBackboneException (this.Name, backbone.Name);

            if (this.OnBeforeAttachBackbone != null) {
                try {
                    this.OnBeforeAttachBackbone.Invoke (this, backbone);
                }
                catch {}
            }

            backbone.AttachEndPoint (this);
            this._backbone = backbone;
        }

        /// <inheritdoc />
        public virtual void ReleaseBackbone () {
            if (this._backbone == null)
                throw new InterfaceNotAttachedToBackboneException (this.Name);

            if (this.OnBeforeDetachBackbone != null) {
                try {
                    this.OnBeforeDetachBackbone.Invoke (this);
                }
                catch {}
            }

            this._backbone.DetachEndPoint (this);
            this._backbone = null;
        }

        /// <inheritdoc />
        public virtual void SetDevice (IDeviceView device) {
            if (this.Device != null)
                throw new InterfaceAlreadyAttachedToDeviceException (this.Name);

            if (! device.Interfaces.Contains (this))
                throw new InterfaceNotAttachedToDeviceException (this.Name, device.Name);

            this.Device = device;
        }

        /// <inheritdoc />
        public virtual void ReleaseDevice () {
            if (this.Device == null)
                throw new InterfaceNotAttachedToDeviceException (this.Name);

            this.Device = null;
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
        }

        /// <inheritdoc />
        public virtual void SendPacket (IPacket packet) {
            if (this.Backbone == null)
                throw new InterfaceNotAttachedToBackboneException (this.Name);

            if (!this.Enabled)
                return;

            if (this.OnBeforeSend != null) {
                try {
                    this.OnBeforeSend.Invoke (this);
                }
                catch {}
            }

            this.Backbone.SendPacket (packet, this);
        }

        /// <inheritdoc />
        public virtual void ReceivePacket (IPacket packet) {
            if (this.Device == null)
                throw new InterfaceNotAttachedToDeviceException (this.Name);

            if (!this.Enabled)
                return;

            if (this.OnBeforeReceive != null) {
                try {
                    this.OnBeforeReceive.Invoke (this);
                }
                catch {}
            }

            this.Device.ProcessPacket (packet, this);
        }

        /// <inheritdoc />
        public event InterfaceSendPacketEventHandler OnBeforeSend;

        /// <inheritdoc />
        public event InterfaceReceivePacketEventHandler OnBeforeReceive;

        /// <inheritdoc />
        public event InterfaceStateChangedEventHandler OnBeforeEnable;

        /// <inheritdoc />
        public event InterfaceStateChangedEventHandler OnBeforeDisable;

        /// <inheritdoc />
        public event BackboneAttachedEventHandler OnBeforeAttachBackbone;

        /// <inheritdoc />
        public event BackboneDetachedEventHandler OnBeforeDetachBackbone;

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
        ///           ��������� �������� ��������� "��������� �������", ������������ ������
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
        ///         <description>�������� ��������� "��������� �������".</description>
        ///       </item>
        ///     </list>
        ///   </para>
        ///   <para>
        ///     ���������� ���������� ����� ����� ��������� ������:
        ///     <code>
        ///       &lt;name&gt;��� ����������&lt;/name&gt;
        ///       &lt;enabled&gt;��������� �������?&lt;/enabled&gt;
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

        /// <summary>
        ///   ��������������. ��������� ������������� ���������� � ����� �������� ������.
        /// </summary>
        /// <param name = "backbone">����� ��������, � ������� ����������� �������������.</param>
        /// <returns><c>true</c>, ���� ������ ��������� ��������� �� ������ ��������, ��������� � <paramref name = "backbone" />.</returns>
        protected abstract bool IsCompatibleWith (IBackboneView backbone);
    }
}
