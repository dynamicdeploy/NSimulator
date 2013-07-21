#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������, �������������� ��� �������� ���������� ���������.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� ������ ��������� ���������, ��� ����
    ///   ���������� ���������� ������, ��� ������� ��������.
    /// </remarks>
    [Serializable]
    public sealed class NotEnoughParametersException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "context">�������� ���������.</param>
        /// <param name = "actual">���������� ���������� ����������.</param>
        /// <param name = "expected">����������� ���������� ����������.</param>
        public NotEnoughParametersException (string context, int actual, int expected)
            : base (string.Format (Strings.NotEnoughParametersException, context, expected, actual)) {
            this.Context = context;
            this.ActualParameters = actual;
            this.ExpectedParameters = expected;
        }

        /// <summary>
        ///   �������� �������� ���������.
        /// </summary>
        /// <value>
        ///   �������� ���������, �������� �������� �������� ����� ����������.
        /// </value>
        public string Context { get; private set; }

        /// <summary>
        ///   �������� ���������� ���������� ����������.
        /// </summary>
        /// <value>
        ///   ���������� ��������� ���������� ��������� <see cref = "Context" />.
        /// </value>
        public int ActualParameters { get; private set; }

        /// <summary>
        ///   �������� ��������� ���������� ����������.
        /// </summary>
        /// <value>
        ///   ��������� ���������� <see cref = "Context" /> ���������� ����������.
        /// </value>
        public int ExpectedParameters { get; private set; }
    }
}
