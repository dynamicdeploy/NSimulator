#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при нарушении уникальности названия контекста.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавления в
    ///   контекст подконтекста, имя которого совпадает с именем некоторого
    ///   другого подконтекста в этом контексте.
    /// </remarks>
    [Serializable]
    public sealed class MenuContextNameMustBeUniqueException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "subcontext">Название подконтекста.</param>
        /// <param name = "context">Название контекста.</param>
        public MenuContextNameMustBeUniqueException (string subcontext, string context)
            : base (string.Format (Strings.MenuContextNameMustBeUniqueException, subcontext, context)) {
            this.Subcontext = subcontext;
            this.Context = context;
        }

        /// <summary>
        ///   Получает название подконтекста.
        /// </summary>
        /// <value>
        ///   Название подконтекста, которое не является уникальным.
        /// </value>
        public string Subcontext { get; private set; }

        /// <summary>
        ///   Получает название контекста.
        /// </summary>
        /// <value>
        ///   Название контекста, в котором нарушена уникальность.
        /// </value>
        public string Context { get; private set; }
    }
}
