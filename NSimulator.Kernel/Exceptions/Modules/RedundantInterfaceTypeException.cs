#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при наличии ошибочной метаинформации модуля.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавления модуля ненулевого уровня работы,
    ///   содержащего информацию о типах интерфейсов, с которых нужно напрямую обрабатывать данные.
    /// </remarks>
    [Serializable]
    public sealed class RedundantInterfaceTypeException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "className">Название класса.</param>
        public RedundantInterfaceTypeException (string className)
            : base (string.Format (Strings.RedundantInterfaceTypeException, className)) {
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
