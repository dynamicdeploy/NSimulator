#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public sealed class IPAddress_Test {
        private IPAddress address;
        private IArrayView <byte> data;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {
            this.data = new Array <byte> (new byte [] { 124, 224, 0, 75 });
            this.address = new IPAddress (this.data);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullArgument () {
            new IPAddress (null);
        }

        [TestMethod]
        [ExpectedException (typeof (IncorrectFieldLengthException))]
        public void CheckIncorrectLength1 () {
            new IPAddress (new Array <byte> (new byte[5]));
        }

        [TestMethod]
        [ExpectedException (typeof (IncorrectFieldLengthException))]
        public void CheckIncorrectLength2 () {
            new IPAddress (new Array <byte> (new byte[2]));
        }

        [TestMethod]
        public void CheckAddress () {
            Assert.AreEqual (this.data, this.address.Address);
        }

        [TestMethod]
        public void CheckToString () {
            Assert.AreEqual (string.Join (".", this.data), this.address.ToString ());
        }

        [TestMethod]
        public void CheckParse () {
            Assert.AreEqual (this.address.Address, IPAddress.Parse (this.address.ToString ()).Address);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckParseNull () {
            IPAddress.Parse (null);
        }

        [TestMethod]
        [ExpectedException (typeof (IncorrectIPAddressException))]
        public void CheckParseIncorrectString1 () {
            IPAddress.Parse ("incorrect string");
        }

        [TestMethod]
        [ExpectedException (typeof (IncorrectIPAddressException))]
        public void CheckParseIncorrectString2 () {
            IPAddress.Parse ("1.1.1");
        }

        [TestMethod]
        public void CheckEquals1 () {
            Assert.AreEqual (this.address, this.address);
        }

        [TestMethod]
        public void CheckEquals2 () {
            Assert.AreEqual (this.address, IPAddress.Parse (this.address.ToString ()));
        }

        [TestMethod]
        public void CheckNotEquals1 () {
            Assert.AreNotEqual (null, this.address);
        }

        [TestMethod]
        public void CheckNotEquals2 () {
            Assert.AreNotEqual (this.address, IPAddress.Parse ("0.0.0.0"));
        }

        [TestCleanup]
        public void Done () {}
    }
}
