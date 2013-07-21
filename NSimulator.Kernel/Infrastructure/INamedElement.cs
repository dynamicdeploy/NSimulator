#region

using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс именованного элемента.
    /// </summary>
    [ContractClass (typeof (Contract_INamedElement))]
    public interface INamedElement {
        /// <summary>
        ///   Получает имя элемента.
        /// </summary>
        /// <value>
        ///   Имя элемента.
        /// </value>
        string Name { get; }

        /// <summary>
        ///   Происходит при изменении имени элемента.
        /// </summary>
        event NamedElementChangedNameEventHandler OnBeforeChangeName;
    }
}
