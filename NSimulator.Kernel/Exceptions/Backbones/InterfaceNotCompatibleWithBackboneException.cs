#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������, �������������� ��� ��������������� ����������
    ///   �� ������ �������� ������.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� ���������� ��������
    ///   ���������� � ����� ��������, � ������� ��������� �����������.
    /// </remarks>
    [Serializable]
    public sealed class InterfaceNotCompatibleWithBackboneException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "iface_name">��� ����������.</param>
        /// <param name = "backbone">��� ����� ��������.</param>
        public InterfaceNotCompatibleWithBackboneException (string iface_name, string backbone)
            : base (string.Format (Strings.InterfaceNotCompatibleWithBackboneException, iface_name, backbone)) {
            this.Interface = iface_name;
            this.Backbone = backbone;
        }

        /// <summary>
        ///   �������� �������� ����������.
        /// </summary>
        /// <value>
        ///   �������� ����������, �������������� �� ������ �������� ������.
        /// </value>
        public string Interface { get; private set; }

        /// <summary>
        ///   �������� ����� �������� ������.
        /// </summary>
        /// <value>
        ///   ����� �������� ������, ������������� � ����������� <see cref = "Interface" />.
        /// </value>
        public string Backbone { get; private set; }
    }
}
