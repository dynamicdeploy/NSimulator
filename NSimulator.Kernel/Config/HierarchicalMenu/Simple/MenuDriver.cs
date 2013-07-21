#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Драйвер меню.
    /// </summary>
    public sealed class MenuDriver : IMenuDriver {
        private const string COMMAND_PARENT = "..";
        private const string COMMAND_ROOT = ".root";
        private MenuContext current;
        private MenuContext rootContext;

        /// <summary>
        ///   Инициализация драйвера меню.
        /// </summary>
        public MenuDriver () {
            this.rootContext = new MenuContext ();
            this.current = this.rootContext;
        }

        #region IMenuDriver Members

        /// <inheritdoc />
        public string Prompt {
            get {
                var parameters = string.Join (",",
                                              from param in this.current.Parameters
                                              select param.Value);
                return string.Format ("{0}{1}#",
                                      this.current.Name,
                                      parameters.Length == 0 ? string.Empty : string.Format ("({0})", parameters));
            }
        }

        /// <summary>
        ///   Исполняет команду, указанную в строке.
        /// </summary>
        /// <remarks>
        ///   При исполнении команды происходит автодополнение контекстов и команды.
        /// </remarks>
        /// <param name = "command">Команда, которую нужно исполнить.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "command" /> является <c>null</c>.</exception>
        /// <exception cref = "MenuCommandNotFoundException">Команда не найдена.</exception>
        /// <exception cref = "MenuElementNameIsAmbiguousException">Название элемента меню неоднозначно.</exception>
        /// <exception cref = "NotEnoughParametersException">Недостаточно параметров контекста.</exception>
        public void Invoke (string command) {
            if (command == null)
                throw new ArgumentNullException ("command");

            var tokens = new Queue <string> (command.Split (new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            var context = this.current;

            var context_params = new Dictionary <MenuContext, List <string>> ();

            while (tokens.Count != 0) {
                var token = tokens.Dequeue ();

                if (token.Equals (COMMAND_PARENT)) {
                    context = (context.Parent ?? this.rootContext) as MenuContext;
                    continue;
                }

                if (token.Equals (COMMAND_ROOT)) {
                    context = this.rootContext;
                    continue;
                }

                var completion = Completion (context, token);
                var count = completion.Count (_ => _.Name == token);

                if (completion.Count () == 0)
                    throw new MenuCommandNotFoundException (token, context.Name);

                if (count == 1)
                    completion = completion.Where (_ => _.Name == token);
                else if (completion.Count () > 1)
                    throw new MenuElementNameIsAmbiguousException (token, context.Name);

                var elem = completion.First ();

                if (elem is IMenuContextView) {
                    context = elem as MenuContext;
                    if (context.ParametersCount > tokens.Count)
                        throw new NotEnoughParametersException (context.Name, tokens.Count, context.ParametersCount);

                    var parameters = new List <string> ();
                    for (var i = 0; i < context.ParametersCount; ++ i)
                        parameters.Add (tokens.Dequeue ());
                    if (!context_params.ContainsKey (context))
                        context_params.Add (context, parameters);
                    else
                        context_params [context] = parameters;

                    continue;
                }

                if (elem is IMenuCommandView) {
                    this.current = context;
                    foreach (var param in context_params) {
                        for (var i = 0; i < param.Value.Count; ++i)
                            param.Key [i] = param.Value [i];
                    }

                    (elem as MenuCommand).Invoke (tokens.ToArray ());
                    break;
                }
            }

            this.current = context;
            foreach (var param in context_params) {
                for (var i = 0; i < param.Value.Count; ++i)
                    param.Key [i] = param.Value [i];
            }
        }

        /// <inheritdoc />
        public void ToParent () {
            if (this.current == this.rootContext)
                return;

            this.current = this.current.Parent as MenuContext;
        }

        /// <inheritdoc />
        public void ToRoot () {
            this.current = this.rootContext;
        }

        /// <inheritdoc />
        public IMenuContextView Current {
            get { return this.current; }
        }

        /// <summary>
        ///   Получает список автодополнения последнего токена команды.
        /// </summary>
        /// <remarks>
        ///   При автодополнении происходит автодополнение всех контекстов в команде.
        /// </remarks>
        /// <param name = "command">Команда, которую нужно автодополнить.</param>
        /// <returns>Перечисление <see cref = "IEnumerable{T}" /> вариантов автодополнения.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "command" /> является <c>null</c>.</exception>
        /// <exception cref = "MenuCommandNotFoundException">Команда не найдена.</exception>
        /// <exception cref = "MenuElementNameIsAmbiguousException">Название элемента меню неоднозначно.</exception>
        public IEnumerable <string> AutoCompletion (string command) {
            if (command == null)
                throw new ArgumentNullException ("command");

            var tokens = new Queue <string> (command.Split (new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            if (tokens.Count == 0)
                tokens.Enqueue (string.Empty);

            var context = this.current;

            IEnumerable <INamedElement> completion = null;
            var completion_token = string.Empty;

            while (tokens.Count != 0) {
                var token = tokens.Dequeue ();

                if (token.Equals (COMMAND_PARENT)) {
                    if (context != this.rootContext)
                        context = (context.Parent ?? this.rootContext) as MenuContext;

                    continue;
                }

                if (token.Equals (COMMAND_ROOT)) {
                    context = this.rootContext;
                    continue;
                }

                if (completion != null) {
                    if (completion.Count () == 0)
                        throw new MenuCommandNotFoundException (token, context.Name);

                    var completionToken = completion_token;
                    var count = completion.Count (_ => _.Name == completionToken);

                    if (count == 1)
                        completion = completion.Where (_ => _.Name == completionToken);
                    else if (completion.Count () > 1)
                        throw new MenuElementNameIsAmbiguousException (completion_token, context.Name);

                    var elem = completion.First ();

                    if (elem is IMenuContextView) {
                        context = elem as MenuContext;
                        if (context.ParametersCount > tokens.Count)
                            throw new NotEnoughParametersException (context.Name, tokens.Count, context.ParametersCount);

                        for (var i = 1; i < context.ParametersCount; ++i)
                            tokens.Dequeue ();

                        completion = null;

                        continue;
                    }

                    if (elem is IMenuCommandView)
                        return new string [] { };
                }
                else {
                    completion = Completion (context, token);
                    completion_token = token;
                }
            }

            if (completion == null || completion.Count () == 0)
                throw new MenuCommandNotFoundException (completion_token, context.Name);

            return completion.Select (_ => _.Name);
        }

        /// <inheritdoc />
        public void SetRootContext (IMenuContext context) {
            if (context == null)
                throw new ArgumentNullException ("context");

            if (!(context is MenuContext))
                throw new InvalidCastException ();

            this.rootContext = context as MenuContext;
            this.current = this.rootContext;
        }

        #endregion

        private static IEnumerable <INamedElement> Completion (IMenuContextView context, string token) {
            return
                context.ChildContexts.Where (_ => _.Name.StartsWith (token)).Cast <INamedElement> ().Concat (
                    context.Commands.Where (_ => _.Name.StartsWith (token)));
        }
    }
}
