#region

using System;
using System.Linq;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public sealed class BroadcastBackboneMock : BackboneMock {
        private readonly IClock clock;

        private BroadcastBackboneMock (IClock clock) {
            this.clock = clock;
        }

        public BroadcastBackboneMock (string id, int capacity, ulong speed, IClock clock)
            : base (id, capacity, speed) {
            this.clock = clock;
        }

        public override void SendPacket (IPacket packet, IInterfaceView from) {
            if (packet == null)
                throw new ArgumentNullException ();

            if (from == null)
                throw new ArgumentNullException ();

            if (!this.endpoints.Contains (from))
                throw new ArgumentException ();

            this.State = StateMock.BUSY;

            foreach (var endpoint in this.EndPoints.Where (_ => _ != from)) {
                var endpoint1 = endpoint;
                this.clock.RegisterActionAtTime (MathHelper.CalculateTime (packet.Data.Length, this.Speed),
                                                 () => {
                                                     this.State = StateMock.FREE;
                                                     this.OnTransmit (this, packet);
                                                     endpoint1.ReceivePacket (packet);
                                                 });
            }
        }

        public override event TransmitEventHandler OnTransmit;
    }
}
