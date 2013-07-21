#region

using System.Collections.Generic;

#endregion

namespace NModel.Extension {
    /// <summary>
    ///   Верификатор.
    /// </summary>
    /// <typeparam name = "T">Тип верифицируемых темпоральных формул.</typeparam>
    public abstract class Verifier <T>
        where T : ITemporalFormula {
        /// <summary>
        ///   Формула темпоральной логики.
        /// </summary>
        protected readonly T formula;

        /// <summary>
        ///   Верифицируемая модель.
        /// </summary>
        protected readonly IModel model;

        /// <summary>
        ///   Инициализирует верификатор верифицируемой моделью и формулой.
        /// </summary>
        /// <param name = "model">Верифицируемая модель.</param>
        /// <param name = "formula">Формула.</param>
        protected Verifier (IModel model, T formula) {
            this.model = model;
            this.formula = formula;
        }

        /// <summary>
        ///   Получает множество состояний, в которых выполняется формула.
        /// </summary>
        /// <value>
        ///   Множество состояний, в которых выполняется формула.
        /// </value>
        public abstract IEnumerable <int> States { get; }

        /// <summary>
        ///   Проверяет выполнимость формулы в состоянии с номером <paramref name = "state" /> модели.
        /// </summary>
        /// <param name = "state">Номер состояния.</param>
        /// <returns><c>true</c>, если формула выполняется.</returns>
        public abstract bool CheckState (int state);
        }
}
