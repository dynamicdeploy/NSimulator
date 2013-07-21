namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при выполнении элемента меню "команда".
    /// </summary>
    /// <param name = "command">Команда, которая была исполнена.</param>
    /// <remarks>
    ///   Переданные команде параметры сохраняются в <see cref = "IMenuCommandView.Parameters" />.
    /// </remarks>
    public delegate void MenuCommandHandler (IMenuCommandView command);
}
