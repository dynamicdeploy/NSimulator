#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Исключение, пробрасываемое при некорректной длине пакета.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке инициализации пакета
    ///   данными, имеющими неверную длину.
    /// </remarks>
    [Serializable]
    public class IncorrectPacketLengthException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "packet">Название пакета.</param>
        public IncorrectPacketLengthException (string packet)
            : base (string.Format (Strings.IncorrectPacketLengthException, packet)) {
            this.Packet = packet;
        }

        /// <summary>
        ///   Получает название пакета.
        /// </summary>
        /// <value>
        ///   Название пакета, который неверно проинициализирован.
        /// </value>
        public string Packet { get; private set; }
    }
}
