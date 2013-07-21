#region

using System;
using System.Collections.Generic;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public sealed class ClockMock : IClock {
        private readonly IDictionary <ClockHandler, ClockAction> all_handlers;
        private readonly DateTime baseTime;
        private readonly IDictionary <Func <bool>, ClockHandler> cond_handlers;
        private readonly IDictionary <ulong, IList <ClockHandler>> tick_handlers;

        public ClockMock (ulong tick_length) {
            this.TickLength = tick_length;
            this.CurrentTick = 0;
            this.IsSuspended = false;
            this.tick_handlers = new Dictionary <ulong, IList <ClockHandler>> ();
            this.cond_handlers = new Dictionary <Func <bool>, ClockHandler> ();
            this.all_handlers = new Dictionary <ClockHandler, ClockAction> ();
            this.baseTime = new DateTime (1970, 1, 1, 0, 0, 0);
        }

        #region IClock Members

        public ulong TickLength { get; private set; }

        public ulong CurrentTick { get; private set; }

        public bool IsSuspended { get; private set; }

        public DateTime CurrentTime {
            get { return this.baseTime.AddMilliseconds (this.TickLength * this.CurrentTick); }
        }

        public void Dispose () {
            this.Suspend ();
        }

        public ClockHandler RegisterAction (ulong tick, ClockAction action) {
            if (this.CurrentTick >= tick)
                return null;

            var new_id = new ClockHandler ();

            this.all_handlers.Add (new_id, action);
            if (! this.tick_handlers.ContainsKey (tick))
                this.tick_handlers.Add (tick, new List <ClockHandler> ());
            this.tick_handlers [tick].Add (new_id);

            return new_id;
        }

        public ClockHandler RegisterAction (ClockAction action) {
            return this.RegisterAction (this.CurrentTick + 1, action);
        }

        public ClockHandler RegisterActionAtTime (ulong delta_time, ClockAction action) {
            return this.RegisterAction (this.CurrentTick + this.CalculateTime (delta_time), action);
        }

        public ClockHandler RegisterConditionalAction (Func <bool> condition, ClockAction action) {
            var new_id = new ClockHandler ();

            this.all_handlers.Add (new_id, action);
            this.cond_handlers.Add (condition, new_id);

            return new_id;
        }

        public void RemoveAction (ClockHandler handler) {
            if (this.all_handlers.ContainsKey (handler))
                this.all_handlers [handler] = () => { };
        }

        public void Start () {
            while (!this.IsSuspended && this.tick_handlers.Count != 0) {
                if (this.tick_handlers.ContainsKey (this.CurrentTick)) {
                    foreach (var action in this.tick_handlers [this.CurrentTick]) {
                        try {
                            this.all_handlers [action].Invoke ();
                        }
                        catch (Exception exception) {
                            if (this.OnError != null)
                                this.OnError.Invoke (this, this.all_handlers [action], exception);
                        }
                    }

                    this.tick_handlers.Remove (this.CurrentTick);
                }

                foreach (var action in this.cond_handlers) {
                    try {
                        if (action.Key.Invoke ())
                            this.all_handlers [action.Value].Invoke ();
                    }
                    catch (Exception exception) {
                        if (this.OnError != null)
                            this.OnError.Invoke (this, this.all_handlers [action.Value], exception);
                    }
                }

                ++ this.CurrentTick;
            }
        }

        public void Suspend () {
            this.IsSuspended = true;
        }

        public void Resume () {
            this.IsSuspended = false;
        }

        public event ClockActionErrorEventHandler OnError;

        #endregion

        private ulong CalculateTime (ulong delta) {
            return (this.TickLength == 0) ? 1 : delta / this.TickLength + (ulong) (delta % this.TickLength == 0 ? 0 : 1);
        }
    }
}
