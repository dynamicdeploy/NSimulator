#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при наличии ошибочной метаинформации модуля.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавления модуля нулевого уровня, содержащего
    ///   информацию о модулях, зависимых в направлении "от интерфейса".
    /// </remarks>
    [Serializable]
    public sealed class RedundantDependsException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "className">Название класса.</param>
        public RedundantDependsException (string className)
            : base (string.Format (Strings.RedundantDependsException, className)) {
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
