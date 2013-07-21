#region

using System.Xml;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    public sealed class BaseHubMock : BaseHub <InterfaceMock> {
        public BaseHubMock (int interfaces)
            : base (interfaces) {}

        public override void Load (XmlNode data) {}

        public override void Store (XmlNode node) {}
    }
}
