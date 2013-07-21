#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Отмечает уровень работы модуля.
    /// </summary>
    /// <seealso cref = "IModule" />
    [AttributeUsage (AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class LevelAttribute : Attribute {
        /// <summary>
        ///   Инициализирует атрибут.
        /// </summary>
        /// <param name = "level">Уровень работы модуля.</param>
        public LevelAttribute (byte level) {
            this.Level = level;
        }

        /// <summary>
        ///   Возвращает уровень работы модуля.
        /// </summary>
        /// <value>
        ///   Уровень работы модуля.
        /// </value>
        public byte Level { get; private set; }
    }
}
