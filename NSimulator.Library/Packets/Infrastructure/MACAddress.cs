#region

using System;
using System.Globalization;
using System.Linq;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   MAC-адрес интерфейса.
    /// </summary>
    public sealed class MACAddress : IEquatable <MACAddress> {
        /// <summary>
        ///   Нулевой адрес.
        /// </summary>
        public static readonly MACAddress NullAddress = Parse ("00-00-00-00-00-00");

        /// <summary>
        ///   Инициализирует адрес байтовым массивом.
        /// </summary>
        /// <param name = "address">Байтовое представление MAC-адреса.</param>
        /// <remarks>
        ///   Длина массива <paramref name = "address" /> должна быть 6 байт.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "address" /> является <c>null</c>.</exception>
        /// <exception cref = "IncorrectFieldLengthException">Длина параметра <paramref name = "address" /> неверна.</exception>
        public MACAddress (IArrayView <byte> address) {
            if (address == null)
                throw new ArgumentNullException ("address");

            if (address.Length != 6)
                throw new IncorrectFieldLengthException (Strings.MACAddress, address.Length, 6);

            this.Address = address;
        }

        /// <summary>
        ///   Получает байтовое представление адреса.
        /// </summary>
        /// <value>
        ///   Байтовое представление адреса.
        /// </value>
        public IArrayView <byte> Address { get; private set; }

        #region IEquatable<MACAddress> Members

        /// <inheritdoc />
        public bool Equals (MACAddress other) {
            return other != null && this.Address.Equals (other.Address);
        }

        #endregion

        /// <inheritdoc />
        public override bool Equals (object obj) {
            return this.Equals (obj as MACAddress);
        }

        /// <inheritdoc />
        public override int GetHashCode () {
            return this.Address.Aggregate (0, (current, value) => current ^ value.GetHashCode ());
        }

        /// <inheritdoc />
        public override string ToString () {
            return string.Join ("-", Array <byte>.ConvertAll (this.Address, _ => _.ToString ("X2")));
        }

        /// <summary>
        ///   Парсит строку, содержащую MAC-адрес.
        /// </summary>
        /// <param name = "address">Строка с MAC-адресом.</param>
        /// <returns>Экземпляр <see cref = "MACAddress" />, проинициализированный <paramref name = "address" />.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "address" /> является <c>null</c>.</exception>
        /// <exception cref = "IncorrectMACAddressException"><paramref name = "address" /> имеет недопустимый формат.</exception>
        public static MACAddress Parse (string address) {
            if (address == null)
                throw new ArgumentNullException ("address");

            try {
                return
                    new MACAddress (
                        new Array <byte> (Array.ConvertAll (address.Split (new [] { '-' }),
                                                            _ => byte.Parse (_, NumberStyles.HexNumber))));
            }
            catch {
                throw new IncorrectMACAddressException (address);
            }
        }
    }
}
