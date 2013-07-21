#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������, �������������� ��� ������� ��������� �������������� ������.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� ���������� ������ � �������������
    ///   ����������� �� ������ ������.
    /// </remarks>
    [Serializable]
    public sealed class UnknownModuleLevelException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "className">�������� ������.</param>
        public UnknownModuleLevelException (string className)
            : base (string.Format (Strings.UnknownModuleLevelException, className)) {
            this.ClassName = className;
        }

        /// <summary>
        ///   �������� �������� ������.
        /// </summary>
        /// <value>
        ///   �������� ������.
        /// </value>
        public string ClassName { get; private set; }
    }
}
