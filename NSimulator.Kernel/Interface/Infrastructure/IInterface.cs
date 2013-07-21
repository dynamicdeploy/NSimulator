#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ��������� ����������� ����������.
    ///   ��������������� ���������� ���������� ������ ��� ������ �� ������� ����������.
    /// </summary>
    /// <seealso cref = "IInterfaceView" />
    [ContractClass (typeof (Contract_IInterface))]
    public interface IInterface : IInterfaceView {
        /// <summary>
        ///   �������� ����� �������� ������, ����������� � ����������.
        /// </summary>
        /// <value>
        ///   ����� �������� ������, ������������ � ���������� ��� <c>null</c>, ���� ������� ���.
        /// </value>
        new IBackbone Backbone { get; }

        /// <summary>
        ///   ������������� �������� ����������.
        /// </summary>
        /// <param name = "name">�������� ����������.</param>
        /// <remarks>
        ///   ������ �������� ���������� ������������� ���������� ��� ���������� � ����
        ///   ������� ����������.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> �������� <c>null</c>.</exception>
        void SetName (string name);

        /// <summary>
        ///   ������������� ����� �������� ������, ����������� � ����������.
        /// </summary>
        /// <param name = "backbone">����������� ����� �������� ������.</param>
        /// <remarks>
        ///   ����� ������ ��������� �������� ������������ ������������ �
        ///   ����������� ������ ��������� ���������� � ����� �������� ������.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> �������� <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToBackboneException">��������� ��� �������� � ����� �������� ������.</exception>
        /// <exception cref = "InterfaceNotCompatibleWithBackboneException">��������� ����������� �� ������ �������� ������.</exception>
        /// <exception cref = "EndPointsOverflowException">����� �������� ������ ��� ����� ����������� ��������� ����� �������� �����.</exception>
        void SetBackbone (IBackbone backbone);

        /// <summary>
        ///   ����������� ����� �������� ������, ����������� � ����������.
        /// </summary>
        /// <exception cref = "InterfaceNotAttachedToBackboneException">��������� �� �������� � ����� �������� ������.</exception>
        void ReleaseBackbone ();

        /// <summary>
        ///   ������������� ����������, ���������� ������ ���������.
        /// </summary>
        /// <param name = "device">����������, ���������� ���������.</param>
        /// <remarks>
        ///   <para>
        ///     ����� ������ ������ ��������� �������� ������������ � �����������
        ///     ���������� �������� <see cref = "IInterfaceView.Device" />
        ///     ����������, ����������� � <paramref name = "device" />.
        ///   </para>
        ///   <para>
        ///     ������ ����� ���������� ��� ���������� ���������� � ����������.
        ///   </para>
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "device" /> �������� <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToDeviceException">��������� ��� �������� � ����������.</exception>
        /// <exception cref = "InterfaceNotAttachedToDeviceException">��������� �� ���������� � ���������� <paramref name = "device" />.</exception>
        void SetDevice (IDeviceView device);

        /// <summary>
        ///   ����������� ����������, ���������� ������ ���������.
        /// </summary>
        /// <exception cref = "InterfaceNotAttachedToDeviceException">��������� �� �������� � ����������.</exception>
        void ReleaseDevice ();

        /// <summary>
        ///   "���������" ���������.
        ///   ����� �������� ��������� �������� ���������� � ��������� ������.
        /// </summary>
        void Enable ();

        /// <summary>
        ///   "��������" ���������.
        ///   ����� ��������� ��������� ���������� ���������� � ��������� ������.
        /// </summary>
        void Disable ();
    }
}
