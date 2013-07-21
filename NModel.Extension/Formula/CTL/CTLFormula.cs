#region

using System;
using System.IO;
using System.Text;
using Antlr.Runtime;

#endregion

namespace NModel.Extension {
    /// <summary>
    ///   Формула логики CTL.
    /// </summary>
    /// <remarks>
    ///   Формула хранится в виде бинарного дерева.
    ///   Листья дерева - атомарные высказывания.
    ///   Остальные узлы дерева содержат операторы.
    /// </remarks>
    /// <seealso cref = "ITemporalFormula" />
    public sealed class CTLFormula : ITemporalFormula {
        /// <summary>
        ///   Тождественно истинная формула.
        /// </summary>
        public static readonly CTLFormula TRUE = new CTLFormula (null, null, CTLOperator.TRUE, null);

        /// <summary>
        ///   Тождественно ложная формула.
        /// </summary>
        public static readonly CTLFormula FALSE = new CTLFormula (null, null, CTLOperator.FALSE, null);

        private CTLFormula (CTLFormula left, CTLFormula right, CTLOperator op, string atom) {
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
        public CTLFormula (string atomic)
            :
                this (null, null, CTLOperator.NONE, atomic) {
            if (atomic == null)
                throw new ArgumentNullException ("atomic");
        }

        /// <summary>
        ///   Возвращает левую подформулу или <c>null</c>, если она не определена.
        /// </summary>
        /// <value>
        ///   Левая подформула или <c>null</c>.
        /// </value>
        public CTLFormula Left { get; private set; }

        /// <summary>
        ///   Возвращает правую подформулу или <c>null</c>, если она не определена.
        /// </summary>
        /// <value>
        ///   Правая подформула или <c>null</c>.
        /// </value>
        public CTLFormula Right { get; private set; }

        /// <summary>
        ///   Возвращает оператор, связывающий левую и правую подформулы.
        /// </summary>
        /// <value>
        ///   Оператор, связывающий левую и правую подформулы.
        /// </value>
        /// <seealso cref = "CTLOperator" />
        public CTLOperator Operator { get; private set; }

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
        public static CTLFormula And (CTLFormula left, CTLFormula right) {
            if (left == null)
                throw new ArgumentNullException ("left");

            if (right == null)
                throw new ArgumentNullException ("right");

            return new CTLFormula (left, right, CTLOperator.LOGIC_AND, null);
        }

        /// <see cref = "And" />
        public static CTLFormula operator & (CTLFormula left, CTLFormula right) {
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
        public static CTLFormula Or (CTLFormula left, CTLFormula right) {
            if (left == null)
                throw new ArgumentNullException ("left");

            if (right == null)
                throw new ArgumentNullException ("right");

            return new CTLFormula (left, right, CTLOperator.LOGIC_OR, null);
        }

        /// <see cref = "Or" />
        public static CTLFormula operator | (CTLFormula left, CTLFormula right) {
            return Or (left, right);
        }

        /// <summary>
        ///   Возвращает формулу "NOT <paramref name = "formula" />".
        /// </summary>
        /// <param name = "formula">Операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <returns>Логическое "NOT" формулы.</returns>
        public static CTLFormula Not (CTLFormula formula) {
            if (formula == null)
                throw new ArgumentNullException ("formula");

            return new CTLFormula (formula, null, CTLOperator.LOGIC_NOT, null);
        }

        /// <see cref = "Not" />
        public static CTLFormula operator ! (CTLFormula formula) {
            return Not (formula);
        }

        /// <summary>
        ///   Возвращает формулу "AX <paramref name = "formula" />".
        /// </summary>
        /// <remarks>
        ///   Темпоральная формула "AX f" будет представлена в виде "NOT (EX (NOT f))".
        /// </remarks>
        /// <param name = "formula">Операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <seealso cref = "EX" />
        /// <seealso cref = "Not" />
        /// <returns>Темпоральное "AX" формулы.</returns>
        public static CTLFormula AX (CTLFormula formula) {
            return !EX (!formula);
        }

        /// <summary>
        ///   Возвращает формулу "EX <paramref name = "formula" />".
        /// </summary>
        /// <param name = "formula">Операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <returns>Темпоральное "EX" формулы.</returns>
        public static CTLFormula EX (CTLFormula formula) {
            if (formula == null)
                throw new ArgumentNullException ("formula");

            return new CTLFormula (formula, null, CTLOperator.CTL_EX, null);
        }

        /// <summary>
        ///   Возвращает формулу "AF <paramref name = "formula" />".
        /// </summary>
        /// <remarks>
        ///   Темпоральная формула "AF f" будет представлена в виде "NOT (EG (NOT f))".
        /// </remarks>
        /// <param name = "formula">Операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <seealso cref = "EG" />
        /// <seealso cref = "Not" />
        /// <returns>Темпоральное "AF" формулы.</returns>
        public static CTLFormula AF (CTLFormula formula) {
            return !EG (!formula);
        }

        /// <summary>
        ///   Возвращает формулу "EF <paramref name = "formula" />".
        /// </summary>
        /// <remarks>
        ///   Темпоральная формула "EF f" будет представлена в виде "TRUE EU f".
        /// </remarks>
        /// <param name = "formula">Операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <seealso cref = "EU" />
        /// <seealso cref = "TRUE" />
        /// <returns>Темпоральное "EF" формулы.</returns>
        public static CTLFormula EF (CTLFormula formula) {
            return EU (TRUE, formula);
        }

        /// <summary>
        ///   Возвращает формулу "AG <paramref name = "formula" />".
        /// </summary>
        /// <remarks>
        ///   Темпоральная формула "AG f" будет представлена в виде "NOT (EF (NOT f))".
        /// </remarks>
        /// <param name = "formula">Операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <seealso cref = "EF" />
        /// <seealso cref = "Not" />
        /// <returns>Темпоральное "AG" формулы.</returns>
        public static CTLFormula AG (CTLFormula formula) {
            return !EF (!formula);
        }

        /// <summary>
        ///   Возвращает формулу "EG <paramref name = "formula" />".
        /// </summary>
        /// <param name = "formula">Операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <returns>Темпоральное "EG" формулы.</returns>
        public static CTLFormula EG (CTLFormula formula) {
            if (formula == null)
                throw new ArgumentNullException ("formula");

            return new CTLFormula (formula, null, CTLOperator.CTL_EG, null);
        }

        /// <summary>
        ///   Возвращает формулу "A[<paramref name = "left" /> U <paramref name = "right" />]".
        /// </summary>
        /// <remarks>
        ///   Темпоральная формула "A[f U g]" будет представлена в виде "(NOT E[(NOT f) U ((NOT f) AND (NOT g))]) AND (NOT (EG (NOT g)))".
        /// </remarks>
        /// <param name = "left">Левый операнд.</param>
        /// <param name = "right">Правый операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> является <c>null</c>.</exception>
        /// <seealso cref = "EU" />
        /// <seealso cref = "EG" />
        /// <seealso cref = "Not" />
        /// <seealso cref = "And" />
        /// <returns>Темпоральное "AU" двух формул.</returns>
        public static CTLFormula AU (CTLFormula left, CTLFormula right) {
            return (!EU (!right, (!left) & (!right))) & (!EG (!right));
        }

        /// <summary>
        ///   Возвращает формулу "E[<paramref name = "left" /> U <paramref name = "right" />]".
        /// </summary>
        /// <param name = "left">Левый операнд.</param>
        /// <param name = "right">Правый операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> является <c>null</c>.</exception>
        /// <returns>Темпоральное "EU" двух формул.</returns>
        public static CTLFormula EU (CTLFormula left, CTLFormula right) {
            if (left == null)
                throw new ArgumentNullException ("left");

            if (right == null)
                throw new ArgumentNullException ("right");

            return new CTLFormula (left, right, CTLOperator.CTL_EU, null);
        }

        /// <summary>
        ///   Возвращает формулу "A[<paramref name = "left" /> R <paramref name = "right" />]".
        /// </summary>
        /// <remarks>
        ///   Темпоральная формула "A[f R g]" будет представлена в виде "NOT E[(NOT f) U (NOT g)]".
        /// </remarks>
        /// <param name = "left">Левый операнд.</param>
        /// <param name = "right">Правый операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> является <c>null</c>.</exception>
        /// <seealso cref = "EU" />
        /// <seealso cref = "Not" />
        /// <returns>Темпоральное "AR" двух формул.</returns>
        public static CTLFormula AR (CTLFormula left, CTLFormula right) {
            return !EU (!left, !right);
        }

        /// <summary>
        ///   Возвращает формулу "E[<paramref name = "left" /> R <paramref name = "right" />]".
        /// </summary>
        /// <remarks>
        ///   Темпоральная формула "E[f R g]" будет представлена в виде "NOT A[(NOT f) U (NOT g)]".
        /// </remarks>
        /// <param name = "left">Левый операнд.</param>
        /// <param name = "right">Правый операнд.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "left" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "right" /> является <c>null</c>.</exception>
        /// <seealso cref = "AU" />
        /// <seealso cref = "Not" />
        /// <returns>Темпоральное "ER" двух формул.</returns>
        public static CTLFormula ER (CTLFormula left, CTLFormula right) {
            return !AU (!left, !right);
        }

        internal MjuFormula Tr () {
            var index = 0;
            return this.Tr (ref index);
        }

        internal MjuFormula Tr (ref int q_index) {
            switch (this.Operator) {
                case CTLOperator.TRUE :
                    return MjuFormula.TRUE;

                case CTLOperator.FALSE :
                    return MjuFormula.FALSE;

                case CTLOperator.LOGIC_NOT :
                    return MjuFormula.Not (this.Left.Tr (ref q_index));

                case CTLOperator.LOGIC_AND :
                    return MjuFormula.And (this.Left.Tr (ref q_index), this.Right.Tr (ref q_index));

                case CTLOperator.LOGIC_OR :
                    return MjuFormula.Or (this.Left.Tr (ref q_index), this.Right.Tr (ref q_index));

                case CTLOperator.CTL_EX :
                    return MjuFormula.ExistTrans (this.Left.Tr (ref q_index));

                case CTLOperator.CTL_EG : {
                    var v = string.Format ("Q{0}", q_index ++);
                    return
                        MjuFormula.GreatestFixedPoint (
                            MjuFormula.And (this.Left.Tr (ref q_index), MjuFormula.ExistTrans (MjuFormula.Var (v))), v);
                }

                case CTLOperator.CTL_EU : {
                    var v = string.Format ("Q{0}", q_index++);
                    return
                        MjuFormula.LeastFixedPoint (
                            MjuFormula.Or (this.Right.Tr (ref q_index),
                                           MjuFormula.And (
                                               this.Left.Tr (ref q_index), MjuFormula.ExistTrans (MjuFormula.Var (v)))
                                ),
                            v);
                }

                default :
                    return MjuFormula.Atomic (this.Atomic);
            }
        }

        /// <inheritdoc />
        public override bool Equals (object obj) {
            if (ReferenceEquals (null, obj))
                return false;
            if (ReferenceEquals (this, obj))
                return true;
            return obj.GetType () == typeof (CTLFormula) && this.Equals ((CTLFormula) obj);
        }

        /// <summary>
        ///   Переводит строковое представление формулы логики CTL в
        ///   эквивалентный экземпляр <see cref = "CTLFormula" />.
        /// </summary>
        /// <param name = "s">Строка.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "s" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentException"><paramref name = "s" /> не содержит формулу.</exception>
        /// <returns>Формула логики CTL.</returns>
        public static CTLFormula Parse (string s) {
            if (s == null)
                throw new ArgumentNullException ("s");

            try {
                using (var memoryStream = new MemoryStream (Encoding.Default.GetBytes (s))) {
                    var stream = new ANTLRInputStream (memoryStream);
                    var parser = new CTLParserParser (new CommonTokenStream (new CTLParserLexer (stream)));
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
                case CTLOperator.TRUE :
                    return "TRUE";
                case CTLOperator.FALSE :
                    return "FALSE";
                case CTLOperator.NONE :
                    return string.Format ("{{{0}}}", this.Atomic);
                case CTLOperator.LOGIC_NOT :
                    return string.Format ("not ({0})", this.Left);
                case CTLOperator.LOGIC_AND :
                    return string.Format ("({0}) and ({1})", this.Left, this.Right);
                case CTLOperator.LOGIC_OR :
                    return string.Format ("({0}) or ({1})", this.Left, this.Right);
                case CTLOperator.CTL_EX :
                    return string.Format ("EX ({0})", this.Left);
                case CTLOperator.CTL_EG :
                    return string.Format ("EG ({0})", this.Left);
                case CTLOperator.CTL_EU :
                    return string.Format ("E[({0}) U ({1})]", this.Left, this.Right);
                default :
                    return string.Empty;
            }
        }

        private bool Equals (CTLFormula other) {
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
