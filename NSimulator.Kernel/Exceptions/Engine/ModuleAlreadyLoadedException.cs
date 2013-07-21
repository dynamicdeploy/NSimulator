using System;

namespace NSimulator.Kernel {
    /// <summary>
    ///   Исключение, пробрасываемое при ошибочной работе с диспетчером модулей.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке добавить в диспетчер модулей прошивки
    ///   модуль, который уже добавлен в диспетчер.
    /// </remarks>
    [Serializable]
    public sealed class ModuleAlreadyLoadedException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "className">Название класса.</param>
        public ModuleAlreadyLoadedException (string className)
            : base (string.Format (Strings.ModuleAlreadyLoadedException, className)) {
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
