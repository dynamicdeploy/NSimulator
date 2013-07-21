#region

using System;
using System.Collections.Generic;
using System.Linq;
using NModel.Execution;

#endregion

namespace NModel.Extension {
    /// <summary>
    ///   Модель, заданная средствами NModel.
    /// </summary>
    /// <seealso cref = "IModel" />
    /// <seealso cref = "ExplicitModel" />
    public sealed class Model : IModel {
        private readonly Dictionary <int, List <string>> states;
        private readonly Dictionary <int, List <int>> transitions;

        /// <summary>
        ///   Инициализация модели.
        /// </summary>
        /// <param name = "program">Модель программы.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "program" /> является <c>null</c>.</exception>
        /// <remarks>
        ///   При инициализации модели совершается полный обход пространства
        ///   состояний. Состояния должны иметь тип <see cref = "SimpleState" />.
        ///   Списком атомарных высказываний назначается список всевозможных
        ///   существующих пар <c>x=y</c>, где <c>x</c> - имя переменной,
        ///   <c>y</c> - её значение.
        /// </remarks>
        public Model (ModelProgram program) {
            if (program == null)
                throw new ArgumentNullException ("program");

            this.states = new Dictionary <int, List <string>> ();
            this.transitions = new Dictionary <int, List <int>> ();

            this.Prepare (program);
        }

        #region IModel Members

        /// <inheritdoc />
        public List <string> this [int index] {
            get { return this.states [index]; }
        }

        /// <inheritdoc />
        public List <int> States {
            get { return this.states.Keys.ToList (); }
        }

        /// <inheritdoc />
        public int StatesCount {
            get { return this.states.Count; }
        }

        /// <inheritdoc />
        public List <int> Transitions (int from) {
            return this.transitions [from];
        }

        /// <inheritdoc />
        public bool HasTransition (int from, int to) {
            return this.transitions.ContainsKey (from) && this.transitions [from].Contains (to);
        }

        #endregion

        private void Prepare (ModelProgram program) {
            var actionSymbols = program.ActionSymbols ();
            var _states = new Dictionary <SimpleState, int> ();
            var _trans = new Dictionary <SimpleState, List <SimpleState>> ();
            var frontier = new LinkedList <IState> (new [] { program.InitialState });
            var stateIndex = 0;

            while (frontier.Count > 0) {
                var state = (SimpleState) frontier.First.Value;
                frontier.RemoveFirst ();

                if (! _states.ContainsKey (state))
                    _states.Add (state, stateIndex ++);

                foreach (var action in actionSymbols.
                    Where (symbol => program.IsPotentiallyEnabled (state, symbol)).
                    SelectMany (symbol => program.GetActions (state, symbol))) {
                    TransitionProperties properties;
                    var target =
                        (SimpleState) program.GetTargetState (state, action, Set <string>.EmptySet, out properties);

                    if (! _trans.ContainsKey (state))
                        _trans.Add (state, new List <SimpleState> ());
                    _trans [state].Add (target);

                    if (! (_states.ContainsKey (target) || frontier.Contains (target)))
                        frontier.AddFirst (target);
                }
            }

            foreach (var state in _states) {
                var list = new List <string> ();

                for (var i = 0; i < state.Key.LocationValuesCount; ++ i)
                    list.Add (string.Format ("{0}={1}", state.Key.GetLocationName (i), state.Key.GetLocationValue (i)));

                this.states.Add (state.Value, list);
            }

            foreach (var transition in _trans) {
                this.transitions.Add (_states [transition.Key],
                                      transition.Value.Select (state => _states [state]).ToList ());
            }

            for (var i = 0; i < stateIndex; ++ i) {
                if (! this.transitions.ContainsKey (i))
                    this.transitions.Add (i, new List <int> ());
            }
        }
    }
}
