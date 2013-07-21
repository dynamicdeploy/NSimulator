#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ��������� �������� ����.
    ///   ��������������� ���������� ���������� ������ ��� ������ �� ������� ����������.
    /// </summary>
    /// <seealso cref = "IMenuDriverView" />
    public interface IMenuDriver : IMenuDriverView {
        /// <summary>
        ///   ������������� ����� �������� ��������.
        /// </summary>
        /// <param name = "context">����� �������� ��������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "context" /> �������� <c>null</c>.</exception>
        void SetRootContext (IMenuContext context);
    }
}
