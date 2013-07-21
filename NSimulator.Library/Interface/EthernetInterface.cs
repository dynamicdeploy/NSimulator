#region

using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   Ethernet-���������.
    /// </summary>
    [InterfacePrefix ("e")]
    public class EthernetInterface : EthernetInterfaceBase {
        /// <summary>
        ///   �������������� ���������.
        /// </summary>
        /// <remarks>
        ///   ���������� ����������� <see cref = "MACAddress.NullAddress">�������</see> MAC-�����.
        /// </remarks>
        public EthernetInterface ()
            : base (MACAddress.NullAddress) {}

        /// <summary>
        ///   �������������� ��������� MAC-�������.
        /// </summary>
        /// <param name = "macAddress">MAC-����� ����������.</param>
        public EthernetInterface (MACAddress macAddress)
            : base (macAddress) {}

        /// <inheritdoc />
        protected override bool IsCompatibleWith (IBackboneView backbone) {
            return (backbone is Ethernet);
        }
    }
}
