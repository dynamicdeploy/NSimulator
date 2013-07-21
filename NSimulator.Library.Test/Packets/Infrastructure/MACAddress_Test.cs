#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class MACAddress_Test {
        private MACAddress address;
        private IArrayView <byte> data;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {
            this.data = new Array <byte> (new byte [] { 11, 65, 23, 198, 0, 37 });
            this.address = new MACAddress (this.data);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullArgument () {
            new MACAddress (null);
        }

        [TestMethod]
        [ExpectedException (typeof (IncorrectFieldLengthException))]
        public void CheckIncorrectLength1 () {
            new MACAddress (new Array <byte> (new byte[10]));
        }

        [TestMethod]
        [ExpectedException (typeof (IncorrectFieldLengthException))]
        public void CheckIncorrectLength2 () {
            new MACAddress (new Array <byte> (new byte[2]));
        }

        [TestMethod]
        public void CheckAddress () {
            Assert.AreEqual (this.data, this.address.Address);
        }

        [TestMethod]
        public void CheckToString () {
            Assert.AreEqual (string.Join ("-", Array <byte>.ConvertAll (this.data, _ => _.ToString ("X2"))),
                             this.address.ToString ());
        }

        [TestMethod]
        public void CheckParse1 () {
            Assert.AreEqual (this.address.Address, MACAddress.Parse (this.address.ToString ().ToLower ()).Address);
        }

        [TestMethod]
        public void CheckParse2 () {
            Assert.AreEqual (this.address.Address, MACAddress.Parse (this.address.ToString ().ToUpper ()).Address);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckParseNull () {
            MACAddress.Parse (null);
        }

        [TestMethod]
        [ExpectedException (typeof (IncorrectMACAddressException))]
        public void CheckParseIncorrect1 () {
            MACAddress.Parse ("incorrect string");
        }

        [TestMethod]
        [ExpectedException (typeof (IncorrectMACAddressException))]
        public void CheckParseIncorrect2 () {
            MACAddress.Parse ("11-22-33-44");
        }

        [TestMethod]
        public void CheckEquals1 () {
            Assert.AreEqual (this.address, this.address);
        }

        [TestMethod]
        public void CheckEquals2 () {
            Assert.AreEqual (this.address, MACAddress.Parse (this.address.ToString ()));
        }

        [TestMethod]
        public void CheckNotEquals1 () {
            Assert.AreNotEqual (null, this.address);
        }

        [TestMethod]
        public void CheckNotEquals2 () {
            Assert.AreNotEqual (this.address, MACAddress.Parse ("11-22-33-44-55-66"));
        }

        [TestCleanup]
        public void Done () {}
    }
}
