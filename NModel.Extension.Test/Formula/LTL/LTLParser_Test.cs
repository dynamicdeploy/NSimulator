#region

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace NModel.Extension.Test {
    [TestClass]
    public sealed class LTLParser_Test {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckParseNull () {
            LTLFormula.Parse (null);
        }

        private static void Check (LTLFormula f) {
            Assert.AreEqual (f, LTLFormula.Parse (f.ToString ().Replace ('[', '(').Replace (']', ')')));
        }

        [TestMethod]
        public void CheckParseTrue () {
            Check (LTLFormula.TRUE);
        }

        [TestMethod]
        public void CheckParseFalse () {
            Check (LTLFormula.FALSE);
        }

        [TestMethod]
        public void CheckParseAtomic () {
            Check (new LTLFormula ("ap"));
        }

        [TestMethod]
        public void CheckParseAndOperator () {
            Check ((new LTLFormula ("a")) & (new LTLFormula ("b")));
        }

        [TestMethod]
        public void CheckParseOrOperator () {
            Check ((new LTLFormula ("a")) | (new LTLFormula ("b")));
        }

        [TestMethod]
        public void CheckParseNotOperator () {
            Check (!(new LTLFormula ("a")));
        }

        [TestMethod]
        public void CheckXOperator () {
            Check (LTLFormula.X (new LTLFormula ("a")));
        }

        [TestMethod]
        public void CheckGOperator () {
            Check (LTLFormula.G (new LTLFormula ("a")));
        }

        [TestMethod]
        public void CheckFOperator () {
            Check (LTLFormula.F (new LTLFormula ("a")));
        }

        [TestMethod]
        public void CheckUOperator () {
            Check (LTLFormula.U (new LTLFormula ("a"), new LTLFormula ("b")));
        }

        [TestMethod]
        public void CheckROperator () {
            Check (LTLFormula.R (new LTLFormula ("a"), new LTLFormula ("b")));
        }

        [TestMethod]
        public void CheckComplexFormula1 () {
            var f = LTLFormula.Parse ("X ({qwe} and {abc} && ({rty} U FALSE))");
            Assert.AreEqual (
                LTLFormula.X (new LTLFormula ("qwe") & new LTLFormula ("abc") &
                              LTLFormula.U (new LTLFormula ("rty"), LTLFormula.FALSE)),
                f);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_Unclosed1 () {
            LTLFormula.Parse ("( {x}");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_Unclosed2 () {
            LTLFormula.Parse ("{x");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula0 () {
            LTLFormula.Parse ("string");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoNotOperands () {
            LTLFormula.Parse ("!");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoOrOperands () {
            LTLFormula.Parse ("||");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoOrLeftOperand () {
            LTLFormula.Parse (" || {a}");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoOrRightOperand () {
            LTLFormula.Parse ("{a} ||");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAndOperands () {
            LTLFormula.Parse ("&&");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAndLeftOperand () {
            LTLFormula.Parse (" && {a}");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoAndRightOperand () {
            LTLFormula.Parse ("{a} &&");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoXOperand () {
            LTLFormula.Parse ("X");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoFOperand () {
            LTLFormula.Parse ("F");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoGOperand () {
            LTLFormula.Parse ("G");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoUOperands1 () {
            LTLFormula.Parse ("U");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoUOperands2 () {
            LTLFormula.Parse ("(U)");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoULeftOperand () {
            LTLFormula.Parse ("U {a}");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoURightOperand () {
            LTLFormula.Parse ("{a} U");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoROperands1 () {
            LTLFormula.Parse ("R");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoROperands2 () {
            LTLFormula.Parse ("(R)");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoRLeftOperand () {
            LTLFormula.Parse ("R {a}");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentException))]
        public void CheckIncorrectFormula_NoRRightOperand () {
            LTLFormula.Parse ("{a} R");
        }
    }
}
