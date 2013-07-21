#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������, �������������� ��� ��������� ������������ ����� ����������.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� ���������� � ����������
    ///   ����������� ����������, ��� �������� ��������� � ������ ����������
    ///   ������� ���������� � ��� �� ����������.
    /// </remarks>
    [Serializable]
    public class InterfaceNameMustBeUniqueException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "iface_name">��� ����������.</param>
        /// <param name = "device_name">��� ����������.</param>
        public InterfaceNameMustBeUniqueException (string iface_name, string device_name)
            : base (string.Format (Strings.InterfaceNameMustBeUniqueException, iface_name, device_name)) {
            this.Interface = iface_name;
            this.Device = device_name;
        }

        /// <summary>
        ///   �������� �������� ����������.
        /// </summary>
        /// <value>
        ///   �������� ����������, ������� �� �������� ����������.
        /// </value>
        public string Interface { get; private set; }

        /// <summary>
        ///   �������� �������� ����������.
        /// </summary>
        /// <value>
        ///   �������� ����������, ��� ����������� ��������� � ��������� <see cref = "Interface" />.
        /// </value>
        public string Device { get; private set; }
    }
}
