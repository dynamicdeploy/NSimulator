namespace NSimulator.Kernel {
    /// <summary>
    ///   �������� �������� ����.
    /// </summary>
    public sealed class MenuItemParameter : IMenuItemParameter {
        /// <summary>
        ///   ������������� ��������� �������� ����.
        /// </summary>
        /// <param name = "name">�������� ���������.</param>
        /// <param name = "description">�������� ���������.</param>
        public MenuItemParameter (string name, string description) {
            this.Name = name;
            this.Description = description;
        }

        #region IMenuItemParameter Members

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
        public string Value { get; private set; }

        /// <inheritdoc />
        public void SetValue (string value) {
            this.Value = value;
        }

        #endregion
    }
}
