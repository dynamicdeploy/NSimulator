#region

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public sealed class MenuDriver_Test {
        public TestContext TestContext;

        private MenuDriver driver;

        [TestInitialize]
        public void Init () {
            this.driver = new MenuDriver ();

            var root_context = new MenuContext ("", "");
            root_context.AddCommand (new MenuCommand ("a", ""));
            root_context.AddCommand (new MenuCommand ("aaa", ""));
            root_context.AddCommand (new MenuCommand ("aab", ""));
            root_context.AddCommand (new MenuCommand ("ccc", ""));

            var context1 = new MenuContext ("context1",
                                            "",
                                            new MenuItemParameter ("param1", ""),
                                            new MenuItemParameter ("param2", ""));
            context1.AddCommand (new MenuCommand ("", ""));
            context1.AddCommand (new MenuCommand ("cmd", ""));

            var context2 = new MenuContext ("context2", "");
            context2.AddCommand (new MenuCommand ("cmd", ""));

            var context = new MenuContext ("context",
                                           "",
                                           new MenuItemParameter ("param1", ""),
                                           new MenuItemParameter ("param2", ""),
                                           new MenuItemParameter ("param3", ""));
            context.AddCommand (new MenuCommand ("cmd", ""));

            var context10 = new MenuContext ("context10", "", new MenuItemParameter ("param1", ""));
            context10.AddCommand (new MenuCommand ("cmd", ""));

            var sub_a = new MenuContext ("sub_A", "");
            sub_a.AddCommand (new MenuCommand ("command", ""));
            sub_a.AddCommand (new MenuCommand ("command1", ""));
            sub_a.AddCommand (new MenuCommand ("command2", ""));
            sub_a.AddCommand (new MenuCommand ("command12", ""));

            var sub_b = new MenuContext ("Sub_B", "");
            sub_b.AddCommand (new MenuCommand ("aaa", ""));
            sub_b.AddCommand (new MenuCommand ("bbb", ""));
            sub_b.AddCommand (new MenuCommand ("ccc", ""));

            var ctx = new MenuContext ("ctx", "", new MenuItemParameter ("param1", ""));
            ctx.AddCommand (new MenuCommand ("cmd", ""));

            var myctx = new MenuContext ("myctx",
                                         "",
                                         new MenuItemParameter ("param1", ""),
                                         new MenuItemParameter ("param2", ""),
                                         new MenuItemParameter ("param3", ""),
                                         new MenuItemParameter ("param4", ""));
            myctx.AddCommand (new MenuCommand ("cmd", ""));

            var context2__sub = new MenuContext ("sub", "", new MenuItemParameter ("param1", ""));

            var context__sub = new MenuContext ("sub", "", new MenuItemParameter ("param1", ""));
            context__sub.AddCommand (new MenuCommand ("cmd", ""));

            var sub_b__a = new MenuContext ("a", "");

            sub_b.AddSubContext (sub_b__a);
            context.AddSubContext (context__sub);
            context2.AddSubContext (context2__sub);
            context1.AddSubContext (sub_a);
            context1.AddSubContext (sub_b);
            context1.AddSubContext (ctx);
            context1.AddSubContext (myctx);
            root_context.AddSubContext (context);
            root_context.AddSubContext (context1);
            root_context.AddSubContext (context2);
            root_context.AddSubContext (context10);

            this.driver.SetRootContext (root_context);
        }

        [TestMethod]
        public void CheckAfterConstruct () {
            this.driver = new MenuDriver ();
            Assert.AreEqual ("#", this.driver.Prompt);
            Assert.IsNotNull (this.driver.Current);
            Assert.AreEqual (0, this.driver.Current.ChildContexts.Count ());
            Assert.AreEqual (0, this.driver.Current.Commands.Count ());
        }

        [TestMethod]
        public void CheckSetRootContext () {
            this.driver = new MenuDriver ();

            var root = new MenuContext ("root", "");
            root.AddCommand (new MenuCommand ("cmd-1", ""));
            root.AddCommand (new MenuCommand ("cmd-2", ""));

            var sub = new MenuContext ("sub", "");
            sub.AddCommand (new MenuCommand ("cmd", ""));

            root.AddSubContext (sub);
            this.driver.SetRootContext (root);

            Assert.AreEqual (root, this.driver.Current);
        }

        [TestMethod]
        public void CheckSetRootContext_NotRoot () {
            this.driver = new MenuDriver ();

            var root = new MenuContext ("root", "");
            root.AddCommand (new MenuCommand ("cmd-1", ""));
            root.AddCommand (new MenuCommand ("cmd-2", ""));

            var sub = new MenuContext ("sub", "");
            sub.AddCommand (new MenuCommand ("cmd", ""));

            root.AddSubContext (sub);
            this.driver.SetRootContext (sub);

            Assert.AreEqual (sub, this.driver.Current);
        }

        [TestMethod]
        public void CheckSetRootContext_NotRoot_ToParent () {
            this.driver = new MenuDriver ();

            var root = new MenuContext ("root", "");
            root.AddCommand (new MenuCommand ("cmd-1", ""));
            root.AddCommand (new MenuCommand ("cmd-2", ""));

            var sub = new MenuContext ("sub", "");
            sub.AddCommand (new MenuCommand ("cmd", ""));

            root.AddSubContext (sub);
            this.driver.SetRootContext (sub);
            this.driver.ToParent ();

            Assert.AreEqual (sub, this.driver.Current);
        }

        [TestMethod]
        public void CheckToParent () {
            var root = this.driver.Current;
            this.driver.Invoke ("context1 a b");
            Assert.AreNotEqual (root, this.driver.Current);

            this.driver.ToParent ();
            Assert.AreEqual (root, this.driver.Current);
        }

        [TestMethod]
        public void CheckSetRootContext_NotRoot_ToRoot () {
            this.driver = new MenuDriver ();

            var root = new MenuContext ("root", "");
            root.AddCommand (new MenuCommand ("cmd-1", ""));
            root.AddCommand (new MenuCommand ("cmd-2", ""));

            var sub = new MenuContext ("sub", "");
            sub.AddCommand (new MenuCommand ("cmd", ""));

            root.AddSubContext (sub);
            this.driver.SetRootContext (sub);
            this.driver.ToRoot ();

            Assert.AreEqual (sub, this.driver.Current);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckInvokeNullCommand () {
            this.driver.Invoke (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAutoCompletionNullCommand () {
            this.driver.AutoCompletion (null);
        }

        [TestMethod]
        [ExpectedException (typeof (MenuElementNameIsAmbiguousException))]
        public void CheckInvokeAmbigiousCommand () {
            this.driver.Invoke ("aa 1 2");
        }

        [TestMethod]
        [ExpectedException (typeof (MenuElementNameIsAmbiguousException))]
        public void CheckInvokeCommand_AmbigiousContext () {
            this.driver.Invoke ("cont cmd");
        }

        [TestMethod]
        [ExpectedException (typeof (MenuElementNameIsAmbiguousException))]
        public void CheckAutoCompletion_AmbigiousContext () {
            this.driver.AutoCompletion ("cont c");
        }

        [TestMethod]
        [ExpectedException (typeof (MenuCommandNotFoundException))]
        public void CheckInvokeNonexistsCommand () {
            this.driver.Invoke ("nonexists 1 2");
        }

        [TestMethod]
        [ExpectedException (typeof (MenuCommandNotFoundException))]
        public void CheckInvokeNonexistsCommand_Context () {
            this.driver.Invoke ("context1 1 2 nonexists 11 22 33");
        }

        [TestMethod]
        [ExpectedException (typeof (MenuCommandNotFoundException))]
        public void CheckAutoCompletionNonexistsCommand () {
            this.driver.AutoCompletion ("nonexists p q");
        }

        [TestMethod]
        [ExpectedException (typeof (MenuCommandNotFoundException))]
        public void CheckAutoCompletionNonexistsCommand_Context () {
            this.driver.AutoCompletion ("context1 x y nonexists p p q");
        }

        [TestMethod]
        [ExpectedException (typeof (NotEnoughParametersException))]
        public void CheckInvokeCommand_NotEnoughParameters () {
            this.driver.Invoke ("context1 cmd");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSetRootNullContext () {
            this.driver.SetRootContext (null);
        }

        [TestMethod]
        [ExpectedException (typeof (InvalidCastException))]
        public void CheckSetRootIncompatibleContext () {
            this.driver.SetRootContext (new MenuContextMock ());
        }

        [TestMethod]
        public void CheckInvoke_ChangeContext () {
            var root = this.driver.Current;
            var tokens = new [] { "context1", "a", "b" };

            this.driver.Invoke (string.Join (" ", tokens));
            Assert.AreEqual (root, this.driver.Current.Parent);
            Assert.AreEqual (tokens [0], this.driver.Current.Name);
            Assert.AreEqual (tokens [1], this.driver.Current.Parameters [0].Value);
            Assert.AreEqual (tokens [2], this.driver.Current.Parameters [1].Value);
        }

        [TestMethod]
        public void CheckInvoke_ChangeContext2 () {
            var root = this.driver.Current;
            var tokens = new [] { "context1", "a", "b", "ctx", "cc" };

            this.driver.Invoke (string.Join (" ", tokens));
            Assert.AreEqual (root, this.driver.Current.Parent.Parent);
            Assert.AreEqual (tokens [0], this.driver.Current.Parent.Name);
            Assert.AreEqual (tokens [1], this.driver.Current.Parent.Parameters [0].Value);
            Assert.AreEqual (tokens [2], this.driver.Current.Parent.Parameters [1].Value);
            Assert.AreEqual (tokens [3], this.driver.Current.Name);
            Assert.AreEqual (tokens [4], this.driver.Current.Parameters [0].Value);
        }

        [TestMethod]
        public void CheckInvoke_ChangeContext2_Completion () {
            var root = this.driver.Current;
            var tokens = new [] { "context1", "a", "b", "ct", "c" };

            this.driver.Invoke (string.Join (" ", tokens));
            Assert.AreEqual (root, this.driver.Current.Parent.Parent);
            Assert.AreEqual (tokens [0], this.driver.Current.Parent.Name);
            Assert.AreEqual (tokens [1], this.driver.Current.Parent.Parameters [0].Value);
            Assert.AreEqual (tokens [2], this.driver.Current.Parent.Parameters [1].Value);
            Assert.AreEqual ("ctx", this.driver.Current.Name);
            Assert.AreEqual (tokens [4], this.driver.Current.Parameters [0].Value);
        }

        [TestMethod]
        public void CheckInvoke_ChangeContext_Parent () {
            var root = this.driver.Current;
            var tokens = new [] { "context1", "a", "b", ".." };

            this.driver.Invoke (string.Join (" ", tokens));
            Assert.AreEqual (root, this.driver.Current);
            Assert.AreEqual (tokens [1], this.driver.Current.GetChild (tokens [0]).Parameters [0].Value);
            Assert.AreEqual (tokens [2], this.driver.Current.GetChild (tokens [0]).Parameters [1].Value);
        }

        [TestMethod]
        public void CheckInvoke_ChangeContext_Root () {
            var root = this.driver.Current;
            var tokens = new [] { "context1", "a", "b", "Sub_B", "a", ".root" };

            this.driver.Invoke (string.Join (" ", tokens));
            Assert.AreEqual (root, this.driver.Current);
            Assert.AreEqual (tokens [1], this.driver.Current.GetChild (tokens [0]).Parameters [0].Value);
            Assert.AreEqual (tokens [2], this.driver.Current.GetChild (tokens [0]).Parameters [1].Value);
        }

        [TestMethod]
        public void CheckInvoke_InvokeCommand () {
            var root = this.driver.Current;
            var invoked = false;
            var tokens = new [] { "a", "param1", "param2", "p3" };

            this.driver.Current.GetCommand (tokens [0]).OnInvoke += cmd => {
                                                                        invoked = true;
                                                                        Assert.AreEqual (tokens [0], cmd.Name);
                                                                        Assert.AreEqual (root, cmd.Context);
                                                                        Assert.AreEqual (3, cmd.Parameters.Count);
                                                                        Assert.AreEqual (tokens [1],
                                                                                         cmd.Parameters [0].Value);
                                                                        Assert.AreEqual (tokens [2],
                                                                                         cmd.Parameters [1].Value);
                                                                        Assert.AreEqual (tokens [3],
                                                                                         cmd.Parameters [2].Value);
                                                                    };

            this.driver.Invoke (string.Join (" ", tokens));
            Assert.IsTrue (invoked);
            Assert.AreEqual (root, this.driver.Current);
        }

        [TestMethod]
        public void CheckInvoke_InvokeComand_Completion () {
            var root = this.driver.Current;
            var invoked = false;
            var tokens = new [] { "cc", "param1", "param2", "p3" };

            this.driver.Current.GetCommand ("ccc").OnInvoke += cmd => {
                                                                   invoked = true;
                                                                   Assert.AreEqual ("ccc", cmd.Name);
                                                                   Assert.AreEqual (root, cmd.Context);
                                                                   Assert.AreEqual (3, cmd.Parameters.Count);
                                                                   Assert.AreEqual (tokens [1], cmd.Parameters [0].Value);
                                                                   Assert.AreEqual (tokens [2], cmd.Parameters [1].Value);
                                                                   Assert.AreEqual (tokens [3], cmd.Parameters [2].Value);
                                                               };

            this.driver.Invoke (string.Join (" ", tokens));
            Assert.IsTrue (invoked);
            Assert.AreEqual (root, this.driver.Current);
        }

        [TestMethod]
        public void CheckInvoke_ChangeContextInvokeCommand () {
            var root = this.driver.Current;
            var invoked = false;
            var tokens = new [] { "context", "a", "b", "c", "cmd", "param1", "p2", "p3" };

            this.driver.Current.GetChild (tokens [0]).GetCommand (tokens [4]).OnInvoke += cmd => {
                                                                                              invoked = true;
                                                                                              Assert.AreEqual (
                                                                                                  tokens [4], cmd.Name);
                                                                                              Assert.AreEqual (root,
                                                                                                               cmd.
                                                                                                                   Context
                                                                                                                   .
                                                                                                                   Parent);
                                                                                              Assert.AreEqual (
                                                                                                  tokens [1],
                                                                                                  cmd.Context.Parameters
                                                                                                      [0].Value);
                                                                                              Assert.AreEqual (
                                                                                                  tokens [2],
                                                                                                  cmd.Context.Parameters
                                                                                                      [1].Value);
                                                                                              Assert.AreEqual (
                                                                                                  tokens [3],
                                                                                                  cmd.Context.Parameters
                                                                                                      [2].Value);
                                                                                              Assert.AreEqual (3,
                                                                                                               cmd.
                                                                                                                   Parameters
                                                                                                                   .
                                                                                                                   Count);
                                                                                              Assert.AreEqual (
                                                                                                  tokens [5],
                                                                                                  cmd.Parameters [0].
                                                                                                      Value);
                                                                                              Assert.AreEqual (
                                                                                                  tokens [6],
                                                                                                  cmd.Parameters [1].
                                                                                                      Value);
                                                                                              Assert.AreEqual (
                                                                                                  tokens [7],
                                                                                                  cmd.Parameters [2].
                                                                                                      Value);
                                                                                          };

            this.driver.Invoke (string.Join (" ", tokens));
            Assert.IsTrue (invoked);
            Assert.AreEqual (tokens [0], this.driver.Current.Name);
            Assert.AreEqual (root, this.driver.Current.Parent);
        }

        [TestMethod]
        public void CheckAutoCompletion_Empty () {
            var current_context = this.driver.Current;

            var completion = this.driver.AutoCompletion (string.Empty);
            Assert.AreEqual (current_context, this.driver.Current);

            Assert.AreEqual (this.driver.Current.ChildContexts.Count () + this.driver.Current.Commands.Count (),
                             completion.Count ());

            foreach (var childContext in this.driver.Current.ChildContexts)
                AssertHelper.EnumerableContains (completion, childContext.Name);

            foreach (var command in this.driver.Current.Commands)
                AssertHelper.EnumerableContains (completion, command.Name);
        }

        [TestMethod]
        public void CheckAutoCompletion () {
            var current_context = this.driver.Current;
            var tokens = new [] { "c" };

            var completion = this.driver.AutoCompletion (string.Join (" ", tokens));
            Assert.AreEqual (current_context, this.driver.Current);
            Assert.AreEqual (5, completion.Count ());
            AssertHelper.EnumerableContains (completion, "context");
            AssertHelper.EnumerableContains (completion, "context1");
            AssertHelper.EnumerableContains (completion, "context2");
            AssertHelper.EnumerableContains (completion, "context10");
            AssertHelper.EnumerableContains (completion, "ccc");
        }

        [TestMethod]
        public void CheckPrompt_NoParameters () {
            var tokens = new [] { "context1", "p", "q", "sub_A" };
            this.driver.Invoke (string.Join (" ", tokens));

            Assert.AreEqual (string.Format ("{0}#", tokens [3]), this.driver.Prompt);
        }

        [TestMethod]
        public void CheckPrompt_OneParameter () {
            var tokens = new [] { "context1", "p", "q", "ctx", "c" };
            this.driver.Invoke (string.Join (" ", tokens));

            Assert.AreEqual (string.Format ("{0}({1})#", tokens [3], tokens [4]), this.driver.Prompt);
        }

        [TestMethod]
        public void CheckPrompt_LessThanOneParameter () {
            var tokens = new [] { "context1", "p", "q", "myctx", "param1", "p2", "param-3", "p_4" };
            this.driver.Invoke (string.Join (" ", tokens));

            Assert.AreEqual (
                string.Format ("{0}({1})#",
                               tokens [3],
                               string.Join (",", tokens [4], tokens [5], tokens [6], tokens [7])),
                this.driver.Prompt);
        }

        [TestCleanup]
        public void Done () {}
    }
}
