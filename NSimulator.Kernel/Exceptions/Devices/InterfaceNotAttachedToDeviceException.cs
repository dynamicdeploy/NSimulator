#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������, �������������� ��� ��������������� �������������
    ///   ����������� ���������� � ����������.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     ���������� �������������� ��� ������� ���������� ��������
    ///     ����������� ���������� � ����������, ������� �� ��������
    ///     �������������� ����������.
    ///   </para>
    /// 
    ///   <para>
    ///     ���������� ����� ����� ���� ���������� ��� ������� ��������� �
    ///     "���������" ����������� ���������� ������ �� ����������, �� ���
    ///     ���� ������� ���������� �� ��������� � ����������.
    ///   </para>
    /// </remarks>
    [Serializable]
    public class InterfaceNotAttachedToDeviceException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "iface_name">��� �������������� ����������.</param>
        /// <param name = "device_name">��� ����������, � ������� ��������� �����������.</param>
        public InterfaceNotAttachedToDeviceException (string iface_name, string device_name)
            : base (string.Format (Strings.InterfaceNotAttachedToDeviceException2, iface_name, device_name)) {
            this.Interface = iface_name;
            this.Device = device_name;
        }

        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "iface_name">��� �������������� ����������.</param>
        public InterfaceNotAttachedToDeviceException (string iface_name)
            : base (string.Format (Strings.InterfaceNotAttachedToDeviceException1, iface_name)) {
            this.Interface = iface_name;
            this.Device = null;
        }

        /// <summary>
        ///   �������� �������� ����������.
        /// </summary>
        /// <value>
        ///   �������� ����������, ������� ����������� � ����������.
        /// </value>
        public string Interface { get; private set; }

        /// <summary>
        ///   �������� �������� ����������.
        /// </summary>
        /// <value>
        ///   �������� ����������, � ������� ����������� ��������� <see cref = "Interface" />
        ///   ��� <c>null</c>, ���� ��������� �� ���������� �� � ����� ����������.
        /// </value>
        public string Device { get; private set; }
    }
}
