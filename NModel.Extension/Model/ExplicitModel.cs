#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace NModel.Extension {
    /// <summary>
    ///   ������, �������� ����.
    /// </summary>
    /// <seealso cref = "IModel" />
    /// <seealso cref = "Model" />
    public sealed class ExplicitModel : IModel {
        private readonly Dictionary <int, List <string>> states;
        private readonly Dictionary <int, List <int>> transitions;

        /// <summary>
        ///   ������������� ������.
        /// </summary>
        /// <remarks>
        ///   ����� ������������� �������� ������ ������.
        /// </remarks>
        public ExplicitModel () {
            this.states = new Dictionary <int, List <string>> ();
            this.transitions = new Dictionary <int, List <int>> ();
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

        /// <summary>
        ///   ��������� ��������� � ������.
        /// </summary>
        /// <remarks>
        ///   �������� <paramref name = "labels" /> �������� ������ ��������� ������������,
        ///   ������������� � ��������� ������.
        /// </remarks>
        /// <param name = "labels">������ ��������� ������������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "labels" /> �������� <c>null</c>.</exception>
        public void AddState (params string [] labels) {
            if (labels == null)
                throw new ArgumentNullException ("labels");

            var stateIndex = this.states.Count;
            this.states.Add (stateIndex, labels.ToList ());
            this.transitions.Add (stateIndex, new List <int> ());
        }

        /// <summary>
        ///   ��������� ������� � ������.
        /// </summary>
        /// <param name = "from">��������� ���������.</param>
        /// <param name = "to">�������� ���������.</param>
        /// <exception cref = "ArgumentOutOfRangeException">��������� <paramref name = "from" /> �� ����������.</exception>
        /// <exception cref = "ArgumentOutOfRangeException">��������� <paramref name = "to" /> �� ����������.</exception>
        public void AddTransition (int from, int to) {
            if (from < 0 || from >= this.StatesCount)
                throw new ArgumentOutOfRangeException ("from");

            if (to < 0 || to >= this.StatesCount)
                throw new ArgumentOutOfRangeException ("to");

            this.transitions [from].Add (to);
        }
    }
}
