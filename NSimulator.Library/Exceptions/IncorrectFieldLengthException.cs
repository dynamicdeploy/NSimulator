#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   ����������, �������������� ��� ������������ ����� ���� ������.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� ������������� ���� ������
    ///   ���������, ������� �������� �����.
    /// </remarks>
    [Serializable]
    public sealed class IncorrectFieldLengthException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "field">�������� ����.</param>
        /// <param name = "actual">���������� �����.</param>
        /// <param name = "expected">��������� �����.</param>
        public IncorrectFieldLengthException (string field, int actual, int expected)
            : base (string.Format (Strings.IncorrectFieldLengthException, field, actual, expected)) {
            this.Field = field;
        }

        /// <summary>
        ///   �������� �������� ����.
        /// </summary>
        /// <value>
        ///   �������� ����, ������� ������� �������������������.
        /// </value>
        public string Field { get; private set; }

        /// <summary>
        ///   �������� ���������� ����� ������.
        /// </summary>
        /// <value>
        ///   ����� ������, �������� ���� ����������� ������� �������������.
        /// </value>
        public int Actual { get; private set; }

        /// <summary>
        ///   �������� ����� ����.
        /// </summary>
        /// <value>
        ///   ����� ����. ������ ��������� � �������� ������.
        /// </value>
        public int Expected { get; private set; }
    }
}
