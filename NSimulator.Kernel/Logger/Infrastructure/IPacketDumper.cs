#region

using System;
using System.Diagnostics.Contracts;
using System.IO;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ��������� ������������ ������������ �������.
    /// </summary>
    [ContractClass (typeof (Contract_IPacketDumper))]
    public interface IPacketDumper : IDisposable {
        /// <summary>
        ///   �������� ������������ �������, ������������ � ��������� ����� ��������.
        /// </summary>
        /// <param name = "backbone">����� ��������, � ������� ����� �������� ������������.</param>
        /// <remarks>
        ///   ������������ ������������ ������� ���������� � ��������� ���������, ���������
        ///   �� ����������� ���������� ����� ����������.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> �������� <c>null</c>.</exception>
        void AttachToBackbone (IBackboneView backbone);

        /// <summary>
        ///   �������� ������������ �������, ������������ � ��������� ����� ��������.
        /// </summary>
        /// <param name = "backbone">����� ��������, � ������� ����� �������� ������������.</param>
        /// <param name = "stream">�����, � ������� ��������� �������� ���.</param>
        /// <remarks>
        ///   <para>
        ///     ������������ ������������ ������� ���������� � ��������� � <paramref name = "stream" />
        ///     ����� ������.
        ///   </para>
        ///   <para>
        ///     ��� ������ <see cref = "IDisposable.Dispose" /> ���������� ����� ������������� �� ������.
        ///   </para>
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "stream" /> �������� <c>null</c>.</exception>
        void AttachToBackbone (IBackboneView backbone, Stream stream);

        /// <summary>
        ///   ��������� ������������ �������, ������������ � ��������� ����� ��������.
        /// </summary>
        /// <param name = "backbone">����� ��������, � ������� ����� ��������� ������������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> �������� <c>null</c>.</exception>
        // / <exception cref="DumperNotAttachedToBackboneException">������������ � ����� �������� <paramref name="backbone"/> �� ��������.</exception>
        void DetachFromBackbone (IBackboneView backbone);
    }
}
