#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   IP-адрес узла.
    /// </summary>
    public sealed class IPAddress : IEquatable <IPAddress> {
        /// <summary>
        ///   Инициализирует адрес байтовым массивом.
        /// </summary>
        /// <param name = "address">Байтовое представление IP-адреса.</param>
        /// <remarks>
        ///   Длина массива <paramref name = "address" /> должна быть 4 байта.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "address" /> является <c>null</c>.</exception>
        /// <exception cref = "IncorrectFieldLengthException">Длина параметра <paramref name = "address" /> неверна.</exception>
        public IPAddress (IArrayView <byte> address) {
            if (address == null)
                throw new ArgumentNullException ("address");

            if (address.Length != 4)
                throw new IncorrectFieldLengthException (Strings.IPAddress, address.Length, 4);

            this.Address = address;
        }

        /// <summary>
        ///   Получает байтовое представление адреса.
        /// </summary>
        /// <value>
        ///   Байтовое представление адреса.
        /// </value>
        public IArrayView <byte> Address { get; private set; }

        #region IEquatable<IPAddress> Members

        /// <inheritdoc />
        public bool Equals (IPAddress other) {
            if (other == null)
                return false;

            return ReferenceEquals (this, other) || this.Address.Equals (other.Address);
        }

        #endregion

        /// <inheritdoc />
        public override bool Equals (object obj) {
            return this.Equals (obj as IPAddress);
        }

        /// <inheritdoc />
        public override int GetHashCode () {
            return this.Address.GetHashCode ();
        }

        /// <inheritdoc />
        public override string ToString () {
            return string.Join (".", this.Address);
        }

        /// <summary>
        ///   Парсит строку, содержащую IP-адрес.
        /// </summary>
        /// <param name = "address">Строка с IP-адресом.</param>
        /// <returns>Экземпляр <see cref = "IPAddress" />, проинициализированный <paramref name = "address" />.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "address" /> является <c>null</c>.</exception>
        /// <exception cref = "IncorrectIPAddressException"><paramref name = "address" /> имеет недопустимый формат.</exception>
        public static IPAddress Parse (string address) {
            if (address == null)
                throw new ArgumentNullException ("address");

            try {
                return new IPAddress (new Array <byte> (Array.ConvertAll (address.Split (new [] { '.' }), byte.Parse)));
            }
            catch {
                throw new IncorrectIPAddressException (address);
            }
        }
    }
}
