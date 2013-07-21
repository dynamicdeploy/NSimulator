namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при изменении имени именованного элемента.
    /// </summary>
    /// <param name = "element">Именованный элемент, сменивший имя.</param>
    /// <param name = "name">Новое имя.</param>
    public delegate void NamedElementChangedNameEventHandler (INamedElement element, string name);
}
