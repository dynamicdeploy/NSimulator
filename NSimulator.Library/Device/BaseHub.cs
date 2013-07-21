#region

using System;
using System.Linq;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   ����������� ��� � ����������� ������������.
    /// </summary>
    /// <typeparam name = "T">��� �����������.</typeparam>
    /// <remarks>
    ///   ����� ���������� <typeparamref name = "T" /> ������ ����� ����������� �� ���������.
    /// </remarks>
    public abstract class BaseHub <T> : DeviceBase
        where T : IInterface, new ( ) {
        /// <summary>
        ///   �������������� ��� �������� ������ �����������.
        /// </summary>
        /// <param name = "interfaces">���������� �����������.</param>
        /// <exception cref = "ArgumentOutOfRangeException"><paramref name = "interfaces" /> ������ 0.</exception>
        protected BaseHub (int interfaces) {
            if (interfaces < 0)
                throw new ArgumentOutOfRangeException ("interfaces");

            for (var i = 0; i < interfaces; ++ i)
                base.AddInterface (new T ());

            base.Enable ();
        }

        /// <summary>
        ///   ����� ������������ ���������� <see cref = "FeatureNotSupportedException" />, ������ ���
        ///   ��� ������ ���������.
        /// </summary>
        /// <exception cref = "FeatureNotSupportedException">��� ������ ���������.</exception>
        public override sealed void Disable () {
            throw new FeatureNotSupportedException (this.Name, Features.Disable);
        }

        /// <summary>
        ///   ����� ������������ ���������� <see cref = "FeatureNotSupportedException" />, ������ ���
        ///   ��� �������� ������� �������� � �� ����� ��������.
        /// </summary>
        /// <param name = "engine">��������������� ��������.</param>
        /// <exception cref = "FeatureNotSupportedException">��� ������ "�������".</exception>
        public override sealed void SetEngine (IDeviceEngine engine) {
            throw new FeatureNotSupportedException (this.Name, Features.SetEngine);
        }

        /// <summary>
        ///   ����� ������������ �����.
        /// </summary>
        /// <param name = "packet">�����, ������� ����� ����������.</param>
        /// <param name = "from">���������, � �������� ������� �����.</param>
        /// <remarks>
        ///   ��� ��������� ������ ����� ���������� �� ��� ����������, �����
        ///   ����, � �������� �� ��� �������, �.�. <paramref name = "from" />.
        /// </remarks>
        public override void ProcessPacket (IPacket packet, IInterfaceView from) {
            if (from.Device != this)
                throw new InterfaceNotAttachedToDeviceException (from.Name);

            foreach (var iface in this.Interfaces.Where (iface => iface != from)) {
                try {
                    iface.SendPacket (packet);
                }
                catch {}
            }
        }
        }
}
