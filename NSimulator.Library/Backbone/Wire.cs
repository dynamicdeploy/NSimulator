#region

using System;
using System.Collections.Generic;
using System.Linq;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   ����������� ����� �������� "������".
    /// </summary>
    /// <remarks>
    ///   ����� �������� "������" ���������� �� ����������� ����� �������� ���,
    ///   ��� ������ ����� ����� ��� �������� �����.
    /// </remarks>
    public abstract class Wire : BackboneBase {
        /// <summary>
        ///   ����, ������������ ��� �������� ������.
        /// </summary>
        protected readonly IClock clock;

        /// <summary>
        ///   ������� ��������� �������� �������.
        /// </summary>
        protected readonly Queue <Pair <IPacket, IInterfaceView>> packets;

        /// <summary>
        ///   ������������� ����� ��������.
        /// </summary>
        /// <param name = "clock">����, ������������ ��� ��������.</param>
        protected Wire (IClock clock)
            : base (2) {
            if (clock == null)
                throw new ArgumentNullException ("clock");

            this.clock = clock;
            this.State = WireState.WIRE_FREE;
            this.packets = new Queue <Pair <IPacket, IInterfaceView>> ();
        }

        /// <inheritdoc />
        public override void SendPacket (IPacket packet, IInterfaceView from) {
            /*
             * todo:
             * 1. ����������� ����������� full-duplex, half-duplex
             * 2. �������� ������������ �������� �������� ������������ � ������������.
             * 3. �������� ��������� �������� �������� (��� ����������� ���������������).
             * 4. ������� ������ ������� �������� ���������.
             */

            if (packet == null)
                throw new ArgumentNullException ("packet");

            if (from == null)
                throw new ArgumentNullException ("from");

            if (!this.endpoints.Contains (from))
                throw new InterfaceNotAttachedToBackboneException (from.Name, this.Name);

            if (this.endpoints.Count != 2)
                throw new Exception ();

            //if (this.State.Equals (WireState.WIRE_BUSY)) {
            //    this.packets.Enqueue (new Pair <IPacket, IInterfaceView> (packet, from));
            //    var time = (ulong) new Random ().Next (100);
            //    this.clock.RegisterActionAtTime (time,
            //                                     () => {
            //                                         var entry = this.packets.Dequeue ();
            //                                         this.SendPacket (entry.First, entry.Second);
            //                                     });
            //    return;
            //}

            this.State = WireState.WIRE_BUSY;

            var iface = (this.EndPoints.First () == from) ? this.EndPoints.Last () : this.EndPoints.First ();

            if (new Random ().NextDouble () < this.LossPercent)
                return;

            this.clock.RegisterAction (() => {
                                           this.State = WireState.WIRE_FREE;
                                           this.OnTransmit.Invoke (this, packet);
                                           iface.ReceivePacket (packet);
                                       });
        }

        /// <inheritdoc />
        public override event TransmitEventHandler OnTransmit;
    }
}
