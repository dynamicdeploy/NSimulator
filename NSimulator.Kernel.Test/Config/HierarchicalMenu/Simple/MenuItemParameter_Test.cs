#region

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public sealed class MenuItemParameter_Test {
        public TestContext TestContext;

        private MenuItemParameter parameter;

        [TestInitialize]
        public void Init () {
            this.parameter = new MenuItemParameter (string.Empty, string.Empty);
        }

        [TestMethod]
        public void CheckNullNameConstructor () {
            this.parameter = new MenuItemParameter (null, string.Empty);
            Assert.IsNull (this.parameter.Name);
        }

        [TestMethod]
        public void CheckNullDescriptionConstructor () {
            this.parameter = new MenuItemParameter (string.Empty, null);
            Assert.IsNull (this.parameter.Description);
        }

        [TestMethod]
        public void CheckValueAfterConstruct () {
            Assert.IsNull (this.parameter.Value);
        }

        public void CheckCorrectnessConstruction () {
            const string name = "name";
            const string description = "desc";

            this.parameter = new MenuItemParameter (name, description);
            Assert.AreEqual (name, this.parameter.Name);
            Assert.AreEqual (description, this.parameter.Description);
            Assert.IsNull (this.parameter.Value);
        }

        [TestMethod]
        public void CheckSetNullValue () {
            this.parameter.SetValue (null);
            Assert.IsNull (this.parameter.Value);
        }

        [TestMethod]
        public void CheckSetNotNullValue () {
            const string value = "val";

            this.parameter.SetValue (value);
            Assert.AreEqual (value, this.parameter.Value);
        }

        [TestCleanup]
        public void Done () {}
    }
}
