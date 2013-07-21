#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Исключение, пробрасываемое при неверной записи MAC-адреса.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке перевода строки в MAC-адрес,
    ///   при этом строка имеет неверный формат.
    /// </remarks>
    [Serializable]
    public sealed class IncorrectMACAddressException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "address">Строка с MAC-адресом.</param>
        public IncorrectMACAddressException (string address)
            : base (string.Format (Strings.IncorrectMACAddressException, address)) {
            this.Address = address;
        }

        /// <summary>
        ///   Получает строку с MAC-адресом.
        /// </summary>
        /// <value>
        ///   Строка с MAC-адресом, неудовлетворяющая формату.
        /// </value>
        public string Address { get; private set; }
    }
}
