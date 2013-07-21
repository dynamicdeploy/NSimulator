namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс ошибки, возникшей при загрузке топологии.
    /// </summary>
    /// <remarks>
    ///   Каждая реализация интерфейса содержит информацию об определённом типе ошибок.
    ///   Чтобы получить информацию в строковом вижде, можно использовать метод <see cref = "object.ToString" />.
    /// </remarks>
    public interface ILoadTopologyError {}
}
