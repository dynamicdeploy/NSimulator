#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при нарушении уникальности названия команды.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавления в
    ///   контекст команды, имя которой совпадает с именем некоторой
    ///   другой команды в этом контексте.
    /// </remarks>
    [Serializable]
    public sealed class MenuCommandNameMustBeUniqueException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "command">Название команды.</param>
        /// <param name = "context">Название контекста.</param>
        public MenuCommandNameMustBeUniqueException (string command, string context)
            : base (string.Format (Strings.MenuCommandNameMustBeUniqueException, command, context)) {
            this.Command = command;
            this.Context = context;
        }

        /// <summary>
        ///   Получает название команды.
        /// </summary>
        /// <value>
        ///   Название команды, которое не является уникальным.
        /// </value>
        public string Command { get; private set; }

        /// <summary>
        ///   Получает название контекста.
        /// </summary>
        /// <value>
        ///   Название контекста, в котором нарушена уникальность.
        /// </value>
        public string Context { get; private set; }
    }
}
