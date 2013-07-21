#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Описатель зарегистрированного действия часов.
    /// </summary>
    /// <seealso cref = "IClock" />
    public sealed class ClockHandler {
        internal ClockHandler (ulong id) {
            if (id == ulong.MaxValue)
                throw new ArgumentOutOfRangeException ("id");

            this.Id = id;
        }

        /// <summary>
        ///   Инициализирует описатель действия по умолчанию.
        /// </summary>
        public ClockHandler () {
            this.Id = ulong.MaxValue;
        }

        internal ulong Id { get; private set; }

        /// <inheritdoc />
        public override string ToString () {
            return string.Format ("HANDLE(ClockHandler:0x{0:X16})", this.Id);
        }
    }
}
