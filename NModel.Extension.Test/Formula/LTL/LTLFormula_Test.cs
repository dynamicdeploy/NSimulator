#region

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NModel.Extension.Test {
    [TestClass]
    public sealed class LTLFormula_Test {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullConstructor () {
            new LTLFormula (null);
        }

        [TestMethod]
        public void CheckAtomic () {
            const string s = "atom";
            var f = new LTLFormula (s);
            Assert.AreEqual (s, f.Atomic);
            Assert.IsNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (LTLOperator.NONE, f.Operator);
        }

        [TestMethod]
        public void CheckTrueFormula () {
            var f = LTLFormula.TRUE;
            Assert.IsNull (f.Atomic);
            Assert.IsNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (LTLOperator.TRUE, f.Operator);
        }

        [TestMethod]
        public void CheckFalseFormula () {
            var f = LTLFormula.FALSE;
            Assert.IsNull (f.Atomic);
            Assert.IsNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (LTLOperator.FALSE, f.Operator);
        }

        private static void Check (object x) {}

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAndNullLeftArgument1 () {
            Check (null & new LTLFormula ("qwe"));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAndNullLeftArgument2 () {
            Check (LTLFormula.And (null, new LTLFormula ("qwe")));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAndNullRightArgument1 () {
            Check (new LTLFormula ("qwe") & null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAndNullRightArgument2 () {
            Check (LTLFormula.And (new LTLFormula ("qwe"), null));
        }

        [TestMethod]
        public void CheckAndOperator1 () {
            var l = new LTLFormula ("qwe");
            var r = new LTLFormula ("rty");
            var f = l & r;
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.AreEqual (r, f.Right);
            Assert.AreEqual (LTLOperator.LOGIC_AND, f.Operator);
        }

        [TestMethod]
        public void CheckAndOperator2 () {
            var l = new LTLFormula ("qwe");
            var r = new LTLFormula ("rty");
            var f = LTLFormula.And (l, r);
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.AreEqual (r, f.Right);
            Assert.AreEqual (LTLOperator.LOGIC_AND, f.Operator);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckOrNullLeftArgument1 () {
            Check (null | new LTLFormula ("qwe"));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckOrNullLeftArgument2 () {
            Check (LTLFormula.Or (null, new LTLFormula ("qwe")));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckOrNullRightArgument1 () {
            Check (new LTLFormula ("qwe") | null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckOrNullRightArgument2 () {
            Check (LTLFormula.Or (new LTLFormula ("qwe"), null));
        }

        [TestMethod]
        public void CheckOrOperator1 () {
            var l = new LTLFormula ("qwe");
            var r = new LTLFormula ("rty");
            var f = l | r;
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.AreEqual (r, f.Right);
            Assert.AreEqual (LTLOperator.LOGIC_OR, f.Operator);
        }

        [TestMethod]
        public void CheckOrOperator2 () {
            var l = new LTLFormula ("qwe");
            var r = new LTLFormula ("rty");
            var f = LTLFormula.Or (l, r);
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.AreEqual (r, f.Right);
            Assert.AreEqual (LTLOperator.LOGIC_OR, f.Operator);
        }

        [TestMethod]
        public void CheckXOperator () {
            var l = new LTLFormula ("qwe");
            var f = LTLFormula.X (l);

            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (LTLOperator.LTL_X, f.Operator);
        }

        [TestMethod]
        public void CheckFOperator () {
            var l = new LTLFormula ("qwe");
            var f = LTLFormula.F (l);

            Assert.IsNull (f.Atomic);
            Assert.AreEqual (LTLFormula.TRUE, f.Left);
            Assert.AreEqual (l, f.Right);
            Assert.AreEqual (LTLOperator.LTL_U, f.Operator);
        }

        [TestMethod]
        public void CheckGOperator () {
            var l = new LTLFormula ("qwe");
            var f = LTLFormula.G (l);

            Assert.IsNull (f.Atomic);
            Assert.IsNotNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (LTLOperator.LOGIC_NOT, f.Operator);

            var f1 = f.Left;
            Assert.IsNull (f1.Atomic);
            Assert.AreEqual (LTLFormula.TRUE, f1.Left);
            Assert.IsNotNull (f1.Right);
            Assert.AreEqual (LTLOperator.LTL_U, f1.Operator);

            var f2 = f1.Right;
            Assert.IsNull (f2.Atomic);
            Assert.AreEqual (l, f2.Left);
            Assert.IsNull (f2.Right);
            Assert.AreEqual (LTLOperator.LOGIC_NOT, f2.Operator);
        }

        [TestMethod]
        public void CheckUOperator () {
            var l = new LTLFormula ("qwe");
            var r = new LTLFormula ("rty");
            var f = LTLFormula.U (l, r);

            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.AreEqual (r, f.Right);
            Assert.AreEqual (LTLOperator.LTL_U, f.Operator);
        }

        [TestMethod]
        public void CheckROperator () {
            var l = new LTLFormula ("qwe");
            var r = new LTLFormula ("rty");
            var f = LTLFormula.R (l, r);

            Assert.IsNull (f.Atomic);
            Assert.IsNotNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (LTLOperator.LOGIC_NOT, f.Operator);

            var f1 = f.Left;
            Assert.IsNull (f1.Atomic);
            Assert.IsNotNull (f1.Left);
            Assert.IsNotNull (f1.Right);
            Assert.AreEqual (LTLOperator.LTL_U, f1.Operator);

            var f2 = f1.Left;
            Assert.IsNull (f2.Atomic);
            Assert.AreEqual (l, f2.Left);
            Assert.IsNull (f2.Right);
            Assert.AreEqual (LTLOperator.LOGIC_NOT, f2.Operator);

            var f3 = f1.Right;
            Assert.IsNull (f3.Atomic);
            Assert.AreEqual (r, f3.Left);
            Assert.IsNull (f3.Right);
            Assert.AreEqual (LTLOperator.LOGIC_NOT, f3.Operator);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNotNullArgument1 () {
            Check (!(LTLFormula) null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNotNullArgument2 () {
            Check (LTLFormula.Not (null));
        }

        [TestMethod]
        public void CheckNotOperator1 () {
            var l = new LTLFormula ("qwe");
            var f = !l;
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (LTLOperator.LOGIC_NOT, f.Operator);
        }

        [TestMethod]
        public void CheckNotOperator2 () {
            var l = new LTLFormula ("qwe");
            var f = LTLFormula.Not (l);
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (LTLOperator.LOGIC_NOT, f.Operator);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckXNullArgument () {
            Check (LTLFormula.X (null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckFNullArgument () {
            Check (LTLFormula.F (null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckGNullArgument () {
            Check (LTLFormula.G (null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckUNullLeftArgument () {
            Check (LTLFormula.U (null, new LTLFormula ("qwe")));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckUNullRightArgument () {
            Check (LTLFormula.U (new LTLFormula ("qwe"), null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckRNullLeftArgument () {
            Check (LTLFormula.R (null, new LTLFormula ("qwe")));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckRNullRightArgument () {
            Check (LTLFormula.R (new LTLFormula ("qwe"), null));
        }

        [TestMethod]
        public void CheckPrintAtom () {
            const string s = "zxcasd";
            var f = new LTLFormula (s);

            Assert.AreEqual ('{' + s + '}', f.ToString ());
        }

        [TestMethod]
        public void CheckPrintTrue () {
            Assert.AreEqual ("TRUE", LTLFormula.TRUE.ToString ());
        }

        [TestMethod]
        public void CheckPrintFalse () {
            Assert.AreEqual ("FALSE", LTLFormula.FALSE.ToString ());
        }

        [TestMethod]
        public void CheckPrintNot () {
            const string s = "asd";
            var f = !new LTLFormula (s);

            Assert.AreEqual ("not ({" + s + "})", f.ToString ());
        }

        [TestMethod]
        public void CheckPrintAnd () {
            const string s1 = "qwe";
            const string s2 = "asd";
            var f = new LTLFormula (s1) & new LTLFormula (s2);

            Assert.AreEqual ("({" + s1 + "}) and ({" + s2 + "})", f.ToString ());
        }

        [TestMethod]
        public void CheckPrintOr () {
            const string s1 = "qwe";
            const string s2 = "asd";
            var f = new LTLFormula (s1) | new LTLFormula (s2);

            Assert.AreEqual ("({" + s1 + "}) or ({" + s2 + "})", f.ToString ());
        }

        [TestMethod]
        public void CheckPrintX () {
            const string s = "qwe";
            var f = LTLFormula.X (new LTLFormula (s));

            Assert.AreEqual ("X ({" + s + "})", f.ToString ());
        }

        [TestMethod]
        public void CheckPrintU () {
            const string s1 = "qwe";
            const string s2 = "asd";
            var f = LTLFormula.U (new LTLFormula (s1), new LTLFormula (s2));

            Assert.AreEqual ("({" + s1 + "}) U ({" + s2 + "})", f.ToString ());
        }

        [TestMethod]
        public void CheckPrintComplexFormula () {
            const string s1 = "qwe";
            const string s2 = "asd";
            const string s3 = "zxc";

            var f1 = (new LTLFormula (s1) & new LTLFormula (s2)) | new LTLFormula (s3);
            var f2 = new LTLFormula (s1) & (new LTLFormula (s2) | new LTLFormula (s3));

            Assert.AreEqual ("(({" + s1 + "}) and ({" + s2 + "})) or ({" + s3 + "})", f1.ToString ());
            Assert.AreEqual ("({" + s1 + "}) and (({" + s2 + "}) or ({" + s3 + "}))", f2.ToString ());
        }

        [TestMethod]
        public void CheckCompare_General () {
            const string s1 = "qwe";
            const string s2 = "asd";
            var unary = new HashSet <Func <LTLFormula, LTLFormula>> {
                                                                        LTLFormula.Not,
                                                                        LTLFormula.X,
                                                                    };
            var binary = new HashSet <Func <LTLFormula, LTLFormula, LTLFormula>> {
                                                                                     LTLFormula.And,
                                                                                     LTLFormula.Or,
                                                                                     LTLFormula.U
                                                                                 };
            foreach (var f in unary) {
                var f1 = f (new LTLFormula (s1));
                var f2 = f (new LTLFormula (s1));
                Assert.IsTrue (f1.Equals (f2));
            }

            foreach (var f in binary) {
                var f1 = f (new LTLFormula (s1), new LTLFormula (s2));
                var f2 = f (new LTLFormula (s1), new LTLFormula (s2));
                var f3 = f (new LTLFormula (s2), new LTLFormula (s1));

                Assert.IsTrue (f1.Equals (f2));
                Assert.IsFalse (f1.Equals (f3));
            }
        }
    }
}
