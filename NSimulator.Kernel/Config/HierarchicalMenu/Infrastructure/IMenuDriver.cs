#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс драйвера меню.
    ///   Предоставляется расширение интерфейса только для чтения до полного интерфейса.
    /// </summary>
    /// <seealso cref = "IMenuDriverView" />
    public interface IMenuDriver : IMenuDriverView {
        /// <summary>
        ///   Устанавливает новый корневой контекст.
        /// </summary>
        /// <param name = "context">Новый корневой контекст.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "context" /> является <c>null</c>.</exception>
        void SetRootContext (IMenuContext context);
    }
}
