#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Абстрактное устройство.
    /// </summary>
    /// <remarks>
    ///   Абстрактное устройство содержит реализации методов добавления и удаления интерфейсов,
    ///   а также методы включения-выключения устройства и установки прошивки.
    ///   Единственный нереализованный метод - метод обработки пакета.
    /// </remarks>
    public abstract class DeviceBase : IDevice {
        /// <summary>
        ///   Название xml-элемента "имя".
        /// </summary>
        protected const string XML_NAME_NODE = "name";

        /// <summary>
        ///   Название xml-элемента "устройство включено".
        /// </summary>
        protected const string XML_ENABLED_NODE = "enabled";

        /// <summary>
        ///   Коллекция интерфейсов.
        /// </summary>
        /// <remarks>
        ///   Ключами коллекции являются названия интерфейсов, значениями - ссылки на интерфейсы.
        /// </remarks>
        protected readonly IDictionary <string, IInterface> interfaces;

        /// <summary>
        ///   Инициализирует абстрактное устройство.
        /// </summary>
        /// <remarks>
        ///   После инициализации устройство не содержит прошивку и не содержит интерфейсов.
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
        ///   Добавляет физический интерфейс в устройство.
        /// </summary>
        /// <param name = "iface">Физический интерфейс, который нужно добавить.</param>
        /// <remarks>
        ///   Название интерфейса получается автоматически. Для этого используется префикс,
        ///   указанный в атрибуте <see cref = "InterfacePrefixAttribute" /> класса интерфейса.
        ///   Если атрибут не указан, то в качестве префикса будет использована строка <i>"iface"</i>.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToDeviceException">Интерфейс уже привязан к устройству.</exception>
        public virtual void AddInterface (IInterface iface) {
            if (iface.Device != null)
                throw new InterfaceAlreadyAttachedToDeviceException (iface.Name);

            var prefix = InterfacePrefixAttribute.GetPrefix (iface) ?? "iface";
            this.AddInterface_PrefixNamed (iface, prefix);
        }

        /// <summary>
        ///   Добавляет физический интерфейс в устройство.
        /// </summary>
        /// <param name = "iface">Физический интерфейс, который нужно добавить.</param>
        /// <param name = "name">Название интерфейса.</param>
        /// <remarks>
        ///   В качестве названия интерфейса используется заданное в <paramref name = "name" /> имя.
        ///   После добавления интерфейса в устройство ему присваивается указанное имя.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToDeviceException">Интерфейс уже привязан к устройству.</exception>
        /// <exception cref = "InterfaceNameMustBeUniqueException">Имя интерфейса не уникально в пределах устройства.</exception>
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
        ///   Добавляет физический интерфейс в устройство.
        /// </summary>
        /// <param name = "iface">Физический интерфейс, который нужно добавить.</param>
        /// <param name = "prefix">Префикс названия интерфейса.</param>
        /// <remarks>
        ///   Название интерфейса получается автоматически из указанного в <paramref name = "prefix" /> префикса.
        ///   Из устройства выбираются все интерфейсы название которых состоит из префикса и числа.
        ///   В качестве имени берётся строка из префикса с числом, которое на 1 больше, чем максимальное
        ///   в выбранных интерфейсах, или 0, если ни один интерфейс не выбран.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "prefix" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToDeviceException">Интерфейс уже привязан к устройству.</exception>
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
        ///   Выполняет загрузку информации из заданного xml-узла.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     Стандартный алгоритм загрузки информации об устройстве работает следующим образом:
        ///     <list type = "number">
        ///       <item>
        ///         <description>
        ///           Загружаем имя устройства - текст, содержащийся внутри тега &lt;name&gt;.
        ///         </description>
        ///       </item>
        ///       <item>
        ///         <description>
        ///           Загружаем значение параметра "устройство включено", содержащееся внутри
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
        ///     Стандартный алгоритм сохранения информации об устройстве
        ///     сохраняет следующую информацию:
        ///     <list type = "bullet">
        ///       <item>
        ///         <description>Название устройства.</description>
        ///       </item>
        ///       <item>
        ///         <description>Значение параметра "устройство включено".</description>
        ///       </item>
        ///     </list>
        ///   </para>
        ///   <para>
        ///     Сохранённая информация будет иметь следующий формат:
        ///     <code>
        ///       &lt;name&gt;Имя устройства&lt;/name&gt;
        ///       &lt;enabled&gt;Устройство включено?&lt;/enabled&gt;
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
    }
}
