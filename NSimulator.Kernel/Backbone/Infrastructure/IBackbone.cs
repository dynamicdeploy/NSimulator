#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ��������� ����� �������� ������.
    ///   ��������������� ���������� ���������� ������ ��� ������ �� ������� ����������.
    /// </summary>
    /// <seealso cref = "IBackboneView" />
    [ContractClass (typeof (Contract_IBackbone))]
    public interface IBackbone : IBackboneView {
        /// <summary>
        ///   ������������� �������� ����� �������� ������.
        /// </summary>
        /// <param name = "name">����� �������� ����� ��������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> �������� <c>null</c>.</exception>
        void SetName (string name);

        /// <summary>
        ///   ����������� ��������� � �������� ����� ����� ��������.
        /// </summary>
        /// <remarks>
        ///   ����� ������ ������ ��������� �������� ������������ �������� � ��������� �.
        ///   �������� ����� ������ ��������� ���������� <see cref = "IInterface.SetBackbone" />.
        /// </remarks>
        /// <param name = "iface">���������, ������� ���������� ��������� � �������� �����.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> �������� <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToBackboneException">��������� <paramref name = "iface" /> ��� �������� � ����� ��������.</exception>
        /// <exception cref = "EndPointsOverflowException">���������� �������� ����� ��������� �����.</exception>
        void AttachEndPoint (IInterfaceView iface);

        /// <summary>
        ///   ���������� �������� ����� ����� �������� ������ �� ����������.
        /// </summary>
        /// <remarks>
        ///   ����� ������ ������ ��������� �������� ������������ ������� � ��������� �.
        ///   �������� ����� ������ ���������� ���������� <see cref = "IInterface.ReleaseBackbone" />.
        /// </remarks>
        /// <param name = "iface">���������, ������� ���������� �������� �� �������� �����.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> �������� <c>null</c>.</exception>
        /// <exception cref = "InterfaceNotAttachedToBackboneException"><paramref name = "iface" /> �� ��������� � ������ ����� ��������.</exception>
        void DetachEndPoint (IInterfaceView iface);

        /// <summary>
        ///   �������� �������� ����� �������� (���/�).
        /// </summary>
        /// <param name = "speed">�������� �������� ������.</param>
        void ChangeSpeed (ulong speed);

        /// <summary>
        ///   �������� ���� ������ �������.
        /// </summary>
        /// <param name = "percent">���� ������ �������.</param>
        /// <remarks>
        ///   ����������� 0, ���� ������ �� ������ ����, � 1, ���� ������ �������� ��.
        /// </remarks>
        /// <exception cref = "ArgumentOutOfRangeException"><paramref name = "percent" /> �� ����� � ��������� [0..1].</exception>
        void ChangeLossPercent (double percent);
    }
}
