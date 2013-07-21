#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.TestMocks {
    public sealed class UnicastBackboneMock : BackboneMock {
        private readonly IClock clock;

        private UnicastBackboneMock (IClock clock) {
            this.clock = clock;
        }

        public UnicastBackboneMock (string id, int capacity, ulong speed, IClock clock)
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

            var endpoint_index = packet.Data [0];
            if (endpoint_index >= this.EndPointsCount)
                return;

            this.State = StateMock.BUSY;

            this.clock.RegisterActionAtTime (MathHelper.CalculateTime (packet.Data.Length, this.Speed),
                                             () => {
                                                 this.State = StateMock.FREE;
                                                 this.OnTransmit (this, packet);
                                                 this.endpoints [endpoint_index].ReceivePacket (
                                                     packet);
                                             });
        }

        public override event TransmitEventHandler OnTransmit;
    }
}
