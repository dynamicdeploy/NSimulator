#region

using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ��������� ������������ ��������.
    /// </summary>
    [ContractClass (typeof (Contract_INamedElement))]
    public interface INamedElement {
        /// <summary>
        ///   �������� ��� ��������.
        /// </summary>
        /// <value>
        ///   ��� ��������.
        /// </value>
        string Name { get; }

        /// <summary>
        ///   ���������� ��� ��������� ����� ��������.
        /// </summary>
        event NamedElementChangedNameEventHandler OnBeforeChangeName;
    }
}
