#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ������� ���� "��������".
    /// </summary>
    public sealed class MenuContext : IMenuContext {
        private readonly HashSet <MenuCommand> _commands;
        private readonly HashSet <MenuContext> _contexts;
        private readonly IList <MenuItemParameter> _parameters;

        internal MenuContext ()
            : this (string.Empty) {}

        internal MenuContext (string name)
            : this (name, string.Empty) {}

        /// <summary>
        ///   ������������� �������� ���� "��������".
        /// </summary>
        /// <param name = "name">�������� ���������.</param>
        /// <param name = "description">�������� ���������.</param>
        /// <param name = "parameters">��������� ���������.</param>
        public MenuContext (string name, string description, params MenuItemParameter [] parameters) {
            this.Name = name;
            this.Description = description;

            this._parameters = new List <MenuItemParameter> (parameters);

            this._contexts = new HashSet <MenuContext> ();
            this._commands = new HashSet <MenuCommand> ();

            this.Parent = null;
        }

        #region IMenuContext Members

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
        public IMenuContextView Parent { get; internal set; }

        /// <inheritdoc />
        public int ParametersCount {
            get { return this._parameters.Count; }
        }

        /// <inheritdoc />
        public IList <IMenuItemParameterView> Parameters {
            get { return new List <IMenuItemParameterView> (this._parameters); }
        }

        /// <inheritdoc />
        public IEnumerable <IMenuContextView> ChildContexts {
            get { return this._contexts; }
        }

        /// <inheritdoc />
        public IEnumerable <IMenuCommandView> Commands {
            get { return this._commands; }
        }

        /// <inheritdoc />
        public IMenuContextView GetChild (string name) {
            if (name == null)
                throw new ArgumentNullException ("name");

            if (!this._contexts.Contains (new MenuContext (name), Singleton <NamedElementEqualityComparer>.Instance))
                throw new MenuContextNotFoundException (name, this.Name);

            return this._contexts.Where (_ => _.Name == name).First ();
        }

        /// <inheritdoc />
        public IMenuCommandView GetCommand (string name) {
            if (name == null)
                throw new ArgumentNullException ("name");

            if (!this._commands.Contains (new MenuCommand (name), Singleton <NamedElementEqualityComparer>.Instance))
                throw new MenuCommandNotFoundException (name, this.Name);

            return this._commands.Where (_ => _.Name == name).First ();
        }

        /// <summary>
        ///   ��������� �������� ��������.
        /// </summary>
        /// <param name = "context">����������� ��������.</param>
        /// <remarks>
        ///   � �������� ����������� ��������� ��������� ��������� ���� ������� ���� <see cref = "MenuContext" />.
        ///   � ��������� ������ ����� ��������� ���������� <see cref = "InvalidCastException" />.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "context" /> �������� <c>null</c>.</exception>
        /// <exception cref = "MenuContextAlreadyHasParentException">�������� <paramref name = "context" /> ��� �������� ��������.</exception>
        /// <exception cref = "MenuContextNameMustBeUniqueException">��� ��������� ��������� ������ ���� ���������� � �������� ���������.</exception>
        /// <exception cref = "InvalidCastException">�������� <paramref name = "context" /> �� �������� �������� ���� <see cref = "MenuContext" />.</exception>
        public void AddSubContext (IMenuContext context) {
            if (context == null)
                throw new ArgumentNullException ("context");

            if (context.Parent != null)
                throw new MenuContextAlreadyHasParentException (context.Name);

            if (! (context is MenuContext))
                throw new InvalidCastException ();

            if (this._contexts.Contains (context, Singleton <NamedElementEqualityComparer>.Instance) ||
                this._commands.Contains (new MenuCommand (context.Name),
                                         Singleton <NamedElementEqualityComparer>.Instance))
                throw new MenuContextNameMustBeUniqueException (context.Name, this.Name);

            this._contexts.Add (context as MenuContext);
            (context as MenuContext).Parent = this;
        }

        /// <inheritdoc />
        public void RemoveSubContext (string name) {
            if (name == null)
                throw new ArgumentNullException ("name");

            if (!this._contexts.Contains (new MenuContext (name), Singleton <NamedElementEqualityComparer>.Instance))
                throw new MenuContextNotFoundException (name, this.Name);

            foreach (var context in this._contexts.Where (_ => _.Name == name))
                context.Parent = null;

            this._contexts.RemoveWhere (_ => _.Name == name);
        }

        /// <inheritdoc />
        public string this [int index] {
            set {
                if (index < 0 || index > this._parameters.Count)
                    throw new ArgumentOutOfRangeException ("index");

                this._parameters [index].SetValue (value);
            }
        }

        /// <summary>
        ///   ��������� ������� � ��������.
        /// </summary>
        /// <param name = "command">����������� �������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "command" /> �������� <c>null</c>.</exception>
        /// <exception cref = "MenuCommandAlreadyHasContextException">������� <paramref name = "command" /> ��� ��������� � ��������� ��������.</exception>
        /// <exception cref = "MenuCommandNameMustBeUniqueException">��� ������� ������ ���� ���������� � �������� ���������.</exception>
        /// <exception cref = "InvalidCastException">������� <paramref name = "command" /> �� �������� �������� ���� <see cref = "MenuCommand" />.</exception>
        public void AddCommand (IMenuCommand command) {
            if (command == null)
                throw new ArgumentNullException ("command");

            if (command.Context != null)
                throw new MenuCommandAlreadyHasContextException (command.Name);

            if (!(command is MenuCommand))
                throw new InvalidCastException ();

            if (this._commands.Contains (command, Singleton <NamedElementEqualityComparer>.Instance) ||
                this._contexts.Contains (new MenuContext (command.Name),
                                         Singleton <NamedElementEqualityComparer>.Instance))
                throw new MenuCommandNameMustBeUniqueException (command.Name, this.Name);

            this._commands.Add (command as MenuCommand);
            (command as MenuCommand).Context = this;
        }

        /// <inheritdoc />
        public void RemoveCommand (string name) {
            if (name == null)
                throw new ArgumentNullException ("name");

            if (!this._commands.Contains (new MenuCommand (name), Singleton <NamedElementEqualityComparer>.Instance))
                throw new MenuCommandNotFoundException (name, this.Name);

            foreach (var command in this._commands.Where (_ => _.Name == name))
                command.Context = null;

            this._commands.RemoveWhere (_ => _.Name == name);
        }

        /// <inheritdoc />
        public void SetParametersCount (uint value) {
            var pc = value;

            while (this._parameters.Count > pc)
                this._parameters.RemoveAt ((int) pc);

            while (this._parameters.Count < pc)
                this._parameters.Add (new MenuItemParameter (string.Empty, string.Empty));
        }

        #endregion
    }
}
