#region

using System;
using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс элемента меню "контекст".
    ///   Предоставляется интерфейс только для чтения.
    /// </summary>
    /// <seealso cref = "IMenuContext" />
    public interface IMenuContextView : IElement {
        /// <summary>
        ///   Получает родительский контекст данного контекста.
        /// </summary>
        /// <value>
        ///   Родительский контекст данного контекста.
        /// </value>
        IMenuContextView Parent { get; }

        /// <summary>
        ///   Получает количество параметров данного контекста.
        /// </summary>
        /// <value>
        ///   Количество параметров контекста.
        /// </value>
        int ParametersCount { get; }

        /// <summary>
        ///   Получает список <see cref = "IList{T}" /> параметров контекста.
        /// </summary>
        /// <value>
        ///   Список параметров контекста.
        ///   Если контекст не имеет параметров, возвращается пустой список.
        /// </value>
        IList <IMenuItemParameterView> Parameters { get; }

        /// <summary>
        ///   Получает перечисление <see cref = "IEnumerable{IMenuContextView}" /> дочерних контекстов.
        /// </summary>
        /// <value>
        ///   Перечисление дочерних контекстов.
        ///   Если дочерних контекстов нет, возвращается пустое перечисление.
        /// </value>
        IEnumerable <IMenuContextView> ChildContexts { get; }

        /// <summary>
        ///   Получает перечисление <see cref = "IEnumerable{IMenuCommandView}" /> команд, существующих
        ///   в данном контексте.
        /// </summary>
        /// <value>
        ///   Перечисление команд данного контекста.
        ///   Если команд нет, возвращается пустое перечисление.
        /// </value>
        IEnumerable <IMenuCommandView> Commands { get; }

        /// <summary>
        ///   Получает дочерний контекст по имени.
        /// </summary>
        /// <param name = "name">Имя контекста, который требуется получить.</param>
        /// <returns>Дочерний контекст с заданным в <paramref name = "name" /> именем.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        /// <exception cref = "MenuContextNotFoundException">Контекста с именем <paramref name = "name" /> не существует.</exception>
        IMenuContextView GetChild (string name);

        /// <summary>
        ///   Получает команду контекста по имени.
        /// </summary>
        /// <param name = "name">Имя команды, которую нужно получить.</param>
        /// <returns>Команда данного контекста с указанным в <paramref name = "name" /> именем.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        /// <exception cref = "MenuCommandNotFoundException">Команды с именем <paramref name = "name" /> не существует.</exception>
        IMenuCommandView GetCommand (string name);
    }
}
