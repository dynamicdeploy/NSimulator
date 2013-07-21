#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Абстрактный пакет.
    /// </summary>
    public abstract class PacketBase : IPacket {
        /// <summary>
        ///   Получает срез массива байт данных.
        /// </summary>
        /// <param name = "from">Начало среза.</param>
        /// <param name = "to">Конец среза.</param>
        /// <returns>
        ///   Срез массива байт данных, т.е. элементы с индексами
        ///   <paramref name = "from" /> &lt;= i &lt; <paramref name = "to" />.
        /// </returns>
        protected IArray <byte> this [int from, int to] {
            get { return this.Data.Slice (from, to); }
        }

        #region IPacket Members

        /// <inheritdoc />
        public IArray <byte> Data { get; protected set; }

        /// <inheritdoc />
        public abstract IArray <byte> InternalData { get; }

        #endregion
    }
}
