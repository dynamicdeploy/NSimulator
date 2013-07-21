#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ��������� �����.
    ///   ��������������� ��������� ������ ��� ������.
    /// </summary>
    /// <seealso cref = "IClock" />
    public interface IClockView {
        /// <summary>
        ///   �������� ����� ���� � ������������.
        /// </summary>
        /// <value>
        ///   ����� ���� � ������������.
        /// </value>
        ulong TickLength { get; }

        /// <summary>
        ///   �������� ����� �������� ����.
        /// </summary>
        /// <value>
        ///   ����� �������� ����.
        /// </value>
        ulong CurrentTick { get; }

        /// <summary>
        ///   �������� ������� ������ �����.
        /// </summary>
        /// <value>
        ///   <c>true</c>, ���� � ������� ������ ���� ��������������.
        /// </value>
        bool IsSuspended { get; }

        /// <summary>
        ///   �������� ������� �����.
        /// </summary>
        /// <remarks>
        ///   ������� ����� ������ ���������� �� 00:00:00 1 ������ 1970 ����,
        ///   �� ��� ����� �������� �� ����������. �������� ����� ����������� �����
        ///   ����� ���� <see cref = "TickLength" /> � ����� �������� ���� <see cref = "CurrentTick" />.
        /// </remarks>
        /// <value>
        ///   ������� �����.
        /// </value>
        DateTime CurrentTime { get; }

        /// <summary>
        ///   ���������� ��� ������ ��������� ������������������� ��������.
        /// </summary>
        event ClockActionErrorEventHandler OnError;
    }
}
