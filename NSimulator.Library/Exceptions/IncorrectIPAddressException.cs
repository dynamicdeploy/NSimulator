#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Исключение, пробрасываемое при неверной записи IP-адреса.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке перевода строки в IP-адрес,
    ///   при этом строка имеет неверный формат.
    /// </remarks>
    [Serializable]
    public sealed class IncorrectIPAddressException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "address">Строка с IP-адресом.</param>
        public IncorrectIPAddressException (string address)
            : base (string.Format (Strings.IncorrectIPAddressException, address)) {
            this.Address = address;
        }

        /// <summary>
        ///   Получает строку с IP-адресом.
        /// </summary>
        /// <value>
        ///   Строка с IP-адресом, неудовлетворяющая формату.
        /// </value>
        public string Address { get; private set; }
    }
}
