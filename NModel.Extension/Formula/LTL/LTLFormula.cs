#region

using System;
using System.IO;
using System.Text;
using Antlr.Runtime;

#endregion

namespace NModel.Extension {
    /// <summary>
    ///   ������� ������ LTL.
    /// </summary>
    /// <remarks>
    ///   ������� �������� � ���� ��������� ������.
    ///   ������ ������ - ��������� ������������.
    ///   ��������� ���� ������ �������� ���������.
    /// </remarks>
    /// <seealso cref = "ITemporalFormula" />
    public sealed class LTLFormula : ITemporalFormula {
        /// <summary>
        ///   ������������ �������� �������.
        /// </summary>
        public static readonly LTLFormula TRUE = new LTLFormula (null, null, LTLOperator.TRUE, null);

        /// <summary>
        ///   ������������ ������ �������.
        /// </summary>
        public static readonly LTLFormula FALSE = new LTLFormula (null, null, LTLOperator.FALSE, null);

        private LTLFormula (LTLFormula left, LTLFormula right, LTLOperator op, string atom) {
            this.Left = left;
            this.Right = right;
            this.Operator = op;
            this.Atomic = atom;
        }

        /// <summary>
        ///   ������������� �������, ���������� ���� ��������� ������������.
        /// </summary>
        /// <param name = "atomic">��������� ������������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "atomic" /> �������� <c>null</c>.</exception>
        public LTLFormula (string atomic)
            : this (null, null, LTLOperator.NONE, atomic) {
            if (atomic == null)
                throw new ArgumentNullException ("atomic");
        }

        /// <summary>
        ///   ���������� ����� ���������� ��� <c>null</c>, ���� ��� �� ����������.
        /// </summary>
        /// <value>
        ///   ����� ���������� ��� <c>null</c>.
        /// </value>
        public LTLFormula Left { get; private set; }

        /// <summary>
        ///   ���������� ������ ���������� ��� <c>null</c>, ���� ��� �� ����������.
        /// </summary>
        /// <value>
        ///   ������ ���������� ��� <c>null</c>.
        /// </value>
        public LTLFormula Right { get; private set; }

        /// <summary>
        ///   ���������� ��������, ����������� ����� � ������ ����������.
        /// </summary>
        /// <value>
        ///   ��������, ����������� ����� � ������ ����������.
        /// </value>
        /// <seealso cref = "LTLOperator" />
        public LTLOperator Operator { get; private set; }

        /// <summary>
        ///   ���������� ��������� ������������ ��� <c>null</c>, ���� �������
        ///   �� �������� ������ ��������� �������������.
        /// </summary>
        /// <value>
        ///   ��������� ������������ ��� <c>null</c>.
        /// </value>
        public string Atomic { get; private set; }

        /// <summary>
        ///   ���������� ������� "<paramref name = "left" /> AND <paramref name = "right" />".
        /// </summary>
        /// <param name = "left">����� �������.</param>
        /// <param name = "right">������ �������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> �������� <c>null</c>.</exception>
        /// <returns>���������� "AND" ���� ������.</returns>
        public static LTLFormula And (LTLFormula left, LTLFormula right) {
            if (left == null)
                throw new ArgumentNullException ("left");

            if (right == null)
                throw new ArgumentNullException ("right");

            return new LTLFormula (left, right, LTLOperator.LOGIC_AND, null);
        }

        /// <see cref = "And" />
        public static LTLFormula operator & (LTLFormula left, LTLFormula right) {
            return And (left, right);
        }

        /// <summary>
        ///   ���������� ������� "<paramref name = "left" /> OR <paramref name = "right" />".
        /// </summary>
        /// <param name = "left">����� �������.</param>
        /// <param name = "right">������ �������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> �������� <c>null</c>.</exception>
        /// <returns>���������� "OR" ���� ������.</returns>
        public static LTLFormula Or (LTLFormula left, LTLFormula right) {
            if (left == null)
                throw new ArgumentNullException ("left");

            if (right == null)
                throw new ArgumentNullException ("right");

            return new LTLFormula (left, right, LTLOperator.LOGIC_OR, null);
        }

        /// <see cref = "Or" />
        public static LTLFormula operator | (LTLFormula left, LTLFormula right) {
            return Or (left, right);
        }

        /// <summary>
        ///   ���������� ������� "NOT <paramref name = "formula" />".
        /// </summary>
        /// <param name = "formula">�������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> �������� <c>null</c>.</exception>
        /// <returns>���������� "NOT" �������.</returns>
        public static LTLFormula Not (LTLFormula formula) {
            if (formula == null)
                throw new ArgumentNullException ("formula");

            return new LTLFormula (formula, null, LTLOperator.LOGIC_NOT, null);
        }

        /// <see cref = "Not" />
        public static LTLFormula operator ! (LTLFormula formula) {
            return Not (formula);
        }

        /// <summary>
        ///   ���������� ������� "X <paramref name = "formula" />".
        /// </summary>
        /// <param name = "formula">�������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> �������� <c>null</c>.</exception>
        /// <returns>������������ "����� �� �������" �������.</returns>
        public static LTLFormula X (LTLFormula formula) {
            if (formula == null)
                throw new ArgumentNullException ("formula");

            return new LTLFormula (formula, null, LTLOperator.LTL_X, null);
        }

