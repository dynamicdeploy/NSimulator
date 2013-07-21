#region

using System;
using System.Collections.Generic;
using System.Linq;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Абстрактная среда передачи "провод".
    /// </summary>
    /// <remarks>
    ///   Среда передачи "провод" отличается от абстрактной среды передачи тем,
    ///   что провод имеет ровно две конечные точки.
    /// </remarks>
    public abstract class Wire : BackboneBase {
        /// <summary>
        ///   Часы, используемые при передаче данных.
        /// </summary>
        protected readonly IClock clock;

        /// <summary>
        ///   Очередь ожидающих передачи пакетов.
        /// </summary>
        protected readonly Queue <Pair <IPacket, IInterfaceView>> packets;

        /// <summary>
        ///   Инициализация среды передачи.
        /// </summary>
        /// <param name = "clock">Часы, используемые при передаче.</param>
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
             * 1. Реализовать возможность full-duplex, half-duplex
             * 2. Уточнить максимальный интервал ожидания ретрансляции в документации.
             * 3. Вытащить константу ожидания отдельно (для возможности переопределения).
             * 4. Сделать расчёт времени передачи сообщения.
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
