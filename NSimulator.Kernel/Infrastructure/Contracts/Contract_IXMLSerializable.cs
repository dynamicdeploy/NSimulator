#region

using System;
using System.Diagnostics.Contracts;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (IXMLSerializable))]
    internal abstract class Contract_IXMLSerializable : IXMLSerializable {
        #region IXMLSerializable Members

        public void Load (XmlNode data) {
            Contract.Requires <ArgumentNullException> (data != null);
        }

        public void Store (XmlNode node) {
            Contract.Requires <ArgumentNullException> (node != null);
        }

        #endregion
    }
}
