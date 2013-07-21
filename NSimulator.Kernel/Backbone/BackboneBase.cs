#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Абстрактная среда передачи данных.
    /// </summary>
    public abstract class BackboneBase : IBackbone {
        /// <summary>
        ///   Название xml-элемента "имя".
        /// </summary>
        protected const string XML_NAME_NODE = "name";

        /// <summary>
        ///   Название xml-элемента "ёмкость".
        /// </summary>
        protected const string XML_CAPACITY_NODE = "capacity";

        /// <summary>
        ///   Название xml-элемента "характеристика".
        /// </summary>
        protected const string XML_CHARACTERISTIC_NODE = "characteristic";

        /// <summary>
        ///   Название xml-атрибута "имя".
        /// </summary>
        protected const string XML_NAME_ATTRIBUTE = "name";

        /// <summary>
        ///   Множество конечных точек, привязанных к среде передачи.
        /// </summary>
        protected readonly ISet <IInterfaceView> endpoints;

        /// <summary>
        ///   Инициализирует среду передачи определённым числом максимально
        ///   возможных конечных точек.
        /// </summary>
        /// <param name = "capacity">Максимально возможное число конечных точек.</param>
        protected BackboneBase (int capacity) {
            this.endpoints = new HashSet <IInterfaceView> ();
            this.EndPointsCapacity = capacity;
        }

        #region IBackbone Members

        /// <inheritdoc />
        public string Name { get; protected set; }

        /// <inheritdoc />
        public event NamedElementChangedNameEventHandler OnBeforeChangeName;

        /// <inheritdoc />
        public int EndPointsCount {
            get { return this.endpoints.Count; }
        }

        /// <inheritdoc />
        public int EndPointsCapacity { get; private set; }

        /// <inheritdoc />
        public IEnumerable <IInterfaceView> EndPoints {
            get { return this.endpoints; }
        }

        /// <inheritdoc />
        [BackboneCharacteristic]
        public PCAPNetworkTypes Type { get; protected set; }

        /// <inheritdoc />
        [BackboneCharacteristic]
        public Enum State { get; protected set; }

        /// <inheritdoc />
        [BackboneCharacteristic]
        public ulong Speed { get; protected set; }

        /// <inheritdoc />
        [BackboneCharacteristic]
        public double LossPercent { get; protected set; }

        /// <inheritdoc />
        public abstract void SendPacket (IPacket packet, IInterfaceView from);

        /// <inheritdoc />
        public abstract event TransmitEventHandler OnTransmit;

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

        /// <inheritdoc />
        public virtual void AttachEndPoint (IInterfaceView iface) {
            if (iface.Backbone != null || this.endpoints.Contains (iface))
                throw new InterfaceAlreadyAttachedToBackboneException (iface.Name);

            if (this.EndPointsCount >= this.EndPointsCapacity)
                throw new EndPointsOverflowException (this.Name, this.EndPointsCapacity);

            this.endpoints.Add (iface);
        }

        /// <inheritdoc />
        public virtual void DetachEndPoint (IInterfaceView iface) {
            if (iface.Backbone != this || ! this.endpoints.Contains (iface))
                throw new InterfaceNotAttachedToBackboneException (iface.Name, this.Name);

            this.endpoints.Remove (iface);
        }

        /// <inheritdoc />
        public virtual void ChangeSpeed (ulong speed) {
            this.Speed = speed;
        }

        /// <inheritdoc />
        public virtual void ChangeLossPercent (double percent) {
            this.LossPercent = percent;
        }

        /// <summary>
        ///   Выполняет загрузку информации из заданного xml-узла.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     Стандартный алгоритм загрузки информации о среде передачи работает следующим образом:
        ///     <list type = "number">
        ///       <item>
        ///         <description>
        ///           Загружаем имя среды передачи - текст, содержащийся внутри тега &lt;name&gt;.
        ///         </description>
        ///       </item>
        ///       <item>
        ///         <description>
        ///           Загружаем максимальное количество конечных точек - число, содержащееся
        ///           внутри тега &lt;capacity&gt;.
        ///         </description>
        ///       </item>
        ///       <item>
        ///         <description>
        ///           Для каждого тега с именем &lt;characteristic&gt; загружаем значение
        ///           свойства-характеристики, имя которого содержится в атрибуте <c>name</c>,
        ///           содержащееся внутри тега.
        ///         </description>
        ///       </item>
        ///     </list>
        ///   </para>
        ///   <para>
        ///     При загрузке характеристики используется <see cref = "XmlSerializer" />, инициализированный
        ///     тем типом данных, который возвращает свойство в данный момент.
        ///     <code>
        ///       PropertyInfo property = ...; // Загружаемое свойство
        ///       var xmlSerializer = new XmlSerializer (property.GetValue (this, null).GetType ());
        ///     </code>
        ///   </para>
        ///   <para>
        ///     Важно отметить, что характеристики среды передачи должны иметь сеттер (private или public)
        ///     для установки значения. Установленное значение должно проверяться - либо на уровне контрактов,
        ///     либо на уровне кода сеттера.
        ///   </para>
        /// </remarks>
        /// <param name = "data">xml-узел, из которого загружать информацию.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "data" /> является <c>null</c>.</exception>
        public virtual void Load (XmlNode data) {
            this.Name = data [XML_NAME_NODE].InnerText;
            this.EndPointsCapacity = int.Parse (data [XML_CAPACITY_NODE].InnerText);
            foreach (var p in from XmlNode child in data.ChildNodes
                              where child.Name == XML_CHARACTERISTIC_NODE
                              select child) {
                var property = this.GetType ().GetProperty (p.Attributes [XML_NAME_ATTRIBUTE].Value);
                var xmlSerializer = new XmlSerializer (property.GetValue (this, null).GetType ());
                var xmlReader = XmlReader.Create (new StringReader (p.InnerXml));
                if (property.GetSetMethod (true) != null)
                    property.SetValue (this, xmlSerializer.Deserialize (xmlReader), null);
            }
        }

        /// <summary>
        ///   Выполняет сохранение информации в заданный xml-узел.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     Стандартный алгоритм сохранения информации о среде передачи
        ///     сохраняет следующую информацию:
        ///     <list type = "bullet">
        ///       <item>
        ///         <description>Название среды передачи.</description>
        ///       </item>
        ///       <item>
        ///         <description>Максимальное количество конечных точек.</description>
        ///       </item>
        ///       <item>
        ///         <description>
        ///           Характеристики среды передачи - свойства, отмеченные атрибутом <see cref = "BackboneCharacteristicAttribute" />.
        ///         </description>
        ///       </item>
        ///     </list>
        ///   </para>
        ///   <para>
        ///     Важно отметить, что при сохранении характеристик среды передачи сохранение
        ///     производится с использованием <see cref = "XmlSerializer" />. Типы данных, описывающие
        ///     характеристики должны обладать возможностью сохранять и загружать информацию о
        ///     себе в виде XML.
        ///   </para>
        ///   <para>
        ///     Сохранённая информация будет иметь следующий формат:
        ///     <code>
        ///       &lt;name&gt;Имя среды передачи&lt;/name&gt;
        ///       &lt;capacity&gt;Максимум конечных точек&lt;/capacity&gt;
        ///       &lt;characteristic name="Название поля 1"&gt;
        ///       Сохранённая информация
        ///       &lt;/characteristic&gt;
        ///       &lt;!-- ... --&gt;
        ///       &lt;characteristic name="Название поля N"&gt;
        ///       Сохранённая информация
        ///       &lt;/characteristic&gt;
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

            var capacityNode = doc.CreateElement (XML_CAPACITY_NODE);
            capacityNode.InnerText = this.EndPointsCapacity.ToString ();
            node.AppendChild (capacityNode);

            foreach (var property in from p in this.GetType ().GetProperties ()
                                     where Attribute.IsDefined (p, typeof (BackboneCharacteristicAttribute))
                                     select p) {
                var charNode = doc.CreateElement (XML_CHARACTERISTIC_NODE);

                var nameAttr = doc.CreateAttribute (XML_NAME_ATTRIBUTE);
                nameAttr.Value = property.Name;
                charNode.Attributes.Append (nameAttr);

                var result = new StringBuilder ();
                var value = property.GetValue (this, null);

                var xmlSerializer = new XmlSerializer (value.GetType ());
                var xmlWriter = XmlWriter.Create (result, new XmlWriterSettings { OmitXmlDeclaration = true });

                xmlSerializer.Serialize (xmlWriter, value);
                charNode.InnerXml = result.ToString ();

                node.AppendChild (charNode);
            }
        }

        #endregion
    }
}
