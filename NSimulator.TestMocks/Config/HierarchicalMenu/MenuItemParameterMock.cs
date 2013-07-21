#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public sealed class MenuItemParameterMock : IMenuItemParameter {
        public MenuItemParameterMock (string name = "", string description = "", string value = null) {
            this.Name = name;
            this.Description = description;
            this.Value = value;
        }

        #region IMenuItemParameter Members

        public string Name { get; set; }

        public event NamedElementChangedNameEventHandler OnBeforeChangeName {
            add { }
            remove { }
        }

        public string Description { get; set; }

        public string Value { get; set; }

        public void SetValue (string value) {
            this.Value = value;
        }

        #endregion
    }
}
