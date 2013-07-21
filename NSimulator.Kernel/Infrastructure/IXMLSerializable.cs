#region

using System;
using System.Diagnostics.Contracts;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ��������� xml-������������� ��������.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     �����, ����������� ������ ���������, ������ �����
    ///     ����������� �� ��������� (public ��� private), ������� �����
    ///     ���� ������ ���������� ������������ ��������.
    ///   </para>
    ///   <para>
    ///     ���������� ���������� �������� <see cref = "IBackbone" /> - ��� ������ ����� �����������,
    ///     ����������� ����� ���� �������� ���� <see cref = "IClock" />.
    ///   </para>
    /// </remarks>
    [ContractClass (typeof (Contract_IXMLSerializable))]
    public interface IXMLSerializable {
        /// <summary>
        ///   ��������� �������� ���������� �� ��������� xml-����.
        /// </summary>
        /// <param name = "data">xml-����, �� �������� ��������� ����������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "data" /> �������� <c>null</c>.</exception>
        void Load (XmlNode data);

        /// <summary>
        ///   ��������� ���������� ���������� � �������� xml-����.
        /// </summary>
        /// <param name = "node">xml-����, � ������� ����� ��������� ����������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "node" /> �������� <c>null</c>.</exception>
        void Store (XmlNode node);
    }
}
