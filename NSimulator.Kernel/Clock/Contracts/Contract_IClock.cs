#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    [ContractClassFor (typeof (IClock))]
    internal abstract class Contract_IClock : IClock {
        #region IClock Members

        public abstract ulong TickLength { get; }
        public abstract ulong CurrentTick { get; }
        public abstract bool IsSuspended { get; }
        public abstract DateTime CurrentTime { get; }
        public abstract event ClockActionErrorEventHandler OnError;

        public void Dispose () {}

        public ClockHandler RegisterAction (ulong tick, ClockAction action) {
            Contract.Requires <ArgumentNullException> (action != null);
            Contract.Ensures ((Contract.Result <ClockHandler> () != null && (tick > this.CurrentTick)) ||
                              Contract.Result <ClockHandler> () == null);
            return default (ClockHandler);
        }

        public ClockHandler RegisterAction (ClockAction action) {
            Contract.Requires <ArgumentNullException> (action != null);
            Contract.Ensures (Contract.Result <ClockHandler> () != null);
            return default (ClockHandler);
        }

        public ClockHandler RegisterActionAtTime (ulong delta_time, ClockAction action) {
            Contract.Requires <ArgumentNullException> (action != null);
            Contract.Ensures ((Contract.Result <ClockHandler> () != null && delta_time != 0) ||
                              Contract.Result <ClockHandler> () == null);
            return default (ClockHandler);
        }

        public ClockHandler RegisterConditionalAction (Func <bool> condition, ClockAction action) {
            Contract.Requires <ArgumentNullException> (condition != null);
            Contract.Requires <ArgumentNullException> (action != null);
            Contract.Ensures (Contract.Result <ClockHandler> () != null);
            return default (ClockHandler);
        }

        public void RemoveAction (ClockHandler handler) {
            Contract.Requires <ArgumentNullException> (handler != null);
        }

        public void Start () {}

        public void Suspend () {
            Contract.Ensures (this.IsSuspended);
        }

        public void Resume () {
            Contract.Ensures (! this.IsSuspended);
        }

        #endregion
    }
}
