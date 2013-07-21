#region

using System;
using System.IO;
using System.Text;
using Antlr.Runtime;

#endregion

namespace NModel.Extension {
    /// <summary>
    ///   Формула логики LTL.
    /// </summary>
    /// <remarks>
    ///   Формула хранится в виде бинарного дерева.
    ///   Листья дерева - атомарные высказывания.
    ///   Остальные узлы дерева содержат операторы.
    /// </remarks>
    /// <seealso cref = "ITemporalFormula" />
    public sealed class LTLFormula : ITemporalFormula {
        /// <summary>
        ///   Тождественно истинная формула.
        /// </summary>
        public static readonly LTLFormula TRUE = new LTLFormula (null, null, LTLOperator.TRUE, null);

        /// <summary>
        ///   Тождественно ложная формула.
        /// </summary>
        public static readonly LTLFormula FALSE = new LTLFormula (null, null, LTLOperator.FALSE, null);

        private LTLFormula (LTLFormula left, LTLFormula right, LTLOperator op, string atom) {
            this.Left = left;
            this.Right = right;
            this.Operator = op;
            this.Atomic = atom;
        }

        /// <summary>
        ///   Инициализация формулы, содержащей лишь атомарное высказывание.
        /// </summary>
        /// <param name = "atomic">Атомарное высказывание.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "atomic" /> является <c>null</c>.</exception>
        public LTLFormula (string atomic)
            : this (null, null, LTLOperator.NONE, atomic) {
            if (atomic == null)
                throw new ArgumentNullException ("atomic");
        }

        /// <summary>
        ///   Возвращает левую подформулу или <c>null</c>, если она не определена.
        /// </summary>
        /// <value>
        ///   Левая подформула или <c>null</c>.
        /// </value>
        public LTLFormula Left { get; private set; }

        /// <summary>
        ///   Возвращает правую подформулу или <c>null</c>, если она не определена.
        /// </summary>
        /// <value>
        ///   Правая подформула или <c>null</c>.
        /// </value>
        public LTLFormula Right { get; private set; }

        /// <summary>
        ///   Возвращает оператор, связывающий левую и правую подформулы.
        /// </summary>
        /// <value>
        ///   Оператор, связывающий левую и правую подформулы.
        /// </value>
        /// <seealso cref = "LTLOperator" />
        public LTLOperator Operator { get; private set; }

        /// <summary>
        ///   Возвращает атомарное высказывание или <c>null</c>, если формула
        ///   не является просто атомарным высказыванием.
        /// </summary>
        /// <value>
        ///   Атомарное высказывание или <c>null</c>.
        /// </value>
        public string Atomic { get; private set; }

        /// <summary>
        ///   Возвращает формулу "<paramref name = "left" /> AND <paramref name = "right" />".
        /// </summary>
        /// <param name = "left">Левый операнд.</param>
        /// <param name = "right">Правый операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> является <c>null</c>.</exception>
        /// <returns>Логическое "AND" двух формул.</returns>
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
        ///   Возвращает формулу "<paramref name = "left" /> OR <paramref name = "right" />".
        /// </summary>
        /// <param name = "left">Левый операнд.</param>
        /// <param name = "right">Правый операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> является <c>null</c>.</exception>
        /// <returns>Логическое "OR" двух формул.</returns>
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
        ///   Возвращает формулу "NOT <paramref name = "formula" />".
        /// </summary>
        /// <param name = "formula">Операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <returns>Логическое "NOT" формулы.</returns>
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
        ///   Возвращает формулу "X <paramref name = "formula" />".
        /// </summary>
        /// <param name = "formula">Операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <returns>Темпоральный "сдвиг по времени" формулы.</returns>
        public static LTLFormula X (LTLFormula formula) {
            if (formula == null)
                throw new ArgumentNullException ("formula");

            return new LTLFormula (formula, null, LTLOperator.LTL_X, null);
        }

        /// <summary>
        ///   Возвращает формулу "F <paramref name = "formula" />".
        /// </summary>
        /// <remarks>
        ///   Темпоральная формула "F f" будет представлена в виде "TRUE U f".
        /// </remarks>
        /// <param name = "formula">Операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <seealso cref = "U" />
        /// <seealso cref = "TRUE" />
        /// <returns>Темпоральный "оператор происшествия" формулы.</returns>
        public static LTLFormula F (LTLFormula formula) {
            return U (TRUE, formula);
        }

        /// <summary>
        ///   Возвращает формулу "G <paramref name = "formula" />".
        /// </summary>
        /// <remarks>
        ///   Темпоральная формула "G f" будет представлена в виде "NOT (F (NOT f))".
        /// </remarks>
        /// <param name = "formula">Операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <see cref = "F" />
        /// <see cref = "Not" />
        /// <returns>Темпоральный "оператор инвариантности" формулы.</returns>
        public static LTLFormula G (LTLFormula formula) {
            return !F (!formula);
        }

        /// <summary>
        ///   Возвращает формулу "<paramref name = "left" /> U <paramref name = "right" />".
        /// </summary>
        /// <param name = "left">Левый операнд.</param>
        /// <param name = "right">Правый операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> является <c>null</c>.</exception>
        /// <returns>Темпоральное "условное ожидание" формул.</returns>
        public static LTLFormula U (LTLFormula left, LTLFormula right) {
            if (left == null)
                throw new ArgumentNullException ("left");

            if (right == null)
                throw new ArgumentNullException ("right");

            return new LTLFormula (left, right, LTLOperator.LTL_U, null);
        }

        /// <summary>
        ///   Возвращает формулу "<paramref name = "left" /> R <paramref name = "right" />".
        /// </summary>
        /// <remarks>
        ///   Темпоральная формула "f R g" будет представлена в виде "NOT ((NOT f) U (NOT g))".
        /// </remarks>
        /// <param name = "left">Левый операнд.</param>
        /// <param name = "right">Правый операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> является <c>null</c>.</exception>
        /// <see cref = "U" />
        /// <see cref = "Not" />
        /// <returns>Темпоральная "разблокировка" формул.</returns>
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
        ///   Переводит строковое представление формулы логики LTL в
        ///   эквивалентный экземпляр <see cref = "LTLFormula" />.
        /// </summary>
        /// <param name = "s">Строка.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "s" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentException"><paramref name = "s" /> не содержит формулу.</exception>
        /// <returns>Формула логики LTL.</returns>
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
