#region

using System.Collections.Generic;
using System.Linq;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public sealed class MenuContextMock : IMenuContext {
        private readonly IDictionary <string, IMenuContext> childContexts;
        private readonly IDictionary <string, IMenuCommand> commands;
        private readonly IList <IMenuItemParameter> parameters;

        public MenuContextMock (string name = "", string description = "", int parameters = 0) {
            this.Name = name;
            this.Description = description;

            this.parameters = new List <IMenuItemParameter> ();
            this.SetParametersCount ((uint) parameters);
            this.childContexts = new Dictionary <string, IMenuContext> ();
            this.commands = new Dictionary <string, IMenuCommand> ();
        }

        #region IMenuContext Members

        public string Name { get; set; }

        public event NamedElementChangedNameEventHandler OnBeforeChangeName {
            add { }
            remove { }
        }

        public string Description { get; set; }

        public IMenuContextView Parent { get; set; }

        public int ParametersCount {
            get { return this.parameters.Count; }
        }

        public IList <IMenuItemParameterView> Parameters {
            get { return this.parameters.Cast <IMenuItemParameterView> ().ToList (); }
        }

        public IEnumerable <IMenuContextView> ChildContexts {
            get { return this.childContexts.Values; }
        }

        public IEnumerable <IMenuCommandView> Commands {
            get { return this.commands.Values; }
        }

        public IMenuContextView GetChild (string name) {
            return this.childContexts [name];
        }

        public IMenuCommandView GetCommand (string name) {
            return this.commands [name];
        }

        public void AddSubContext (IMenuContext context) {
            if (!this.childContexts.ContainsKey (context.Name))
                this.childContexts.Add (context.Name, context);
        }

        public void RemoveSubContext (string name) {
            if (this.childContexts.ContainsKey (name))
                this.childContexts.Remove (name);
        }

        public string this [int index] {
            set { this.parameters [index].SetValue (value); }
        }

        public void AddCommand (IMenuCommand command) {
            if (! this.commands.ContainsKey (command.Name))
                this.commands.Add (command.Name, command);
        }

        public void RemoveCommand (string name) {
            if (this.commands.ContainsKey (name))
                this.commands.Remove (name);
        }

        public void SetParametersCount (uint value) {
            var pc = (int) value;

            if (this.parameters.Count > pc) {
                for (var i = this.parameters.Count; i < pc; ++i)
                    this.parameters.RemoveAt (pc);
            }

            if (this.parameters.Count < pc) {
                for (var i = this.parameters.Count; i < pc; ++ i)
                    this.parameters.Add (new MenuItemParameterMock ());
            }
        }

        #endregion

        public override bool Equals (object obj) {
            if (obj == null || ! (obj is MenuContextMock))
                return false;

            return ReferenceEquals (this, obj) || (obj as MenuContextMock).Name.Equals (this.Name);
        }

        public override int GetHashCode () {
            return this.Name.GetHashCode ();
        }
    }
}
