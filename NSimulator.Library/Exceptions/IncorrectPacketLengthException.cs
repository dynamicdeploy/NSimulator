#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   ����������, �������������� ��� ������������ ����� ������.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� ������������� ������
    ///   �������, �������� �������� �����.
    /// </remarks>
    [Serializable]
    public class IncorrectPacketLengthException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "packet">�������� ������.</param>
        public IncorrectPacketLengthException (string packet)
            : base (string.Format (Strings.IncorrectPacketLengthException, packet)) {
            this.Packet = packet;
        }

        /// <summary>
        ///   �������� �������� ������.
        /// </summary>
        /// <value>
        ///   �������� ������, ������� ������� ������������������.
        /// </value>
        public string Packet { get; private set; }
    }
}
