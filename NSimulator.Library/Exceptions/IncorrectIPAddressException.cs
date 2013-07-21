#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   ����������, �������������� ��� �������� ������ IP-������.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� �������� ������ � IP-�����,
    ///   ��� ���� ������ ����� �������� ������.
    /// </remarks>
    [Serializable]
    public sealed class IncorrectIPAddressException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "address">������ � IP-�������.</param>
        public IncorrectIPAddressException (string address)
            : base (string.Format (Strings.IncorrectIPAddressException, address)) {
            this.Address = address;
        }

        /// <summary>
        ///   �������� ������ � IP-�������.
        /// </summary>
        /// <value>
        ///   ������ � IP-�������, ����������������� �������.
        /// </value>
        public string Address { get; private set; }
    }
}
