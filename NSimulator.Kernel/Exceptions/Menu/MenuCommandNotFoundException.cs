#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при отсутствии команды в контексте.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке получения команды по
    ///   её имени, но команды с таким именем нет в контексте.
    /// </remarks>
    [Serializable]
    public sealed class MenuCommandNotFoundException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "command">Название команды.</param>
        /// <param name = "context">Название контекста.</param>
        public MenuCommandNotFoundException (string command, string context)
            : base (string.Format (Strings.MenuContextNotFoundException, command, context)) {
            this.Command = command;
            this.Context = context;
        }

        /// <summary>
        ///   Получает название команды.
        /// </summary>
        /// <value>
        ///   Название отсутствующей команды.
        /// </value>
        public string Command { get; private set; }

        /// <summary>
        ///   Получает название контекста.
        /// </summary>
        /// <value>
        ///   Название контекста.
        /// </value>
        public string Context { get; private set; }
    }
}
