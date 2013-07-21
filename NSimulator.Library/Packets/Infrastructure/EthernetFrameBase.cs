#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Абстрактный Ethernet-фрейм.
    /// </summary>
    public abstract class EthernetFrameBase : PacketBase {
        /// <summary>
        ///   Инициализация фрейма.
        /// </summary>
        /// <param name = "length">Длина данных.</param>
        /// <param name = "proto">Тип данных (протокол).</param>
        /// <param name = "source">MAC-адрес источника.</param>
        /// <param name = "destination">MAC-адрес назначения.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "source" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "destination" /> является <c>null</c>.</exception>
        protected EthernetFrameBase (int length, EtherType proto, MACAddress source, MACAddress destination) {
            if (source == null)
                throw new ArgumentNullException ("source");

            if (destination == null)
                throw new ArgumentNullException ("destination");

            this.Data = new Array <byte> (new byte[14 + length]);

            this.Destination = destination;
            this.Source = source;
            this.SetType (proto);
        }

        /// <summary>
        ///   Инициализация "сырого" фрейма.
        /// </summary>
        /// <param name = "data">Данные фрейма.</param>
        protected EthernetFrameBase (IArray <byte> data) {
            this.Data = data;
        }

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
        protected EthernetFrameBase (IPacket packet, EtherType proto, MACAddress source, MACAddress destination) {
            if (packet == null)
                throw new ArgumentNullException ("packet");

            if (source == null)
                throw new ArgumentNullException ("source");

            if (destination == null)
                throw new ArgumentNullException ("destination");

            this.Data = new Array <byte> (new byte[14 + packet.Data.Length]);

            this.Destination = destination;
            this.Source = source;
            this.SetType (proto);

            for (var i = 0; i < packet.Data.Length; ++i)
                this.Data [14 + i] = packet.Data [i];
        }

        /// <summary>
        ///   Получает MAC-адрес назначения.
        /// </summary>
        /// <value>
        ///   MAC-адрес назначения.
        /// </value>
        public MACAddress Destination {
            get { return new MACAddress (this [0, 6]); }
            private set {
                for (var i = 0; i < 6; ++i)
                    this.Data [i] = value.Address [i];
            }
        }

        /// <summary>
        ///   Получает MAC-адрес источника.
        /// </summary>
        /// <value>
        ///   MAC-адрес источника.
        /// </value>
        public MACAddress Source {
            get { return new MACAddress (this [6, 12]); }
            private set {
                for (var i = 0; i < 6; ++i)
                    this.Data [6 + i] = value.Address [i];
            }
        }

        /// <summary>
        ///   Получает тип данных (протокол).
        /// </summary>
        /// <value>
        ///   Тип данных (протокол).
        /// </value>
        public virtual EtherType Type {
            get { return (EtherType) (this.Data [12] << 8 + this.Data [13]); }
            protected set { this.SetType (value); }
        }

        private void SetType (EtherType value) {
            this.Data [12] = (byte) ((ushort) value >> 8);
            this.Data [13] = (byte) ((ushort) value & 0xFF);
        }
    }
}
