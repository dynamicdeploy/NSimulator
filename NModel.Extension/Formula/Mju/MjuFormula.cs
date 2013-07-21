#region

using System;

#endregion

namespace NModel.Extension {
    internal sealed class MjuFormula {
        public static readonly MjuFormula TRUE = new MjuFormula (null, null, MjuOperator.TRUE, null);

        public static readonly MjuFormula FALSE = new MjuFormula (null, null, MjuOperator.FALSE, null);

        private MjuFormula (MjuFormula left, MjuFormula right, MjuOperator op, string name) {
            this.Left = left;
            this.Right = right;
            this.Operator = op;
            this.Name = name;
        }

        public MjuFormula Left { get; private set; }

        public MjuFormula Right { get; private set; }

        public MjuOperator Operator { get; private set; }

        public string Name { get; private set; }

        public static MjuFormula Atomic (string name) {
            if (name == null)
                throw new ArgumentNullException ("name");

            return new MjuFormula (null, null, MjuOperator.ATOMIC, name);
        }

        public static MjuFormula Var (string name) {
            if (name == null)
                throw new ArgumentNullException ("name");

            return new MjuFormula (null, null, MjuOperator.VARIABLE, name);
        }

        public static MjuFormula Not (MjuFormula formula) {
            if (formula == null)
                throw new ArgumentNullException ("formula");

            switch (formula.Operator) {
                case MjuOperator.TRUE :
                    return FALSE;
                case MjuOperator.FALSE :
                    return TRUE;
                case MjuOperator.LOGIC_NOT :
                    return formula.Left;
                case MjuOperator.LOGIC_AND :
                    return new MjuFormula (Not (formula.Left), Not (formula.Right), MjuOperator.LOGIC_OR, null);
                case MjuOperator.LOGIC_OR :
                    return new MjuFormula (Not (formula.Left), Not (formula.Right), MjuOperator.LOGIC_AND, null);
                case MjuOperator.MJU_ALL :
                    return ExistTrans (Not (formula.Left));
                case MjuOperator.MJU_EXISTS :
                    return AllTrans (Not (formula.Left));
                case MjuOperator.MJU_LFP :
                    return GreatestFixedPoint (Not (formula.Left), formula.Right.Name);
                case MjuOperator.MJU_GFP :
                    return LeastFixedPoint (Not (formula.Left), formula.Right.Name);
                case MjuOperator.VARIABLE :
                    return formula;
                default :
                    return new MjuFormula (formula, null, MjuOperator.LOGIC_NOT, null);
            }
        }

        public static MjuFormula And (MjuFormula left, MjuFormula right) {
            if (left == null)
                throw new ArgumentNullException ("left");

            if (right == null)
                throw new ArgumentNullException ("right");

            return new MjuFormula (left, right, MjuOperator.LOGIC_AND, null);
        }

        public static MjuFormula Or (MjuFormula left, MjuFormula right) {
            if (left == null)
                throw new ArgumentNullException ("left");

            if (right == null)
                throw new ArgumentNullException ("right");

            return new MjuFormula (left, right, MjuOperator.LOGIC_OR, null);
        }

        public static MjuFormula AllTrans (MjuFormula formula) {
            if (formula == null)
                throw new ArgumentNullException ("formula");

            return new MjuFormula (formula, null, MjuOperator.MJU_ALL, null);
        }

        public static MjuFormula ExistTrans (MjuFormula formula) {
            if (formula == null)
                throw new ArgumentNullException ("formula");

            return new MjuFormula (formula, null, MjuOperator.MJU_EXISTS, null);
        }

        public static MjuFormula LeastFixedPoint (MjuFormula formula, string var) {
            if (formula == null)
                throw new ArgumentNullException ("formula");

            if (var == null)
                throw new ArgumentNullException ("var");

            return new MjuFormula (formula, Var (var), MjuOperator.MJU_LFP, null);
        }

        public static MjuFormula GreatestFixedPoint (MjuFormula formula, string var) {
            if (formula == null)
                throw new ArgumentNullException ("formula");

            if (var == null)
                throw new ArgumentNullException ("var");

            return new MjuFormula (formula, Var (var), MjuOperator.MJU_GFP, null);
        }

        public override string ToString () {
            switch (this.Operator) {
                case MjuOperator.TRUE :
                    return "TRUE";
                case MjuOperator.FALSE :
                    return "FALSE";
                case MjuOperator.ATOMIC :
                    return string.Format ("{{{0}}}", this.Name);
                case MjuOperator.VARIABLE :
                    return string.Format ("{0}", this.Name.ToUpper ());
                case MjuOperator.LOGIC_NOT :
                    return string.Format ("! ({0})", this.Left);
                case MjuOperator.LOGIC_AND :
                    return string.Format ("({0}) and ({1})", this.Left, this.Right);
                case MjuOperator.LOGIC_OR :
                    return string.Format ("({0}) or ({1})", this.Left, this.Right);
                case MjuOperator.MJU_ALL :
                    return string.Format ("[a] ({0})", this.Left);
                case MjuOperator.MJU_EXISTS :
                    return string.Format ("<a> ({0})", this.Left);
                case MjuOperator.MJU_GFP :
                    return string.Format ("v({0}).({1})", this.Right.Name, this.Left);
                case MjuOperator.MJU_LFP :
                    return string.Format ("m({0}).({1})", this.Right.Name, this.Left);
                default :
                    return string.Empty;
            }
        }
    }
}
