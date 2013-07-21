#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс элемента меню "контекст".
    ///   Предоставляется расширение интерфейса только для чтения до полного интерфейса.
    /// </summary>
    /// <seealso cref = "IMenuContextView" />
    public interface IMenuContext : IMenuContextView {
        /// <summary>
        ///   Устанавливает значение указанного параметра.
        /// </summary>
        /// <param name = "index">Номер параметра.</param>
        /// <exception cref = "ArgumentOutOfRangeException">Параметра с номером <paramref name = "index" /> не существует.</exception>
        /// <returns>Значение параметра.</returns>
        string this [int index] { set; }

        /// <summary>
        ///   Добавляет дочерний контекст.
        /// </summary>
        /// <param name = "context">Добавляемый контекст.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "context" /> является <c>null</c>.</exception>
        /// <exception cref = "MenuContextAlreadyHasParentException">Контекст <paramref name = "context" /> уже является дочерним.</exception>
        /// <exception cref = "MenuContextNameMustBeUniqueException">Имя дочернего контекста должно быть уникальным в пределах контекста.</exception>
        void AddSubContext (IMenuContext context);

        /// <summary>
        ///   Удаляет дочерний контекст.
        /// </summary>
        /// <param name = "name">Имя удаляемого контекста.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        /// <exception cref = "MenuContextNotFoundException">Дочернего контекста с именем <paramref name = "name" /> не существует.</exception>
        void RemoveSubContext (string name);

        /// <summary>
        ///   Устанавливает количество параметров контекста.
        /// </summary>
        /// <param name = "value">Количество параметров контекста.</param>
        void SetParametersCount (uint value);

        /// <summary>
        ///   Добавляет команду в контекст.
        /// </summary>
        /// <param name = "command">Добавляемая команда.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "command" /> является <c>null</c>.</exception>
        /// <exception cref = "MenuCommandAlreadyHasContextException">Команда <paramref name = "command" /> уже добавлена в некоторый контекст.</exception>
        /// <exception cref = "MenuCommandNameMustBeUniqueException">Имя команды должно быть уникальным в пределах контекста.</exception>
        void AddCommand (IMenuCommand command);

        /// <summary>
        ///   Удаляет команду из контекста.
        /// </summary>
        /// <param name = "name">Имя удаляемой команды.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        /// <exception cref = "MenuCommandNotFoundException">Команды с именем <paramref name = "name" /> не существует.</exception>
        void RemoveCommand (string name);
    }
}
