#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при добавлении команды в несколько контекстов.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавления в контекст
    ///   команды, которая уже добавлена в некоторый контекст.
    /// </remarks>
    [Serializable]
    public sealed class MenuCommandAlreadyHasContextException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "command">Название команды, уже имеющей контекст.</param>
        public MenuCommandAlreadyHasContextException (string command)
            : base (string.Format (Strings.MenuCommandAlreadyHasContextException, command)) {
            this.Command = command;
        }

        /// <summary>
        ///   Получает название команды.
        /// </summary>
        /// <value>
        ///   Имя команды, уже добавленной в некоторый контекст.
        /// </value>
        public string Command { get; private set; }
    }
}
