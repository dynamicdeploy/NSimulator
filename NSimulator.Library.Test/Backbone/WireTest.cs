#region

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public abstract class WireTest <T> : BackboneBaseTest <T>
        where T : Wire {}
}
