#region

using System;
using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ��������� �������.
    ///   ��������������� ��������� ������ ��� ������.
    /// </summary>
    /// <seealso cref = "IArray{T}" />
    /// <seealso cref = "ArrayView{T}" />
    /// <typeparam name = "T">��� ��������� �������.</typeparam>
    public interface IArrayView <T> : IEnumerable <T>, IEquatable <IArrayView <T>> {
        /// <summary>
        ///   �������� ������� � ��������� ��������.
        /// </summary>
        /// <param name = "index">������ ��������.</param>
        /// <exception cref = "IndexOutOfRangeException">������ ��������� ��� �������.</exception>
        /// <returns>������� � �������� <paramref name = "index" />.</returns>
        T this [int index] { get; }

        /// <summary>
        ///   �������� �������� ������� � ������� �������.
        /// </summary>
        /// <value>
        ///   �������� ������� � ������� �������.
        /// </value>
        int Offset { get; }

        /// <summary>
        ///   �������� ������ �������.
        /// </summary>
        /// <value>
        ///   ������ �������.
        /// </value>
        int Length { get; }

        /// <summary>
        ///   �������� ������� ������.
        /// </summary>
        /// <value>
        ///   ������� ������.
        /// </value>
        T [] Base { get; }

        /// <summary>
        ///   �������� ���� ������� - ������������ [<paramref name = "from" />..<paramref name = "to" />).
        /// </summary>
        /// <param name = "from">������ ������ �����.</param>
        /// <param name = "to">������ ����� �����.</param>
        /// <exception cref = "ArgumentOutOfRangeException">������ <paramref name = "from" /> ��������� ��� �������.</exception>
        /// <exception cref = "ArgumentOutOfRangeException">������ <paramref name = "to" /> ��������� ��� �������.</exception>
        /// <returns>���� �������.</returns>
        IArrayView <T> Slice (int from, int to);
    }
}
