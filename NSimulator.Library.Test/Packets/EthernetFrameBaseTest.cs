#region

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public abstract class EthernetFrameBaseTest <T>
        where T : EthernetFrameBase {
        protected IArrayView <byte> data;
        protected T frame;

        [TestMethod]
        public void CheckDestination () {
            Assert.AreEqual (this.data.Slice (0, 6), this.frame.Destination.Address);
        }

        [TestMethod]
        public void CheckSource () {
            Assert.AreEqual (this.data.Slice (6, 12), this.frame.Source.Address);
        }

        [TestMethod]
        public void CheckType () {
            Assert.AreEqual (this.data [12], (ushort) this.frame.Type >> 8);
            Assert.AreEqual (this.data [13], (ushort) this.frame.Type & 0xFF);
        }

        [TestMethod]
        public void CheckData () {
            Assert.AreEqual (this.data, this.frame.Data);
        }
        }
}
