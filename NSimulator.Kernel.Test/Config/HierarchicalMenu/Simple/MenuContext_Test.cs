#region

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public sealed class MenuContext_Test {
        public TestContext TestContext;

        private MenuContext context;

        [TestInitialize]
        public void Init () {
            this.context = new MenuContext (string.Empty, string.Empty);
        }

        [TestMethod]
        public void CheckNullNameConstructor () {
            this.context = new MenuContext (null, string.Empty, new MenuItemParameter (string.Empty, string.Empty));
            Assert.IsNull (this.context.Name);
        }

        [TestMethod]
        public void CheckNullDescriptionConstructor () {
            this.context = new MenuContext (string.Empty, null, new MenuItemParameter (string.Empty, string.Empty));
            Assert.IsNull (this.context.Description);
        }

        [TestMethod]
        public void CheckNoParametersConstructor () {
            Assert.AreEqual (0, this.context.ParametersCount);
            Assert.AreEqual (0, this.context.Parameters.Count);
        }

        [TestMethod]
        public void CheckAfterConstruct () {
            const string name = "name";
            const string description = "desc";

            var param1 = new MenuItemParameter ("p1", "dp1");
            var param2 = new MenuItemParameter ("p2", "dp2");

            this.context = new MenuContext (name, description, param1, param2);

            Assert.AreEqual (name, this.context.Name);
            Assert.AreEqual (description, this.context.Description);
            Assert.AreEqual (2, this.context.ParametersCount);
            Assert.AreEqual (param1, this.context.Parameters [0]);
            Assert.AreEqual (param2, this.context.Parameters [1]);

            Assert.IsNull (this.context.Parent);
            Assert.AreEqual (0, this.context.ChildContexts.Count ());
            Assert.AreEqual (0, this.context.Commands.Count ());
        }

        [TestMethod]
        public void CheckSetParameter () {
            var param1 = new MenuItemParameter ("p1", "dp1");
            param1.SetValue (string.Empty);

            var param2 = new MenuItemParameter ("p2", "dp2");
            param2.SetValue (string.Empty);

            const string pv1 = "new-value";

            this.context = new MenuContext (string.Empty, string.Empty, param1, param2);
            this.context [0] = pv1;

            Assert.AreEqual (pv1, this.context.Parameters [0].Value);
            Assert.AreEqual (string.Empty, this.context.Parameters [1].Value);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckSetOutOfRangeParameter_Less () {
            var param1 = new MenuItemParameter ("p1", "dp1");
            param1.SetValue (string.Empty);

            var param2 = new MenuItemParameter ("p2", "dp2");
            param2.SetValue (string.Empty);

            const string pv1 = "new-value";

            this.context = new MenuContext (string.Empty, string.Empty, param1, param2);
            this.context [-1] = pv1;
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckSetOutOfRangeParameter_Greater () {
            var param1 = new MenuItemParameter ("p1", "dp1");
            param1.SetValue (string.Empty);

            var param2 = new MenuItemParameter ("p2", "dp2");
            param2.SetValue (string.Empty);

            const string pv1 = "new-value";

            this.context = new MenuContext (string.Empty, string.Empty, param1, param2);
            this.context [2] = pv1;
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAddNullSubContext () {
            this.context.AddSubContext (null);
        }

        [TestMethod]
        [ExpectedException (typeof (MenuContextNameMustBeUniqueException))]
        public void CheckAddSubContextWithNonuniqueName () {
            const string name = "name";
            const string desc1 = "d1";
            const string desc2 = "d2";

            this.context.AddSubContext (new MenuContext (name, desc1));
            this.context.AddSubContext (new MenuContext (name, desc2));
        }

        [TestMethod]
        [ExpectedException (typeof (MenuContextAlreadyHasParentException))]
        public void CheckAddSubContextToTwoContexts () {
            var ctx1 = new MenuContext (string.Empty, string.Empty);
            var ctx2 = new MenuContext (string.Empty, string.Empty);

            ctx1.AddSubContext (this.context);
            ctx2.AddSubContext (this.context);
        }

        [TestMethod]
        [ExpectedException (typeof (InvalidCastException))]
        public void CheckAddNonCompatibleSubContext () {
            this.context.AddSubContext (new MenuContextMock ());
        }

        [TestMethod]
        public void CheckAddSubContext () {
            const string name1 = "name-1";
            const string name2 = "name-2";

            var ctx1 = new MenuContext (name1, string.Empty);
            var ctx2 = new MenuContext (name2, string.Empty);

            this.context.AddSubContext (ctx1);
            this.context.AddSubContext (ctx2);

            Assert.AreEqual (2, this.context.ChildContexts.Count ());
            Assert.AreEqual (this.context, ctx1.Parent);
            Assert.AreEqual (this.context, ctx2.Parent);
            Assert.IsTrue (this.context.ChildContexts.Contains (ctx1));
            Assert.IsTrue (this.context.ChildContexts.Contains (ctx2));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckRemoveNullSubContext () {
            this.context.RemoveSubContext (null);
        }

        [TestMethod]
        [ExpectedException (typeof (MenuContextNotFoundException))]
        public void CheckRemoveNonexistsSubContext_1 () {
            const string name = "name";

            this.context.AddSubContext (new MenuContext (name, string.Empty));

            this.context.RemoveSubContext ("context");
        }

        [TestMethod]
        [ExpectedException (typeof (MenuContextNotFoundException))]
        public void CheckRemoveNonexistsSubContext_2 () {
            this.context.RemoveSubContext ("context");
        }

        [TestMethod]
        public void CheckRemoveSubContext () {
            const string name1 = "name-1";
            const string name2 = "name-2";

            var ctx1 = new MenuContext (name1, string.Empty);
            var ctx2 = new MenuContext (name2, string.Empty);

            this.context.AddSubContext (ctx1);
            this.context.AddSubContext (ctx2);

            this.context.RemoveSubContext (name2);

            Assert.IsNull (ctx2.Parent);
            Assert.AreEqual (1, this.context.ChildContexts.Count ());
            Assert.AreEqual (ctx1, this.context.ChildContexts.First ());
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckGetNullSubContext () {
            this.context.GetChild (null);
        }

        [TestMethod]
        [ExpectedException (typeof (MenuContextNotFoundException))]
        public void CheckGetNonexistsSubContext () {
            const string name = "name";

            this.context.AddSubContext (new MenuContext (name, string.Empty));

            this.context.GetChild ("context");
        }

        [TestMethod]
        public void CheckGetSubContext () {
            const string name1 = "name-1";
            const string name2 = "name-2";

            var ctx1 = new MenuContext (name1, string.Empty);
            var ctx2 = new MenuContext (name2, string.Empty);

            this.context.AddSubContext (ctx1);
            this.context.AddSubContext (ctx2);

            Assert.AreEqual (ctx1, this.context.GetChild (name1));
            Assert.AreEqual (ctx2, this.context.GetChild (name2));
        }

        [TestMethod]
        public void CheckSetParametersCount_AddParams () {
            var param1 = new MenuItemParameter ("p1", "dp-1");
            var param2 = new MenuItemParameter ("p2", "dp-2");

            this.context = new MenuContext (string.Empty, string.Empty, param1, param2);
            this.context.SetParametersCount (3);

            Assert.AreEqual (3, this.context.ParametersCount);
            Assert.AreEqual (param1, this.context.Parameters [0]);
            Assert.AreEqual (param2, this.context.Parameters [1]);

            Assert.AreEqual (string.Empty, this.context.Parameters [2].Name);
            Assert.AreEqual (string.Empty, this.context.Parameters [2].Description);
            Assert.IsNull (this.context.Parameters [2].Value);
        }

        [TestMethod]
        public void CheckSetParametersCount_RemoveParams () {
            var param1 = new MenuItemParameter ("p1", "dp-1");
            var param2 = new MenuItemParameter ("p2", "dp-2");
            var param3 = new MenuItemParameter ("p3", "dp-3");

            this.context = new MenuContext (string.Empty, string.Empty, param1, param2, param3);
            this.context.SetParametersCount (2);

            Assert.AreEqual (2, this.context.ParametersCount);
            Assert.AreEqual (param1, this.context.Parameters [0]);
            Assert.AreEqual (param2, this.context.Parameters [1]);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAddNullSubCommand () {
            this.context.AddCommand (null);
        }

        [TestMethod]
        [ExpectedException (typeof (MenuCommandNameMustBeUniqueException))]
        public void CheckAddCommandWithNonuniqueName () {
            const string name = "name";
            const string desc1 = "d1";
            const string desc2 = "d2";

            this.context.AddCommand (new MenuCommand (name, desc1));
            this.context.AddCommand (new MenuCommand (name, desc2));
        }

        [TestMethod]
        [ExpectedException (typeof (MenuCommandAlreadyHasContextException))]
        public void CheckAddCommandToTwoContexts () {
            var ctx1 = new MenuContext (string.Empty, string.Empty);
            var ctx2 = new MenuContext (string.Empty, string.Empty);

            var cmd = new MenuCommand (string.Empty, string.Empty);

            ctx1.AddCommand (cmd);
            ctx2.AddCommand (cmd);
        }

        [TestMethod]
        [ExpectedException (typeof (InvalidCastException))]
        public void CheckAddNonCompatibleCommand () {
            this.context.AddCommand (new MenuCommandMock ());
        }

        [TestMethod]
        [ExpectedException (typeof (MenuCommandNameMustBeUniqueException))]
        public void CheckAddCommand_NameEqualContext () {
            const string name = "name";

            this.context.AddSubContext (new MenuContext (name, string.Empty));
            this.context.AddCommand (new MenuCommand (name, string.Empty));
        }

        [TestMethod]
        [ExpectedException (typeof (MenuContextNameMustBeUniqueException))]
        public void CheckAddContext_NameEqualContext () {
            const string name = "name";

            this.context.AddCommand (new MenuCommand (name, string.Empty));
            this.context.AddSubContext (new MenuContext (name, string.Empty));
        }

        [TestMethod]
        public void CheckAddCommand () {
            const string name1 = "name-1";
            const string name2 = "name-2";

            var cmd1 = new MenuCommand (name1, string.Empty);
            var cmd2 = new MenuCommand (name2, string.Empty);

            this.context.AddCommand (cmd1);
            this.context.AddCommand (cmd2);

            Assert.AreEqual (2, this.context.Commands.Count ());
            Assert.AreEqual (this.context, cmd1.Context);
            Assert.AreEqual (this.context, cmd2.Context);
            Assert.IsTrue (this.context.Commands.Contains (cmd1));
            Assert.IsTrue (this.context.Commands.Contains (cmd2));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckRemoveNullCommand () {
            this.context.RemoveCommand (null);
        }

        [TestMethod]
        [ExpectedException (typeof (MenuCommandNotFoundException))]
        public void CheckRemoveNonexistsCommand_1 () {
            const string name = "name";

            this.context.AddCommand (new MenuCommand (name, string.Empty));

            this.context.RemoveCommand ("command");
        }

        [TestMethod]
        [ExpectedException (typeof (MenuCommandNotFoundException))]
        public void CheckRemoveNonexistsCommand_2 () {
            this.context.RemoveCommand ("command");
        }

        [TestMethod]
        public void CheckRemoveCommand () {
            const string name1 = "name-1";
            const string name2 = "name-2";

            var cmd1 = new MenuCommand (name1, string.Empty);
            var cmd2 = new MenuCommand (name2, string.Empty);

            this.context.AddCommand (cmd1);
            this.context.AddCommand (cmd2);

            this.context.RemoveCommand (name2);

            Assert.IsNull (cmd2.Context);
            Assert.AreEqual (1, this.context.Commands.Count ());
            Assert.AreEqual (cmd1, this.context.Commands.First ());
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckGetNullCommand () {
            this.context.GetCommand (null);
        }

        [TestMethod]
        [ExpectedException (typeof (MenuCommandNotFoundException))]
        public void CheckGetNonexistsCommand () {
            const string name = "name";

            this.context.AddCommand (new MenuCommand (name, string.Empty));

            this.context.GetCommand ("context");
        }

        [TestMethod]
        public void CheckGetCommand () {
            const string name1 = "name-1";
            const string name2 = "name-2";

            var cmd1 = new MenuCommand (name1, string.Empty);
            var cmd2 = new MenuCommand (name2, string.Empty);

            this.context.AddCommand (cmd1);
            this.context.AddCommand (cmd2);

            Assert.AreEqual (cmd1, this.context.GetCommand (name1));
            Assert.AreEqual (cmd2, this.context.GetCommand (name2));
        }

        [TestCleanup]
        public void Done () {}
    }
}
