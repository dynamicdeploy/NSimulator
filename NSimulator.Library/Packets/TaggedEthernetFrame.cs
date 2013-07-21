#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Теггированный Ethernet-фрейм.
    /// </summary>
    public sealed class TaggedEthernetFrame : EthernetFrameBase {
        /// <summary>
        ///   Инициализация фрейма.
        /// </summary>
        /// <param name = "packet">Данные.</param>
        /// <param name = "proto">Тип данных (протокол).</param>
        /// <param name = "vlan">Номер vlan.</param>
        /// <param name = "source">MAC-адрес источника.</param>
        /// <param name = "destination">MAC-адрес назначения.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "packet" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "source" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "destination" /> является <c>null</c>.</exception>
        public TaggedEthernetFrame (IPacket packet,
                                    EtherType proto,
                                    short vlan,
                                    MACAddress source,
                                    MACAddress destination)
            : base (packet == null ? 0 : (packet.Data.Length + 4), EtherType.Tagged, source, destination) {
            if (packet == null)
                throw new ArgumentNullException ("packet");

            this.Data [14] = this.Data [15] = 0;
            this.CanonicalFormatIndicator = false;
            this.PriorityCodePoint = 0;

            this.VlanID = vlan;
            this.Type = proto;

            for (var i = 0; i < packet.Data.Length; ++i)
                this.Data [18 + i] = packet.Data [i];
        }

        private TaggedEthernetFrame (IArray <byte> data)
            : base (data) {}

        /// <summary>
        ///   Получает приоритет.
        /// </summary>
        /// <remarks>
        ///   Приоритет используется в IEEE802.1p для задания приоритета трафика.
        /// </remarks>
        /// <value>
        ///   Приоритет.
        /// </value>
        public byte PriorityCodePoint {
            get { return (byte) ((this.Data [14] >> 5) & 0x07); }
            private set { this.Data [14] |= (byte) ((value & 0x07) << 5); }
        }

        /// <summary>
        ///   Получает индикатор канонического формата.
        /// </summary>
        /// <remarks>
        ///   Индикатор используется для совместимости Ethernet и Token Ring сетей.
        /// </remarks>
        /// <value>
        ///   0, если MAC-адрес имеет канонический формат и 1 иначе.
        /// </value>
        public bool CanonicalFormatIndicator {
            get { return (this.Data [14] & 0x10) == 1; }
            private set {
                if (value)
                    this.Data [14] |= 0x10;
                else
                    this.Data [14] &= 0xEF;
            }
        }

        /// <summary>
        ///   Получает номер vlan.
        /// </summary>
        /// <value>
        ///   Номер vlan.
        /// </value>
        public short VlanID {
            get { return (short) (((this.Data [14] & 0x0F) << 8) | this.Data [15]); }
            private set {
                this.Data [14] |= (byte) ((value >> 8) & 0x0F);
                this.Data [15] = (byte) (value & 0xFF);
            }
        }

        /// <inheritdoc />
        public override IArray <byte> InternalData {
            get { return this [18, this.Data.Length]; }
        }

        /// <inheritdoc />
        public override EtherType Type {
            get { return (EtherType) (this.Data [16] << 8 + this.Data [17]); }
            protected set {
                this.Data [16] = (byte) ((ushort) value >> 8);
                this.Data [17] = (byte) ((ushort) value & 0xFF);
            }
        }

        /// <summary>
        ///   Создание фрейма из "сырых" данных.
        /// </summary>
        /// <param name = "data">Данные.</param>
        /// <returns>Ethernet-фрейм, совпадающий с данными, указанными в <paramref name = "data" />.</returns>
        /// <remarks>
        ///   <para>Для корректного создания длина данных не должна быть меньше 18 байт.</para>
        ///   <para>При инициализации фрейма данные не копируются.</para>
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "data" /> является <c>null</c>.</exception>
        /// <exception cref = "IncorrectPacketLengthException"><paramref name = "data" /> имеет неверную длину.</exception>
        public static TaggedEthernetFrame Parse (IArray <byte> data) {
            if (data == null)
                throw new ArgumentNullException ("data");

            if (data.Length < 18)
                throw new IncorrectPacketLengthException (Strings.TaggedEthernetFrame);

            return new TaggedEthernetFrame (data);
        }
    }
}
