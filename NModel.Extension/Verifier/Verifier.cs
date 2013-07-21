#region

using System.Collections.Generic;

#endregion

namespace NModel.Extension {
    /// <summary>
    ///   �����������.
    /// </summary>
    /// <typeparam name = "T">��� �������������� ������������ ������.</typeparam>
    public abstract class Verifier <T>
        where T : ITemporalFormula {
        /// <summary>
        ///   ������� ������������ ������.
        /// </summary>
        protected readonly T formula;

        /// <summary>
        ///   �������������� ������.
        /// </summary>
        protected readonly IModel model;

        /// <summary>
        ///   �������������� ����������� �������������� ������� � ��������.
        /// </summary>
        /// <param name = "model">�������������� ������.</param>
        /// <param name = "formula">�������.</param>
        protected Verifier (IModel model, T formula) {
            this.model = model;
            this.formula = formula;
        }

        /// <summary>
        ///   �������� ��������� ���������, � ������� ����������� �������.
        /// </summary>
        /// <value>
        ///   ��������� ���������, � ������� ����������� �������.
        /// </value>
        public abstract IEnumerable <int> States { get; }

        /// <summary>
        ///   ��������� ������������ ������� � ��������� � ������� <paramref name = "state" /> ������.
        /// </summary>
        /// <param name = "state">����� ���������.</param>
        /// <returns><c>true</c>, ���� ������� �����������.</returns>
        public abstract bool CheckState (int state);
        }
}
