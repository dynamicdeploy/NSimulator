#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Ethernet-фрейм.
    /// </summary>
    public sealed class EthernetFrame : EthernetFrameBase {
        /// <summary>
        ///   Инициализация фрейма.
        /// </summary>
        /// <param name = "packet">Данные.</param>
        /// <param name = "proto">Тип данных (протокол).</param>
        /// <param name = "source">MAC-адрес источника.</param>
        /// <param name = "destination">MAC-адрес назначения.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "packet" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "source" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "destination" /> является <c>null</c>.</exception>
        public EthernetFrame (IPacket packet, EtherType proto, MACAddress source, MACAddress destination)
            : base (packet, proto, source, destination) {}

        private EthernetFrame (IArray <byte> data)
            : base (data) {}

        /// <inheritdoc />
        public override IArray <byte> InternalData {
            get { return this [14, this.Data.Length]; }
        }

        /// <summary>
        ///   Создание фрейма из "сырых" данных.
        /// </summary>
        /// <param name = "data">Данные.</param>
        /// <returns>Ethernet-фрейм, совпадающий с данными, указанными в <paramref name = "data" />.</returns>
        /// <remarks>
        ///   Для корректного создания длина данных не должна быть меньше 14 байт.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "data" /> является <c>null</c>.</exception>
        /// <exception cref = "IncorrectPacketLengthException"><paramref name = "data" /> имеет неверную длину.</exception>
        public static EthernetFrame Parse (IArray <byte> data) {
            if (data == null)
                throw new ArgumentNullException ("data");

            if (data.Length < 14)
                throw new IncorrectPacketLengthException (Strings.EthernetFrame);

            return new EthernetFrame (data);
        }
    }
}
