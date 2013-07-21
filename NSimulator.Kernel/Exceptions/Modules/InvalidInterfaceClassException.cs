#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������, �������������� ��� ������������� �������� ������ �������� �����.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� ���������������� ������� ������
    ///   ����������� � ����, �� ���������� ����������� ���������� <see cref = "IInterfaceView" />.
    /// </remarks>
    [Serializable]
    public sealed class InvalidInterfaceClassException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "className">�������� ������.</param>
        public InvalidInterfaceClassException (string className)
            : base (string.Format (Strings.InvalidInterfaceClassException, className)) {
            this.ClassName = className;
        }

        /// <summary>
        ///   �������� �������� ������.
        /// </summary>
        /// <value>
        ///   �������� ������, �� ������������ ��������� <see cref = "IInterfaceView" />.
        /// </value>
        public string ClassName { get; private set; }
    }
}
