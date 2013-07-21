#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace NModel.Extension {
    /// <summary>
    ///   Алгоритм верификации Эмерсона-Ли для логики CTL.
    /// </summary>
    /// <seealso cref = "Verifier{T}" />
    /// <seealso cref = "CTLFormula" />
    public sealed class EmersonLieVerifier : Verifier <CTLFormula> {
        private readonly List <int> states;

        /// <summary>
        ///   Инициализирует верификатор верифицируемой моделью и формулой.
        /// </summary>
        /// <exception cref = "ArgumentNullException"><paramref name = "model" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "formula" /> является <c>null</c>.</exception>
        /// <param name = "model">Верифицируемая модель.</param>
        /// <param name = "formula">Формула логики CTL.</param>
        public EmersonLieVerifier (IModel model, CTLFormula formula)
            : base (model, formula) {
            if (model == null)
                throw new ArgumentNullException ("model");

            if (formula == null)
                throw new ArgumentNullException ("formula");

            this.states = this.Verify (this.formula.Tr (), new Dictionary <string, List <int>> ());
        }

        /// <inheritdoc />
        public override IEnumerable <int> States {
            get { return this.states; }
        }

        /// <inheritdoc />
        public override bool CheckState (int state) {
            return this.states.Contains (state);
        }

        private List <int> Verify (MjuFormula f, IDictionary <string, List <int>> e) {
            switch (f.Operator) {
                case MjuOperator.FALSE :
                    return new List <int> ();

                case MjuOperator.TRUE :
                    return this.model.States;

                case MjuOperator.ATOMIC :
                    return (this.model.States.Where (s => this.model [s].Contains (f.Name))).ToList ();

                case MjuOperator.VARIABLE :
                    return e.ContainsKey (f.Name) ? e [f.Name] : new List <int> ();

                case MjuOperator.LOGIC_NOT : {
                    var r = this.Verify (f.Left, e);
                    return (this.model.States.Where (s => !r.Contains (s))).ToList ();
                }

                case MjuOperator.LOGIC_AND :
                    return this.Verify (f.Left, e).Intersect (this.Verify (f.Right, e)).ToList ();

                case MjuOperator.LOGIC_OR :
                    return this.Verify (f.Left, e).Union (this.Verify (f.Right, e)).ToList ();

                case MjuOperator.MJU_EXISTS : {
                    var ev = this.Verify (f.Left, e);
                    return (this.model.States.Where (s => this.model.Transitions (s).Exists (ev.Contains))).ToList ();
                }

                case MjuOperator.MJU_ALL : {
                    var ev = this.Verify (f.Left, e);
                    return (this.model.States.Where (
                        s => this.model.States.All (t => (!this.model.HasTransition (s, t)) || ev.Contains (t)))).ToList
                        ();
                }

                case MjuOperator.MJU_LFP : {
                    var new_value = new List <int> ();
                    List <int> old_value;
                    do {
                        old_value = new List <int> (new_value);
                        var e1 = new Dictionary <string, List <int>> (e);

                        var v = f.Right.Name;
                        if (! e1.ContainsKey (v))
                            e1.Add (v, new List <int> ());
                        e1 [v] = new_value;

                        new_value = this.Verify (f.Left, e1);
                    } while (! new_value.SequenceEqual (old_value));
                    return new_value;
                }

                case MjuOperator.MJU_GFP : {
                    var new_value = this.model.States;
                    List <int> old_value;
                    do {
                        old_value = new List <int> (new_value);
                        var e1 = new Dictionary <string, List <int>> (e);

                        var v = f.Right.Name;
                        if (! e1.ContainsKey (v))
                            e1.Add (v, new List <int> ());
                        e1 [v] = new_value;

                        new_value = this.Verify (f.Left, e1);
                    } while (! new_value.SequenceEqual (old_value));
                    return new_value;
                }

                default :
                    return new List <int> ();
            }
        }
    }
}
