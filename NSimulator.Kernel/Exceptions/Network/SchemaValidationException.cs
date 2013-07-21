#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������, �������������� ��� ������ ��������� ��������� xsd-������.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� ��������� ������������ xml-��������
    ///   ��������� ���� (�� ��������������� xsd-�����).
    /// </remarks>
    [Serializable]
    public sealed class SchemaValidationException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "exception">���������� ���������� ���������.</param>
        public SchemaValidationException (Exception exception)
            : base (string.Format (Strings.SchemaValidationException, exception.Message)) {
            this.ValidationException = exception;
        }

        /// <summary>
        ///   �������� ����������, ��������� ��� ���������.
        /// </summary>
        /// <value>
        ///   ����������, ��������� ��� ���������.
        /// </value>
        public Exception ValidationException { get; private set; }
    }
}
