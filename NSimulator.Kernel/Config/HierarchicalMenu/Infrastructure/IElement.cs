namespace NSimulator.Kernel {
    /// <summary>
    ///   ��������� �������� ����.
    /// </summary>
    public interface IElement : INamedElement {
        /// <summary>
        ///   �������� �������� ��������.
        /// </summary>
        /// <value>
        ///   �������� ��������.
        /// </value>
        string Description { get; }
    }
}
