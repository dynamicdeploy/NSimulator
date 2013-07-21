#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при наличии ошибочной метаинформации модуля.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавления модуля с отсутствующей
    ///   информацией об уровне работы.
    /// </remarks>
    [Serializable]
    public sealed class UnknownModuleLevelException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "className">Название класса.</param>
        public UnknownModuleLevelException (string className)
            : base (string.Format (Strings.UnknownModuleLevelException, className)) {
            this.ClassName = className;
        }

        /// <summary>
        ///   Получает название класса.
        /// </summary>
        /// <value>
        ///   Название класса.
        /// </value>
        public string ClassName { get; private set; }
    }
}
