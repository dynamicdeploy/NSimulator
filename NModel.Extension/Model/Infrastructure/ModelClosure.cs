#region

using System;
using System.Collections.Generic;

#endregion

namespace NModel.Extension {
    /// <summary>
    ///   Тотальное замыкание модели.
    /// </summary>
    /// <remarks>
    ///   Тотальное замыкание модели строится следующим образом.
    ///   Если из какого-то состояния модели не существует переходов,
    ///   то добавляется переход-петля.
    /// </remarks>
    /// <seealso cref = "ModelTotalityChecker" />
    /// <seealso cref = "IModel" />
    public sealed class ModelClosure : IModel {
        private readonly IModel model;

        /// <summary>
        ///   Инициализирует тотальное замыкание модели.
        /// </summary>
        /// <param name = "model">Замыкаемая модель.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "model" /> является <c>null</c>.</exception>
        public ModelClosure (IModel model) {
            if (model == null)
                throw new ArgumentNullException ("model");

            this.model = model;
        }

        #region IModel Members

        /// <inheritdoc />
        public List <string> this [int index] {
            get { return this.model [index]; }
        }

        /// <inheritdoc />
        public List <int> States {
            get { return this.model.States; }
        }

        /// <inheritdoc />
        public int StatesCount {
            get { return this.model.StatesCount; }
        }

        /// <inheritdoc />
        public List <int> Transitions (int from) {
            var r = this.model.Transitions (from);
            return r.Count == 0 ? new List <int> (new [] { from }) : r;
        }

        /// <inheritdoc />
        public bool HasTransition (int from, int to) {
            return this.Transitions (from).Contains (to);
        }

        #endregion
    }
}
