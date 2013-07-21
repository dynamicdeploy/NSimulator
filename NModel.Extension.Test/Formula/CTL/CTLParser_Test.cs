#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NModel.Extension.Test {
    [TestClass]
    public sealed class CTLParser_Test {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckParseNull () {
            CTLFormula.Parse (null);
        }

        private static void Check (CTLFormula f) {
            Assert.AreEqual (f, CTLFormula.Parse (f.ToString ().Replace ('[', '(').Replace (']', ')')));
        }

        [TestMethod]
        public void CheckParseTrue () {
            Check (CTLFormula.TRUE);
        }

        [TestMethod]
        public void CheckParseFalse () {
            Check (CTLFormula.FALSE);
        }

        [TestMethod]
        public void CheckParseAtomic () {
            Check (new CTLFormula ("ap"));
        }

        [TestMethod]
        public void CheckParseAndOperator () {
            Check ((new CTLFormula ("a")) & (new CTLFormula ("b")));
        }

        [TestMethod]
        public void CheckParseOrOperator () {
            Check ((new CTLFormula ("a")) | (new CTLFormula ("b")));
        }

        [TestMethod]
        public void CheckParseNotOperator () {
            Check (!(new CTLFormula ("a")));
        }

        [TestMethod]
        public void CheckParseAXOperator () {
            Check (CTLFormula.AX (new CTLFormula ("a")));
        }

        [TestMethod]
        public void CheckParseEXOperator () {
            Check (CTLFormula.EX (new CTLFormula ("a")));
        }

        [TestMethod]
        public void CheckParseAFOperator () {
            Check (CTLFormula.AF (new CTLFormula ("a")));
        }

        [TestMethod]
        public void CheckParseEFOperator () {
            Check (CTLFormula.EF (new CTLFormula ("a")));
        }

        [TestMethod]
        public void CheckParseAGOperator () {
            Check (CTLFormula.AG (new CTLFormula ("a")));
        }

        [TestMethod]
        public void CheckParseAUOperator () {
            Check (CTLFormula.AU (new CTLFormula ("a"), new CTLFormula ("b")));
        }

        [TestMethod]
        public void CheckParseEUOperator () {
            Check (CTLFormula.EU (new CTLFormula ("a"), new CTLFormula ("b")));
        }

        [TestMethod]
        public void CheckParseAROperator () {
            Check (CTLFormula.AR (new CTLFormula ("a"), new CTLFormula ("b")));
        }

        [TestMethod]
        public void CheckParseEROperator () {
            Check (CTLFormula.ER (new CTLFormula ("a"), new CTLFormula ("b")));
        }

        [TestMethod]
        public void CheckComplexFormula1 () {
            var f = CTLFormula.Parse ("EX (({qwe} and {abc} && E ({rty} U FALSE)))");
            Assert.AreEqual (
                CTLFormula.EX (new CTLFormula ("qwe") & new CTLFormula ("abc") &
                               CTLFormula.EU (new CTLFormula ("rty"), CTLFormula.FALSE)),
                f);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_Unclosed1 () {
            CTLFormula.Parse ("( {x}");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_Unclosed2 () {
            CTLFormula.Parse ("{x");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula0 () {
            CTLFormula.Parse ("string");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoNotOperands () {
            CTLFormula.Parse ("!");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoOrOperands () {
            CTLFormula.Parse ("||");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoOrLeftOperand () {
            CTLFormula.Parse (" || {a}");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoOrRightOperand () {
            CTLFormula.Parse ("{a} ||");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAndOperands () {
            CTLFormula.Parse ("&&");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAndLeftOperand () {
            CTLFormula.Parse (" && {a}");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAndRightOperand () {
            CTLFormula.Parse ("{a} &&");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAXOperand () {
            CTLFormula.Parse ("AX");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoEXOperand () {
            CTLFormula.Parse ("EX");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAFOperand () {
            CTLFormula.Parse ("AF");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoEFOperand () {
            CTLFormula.Parse ("EF");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAGOperand () {
            CTLFormula.Parse ("AG");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoEGOperand () {
            CTLFormula.Parse ("EG");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAUOperands1 () {
            CTLFormula.Parse ("AU");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAUOperands2 () {
            CTLFormula.Parse ("A( U )");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAULeftOperand () {
            CTLFormula.Parse ("A ( U {a})");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAURightOperand () {
            CTLFormula.Parse ("A ({a} U)");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoEUOperands1 () {
            CTLFormula.Parse ("EU");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoEUOperands2 () {
            CTLFormula.Parse ("E( U )");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoEULeftOperand () {
            CTLFormula.Parse ("E ( U {a})");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoEURightOperand () {
            CTLFormula.Parse ("E ({a} U)");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAROperands1 () {
            CTLFormula.Parse ("AR");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAROperands2 () {
            CTLFormula.Parse ("A( R )");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoARLeftOperand () {
            CTLFormula.Parse ("A ( R {a})");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoARRightOperand () {
            CTLFormula.Parse ("A ({a} R)");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoEROperands1 () {
            CTLFormula.Parse ("ER");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoEROperands2 () {
            CTLFormula.Parse ("E( R )");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoERLeftOperand () {
            CTLFormula.Parse ("E ( R {a})");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoERRightOperand () {
            CTLFormula.Parse ("E ({a} R)");
        }
    }
}
