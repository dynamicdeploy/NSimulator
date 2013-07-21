using System;

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������, �������������� ��� ��������� ������ � ����������� �������.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� �������� � ��������� ������� ��������
    ///   ������, ������� ��� �������� � ���������.
    /// </remarks>
    [Serializable]
    public sealed class ModuleAlreadyLoadedException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "className">�������� ������.</param>
        public ModuleAlreadyLoadedException (string className)
            : base (string.Format (Strings.ModuleAlreadyLoadedException, className)) {
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
