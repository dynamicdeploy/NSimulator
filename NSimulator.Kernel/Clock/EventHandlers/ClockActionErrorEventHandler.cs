#region

using System;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Действие, срабатывающее при ошибке отработки зарегистрированного действия.
    /// </summary>
    /// <seealso cref = "IClock" />
    /// <param name = "clock">Часы, инициировавшие выполнение действия.</param>
    /// <param name = "action">Действие, завершившееся с ошибкой.</param>
    /// <param name = "exception">Исключение, полученное при отработке.</param>
    public delegate void ClockActionErrorEventHandler (IClockView clock, ClockAction action, Exception exception);
}
