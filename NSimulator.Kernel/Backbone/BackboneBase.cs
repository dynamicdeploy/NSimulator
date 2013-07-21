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
    ///   ����������� ����� �������� ������.
    /// </summary>
    public abstract class BackboneBase : IBackbone {
        /// <summary>
        ///   �������� xml-�������� "���".
        /// </summary>
        protected const string XML_NAME_NODE = "name";

        /// <summary>
        ///   �������� xml-�������� "�������".
        /// </summary>
        protected const string XML_CAPACITY_NODE = "capacity";

        /// <summary>
        ///   �������� xml-�������� "��������������".
        /// </summary>
        protected const string XML_CHARACTERISTIC_NODE = "characteristic";

        /// <summary>
        ///   �������� xml-�������� "���".
        /// </summary>
        protected const string XML_NAME_ATTRIBUTE = "name";

        /// <summary>
        ///   ��������� �������� �����, ����������� � ����� ��������.
        /// </summary>
        protected readonly ISet <IInterfaceView> endpoints;

        /// <summary>
        ///   �������������� ����� �������� ����������� ������ �����������
        ///   ��������� �������� �����.
        /// </summary>
        /// <param name = "capacity">����������� ��������� ����� �������� �����.</param>
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
        ///   ��������� �������� ���������� �� ��������� xml-����.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     ����������� �������� �������� ���������� � ����� �������� �������� ��������� �������:
        ///     <list type = "number">
        ///       <item>
        ///         <description>
        ///           ��������� ��� ����� �������� - �����, ������������ ������ ���� &lt;name&gt;.
        ///         </description>
        ///       </item>
        ///       <item>
        ///         <description>
        ///           ��������� ������������ ���������� �������� ����� - �����, ������������
        ///           ������ ���� &lt;capacity&gt;.
        ///         </description>
        ///       </item>
        ///       <item>
        ///         <description>
        ///           ��� ������� ���� � ������ &lt;characteristic&gt; ��������� ��������
        ///           ��������-��������������, ��� �������� ���������� � �������� <c>name</c>,
        ///           ������������ ������ ����.
        ///         </description>
        ///       </item>
        ///     </list>
        ///   </para>
        ///   <para>
        ///     ��� �������� �������������� ������������ <see cref = "XmlSerializer" />, ������������������
        ///     ��� ����� ������, ������� ���������� �������� � ������ ������.
        ///     <code>
        ///       PropertyInfo property = ...; // ����������� ��������
        ///       var xmlSerializer = new XmlSerializer (property.GetValue (this, null).GetType ());
        ///     </code>
        ///   </para>
        ///   <para>
        ///     ����� ��������, ��� �������������� ����� �������� ������ ����� ������ (private ��� public)
        ///     ��� ��������� ��������. ������������� �������� ������ ����������� - ���� �� ������ ����������,
        ///     ���� �� ������ ���� �������.
        ///   </para>
        /// </remarks>
        /// <param name = "data">xml-����, �� �������� ��������� ����������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "data" /> �������� <c>null</c>.</exception>
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
        ///   ��������� ���������� ���������� � �������� xml-����.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     ����������� �������� ���������� ���������� � ����� ��������
        ///     ��������� ��������� ����������:
        ///     <list type = "bullet">
        ///       <item>
        ///         <description>�������� ����� ��������.</description>
        ///       </item>
        ///       <item>
        ///         <description>������������ ���������� �������� �����.</description>
        ///       </item>
        ///       <item>
        ///         <description>
        ///           �������������� ����� �������� - ��������, ���������� ��������� <see cref = "BackboneCharacteristicAttribute" />.
        ///         </description>
        ///       </item>
        ///     </list>
        ///   </para>
        ///   <para>
        ///     ����� ��������, ��� ��� ���������� ������������� ����� �������� ����������
        ///     ������������ � �������������� <see cref = "XmlSerializer" />. ���� ������, �����������
        ///     �������������� ������ �������� ������������ ��������� � ��������� ���������� �
        ///     ���� � ���� XML.
        ///   </para>
        ///   <para>
        ///     ���������� ���������� ����� ����� ��������� ������:
        ///     <code>
        ///       &lt;name&gt;��� ����� ��������&lt;/name&gt;
        ///       &lt;capacity&gt;�������� �������� �����&lt;/capacity&gt;
        ///       &lt;characteristic name="�������� ���� 1"&gt;
        ///       ���������� ����������
        ///       &lt;/characteristic&gt;
        ///       &lt;!-- ... --&gt;
        ///       &lt;characteristic name="�������� ���� N"&gt;
        ///       ���������� ����������
        ///       &lt;/characteristic&gt;
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
