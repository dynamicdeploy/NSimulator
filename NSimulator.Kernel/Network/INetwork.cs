#region

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ��������� ��������� ����.
    ///   ��������������� ���������� ���������� ������ ��� ������ �� ������� ����������.
    /// </summary>
    /// <seealso cref = "INetworkView" />
    [ContractClass (typeof (Contract_INetwork))]
    public interface INetwork : INetworkView {
        /// <summary>
        ///   �������� ������������ <see cref = "IEnumerable{T}" /> ���� �������� ������,
        ///   ��������� � ���������.
        /// </summary>
        /// <value>
        ///   ������ ���� �������� ������. ���� ���� ���, ������������ ������ ������������.
        /// </value>
        new IEnumerable <IBackbone> Backbones { get; }

        /// <summary>
        ///   �������� ������������ <see cref = "IEnumerable{T}" /> �����������,
        ///   ��������� � ���������.
        /// </summary>
        /// <value>
        ///   ������ �����������. ���� ����������� ���, ������������ ������ ������������.
        /// </value>
        new IEnumerable <IInterface> Interfaces { get; }

        /// <summary>
        ///   ���������� ������������ <see cref = "IEnumerable{T}" /> ���������,
        ///   ��������� � ���������.
        /// </summary>
        /// <value>
        ///   ������ ���������. ���� ��������� ���, ������������ ������ ������������.
        /// </value>
        new IEnumerable <IDevice> Devices { get; }

        /// <summary>
        ///   �������� ����, ������������ ��� ������������� ����� �������� ������,
        ///   ����������� �����.
        /// </summary>
        /// <value>
        ///   ����, ������������ ��� ������������� ���� ������� ������.
        /// </value>
        new IClock Clock { get; }

        /// <summary>
        ///   ��������� ����� �������� ������ � ��������� ����.
        /// </summary>
        /// <param name = "backbone">����������� ����� ��������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> �������� <c>null</c>.</exception>
        /// <exception cref = "NetworkContainsInterfaceException">����� �������� <paramref name = "backbone" /> ��� ������� � ���������.</exception>
        /// <returns>��������� ����������� ��������.</returns>
        NetworkEntity AddBackbone (IBackbone backbone);

        /// <summary>
        ///   ������� ����� �������� ������ �� ���������� ����.
        /// </summary>
        /// <param name = "backboneEntity">��������� ��������� ����� ��������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "backboneEntity" /> �������� <c>null</c>.</exception>
        /// <exception cref = "EntityHandlerNotFoundException">��������� <paramref name = "backboneEntity" /> ������������.</exception>
        void RemoveBackbone (NetworkEntity backboneEntity);

        /// <summary>
        ///   ��������� ��������� � ��������� ����.
        /// </summary>
        /// <param name = "iface">����������� ���������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> �������� <c>null</c>.</exception>
        /// <exception cref = "NetworkContainsInterfaceException">��������� <paramref name = "iface" /> ��� ������� � ���������.</exception>
        /// <returns>��������� ����������� ��������.</returns>
        NetworkEntity AddInterface (IInterface iface);

        /// <summary>
        ///   ������� ��������� �� ��������� ����.
        /// </summary>
        /// <param name = "interfaceEntity">��������� ���������� ����������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "interfaceEntity" /> �������� <c>null</c>.</exception>
        /// <exception cref = "EntityHandlerNotFoundException">��������� <paramref name = "interfaceEntity" /> ������������.</exception>
        void RemoveInterface (NetworkEntity interfaceEntity);

        /// <summary>
        ///   ��������� ���������� � ��������� ����.
        /// </summary>
        /// <param name = "device">����������� ����������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "device" /> �������� <c>null</c>.</exception>
        /// <exception cref = "NetworkContainsInterfaceException">���������� <paramref name = "device" /> ��� ������� � ���������.</exception>
        /// <returns>��������� ����������� ��������.</returns>
        NetworkEntity AddDevice (IDevice device);

        /// <summary>
        ///   ������� ���������� �� ��������� ����.
        /// </summary>
        /// <param name = "deviceEntity">��������� ���������� ����������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "deviceEntity" /> �������� <c>null</c>.</exception>
        /// <exception cref = "EntityHandlerNotFoundException">��������� <paramref name = "deviceEntity" /> ������������.</exception>
        void RemoveDevice (NetworkEntity deviceEntity);

        /// <summary>
        ///   ��������� ��������� ���� �� ����� <paramref name = "filename" />.
        /// </summary>
        /// <param name = "filename">��� �����, �� �������� ����� ��������� ���������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "filename" /> �������� <c>null</c>.</exception>
        /// <exception cref = "SchemaValidationException">���� <paramref name = "filename" /> ����� �������� ������.</exception>
        void LoadFromXml (string filename);

        /// <summary>
        ///   ��������� ��������� ���� � ����.
        /// </summary>
        /// <param name = "filename">��� �����, � ������� ����� ��������� ���������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "filename" /> �������� <c>null</c>.</exception>
        void SaveToXml (string filename);
    }
}
