#region

using System;
using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс элемента меню "команда".
    ///   Предоставляется интерфейс только для чтения.
    /// </summary>
    /// <seealso cref = "IMenuCommand" />
    public interface IMenuCommandView : IElement {
        /// <summary>
        ///   Получает контекст, содержащий данную команду.
        /// </summary>
        /// <value>
        ///   Контекст с данной командой.
        ///   Если данная команда не содержится ни в одном контексте, возвращается <c>null</c>.
        /// </value>
        IMenuContextView Context { get; }

        /// <summary>
        ///   Получает список <see cref = "IList{T}" /> параметров команды.
        /// </summary>
        /// <value>
        ///   Список параметров команды.
        ///   Если команда не имеет параметров, возвращается пустой список.
        /// </value>
        IList <IMenuItemParameterView> Parameters { get; }

        /// <summary>
        ///   Выполняет команду с указанными параметрами.
        /// </summary>
        /// <param name = "parameters">Параметры команды.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "parameters" /> является <c>null</c>.</exception>
        void Invoke (params string [] parameters);

        /// <summary>
        ///   Происходит, когда команда исполняется.
        /// </summary>
        event MenuCommandHandler OnInvoke;
    }
}
