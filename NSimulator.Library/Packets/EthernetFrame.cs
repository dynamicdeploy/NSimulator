#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Ethernet-�����.
    /// </summary>
    public sealed class EthernetFrame : EthernetFrameBase {
        /// <summary>
        ///   ������������� ������.
        /// </summary>
        /// <param name = "packet">������.</param>
        /// <param name = "proto">��� ������ (��������).</param>
        /// <param name = "source">MAC-����� ���������.</param>
        /// <param name = "destination">MAC-����� ����������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "packet" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "source" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "destination" /> �������� <c>null</c>.</exception>
        public EthernetFrame (IPacket packet, EtherType proto, MACAddress source, MACAddress destination)
            : base (packet, proto, source, destination) {}

        private EthernetFrame (IArray <byte> data)
            : base (data) {}

        /// <inheritdoc />
        public override IArray <byte> InternalData {
            get { return this [14, this.Data.Length]; }
        }

        /// <summary>
        ///   �������� ������ �� "�����" ������.
        /// </summary>
        /// <param name = "data">������.</param>
        /// <returns>Ethernet-�����, ����������� � �������, ���������� � <paramref name = "data" />.</returns>
        /// <remarks>
        ///   ��� ����������� �������� ����� ������ �� ������ ���� ������ 14 ����.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "data" /> �������� <c>null</c>.</exception>
        /// <exception cref = "IncorrectPacketLengthException"><paramref name = "data" /> ����� �������� �����.</exception>
        public static EthernetFrame Parse (IArray <byte> data) {
            if (data == null)
                throw new ArgumentNullException ("data");

            if (data.Length < 14)
                throw new IncorrectPacketLengthException (Strings.EthernetFrame);

            return new EthernetFrame (data);
        }
    }
}
