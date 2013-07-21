#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при добавлении контекста в несколько контекстов.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавления в контекст
    ///   контекста, который уже добавлен в некоторый контекст.
    /// </remarks>
    [Serializable]
    public sealed class MenuContextAlreadyHasParentException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "context">Название контекста, уже имеющего контекст.</param>
        public MenuContextAlreadyHasParentException (string context)
            : base (string.Format (Strings.MenuContextAlreadyHasParentException, context)) {
            this.Context = context;
        }

        /// <summary>
        ///   Получает название контекста.
        /// </summary>
        /// <value>
        ///   Название контекста, уже добавленного в некоторый контекст.
        /// </value>
        public string Context { get; private set; }
    }
}
