#region

using System.Collections.Generic;
using System.Linq;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public sealed class MenuCommandMock : IMenuCommand {
        private readonly IList <IMenuItemParameter> _parameters;

        public MenuCommandMock (string name = "", string desciption = "") {
            this.Name = name;
            this.Description = desciption;

            this._parameters = new List <IMenuItemParameter> ();
        }

        #region IMenuCommand Members

        public string Name { get; set; }

        public event NamedElementChangedNameEventHandler OnBeforeChangeName {
            add { }
            remove { }
        }

        public string Description { get; set; }

        public IMenuContextView Context { get; set; }

        public IList <IMenuItemParameterView> Parameters {
            get { return this._parameters.Cast <IMenuItemParameterView> ().ToList (); }
        }

        public void Invoke (params string [] parameters) {
            foreach (var param in this._parameters)
                param.SetValue (null);

            if (this._parameters.Count < parameters.Length) {
                for (var i = this._parameters.Count; i < parameters.Length; ++ i)
                    this._parameters.Add (new MenuItemParameterMock ());
            }

            for (var i = 0; i < parameters.Length; ++ i)
                this._parameters [i].SetValue (parameters [i]);

            if (this.OnInvoke != null)
                this.OnInvoke (this);
        }

        public event MenuCommandHandler OnInvoke;

        #endregion
    }
}
