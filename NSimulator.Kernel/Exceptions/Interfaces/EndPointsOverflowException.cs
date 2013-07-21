#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������, �������������� ��� ������������ ����������
    ///   �������� ����� � ����� �������� ������.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� ���������� ����������
    ///   � ����� ��������, ��� ���� ����� �������� �� ����� ����� ����������
    ///   ����������� ������, ��� ��� ����.
    /// </remarks>
    [Serializable]
    public sealed class EndPointsOverflowException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "backbone">����� �������� ������.</param>
        /// <param name = "capacity">������������ ���������� �������� �����.</param>
        public EndPointsOverflowException (string backbone, int capacity)
            : base (string.Format (Strings.EndPointsOverflowException, backbone, capacity)) {
            this.Backbone = backbone;
            this.Capacity = capacity;
        }

        /// <summary>
        ///   �������� ����� �������� ������.
        /// </summary>
        /// <value>
        ///   ����� �������� ������, � ������� ���������� ������������ ����������
        ///   �������� �����.
        /// </value>
        public string Backbone { get; private set; }

        /// <summary>
        ///   �������� ������������ ����� �������� �����.
        /// </summary>
        /// <value>
        ///   ������������� ���������� �������� ����� �� ����� �������� <see cref = "Backbone" />.
        /// </value>
        public int Capacity { get; private set; }
    }
}
