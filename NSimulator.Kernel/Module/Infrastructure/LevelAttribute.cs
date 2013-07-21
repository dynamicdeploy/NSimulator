#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   �������� ������� ������ ������.
    /// </summary>
    /// <seealso cref = "IModule" />
    [AttributeUsage (AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class LevelAttribute : Attribute {
        /// <summary>
        ///   �������������� �������.
        /// </summary>
        /// <param name = "level">������� ������ ������.</param>
        public LevelAttribute (byte level) {
            this.Level = level;
        }

        /// <summary>
        ///   ���������� ������� ������ ������.
        /// </summary>
        /// <value>
        ///   ������� ������ ������.
        /// </value>
        public byte Level { get; private set; }
    }
}
