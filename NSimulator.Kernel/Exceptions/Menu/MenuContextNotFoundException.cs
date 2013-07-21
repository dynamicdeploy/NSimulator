#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при отсутствии подконтекста в контексте.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке получения подконтекста по
    ///   его имени, но подконтекста с таким именем нет в контексте.
    /// </remarks>
    [Serializable]
    public sealed class MenuContextNotFoundException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "subcontext">Название подконтекста.</param>
        /// <param name = "context">Название контекста.</param>
        public MenuContextNotFoundException (string subcontext, string context)
            : base (string.Format (Strings.MenuContextNotFoundException, subcontext, context)) {
            this.Subcontext = subcontext;
            this.Context = context;
        }

        /// <summary>
        ///   Получает название подконтекста.
        /// </summary>
        /// <value>
        ///   Название отсутствущющего подконтекста.
        /// </value>
        public string Subcontext { get; private set; }

        /// <summary>
        ///   Получает название контекста.
        /// </summary>
        /// <value>
        ///   Название контекста.
        /// </value>
        public string Context { get; private set; }
    }
}
