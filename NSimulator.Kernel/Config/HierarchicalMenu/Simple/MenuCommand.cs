#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Элемент меню "команда".
    /// </summary>
    public sealed class MenuCommand : IMenuCommand {
        private readonly IList <IMenuItemParameter> _params;
        private readonly IList <IMenuItemParameter> current_params;

        internal MenuCommand (string name)
            : this (name, string.Empty) {}

        /// <summary>
        ///   Инициализация элемента меню "команда".
        /// </summary>
        /// <param name = "name">Название команды.</param>
        /// <param name = "description">Описание команды.</param>
        /// <param name = "parameters">Параметры команды.</param>
        public MenuCommand (string name, string description, params IMenuItemParameter [] parameters) {
            this.Name = name;
            this.Description = description;
            this.Context = null;

            this._params = new List <IMenuItemParameter> (parameters ?? new MenuItemParameter [] { });

            this.current_params = new List <IMenuItemParameter> ();
        }

        #region IMenuCommand Members

        /// <inheritdoc />
        public string Name { get; private set; }

        /// <inheritdoc />
        public event NamedElementChangedNameEventHandler OnBeforeChangeName {
            add { }
            remove { }
        }

        /// <inheritdoc />
        public string Description { get; private set; }

        /// <inheritdoc />
        public IMenuContextView Context { get; internal set; }

        /// <inheritdoc />
        public IList <IMenuItemParameterView> Parameters {
            get { return this.current_params.Cast <IMenuItemParameterView> ().ToList (); }
        }

        /// <summary>
        ///   Выполняет команду с указанными параметрами.
        /// </summary>
        /// <param name = "parameters">Параметры команды.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "parameters" /> является <c>null</c>.</exception>
        public void Invoke (params string [] parameters) {
            if (parameters == null)
                throw new ArgumentNullException ("parameters");

            this.current_params.Clear ();
            for (var i = 0; i < parameters.Length; ++i) {
                if (i < this._params.Count) {
                    this._params [i].SetValue (parameters [i]);
                    this.current_params.Add (this._params [i]);
                }
                else {
                    var param = new MenuItemParameter (string.Empty, string.Empty);
                    param.SetValue (parameters [i]);
                    this.current_params.Add (param);
                }
            }

            if (this.OnInvoke != null)
                this.OnInvoke.Invoke (this);
        }

        /// <inheritdoc />
        public event MenuCommandHandler OnInvoke;

        #endregion
    }
}
