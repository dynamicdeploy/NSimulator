#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public sealed class MenuCommand_Test {
        public TestContext TestContext;

        private MenuCommand command;

        [TestInitialize]
        public void Init () {
            this.command = new MenuCommand (string.Empty, string.Empty);
        }

        [TestMethod]
        public void CheckNullNameConstructor () {
            this.command = new MenuCommand (null, string.Empty);
            Assert.IsNull (this.command.Name);
        }

        [TestMethod]
        public void CheckNullDescriptionConstructor () {
            this.command = new MenuCommand (string.Empty, null);
            Assert.IsNull (this.command.Description);
        }

        [TestMethod]
        public void CheckNullContextConstructor () {
            this.command = new MenuCommand (string.Empty, string.Empty, null);
            Assert.IsNull (this.command.Context);
        }

        [TestMethod]
        public void CheckAfterConstruct () {
            const string name = "name";
            const string description = "desc";

            this.command = new MenuCommand (name, description);
            Assert.AreEqual (name, this.command.Name);
            Assert.AreEqual (description, this.command.Description);
            Assert.AreEqual (0, this.command.Parameters.Count);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckInvokeWithNull () {
            this.command.Invoke (null);
        }

        [TestMethod]
        public void CheckOnceInvoke () {
            const string param1 = "p1";
            const string param2 = "p2";
            const string param3 = "p3";

            this.command.Invoke (param1, param2, param3);
            Assert.AreEqual (3, this.command.Parameters.Count);
            Assert.AreEqual (param1, this.command.Parameters [0].Value);
            Assert.AreEqual (param2, this.command.Parameters [1].Value);
            Assert.AreEqual (param3, this.command.Parameters [2].Value);
        }

        [TestMethod]
        public void CheckOnceInvoke2 () {
            const string param1 = "p1";
            const string param2 = "p2";
            const string param3 = "p3";

            this.command = new MenuCommand (string.Empty,
                                            string.Empty,
                                            new MenuItemParameter (string.Empty, string.Empty));
            this.command.Invoke (param1, param2, param3);
            Assert.AreEqual (3, this.command.Parameters.Count);
            Assert.AreEqual (param1, this.command.Parameters [0].Value);
            Assert.AreEqual (param2, this.command.Parameters [1].Value);
            Assert.AreEqual (param3, this.command.Parameters [2].Value);
        }

        [TestMethod]
        public void CheckInvokeIncrease () {
            const string param1 = "p1";
            const string param2 = "p2";
            const string param3 = "p3";
            const string param4 = "p4";

            this.command.Invoke (param1, param2);
            this.command.Invoke (param2, param3, param4);
            Assert.AreEqual (3, this.command.Parameters.Count);
            Assert.AreEqual (param2, this.command.Parameters [0].Value);
            Assert.AreEqual (param3, this.command.Parameters [1].Value);
            Assert.AreEqual (param4, this.command.Parameters [2].Value);
        }

        [TestMethod]
        public void CheckInvokeDecrease () {
            const string param1 = "p1";
            const string param2 = "p2";
            const string param3 = "p3";

            this.command.Invoke (param1, param2, param3);
            this.command.Invoke (param1, param3);
            Assert.AreEqual (2, this.command.Parameters.Count);
            Assert.AreEqual (param1, this.command.Parameters [0].Value);
            Assert.AreEqual (param3, this.command.Parameters [1].Value);
        }

        [TestMethod]
        public void CheckInvokeEventInvoking () {
            var invoked = false;

            this.command.OnInvoke += o => {
                                         Assert.AreEqual (this.command, o);
                                         invoked = true;
                                     };

            this.command.Invoke (string.Empty, string.Empty);
            Assert.IsTrue (invoked);
        }

        [TestCleanup]
        public void Done () {}
    }
}
