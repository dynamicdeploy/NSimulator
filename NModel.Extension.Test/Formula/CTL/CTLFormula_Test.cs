#region

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NModel.Extension.Test {
    [TestClass]
    public sealed class CTLFormula_Test {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullConstructor () {
            new CTLFormula (null);
        }

        [TestMethod]
        public void CheckAtomic () {
            const string s = "atom";
            var f = new CTLFormula (s);
            Assert.AreEqual (s, f.Atomic);
            Assert.IsNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.NONE, f.Operator);
        }

        [TestMethod]
        public void CheckTrueFormula () {
            var f = CTLFormula.TRUE;
            Assert.IsNull (f.Atomic);
            Assert.IsNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.TRUE, f.Operator);
        }

        [TestMethod]
        public void CheckFalseFormula () {
            var f = CTLFormula.FALSE;
            Assert.IsNull (f.Atomic);
            Assert.IsNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.FALSE, f.Operator);
        }

        private static void Check (object x) {}

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAndNullLeftArgument1 () {
            Check (null & new CTLFormula ("qwe"));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAndNullLeftArgument2 () {
            Check (CTLFormula.And (null, new CTLFormula ("qwe")));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAndNullRightArgument1 () {
            Check (new CTLFormula ("qwe") & null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAndNullRightArgument2 () {
            Check (CTLFormula.And (new CTLFormula ("qwe"), null));
        }

        [TestMethod]
        public void CheckAndOperator1 () {
            var l = new CTLFormula ("qwe");
            var r = new CTLFormula ("rty");
            var f = l & r;
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.AreEqual (r, f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_AND, f.Operator);
        }

        [TestMethod]
        public void CheckAndOperator2 () {
            var l = new CTLFormula ("qwe");
            var r = new CTLFormula ("rty");
            var f = CTLFormula.And (l, r);
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.AreEqual (r, f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_AND, f.Operator);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckOrNullLeftArgument1 () {
            Check (null | new CTLFormula ("qwe"));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckOrNullLeftArgument2 () {
            Check (CTLFormula.Or (null, new CTLFormula ("qwe")));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckOrNullRightArgument1 () {
            Check (new CTLFormula ("qwe") | null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckOrNullRightArgument2 () {
            Check (CTLFormula.Or (new CTLFormula ("qwe"), null));
        }

        [TestMethod]
        public void CheckOrOperator1 () {
            var l = new CTLFormula ("qwe");
            var r = new CTLFormula ("rty");
            var f = l | r;
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.AreEqual (r, f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_OR, f.Operator);
        }

        [TestMethod]
        public void CheckOrOperator2 () {
            var l = new CTLFormula ("qwe");
            var r = new CTLFormula ("rty");
            var f = CTLFormula.Or (l, r);
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.AreEqual (r, f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_OR, f.Operator);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNotNullArgument1 () {
            Check (! (CTLFormula) null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNotNullArgument2 () {
            Check (CTLFormula.Not (null));
        }

        [TestMethod]
        public void CheckNotOperator1 () {
            var l = new CTLFormula ("qwe");
            var f = !l;
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f.Operator);
        }

        [TestMethod]
        public void CheckNotOperator2 () {
            var l = new CTLFormula ("qwe");
            var f = CTLFormula.Not (l);
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f.Operator);
        }

        [TestMethod]
        public void CheckAXOperator () {
            var l = new CTLFormula ("qwe");
            var f = CTLFormula.AX (l);

            Assert.IsNull (f.Atomic);
            Assert.IsNotNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f.Operator);

            var f1 = f.Left;
            Assert.IsNull (f1.Atomic);
            Assert.IsNotNull (f1.Left);
            Assert.IsNull (f1.Right);
            Assert.AreEqual (CTLOperator.CTL_EX, f1.Operator);

            var f2 = f1.Left;
            Assert.IsNull (f2.Atomic);
            Assert.AreEqual (l, f2.Left);
            Assert.IsNull (f2.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f2.Operator);
        }

        [TestMethod]
        public void CheckEXOperator () {
            var l = new CTLFormula ("qwe");
            var f = CTLFormula.EX (l);
            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.CTL_EX, f.Operator);
        }

        [TestMethod]
        public void CheckAFOperator () {
            var l = new CTLFormula ("qwe");
            var f = CTLFormula.AF (l);

            Assert.IsNull (f.Atomic);
            Assert.IsNotNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f.Operator);

            var f1 = f.Left;
            Assert.IsNull (f1.Atomic);
            Assert.IsNotNull (f1.Left);
            Assert.IsNull (f1.Right);
            Assert.AreEqual (CTLOperator.CTL_EG, f1.Operator);

            var f2 = f1.Left;
            Assert.IsNull (f2.Atomic);
            Assert.AreEqual (l, f2.Left);
            Assert.IsNull (f2.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f2.Operator);
        }

        [TestMethod]
        public void CheckEFOperator () {
            var l = new CTLFormula ("qwe");
            var f = CTLFormula.EF (l);

            Assert.IsNull (f.Atomic);
            Assert.AreEqual (CTLFormula.TRUE, f.Left);
            Assert.AreEqual (l, f.Right);
            Assert.AreEqual (CTLOperator.CTL_EU, f.Operator);
        }

        [TestMethod]
        public void CheckAGOperator () {
            var l = new CTLFormula ("qwe");
            var f = CTLFormula.AG (l);

            Assert.IsNull (f.Atomic);
            Assert.IsNotNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f.Operator);

            var f1 = f.Left;
            Assert.IsNull (f1.Atomic);
            Assert.AreEqual (CTLFormula.TRUE, f1.Left);
            Assert.IsNotNull (f1.Right);
            Assert.AreEqual (CTLOperator.CTL_EU, f1.Operator);

            var f2 = f1.Right;
            Assert.IsNull (f2.Atomic);
            Assert.AreEqual (l, f2.Left);
            Assert.IsNull (f2.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f2.Operator);
        }

        [TestMethod]
        public void CheckEGOperator () {
            var l = new CTLFormula ("qwe");
            var f = CTLFormula.EG (l);

            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.CTL_EG, f.Operator);
        }

        [TestMethod]
        public void CheckAUOperator () {
            var l = new CTLFormula ("qwe");
            var r = new CTLFormula ("asd");
            var f = CTLFormula.AU (l, r);

            Assert.IsNull (f.Atomic);
            Assert.IsNotNull (f.Left);
            Assert.IsNotNull (f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_AND, f.Operator);

            var f1 = f.Left;
            Assert.IsNull (f1.Atomic);
            Assert.IsNotNull (f1.Left);
            Assert.IsNull (f1.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f1.Operator);

            var f2 = f.Right;
            Assert.IsNull (f2.Atomic);
            Assert.IsNotNull (f2.Left);
            Assert.IsNull (f2.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f2.Operator);

            var f3 = f1.Left;
            Assert.IsNull (f3.Atomic);
            Assert.IsNotNull (f3.Left);
            Assert.IsNotNull (f3.Right);
            Assert.AreEqual (CTLOperator.CTL_EU, f3.Operator);

            var f4 = f2.Left;
            Assert.IsNull (f4.Atomic);
            Assert.IsNotNull (f4.Left);
            Assert.IsNull (f4.Right);
            Assert.AreEqual (CTLOperator.CTL_EG, f4.Operator);

            var f5 = f3.Left;
            Assert.IsNull (f5.Atomic);
            Assert.AreEqual (r, f5.Left);
            Assert.IsNull (f5.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f5.Operator);

            var f6 = f3.Right;
            Assert.IsNull (f6.Atomic);
            Assert.IsNotNull (f6.Left);
            Assert.IsNotNull (f6.Right);
            Assert.AreEqual (CTLOperator.LOGIC_AND, f6.Operator);

            var f7 = f4.Left;
            Assert.IsNull (f7.Atomic);
            Assert.AreEqual (r, f7.Left);
            Assert.IsNull (f7.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f7.Operator);

            var f8 = f6.Left;
            Assert.IsNull (f8.Atomic);
            Assert.AreEqual (l, f8.Left);
            Assert.IsNull (f8.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f8.Operator);

            var f9 = f6.Right;
            Assert.IsNull (f9.Atomic);
            Assert.AreEqual (r, f9.Left);
            Assert.IsNull (f9.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f9.Operator);
        }

        [TestMethod]
        public void CheckEUOperator () {
            var l = new CTLFormula ("qwe");
            var r = new CTLFormula ("asd");
            var f = CTLFormula.EU (l, r);

            Assert.IsNull (f.Atomic);
            Assert.AreEqual (l, f.Left);
            Assert.AreEqual (r, f.Right);
            Assert.AreEqual (CTLOperator.CTL_EU, f.Operator);
        }

        [TestMethod]
        public void CheckAROperator () {
            var l = new CTLFormula ("qwe");
            var r = new CTLFormula ("asd");
            var f = CTLFormula.AR (l, r);

            Assert.IsNull (f.Atomic);
            Assert.IsNotNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f.Operator);

            var f1 = f.Left;
            Assert.IsNull (f1.Atomic);
            Assert.IsNotNull (f1.Left);
            Assert.IsNotNull (f1.Right);
            Assert.AreEqual (CTLOperator.CTL_EU, f1.Operator);

            var f2 = f1.Left;
            Assert.IsNull (f2.Atomic);
            Assert.AreEqual (l, f2.Left);
            Assert.IsNull (f2.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f2.Operator);

            var f3 = f1.Right;
            Assert.IsNull (f3.Atomic);
            Assert.AreEqual (r, f3.Left);
            Assert.IsNull (f3.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f3.Operator);
        }

        [TestMethod]
        public void CheckEROperator () {
            var l = new CTLFormula ("qwe");
            var r = new CTLFormula ("asd");
            var f = CTLFormula.ER (l, r);

            Assert.IsNull (f.Atomic);
            Assert.IsNotNull (f.Left);
            Assert.IsNull (f.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f.Operator);

            var f0 = f.Left;
            Assert.IsNull (f0.Atomic);
            Assert.IsNotNull (f0.Left);
            Assert.IsNotNull (f0.Right);
            Assert.AreEqual (CTLOperator.LOGIC_AND, f0.Operator);

            var f1 = f0.Left;
            Assert.IsNull (f1.Atomic);
            Assert.IsNotNull (f1.Left);
            Assert.IsNull (f1.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f1.Operator);

            var f2 = f0.Right;
            Assert.IsNull (f2.Atomic);
            Assert.IsNotNull (f2.Left);
            Assert.IsNull (f2.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f2.Operator);

            var f3 = f1.Left;
            Assert.IsNull (f3.Atomic);
            Assert.IsNotNull (f3.Left);
            Assert.IsNotNull (f3.Right);
            Assert.AreEqual (CTLOperator.CTL_EU, f3.Operator);

            var f4 = f2.Left;
            Assert.IsNull (f4.Atomic);
            Assert.IsNotNull (f4.Left);
            Assert.IsNull (f4.Right);
            Assert.AreEqual (CTLOperator.CTL_EG, f4.Operator);

            var f5 = f3.Left;
            Assert.IsNull (f5.Atomic);
            Assert.IsNotNull (f5.Left);
            Assert.IsNull (f5.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f5.Operator);

            var f51 = f5.Left;
            Assert.IsNull (f51.Atomic);
            Assert.AreEqual (r, f51.Left);
            Assert.IsNull (f51.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f51.Operator);

            var f6 = f3.Right;
            Assert.IsNull (f6.Atomic);
            Assert.IsNotNull (f6.Left);
            Assert.IsNotNull (f6.Right);
            Assert.AreEqual (CTLOperator.LOGIC_AND, f6.Operator);

            var f7 = f4.Left;
            Assert.IsNull (f7.Atomic);
            Assert.IsNotNull (f7.Left);
            Assert.IsNull (f7.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f7.Operator);

            var f71 = f7.Left;
            Assert.IsNull (f71.Atomic);
            Assert.AreEqual (r, f71.Left);
            Assert.IsNull (f71.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f71.Operator);

            var f8 = f6.Left;
            Assert.IsNull (f8.Atomic);
            Assert.IsNotNull (f8.Left);
            Assert.IsNull (f8.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f8.Operator);

            var f81 = f8.Left;
            Assert.IsNull (f81.Atomic);
            Assert.IsNotNull (f81.Left);
            Assert.IsNull (f81.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f81.Operator);

            var f9 = f6.Right;
            Assert.IsNull (f9.Atomic);
            Assert.IsNotNull (f9.Left);
            Assert.IsNull (f9.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f9.Operator);

            var f91 = f9.Left;
            Assert.IsNull (f91.Atomic);
            Assert.IsNotNull (f91.Left);
            Assert.IsNull (f91.Right);
            Assert.AreEqual (CTLOperator.LOGIC_NOT, f91.Operator);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAXNullArgument () {
            Check (CTLFormula.AX (null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckEXNullArgument () {
            Check (CTLFormula.EX (null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAFNullArgument () {
            Check (CTLFormula.AF (null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckEFNullArgument () {
            Check (CTLFormula.EF (null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAGNullArgument () {
            Check (CTLFormula.AG (null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckEGNullArgument () {
            Check (CTLFormula.EG (null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAUNullLeftArgument () {
            Check (CTLFormula.AU (null, new CTLFormula ("qwe")));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAUNullRightArgument () {
            Check (CTLFormula.AU (new CTLFormula ("qwe"), null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckEUNullLeftArgument () {
            Check (CTLFormula.EU (null, new CTLFormula ("qwe")));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckEUNullRightArgument () {
            Check (CTLFormula.EU (new CTLFormula ("qwe"), null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckARNullLeftArgument () {
            Check (CTLFormula.AR (null, new CTLFormula ("qwe")));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckARNullRightArgument () {
            Check (CTLFormula.AR (new CTLFormula ("qwe"), null));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckERNullLeftArgument () {
            Check (CTLFormula.ER (null, new CTLFormula ("qwe")));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckERNullRightArgument () {
            Check (CTLFormula.ER (new CTLFormula ("qwe"), null));
        }

        [TestMethod]
        public void CheckPrintAtom () {
            const string s = "zxcasd";
            var f = new CTLFormula (s);

            Assert.AreEqual ('{' + s + '}', f.ToString ());
        }

        [TestMethod]
        public void CheckPrintTrue () {
            Assert.AreEqual ("TRUE", CTLFormula.TRUE.ToString ());
        }

        [TestMethod]
        public void CheckPrintFalse () {
            Assert.AreEqual ("FALSE", CTLFormula.FALSE.ToString ());
        }

        [TestMethod]
        public void CheckPrintNot () {
            const string s = "asd";
            var f = !new CTLFormula (s);

            Assert.AreEqual ("not ({" + s + "})", f.ToString ());
        }

        [TestMethod]
        public void CheckPrintAnd () {
            const string s1 = "qwe";
            const string s2 = "asd";
            var f = new CTLFormula (s1) & new CTLFormula (s2);

            Assert.AreEqual ("({" + s1 + "}) and ({" + s2 + "})", f.ToString ());
        }

        [TestMethod]
        public void CheckPrintOr () {
            const string s1 = "qwe";
            const string s2 = "asd";
            var f = new CTLFormula (s1) | new CTLFormula (s2);

            Assert.AreEqual ("({" + s1 + "}) or ({" + s2 + "})", f.ToString ());
        }

        [TestMethod]
        public void CheckPrintEX () {
            const string s = "qwe";
            var f = CTLFormula.EX (new CTLFormula (s));

            Assert.AreEqual ("EX ({" + s + "})", f.ToString ());
        }

        [TestMethod]
        public void CheckPrintEG () {
            const string s = "qwe";
            var f = CTLFormula.EG (new CTLFormula (s));

            Assert.AreEqual ("EG ({" + s + "})", f.ToString ());
        }

        [TestMethod]
        public void CheckPrintEU () {
            const string s1 = "qwe";
            const string s2 = "asd";
            var f = CTLFormula.EU (new CTLFormula (s1), new CTLFormula (s2));

            Assert.AreEqual ("E[({" + s1 + "}) U ({" + s2 + "})]", f.ToString ());
        }

        [TestMethod]
        public void CheckPrintComplexFormula () {
            const string s1 = "qwe";
            const string s2 = "asd";
            const string s3 = "zxc";

            var f1 = (new CTLFormula (s1) & new CTLFormula (s2)) | new CTLFormula (s3);
            var f2 = new CTLFormula (s1) & (new CTLFormula (s2) | new CTLFormula (s3));

            Assert.AreEqual ("(({" + s1 + "}) and ({" + s2 + "})) or ({" + s3 + "})", f1.ToString ());
            Assert.AreEqual ("({" + s1 + "}) and (({" + s2 + "}) or ({" + s3 + "}))", f2.ToString ());
        }

        [TestMethod]
        public void CheckCompare_General () {
            const string s1 = "qwe";
            const string s2 = "asd";
            var unary = new HashSet <Func <CTLFormula, CTLFormula>> {
                                                                        CTLFormula.Not,
                                                                        CTLFormula.EX,
                                                                        CTLFormula.EG
                                                                    };
            var binary = new HashSet <Func <CTLFormula, CTLFormula, CTLFormula>> {
                                                                                     CTLFormula.And,
                                                                                     CTLFormula.Or,
                                                                                     CTLFormula.EU
                                                                                 };
            foreach (var f in unary) {
                var f1 = f (new CTLFormula (s1));
                var f2 = f (new CTLFormula (s1));
                Assert.IsTrue (f1.Equals (f2));
            }

            foreach (var f in binary) {
                var f1 = f (new CTLFormula (s1), new CTLFormula (s2));
                var f2 = f (new CTLFormula (s1), new CTLFormula (s2));
                var f3 = f (new CTLFormula (s2), new CTLFormula (s1));

                Assert.IsTrue (f1.Equals (f2));
                Assert.IsFalse (f1.Equals (f3));
            }
        }
    }
}
