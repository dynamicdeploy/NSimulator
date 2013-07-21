#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс часов.
    ///   Предоставляется расширение интерфейса только для чтения до полного интерфейса.
    /// </summary>
    /// <seealso cref = "IClockView" />
    [ContractClass (typeof (Contract_IClock))]
    public interface IClock : IClockView, IDisposable {
        /// <summary>
        ///   Выполняет регистрацию действия, которое произойдёт в заданный тик.
        /// </summary>
        /// <param name = "tick">Номер тика, в который будет выполнено действие.</param>
        /// <param name = "action">Действие, которое должно произойти.</param>
        /// <returns>Описатель зарегистрированного действия или <c>null</c>, если действие зарегистрировано неуспешно.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "action" /> является <c>null</c>.</exception>
        ClockHandler RegisterAction (ulong tick, ClockAction action);

        /// <summary>
        ///   Выполняет регистрацию действия, которое должно произойти в следующий тик.
        /// </summary>
        /// <param name = "action">Действие, которое должно произойти.</param>
        /// <returns>Описатель зарегистрированного действия или <c>null</c>, если действие зарегистрировано неуспешно.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "action" /> является <c>null</c>.</exception>
        ClockHandler RegisterAction (ClockAction action);

        /// <summary>
        ///   Выполняет регистрацию действия, которое должно произойти через заданное время.
        /// </summary>
        /// <param name = "delta_time">Время в наносекундах, через которое действие должно произойти.</param>
        /// <param name = "action">Действие, которое должно произойти.</param>
        /// <returns>Описатель зарегистрированного действия или <c>null</c>, если действие зарегистрировано неуспешно.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "action" /> является <c>null</c>.</exception>
        ClockHandler RegisterActionAtTime (ulong delta_time, ClockAction action);

        /// <summary>
        ///   Выполняет регистрацию действия, которое должно произойти по заданному условию.
        /// </summary>
        /// <param name = "condition">Условие происхождения действия.</param>
        /// <param name = "action">Действие, которое должно произойти.</param>
        /// <returns>Описатель зарегистрированного действия или <c>null</c>, если действие зарегистрировано неуспешно.</returns>
        /// <exception cref = "ArgumentNullException"><paramref name = "condition" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "action" /> является <c>null</c>.</exception>
        ClockHandler RegisterConditionalAction (Func <bool> condition, ClockAction action);

        /// <summary>
        ///   Отменяет регистрацию действия.
        /// </summary>
        /// <param name = "handler">Действие, регистрацию которого нужно отменить.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "handler" /> является <c>null</c>.</exception>
        /// <exception cref = "InvalidClockHandlerException">Описатель действия <paramref name = "handler" /> является неверным.</exception>
        void RemoveAction (ClockHandler handler);

        /// <summary>
        ///   Выполняет запуск обработки действий.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     В зависимости от реализации, обработка действий может происходить как в потоке,
        ///     из которого был вызван метод, так и может быть создан отдельный поток с обработкой
        ///     действий.
        ///   </para>
        /// 
        ///   <para>
        ///     После запуска процесса обработки действий процесс можно либо приостановить, вызвав метод
        ///     <see cref = "IClock.Suspend" />, либо возобновить, вызвав метод <see cref = "IClock.Resume" />.
        ///     Чтобы полностью прекратить обработку, нужно вызвать метод <see cref = "IDisposable.Dispose" />.
        ///     После вызова этого метода обработку возобновить будет невозможно.
        ///   </para>
        /// </remarks>
        /// <exception cref = "InvalidOperationException">Часы уничтожены.</exception>
        void Start ();

        /// <summary>
        ///   Выполняет приостановку обработки действий.
        /// </summary>
        void Suspend ();

        /// <summary>
        ///   Выполняет возобновление обработки действий.
        /// </summary>
        void Resume ();
    }
}
