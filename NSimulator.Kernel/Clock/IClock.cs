#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ��������� �����.
    ///   ��������������� ���������� ���������� ������ ��� ������ �� ������� ����������.
    /// </summary>
    /// <seealso cref = "IClockView" />
    [ContractClass (typeof (Contract_IClock))]
    public interface IClock : IClockView, IDisposable {
        /// <summary>
        ///   ��������� ����������� ��������, ������� ��������� � �������� ���.
        /// </summary>
        /// <param name = "tick">����� ����, � ������� ����� ��������� ��������.</param>
        /// <param name = "action">��������, ������� ������ ���������.</param>
        /// <returns>��������� ������������������� �������� ��� <c>null</c>, ���� �������� ���������������� ���������.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "action" /> �������� <c>null</c>.</exception>
        ClockHandler RegisterAction (ulong tick, ClockAction action);

        /// <summary>
        ///   ��������� ����������� ��������, ������� ������ ��������� � ��������� ���.
        /// </summary>
        /// <param name = "action">��������, ������� ������ ���������.</param>
        /// <returns>��������� ������������������� �������� ��� <c>null</c>, ���� �������� ���������������� ���������.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "action" /> �������� <c>null</c>.</exception>
        ClockHandler RegisterAction (ClockAction action);

        /// <summary>
        ///   ��������� ����������� ��������, ������� ������ ��������� ����� �������� �����.
        /// </summary>
        /// <param name = "delta_time">����� � ������������, ����� ������� �������� ������ ���������.</param>
        /// <param name = "action">��������, ������� ������ ���������.</param>
        /// <returns>��������� ������������������� �������� ��� <c>null</c>, ���� �������� ���������������� ���������.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "action" /> �������� <c>null</c>.</exception>
        ClockHandler RegisterActionAtTime (ulong delta_time, ClockAction action);

        /// <summary>
        ///   ��������� ����������� ��������, ������� ������ ��������� �� ��������� �������.
        /// </summary>
        /// <param name = "condition">������� ������������� ��������.</param>
        /// <param name = "action">��������, ������� ������ ���������.</param>
        /// <returns>��������� ������������������� �������� ��� <c>null</c>, ���� �������� ���������������� ���������.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "condition" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "action" /> �������� <c>null</c>.</exception>
        ClockHandler RegisterConditionalAction (Func <bool> condition, ClockAction action);

        /// <summary>
        ///   �������� ����������� ��������.
        /// </summary>
        /// <param name = "handler">��������, ����������� �������� ����� ��������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "handler" /> �������� <c>null</c>.</exception>
        /// <exception cref = "InvalidClockHandlerException">��������� �������� <paramref name = "handler" /> �������� ��������.</exception>
        void RemoveAction (ClockHandler handler);

        /// <summary>
        ///   ��������� ������ ��������� ��������.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     � ����������� �� ����������, ��������� �������� ����� ����������� ��� � ������,
        ///     �� �������� ��� ������ �����, ��� � ����� ���� ������ ��������� ����� � ����������
        ///     ��������.
        ///   </para>
        /// 
        ///   <para>
        ///     ����� ������� �������� ��������� �������� ������� ����� ���� �������������, ������ �����
        ///     <see cref = "IClock.Suspend" />, ���� �����������, ������ ����� <see cref = "IClock.Resume" />.
        ///     ����� ��������� ���������� ���������, ����� ������� ����� <see cref = "IDisposable.Dispose" />.
        ///     ����� ������ ����� ������ ��������� ����������� ����� ����������.
        ///   </para>
        /// </remarks>
        /// <exception cref = "InvalidOperationException">���� ����������.</exception>
        void Start ();

        /// <summary>
        ///   ��������� ������������ ��������� ��������.
        /// </summary>
        void Suspend ();

        /// <summary>
        ///   ��������� ������������� ��������� ��������.
        /// </summary>
        void Resume ();
    }
}
