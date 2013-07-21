#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ����������, �������������� ��� ������������� ������������� ��������� ��������.
    /// </summary>
    /// <remarks>
    ///   ���������� �������������� ��� ������� �������� ��������, ������������������
    ///   � ����� �� ��������� ���������.
    /// </remarks>
    [Serializable]
    public sealed class InvalidClockHandlerException : NSimulatorException {
        /// <summary>
        ///   ������������� ������ ���������� ����������.
        /// </summary>
        /// <param name = "handler">��������� ��������.</param>
        public InvalidClockHandlerException (ClockHandler handler)
            : base (string.Format (Strings.InvalidClockHandlerException, handler)) {
            this.Handler = handler;
        }

        /// <summary>
        ///   �������� ��������� ��������.
        /// </summary>
        /// <value>
        ///   ��������� ��������� ��������.
        /// </value>
        public ClockHandler Handler { get; private set; }
    }
}
