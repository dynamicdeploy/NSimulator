#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при некорректном значении параметра.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке выполнить команду меню, при этом
    ///   переданные параметры исполнения неверны.
    /// </remarks>
    [Serializable]
    public sealed class InvalidParameterException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "parameter">Неверный параметр.</param>
        /// <param name = "description">Описание ошибки.</param>
        public InvalidParameterException (IMenuItemParameterView parameter, string description)
            : base (string.Format (Strings.InvalidParameterException, parameter.Name, parameter.Value, description)) {
            this.Name = parameter.Name;
            this.Value = parameter.Value;
            this.Description = description;
        }

        /// <summary>
        ///   Получает название параметра.
        /// </summary>
        /// <value>
        ///   Название параметра, значение которого неверно.
        /// </value>
        public string Name { get; private set; }

        /// <summary>
        ///   Получает значение параметра.
        /// </summary>
        /// <value>
        ///   Неверное значение аргумента.
        /// </value>
        public string Value { get; private set; }

        /// <summary>
        ///   Получает описание ошибки.
        /// </summary>
        /// <value>
        ///   Описание ошибки в значении параметра.
        /// </value>
        public string Description { get; private set; }
    }
}
