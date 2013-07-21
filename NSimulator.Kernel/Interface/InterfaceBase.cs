#region

using System;
using System.Linq;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Абстрактный интерфейс.
    /// </summary>
    /// <remarks>
    ///   Абстрактный интерфейс содержит реализации всех методов, кроме инфраструктурного
    ///   метода <see cref = "IsCompatibleWith" />. Инфраструктурный метод проверяет
    ///   совместимость интерфейса и среды передачи данных, подключаемой к нему.
    /// </remarks>
    public abstract class InterfaceBase : IInterface {
        /// <summary>
        ///   Название xml-элемента "имя".
        /// </summary>
        protected const string XML_NAME_NODE = "name";

        /// <summary>
        ///   Название xml-элемента "интерфейс включён".
        /// </summary>
        protected const string XML_ENABLED_NODE = "enabled";

        /// <summary>
        ///   Среда передачи данных, к которой привязан интерфейс.
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
        ///   Выполняет загрузку информации из заданного xml-узла.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     Стандартный алгоритм загрузки информации об интерфейса работает следующим образом:
        ///     <list type = "number">
        ///       <item>
        ///         <description>
        ///           Загружаем имя интерфейса - текст, содержащийся внутри тега &lt;name&gt;.
        ///         </description>
        ///       </item>
        ///       <item>
        ///         <description>
        ///           Загружаем значение параметра "интерфейс включён", содержащееся внутри
        ///           тега &lt;enabled&gt;.
        ///         </description>
        ///       </item>
        ///     </list>
        ///   </para>
        /// </remarks>
        /// <param name = "data">xml-узел, из которого загружать информацию.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "data" /> является <c>null</c>.</exception>
        public virtual void Load (XmlNode data) {
            this.Name = data [XML_NAME_NODE].InnerText;
            this.Enabled = bool.Parse (data [XML_ENABLED_NODE].InnerText);
        }

        /// <summary>
        ///   Выполняет сохранение информации в заданный xml-узел.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     Стандартный алгоритм сохранения информации об интерфейсе
        ///     сохраняет следующую информацию:
        ///     <list type = "bullet">
        ///       <item>
        ///         <description>Название интерфейса.</description>
        ///       </item>
        ///       <item>
        ///         <description>Значение параметра "интерфейс включён".</description>
        ///       </item>
        ///     </list>
        ///   </para>
        ///   <para>
        ///     Сохранённая информация будет иметь следующий формат:
        ///     <code>
        ///       &lt;name&gt;Имя интерфейса&lt;/name&gt;
        ///       &lt;enabled&gt;Интерфейс включён?&lt;/enabled&gt;
        ///     </code>
        ///   </para>
        /// </remarks>
        /// <param name = "node">xml-узел, в который нужно сохранить информацию.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "node" /> является <c>null</c>.</exception>
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
        ///   Инфраструктура. Проверяет совместимость интерфейса и среды передачи данных.
        /// </summary>
        /// <param name = "backbone">Среда передачи, с которой проверяется совместимость.</param>
        /// <returns><c>true</c>, если данный интерфейс совместим со средой передачи, указанной в <paramref name = "backbone" />.</returns>
        protected abstract bool IsCompatibleWith (IBackboneView backbone);
    }
}
