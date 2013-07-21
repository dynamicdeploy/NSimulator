#region

using System;
using System.Xml;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Абстрактный Ethernet-интерфейс.
    /// </summary>
    public abstract class EthernetInterfaceBase : InterfaceBase {
        /// <summary>
        ///   Название xml-элемента "MAC-адрес".
        /// </summary>
        protected const string XML_MACADDRESS_NODE = "MAC";

        private MACAddress _mac;

        /// <summary>
        ///   Инициализирует интерфейс MAC-адресом.
        /// </summary>
        /// <param name = "macAddress">MAC-адрес интерфейса.</param>
        protected EthernetInterfaceBase (MACAddress macAddress) {
            this._mac = macAddress;
        }

        /// <summary>
        ///   Получает MAC-адрес интерфейса.
        /// </summary>
        /// <value>
        ///   MAC-адрес интерфейса.
        /// </value>
        public MACAddress MAC {
            get { return this._mac; }
            set {
                if (value == null)
                    throw new ArgumentNullException ("value");

                this._mac = value;
            }
        }

        /// <inheritdoc />
        public override void Load (XmlNode data) {
            base.Load (data);

            this.MAC = MACAddress.Parse (data [XML_MACADDRESS_NODE].InnerText);
        }

        /// <inheritdoc />
        public override void Store (XmlNode node) {
            var doc = node.OwnerDocument;

            base.Store (node);

            var macNode = doc.CreateElement (XML_MACADDRESS_NODE);
            macNode.InnerText = this.MAC.ToString ();
            node.AppendChild (macNode);
        }
    }
}
