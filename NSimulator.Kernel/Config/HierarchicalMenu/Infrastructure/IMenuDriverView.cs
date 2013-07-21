#region

using System;
using System.Collections.Generic;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс драйвера меню.
    ///   Предоставлятся интерфейс только для чтения.
    /// </summary>
    /// <seealso cref = "IMenuDriver" />
    public interface IMenuDriverView {
        /// <summary>
        ///   Получает строку приглашения.
        /// </summary>
        /// <remarks>
        ///   Строка приглашения может зависеть от текущего контекста и его параметров.
        /// </remarks>
        /// <value>
        ///   Строка приглашения.
        /// </value>
        string Prompt { get; }

        /// <summary>
        ///   Получает текущий контекст меню.
        /// </summary>
        /// <value>
        ///   Текущий контекст меню.
        /// </value>
        IMenuContextView Current { get; }

        /// <summary>
        ///   Исполняет команду, указануую в строке.
        /// </summary>
        /// <param name = "command">Команда, которую нужно исполнить.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "command" /> является <c>null</c>.</exception>
        /// <exception cref = "MenuCommandNotFoundException">Команда не найдена.</exception>
        /// <exception cref = "MenuElementNameIsAmbiguousException">Название элемента меню неоднозначно.</exception>
        /// <exception cref = "NotEnoughParametersException">Недостаточно параметров контекста.</exception>
        void Invoke (string command);

        /// <summary>
        ///   Переходит к родительскому контексту данного контекста.
        /// </summary>
        /// <remarks>
        ///   Если меню находится в корневом контексте, то переход никуда не должен осуществляться.
        /// </remarks>
        void ToParent ();

        /// <summary>
        ///   Переходит к корневому элементу меню.
        /// </summary>
        void ToRoot ();

        /// <summary>
        ///   Получает список автодополнения последнего токена команды.
        /// </summary>
        /// <param name = "command">Команда, которую нужно автодополнить.</param>
        /// <returns>Перечисление <see cref = "IEnumerable{T}" /> вариантов автодополнения.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "command" /> является <c>null</c>.</exception>
        /// <exception cref = "MenuCommandNotFoundException">Команда не найдена.</exception>
        /// <exception cref = "MenuElementNameIsAmbiguousException">Название элемента меню неоднозначно.</exception>
        IEnumerable <string> AutoCompletion (string command);
    }
}
