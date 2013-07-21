#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   ѕростые часы.
    /// </summary>
    /// <seealso cref = "IClock" />
    /// <seealso cref = "RealtimeClock" />
    /// <remarks>
    ///   ¬ простых часах новый тик происходит сразу после завершени€
    ///   зарегистрированных действий предыдущего. ѕри этом продолжительность
    ///   тика может оказатьс€ меньше реальной. ƒл€ того, чтобы тики часов
    ///   были не меньше реальной продолжительности обработки, можно использовать
    ///   <see cref = "RealtimeClock" /> - часы реального времени.
    /// </remarks>
    public class BaseClock : IClock {
        private readonly Dictionary <ulong, ClockAction> all_handlers;
        private readonly Dictionary <Func <bool>, ulong> cond_handlers;

        private readonly UniqueIdGenerator id_gen;

        private readonly DateTime startTime;
        private readonly object syncRoot;
        private readonly Dictionary <ulong, IList <ulong>> tick_handlers;
        private bool disposed;

        /// <summary>
        ///   »нициализаци€ нового экземпл€ра часов.
        ///   ¬ начальный момент номер тика равен нулю, часы приостановлены.
        /// </summary>
        public BaseClock () {
            this.TickLength = 0;
            this.CurrentTick = 0;
            this.IsSuspended = false;
            this.tick_handlers = new Dictionary <ulong, IList <ulong>> ();
            this.cond_handlers = new Dictionary <Func <bool>, ulong> ();
            this.all_handlers = new Dictionary <ulong, ClockAction> ();
            this.id_gen = new UniqueIdGenerator ();
            this.syncRoot = new object ();
            this.startTime = new DateTime (1970, 1, 1, 0, 0, 0);
            this.disposed = false;
        }

        #region IClock Members

        /// <inheritdoc />
        public ulong TickLength { get; protected set; }

        /// <inheritdoc />
        public ulong CurrentTick { get; private set; }

        /// <inheritdoc />
        public bool IsSuspended { get; private set; }

        /// <inhertidoc />
        public DateTime CurrentTime {
            get { return this.startTime.AddMilliseconds (this.TickLength * this.CurrentTick / 1000); }
        }

        /// <summary>
        ///   ¬ыполн€ет регистрацию действи€, которое произойдЄт в заданный тик.
        /// </summary>
        /// <param name = "tick">Ќомер тика, в который будет выполнено действие.</param>
        /// <param name = "action">ƒействие, которое должно произойти.</param>
        /// <returns>ќписатель зарегистрированного действи€ или <c>null</c>, если действие зарегистрировано неуспешно.</returns>
        /// <remarks>
        ///   –егистраци€ действи€ считаетс€ успешной, если оно регистрируетс€ не на текущий тик.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "action" /> €вл€етс€ <c>null</c>.</exception>
        public virtual ClockHandler RegisterAction (ulong tick, ClockAction action) {
            lock (this.syncRoot) {
                if (this.CurrentTick < tick) {
                    var id = this.id_gen.GetNext ();
                    this.all_handlers.Add (id, action);

                    if (!this.tick_handlers.ContainsKey (tick))
                        this.tick_handlers.Add (tick, new List <ulong> ());

                    this.tick_handlers [tick].Add (id);

                    return new ClockHandler (id);
                }

                return null;
            }
        }

        /// <inheritdoc />
        public virtual ClockHandler RegisterAction (ClockAction action) {
            return this.RegisterAction (this.CurrentTick + 1, action);
        }

        /// <inheritdoc />
        public virtual ClockHandler RegisterActionAtTime (ulong delta_time, ClockAction action) {
            return
                this.RegisterAction (
                    this.CurrentTick + (this.TickLength == 0 ? 0 : CeilDiv (delta_time, this.TickLength)), action);
        }

        /// <inheritdoc />
        public ClockHandler RegisterConditionalAction (Func <bool> condition, ClockAction action) {
            lock (this.syncRoot) {
                var id = this.id_gen.GetNext ();

                this.all_handlers.Add (id, action);
                this.cond_handlers.Add (condition, id);

                return new ClockHandler (id);
            }
        }

        /// <inheritdoc />
        public void RemoveAction (ClockHandler handler) {
            lock (this.syncRoot) {
                if (! this.all_handlers.ContainsKey (handler.Id))
                    throw new InvalidClockHandlerException (handler);

                if (this.all_handlers [handler.Id] == null)
                    throw new InvalidClockHandlerException (handler);

                this.all_handlers [handler.Id] = null;
            }
        }

        /// <summary>
        ///   ¬ыполн€ет запуск обработки действий.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     ќбработка действий происходит в том потоке, из которого был вызван метод.
        ///   </para>
        /// 
        ///   <para>
        ///     ѕосле запуска процесса обработки действий процесс можно либо приостановить, вызвав метод
        ///     <see cref = "IClock.Suspend" />, либо возобновить, вызвав метод <see cref = "IClock.Resume" />.
        ///     „тобы полностью прекратить обработку, нужно вызвать метод <see cref = "IDisposable.Dispose" />.
        ///     ѕосле вызова этого метода обработку возобновить будет невозможно.
        ///   </para>
        /// </remarks>
        /// <exception cref = "InvalidOperationException">„асы уничтожены.</exception>
        public virtual void Start () {
            if (this.disposed)
                throw new InvalidOperationException (Strings.ClockDestroyedException);

            while (! this.disposed) {
                var current = DateTime.Now;

                if (!this.IsSuspended) {
                    IList <ulong> tickHandlers = new List <ulong> ();
                    var conditionalHandlers = new HashSet <Pair <Func <bool>, ulong>> ();
                    var removedConditional = new List <Func <bool>> ();

                    lock (this.syncRoot) {
                        if (this.tick_handlers.ContainsKey (this.CurrentTick)) {
                            tickHandlers = this.tick_handlers [this.CurrentTick];
                            this.tick_handlers.Remove (this.CurrentTick);
                        }

                        foreach (var handler in this.cond_handlers)
                            conditionalHandlers.Add (new Pair <Func <bool>, ulong> (handler));
                    }

                    foreach (
                        var handler in
                            tickHandlers.Select (handlerId => this.all_handlers [handlerId]).Where (a => a != null)) {
                        try {
                            handler.Invoke ();
                        }
                        catch (Exception exception) {
                            if (this.OnError != null)
                                this.OnError.Invoke (this, handler, exception);
                        }
                    }

                    foreach (var handler in conditionalHandlers) {
                        var action = this.all_handlers [handler.Second];
                        if (action != null) {
                            try {
                                if (handler.First.Invoke ())
                                    action.Invoke ();
                            }
                            catch (Exception exception) {
                                if (this.OnError != null)
                                    this.OnError.Invoke (this, action, exception);
                            }
                        }
                        else
                            removedConditional.Add (handler.First);
                    }

                    foreach (var handler in tickHandlers)
                        this.all_handlers.Remove (handler);

                    foreach (var key in removedConditional)
                        this.cond_handlers.Remove (key);

                    ++ this.CurrentTick;
                }

                Thread.Sleep (this.SleepLength (DateTime.Now - current));
            }
        }

        /// <inheritdoc />
        public virtual void Suspend () {
            this.IsSuspended = true;
        }

        /// <inheritdoc />
        public virtual void Resume () {
            this.IsSuspended = false;
        }

        /// <inheritdoc />
        public event ClockActionErrorEventHandler OnError;

        /// <summary>
        ///   ¬ыполн€ет уничтожение часов.
        /// </summary>
        /// <remarks>
        ///   ѕосле уничтожени€ часов возобновить обработку действий будет невозможно.
        /// </remarks>
        public virtual void Dispose () {
            this.disposed = true;
        }

        #endregion

        private static ulong CeilDiv (ulong a, ulong b) {
            return a / b + (a % b == 0 ? (ulong) 0 : 1);
        }

        private int SleepLength (TimeSpan span) {
            var result = this.TickLength / 1000 - span.TotalMilliseconds;
            return result < 0 ? 0 : (int) result;
        }
    }
}
