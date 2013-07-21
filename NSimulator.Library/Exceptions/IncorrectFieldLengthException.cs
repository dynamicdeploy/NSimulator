#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Исключение, пробрасываемое при некорректной длине поля пакета.
    /// </summary>
    /// <remarks>
    ///   Исключение пробрасывается при попытке инициализации поля пакета
    ///   значением, имеющем неверную длину.
    /// </remarks>
    [Serializable]
    public sealed class IncorrectFieldLengthException : NSimulatorException {
        /// <summary>
        ///   Инициализация нового экземпляра исключения.
        /// </summary>
        /// <param name = "field">Название поля.</param>
        /// <param name = "actual">Переданная длина.</param>
        /// <param name = "expected">Ожидаемая длина.</param>
        public IncorrectFieldLengthException (string field, int actual, int expected)
            : base (string.Format (Strings.IncorrectFieldLengthException, field, actual, expected)) {
            this.Field = field;
        }

        /// <summary>
        ///   Получает название поля.
        /// </summary>
        /// <value>
        ///   Название поля, которое неверно проинициализировано.
        /// </value>
        public string Field { get; private set; }

        /// <summary>
        ///   Получает переданную длину данных.
        /// </summary>
        /// <value>
        ///   Длина данных, которыми была произведена попытка инициализации.
        /// </value>
        public int Actual { get; private set; }

        /// <summary>
        ///   Получает длину поля.
        /// </summary>
        /// <value>
        ///   Длина поля. Должна совпадать с размером данных.
        /// </value>
        public int Expected { get; private set; }
    }
}
