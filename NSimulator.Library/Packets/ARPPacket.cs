#region

using System;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   ARP-�����.
    /// </summary>
    public sealed class ARPPacket : PacketBase {
        /// <summary>
        ///   ������������� ARP-������.
        /// </summary>
        /// <param name = "htype">��������� ������� (���).</param>
        /// <param name = "ptype">������� ������� (���).</param>
        /// <param name = "oper">�������� ARP.</param>
        /// <param name = "sha">��������� ����� ���������.</param>
        /// <param name = "spa">������� ����� ���������.</param>
        /// <param name = "tha">��������� ����� ����������.</param>
        /// <param name = "tpa">������� ����� ����������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "sha" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "spa" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "tha" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "tpa" /> �������� <c>null</c>.</exception>
        /// <exception cref = "IncorrectFieldLengthException"><paramref name = "sha" /> ����� ������������ �����.</exception>
        /// <exception cref = "IncorrectFieldLengthException"><paramref name = "spa" /> ����� ������������ �����.</exception>
        /// <exception cref = "IncorrectFieldLengthException"><paramref name = "tha" /> ����� ������������ �����.</exception>
        /// <exception cref = "IncorrectFieldLengthException"><paramref name = "tpa" /> ����� ������������ �����.</exception>
        public ARPPacket (HardwareType htype,
                          L3ProtocolType ptype,
                          ARPOperation oper,
                          IArray <byte> sha,
                          IArray <byte> spa,
                          IArray <byte> tha,
                          IArray <byte> tpa) {
            var hlen = FieldLengthAttribute.GetLength (htype);
            var plen = FieldLengthAttribute.GetLength (ptype);

            if (sha == null)
                throw new ArgumentNullException ("sha");

            if (spa == null)
                throw new ArgumentNullException ("spa");

            if (tha == null)
                throw new ArgumentNullException ("tha");

            if (tpa == null)
                throw new ArgumentNullException ("tpa");

            if (sha.Length != hlen)
                throw new IncorrectFieldLengthException ("SHA", sha.Length, hlen);

            if (spa.Length != plen)
                throw new IncorrectFieldLengthException ("SPA", spa.Length, plen);

            if (tha.Length != hlen)
                throw new IncorrectFieldLengthException ("THA", sha.Length, hlen);

            if (tpa.Length != plen)
                throw new IncorrectFieldLengthException ("TPA", spa.Length, plen);

            this.Data = new Array <byte> (new byte[8 + sha.Length + spa.Length + tha.Length + tpa.Length]);

            this.HardwareType = htype;
            this.ProtocolType = ptype;

            this.HardwareLength = hlen;
            this.ProtocolLength = plen;

            this.Operation = oper;

            this.SenderHardwareAddress = sha;
            this.SenderProtocolAddress = spa;

            this.TargetHardwareAddress = tha;
            this.TargetProtocolAddress = tpa;
        }

        /// <summary>
        ///   �������� ��� ���������� ������.
        /// </summary>
        /// <value>
        ///   ��� ���������� ������.
        /// </value>
        public HardwareType HardwareType {
            get { return (HardwareType) ((this.Data [0] << 8) | this.Data [1]); }
            private set {
                this.Data [0] = (byte) ((ushort) value >> 8);
                this.Data [1] = (byte) ((ushort) value & 0xFF);
            }
        }

        /// <summary>
        ///   �������� ��� �������� ������.
        /// </summary>
        /// <value>
        ///   ��� �������� ������.
        /// </value>
        public L3ProtocolType ProtocolType {
            get { return (L3ProtocolType) ((this.Data [2] << 8) | this.Data [3]); }
            private set {
                this.Data [2] = (byte) ((ushort) value >> 8);
                this.Data [3] = (byte) ((ushort) value & 0xFF);
            }
        }

        /// <summary>
        ///   �������� ����� ���������� ������.
        /// </summary>
        /// <value>
        ///   ����� ���������� ������.
        /// </value>
        public byte HardwareLength {
            get { return this.Data [4]; }
            private set { this.Data [4] = value; }
        }

        /// <summary>
        ///   �������� ����� �������� ������.
        /// </summary>
        /// <value>
        ///   ����� �������� ������.
        /// </value>
        public byte ProtocolLength {
            get { return this.Data [5]; }
            private set { this.Data [5] = value; }
        }

        /// <summary>
        ///   �������� ��� ARP-��������.
        /// </summary>
        /// <value>
        ///   ��� ARP-��������.
        /// </value>
        public ARPOperation Operation {
            get { return (ARPOperation) ((this.Data [6] << 8) | this.Data [7]); }
            private set {
                this.Data [6] = 0;
                this.Data [7] = (byte) value;
            }
        }

        /// <summary>
        ///   �������� ��������� ����� �����������.
        /// </summary>
        /// <value>
        ///   ��������� ����� �����������.
        /// </value>
        public IArray <byte> SenderHardwareAddress {
            get { return this [8, 8 + this.HardwareLength]; }
            private set {
                for (var i = 0; i < value.Length; ++ i)
                    this.Data [8 + i] = value [i];
            }
        }

        /// <summary>
        ///   �������� ������� ����� �����������.
        /// </summary>
        /// <value>
        ///   ������� ����� �����������.
        /// </value>
        public IArray <byte> SenderProtocolAddress {
            get { return this [8 + this.HardwareLength, 8 + this.HardwareLength + this.ProtocolLength]; }
            private set {
                for (var i = 0; i < value.Length; ++i)
                    this.Data [8 + this.HardwareLength + i] = value [i];
            }
        }

        /// <summary>
        ///   �������� ��������� ����� ����������.
        /// </summary>
        /// <value>
        ///   ��������� ����� ����������.
        /// </value>
        public IArray <byte> TargetHardwareAddress {
            get {
                return
                    this [
                        8 + this.HardwareLength + this.ProtocolLength, 8 + 2 * this.HardwareLength + this.ProtocolLength
                        ];
            }
            private set {
                for (var i = 0; i < value.Length; ++i)
                    this.Data [8 + this.HardwareLength + this.ProtocolLength + i] = value [i];
            }
        }

        /// <summary>
        ///   �������� ������� ����� ����������.
        /// </summary>
        /// <value>
        ///   ������� ����� ����������.
        /// </value>
        public IArray <byte> TargetProtocolAddress {
            get {
                return
                    this [
                        8 + 2 * this.HardwareLength + this.ProtocolLength,
                        8 + 2 * this.HardwareLength + 2 * this.ProtocolLength];
            }
            private set {
                for (var i = 0; i < value.Length; ++i)
                    this.Data [8 + 2 * this.HardwareLength + this.ProtocolLength + i] = value [i];
            }
        }

        /// <inheritdoc />
        public override IArray <byte> InternalData {
            get { return Array <byte>.Empty; }
        }
    }
}