        /// <summary>
        ///   ���������� ������� "F <paramref name = "formula" />".
        /// </summary>
        /// <remarks>
        ///   ������������ ������� "F f" ����� ������������ � ���� "TRUE U f".
        /// </remarks>
        /// <param name = "formula">�������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> �������� <c>null</c>.</exception>
        /// <seealso cref = "U" />
        /// <seealso cref = "TRUE" />
        /// <returns>������������ "�������� ������������" �������.</returns>
        public static LTLFormula F (LTLFormula formula) {
            return U (TRUE, formula);
        }

        /// <summary>
        ///   ���������� ������� "G <paramref name = "formula" />".
        /// </summary>
        /// <remarks>
        ///   ������������ ������� "G f" ����� ������������ � ���� "NOT (F (NOT f))".
        /// </remarks>
        /// <param name = "formula">�������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> �������� <c>null</c>.</exception>
        /// <see cref = "F" />
        /// <see cref = "Not" />
        /// <returns>������������ "�������� ��������������" �������.</returns>
        public static LTLFormula G (LTLFormula formula) {
            return !F (!formula);
        }

        /// <summary>
        ///   ���������� ������� "<paramref name = "left" /> U <paramref name = "right" />".
        /// </summary>
        /// <param name = "left">����� �������.</param>
        /// <param name = "right">������ �������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> �������� <c>null</c>.</exception>
        /// <returns>������������ "�������� ��������" ������.</returns>
        public static LTLFormula U (LTLFormula left, LTLFormula right) {
            if (left == null)
                throw new ArgumentNullException ("left");

            if (right == null)
                throw new ArgumentNullException ("right");

            return new LTLFormula (left, right, LTLOperator.LTL_U, null);
        }

        /// <summary>
        ///   ���������� ������� "<paramref name = "left" /> R <paramref name = "right" />".
        /// </summary>
        /// <remarks>
        ///   ������������ ������� "f R g" ����� ������������ � ���� "NOT ((NOT f) U (NOT g))".
        /// </remarks>
        /// <param name = "left">����� �������.</param>
        /// <param name = "right">������ �������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> �������� <c>null</c>.</exception>
        /// <see cref = "U" />
        /// <see cref = "Not" />
        /// <returns>������������ "�������������" ������.</returns>
        public static LTLFormula R (LTLFormula left, LTLFormula right) {
            return !(U (!left, !right));
        }

        /// <inheritdoc />
        public override bool Equals (object obj) {
            if (ReferenceEquals (null, obj))
                return false;
            if (ReferenceEquals (this, obj))
                return true;
            return obj.GetType () == typeof (LTLFormula) && this.Equals ((LTLFormula) obj);
        }

        /// <summary>
        ///   ��������� ��������� ������������� ������� ������ LTL �
        ///   ������������� ��������� <see cref = "LTLFormula" />.
        /// </summary>
        /// <param name = "s">������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "s" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentException"><paramref name = "s" /> �� �������� �������.</exception>
        /// <returns>������� ������ LTL.</returns>
        public static LTLFormula Parse (string s) {
            if (s == null)
                throw new ArgumentNullException ("s");

            try {
                using (var memoryStream = new MemoryStream (Encoding.Default.GetBytes (s))) {
                    var stream = new ANTLRInputStream (memoryStream);
                    var parser = new LTLParserParser (new CommonTokenStream (new LTLParserLexer (stream)));
                    var result = parser.start ();

                    if (result == null)
                        throw new ArgumentException ();

                    return result;
                }
            }
            catch (Exception e) {
                throw new ArgumentException (e.Message);
            }
        }

        /// <inheritdoc />
        public override string ToString () {
            switch (this.Operator) {
                case LTLOperator.TRUE :
                    return "TRUE";
                case LTLOperator.FALSE :
                    return "FALSE";
                case LTLOperator.NONE :
                    return string.Format ("{{{0}}}", this.Atomic);
                case LTLOperator.LOGIC_NOT :
                    return string.Format ("not ({0})", this.Left);
                case LTLOperator.LOGIC_AND :
                    return string.Format ("({0}) and ({1})", this.Left, this.Right);
                case LTLOperator.LOGIC_OR :
                    return string.Format ("({0}) or ({1})", this.Left, this.Right);
                case LTLOperator.LTL_X :
                    return string.Format ("X ({0})", this.Left);
                case LTLOperator.LTL_U :
                    return string.Format ("({0}) U ({1})", this.Left, this.Right);
                default :
                    return string.Empty;
            }
        }

        private bool Equals (LTLFormula other) {
            if (ReferenceEquals (null, other))
                return false;
            if (ReferenceEquals (this, other))
                return true;
            return Equals (other.Left, this.Left) && Equals (other.Right, this.Right) &&
                   Equals (other.Operator, this.Operator) && Equals (other.Atomic, this.Atomic);
        }

        /// <inheritdoc />
        public override int GetHashCode () {
            unchecked {
                var result = (this.Left != null ? this.Left.GetHashCode () : 0);
                result = (result * 397) ^ (this.Right != null ? this.Right.GetHashCode () : 0);
                result = (result * 397) ^ this.Operator.GetHashCode ();
                result = (result * 397) ^ (this.Atomic != null ? this.Atomic.GetHashCode () : 0);
                return result;
            }
        }
    }
}
