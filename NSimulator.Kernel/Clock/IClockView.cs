#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс часов.
    ///   Предоставляется интерфейс только для чтения.
    /// </summary>
    /// <seealso cref = "IClock" />
    public interface IClockView {
        /// <summary>
        ///   Получает длину тика в наносекундах.
        /// </summary>
        /// <value>
        ///   Длина тика в наносекундах.
        /// </value>
        ulong TickLength { get; }

        /// <summary>
        ///   Получает номер текущего тика.
        /// </summary>
        /// <value>
        ///   Номер текущего тика.
        /// </value>
        ulong CurrentTick { get; }

        /// <summary>
        ///   Получает текущий статус часов.
        /// </summary>
        /// <value>
        ///   <c>true</c>, если в текущий момент часы приостановлены.
        /// </value>
        bool IsSuspended { get; }

        /// <summary>
        ///   Получает текущее время.
        /// </summary>
        /// <remarks>
        ///   Текущее время обычно измеряется от 00:00:00 1 января 1970 года,
        ///   но это может зависеть от реализации. Прошедше время вычисляется через
        ///   длину тика <see cref = "TickLength" /> и номер текущего тика <see cref = "CurrentTick" />.
        /// </remarks>
        /// <value>
        ///   Текущее время.
        /// </value>
        DateTime CurrentTime { get; }

        /// <summary>
        ///   Происходит при ошибке отработки зарегистрированного действия.
        /// </summary>
        event ClockActionErrorEventHandler OnError;
    }
}
