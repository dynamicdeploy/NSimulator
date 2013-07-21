#region

using System;
using System.Linq;
using System.Xml;
using NSimulator.Kernel;

#endregion

namespace NSimulator.Library {
    /// <summary>
    ///   ���������� "����".
    /// </summary>
    /// <typeparam name = "Interface">��� ����������.</typeparam>
    /// <remarks>
    ///   ��������� "����" ����� ���� ���� ��������� ���������� ����.
    /// </remarks>
    public sealed class Host <Interface> : DeviceBase
        where Interface : IInterface, new ( ) {
        private bool initialized;

        /// <summary>
        ///   ������������� ���������� "����".
        /// </summary>
        /// <remarks>
        ///   ����� ������������� ���������� ���������.
        /// </remarks>
        public Host () {
            base.AddInterface (new Interface ());
            this.Disable ();

            this.initialized = true;
        }

        /// <summary>
        /// </summary>
        public IInterfaceView HostInterface {
            get { return this.Interfaces.First (); }
        }

        /// <summary>
        ///   ��������� ���������� ��������� � ����������.
        /// </summary>
        /// <param name = "iface">���������� ���������, ������� ����� ��������.</param>
        /// <remarks>
        ///   � ���������� "����" ����� ��������� ��������� ������ �� ����� �������������.
        ///   ����� �������� ���������� ������� ����������� �����������.
        /// </remarks>
        /// <exception cref = "FeatureNotSupportedException">����������� �� ��������������.</exception>
        public override void AddInterface (IInterface iface) {
            if (this.initialized)
                throw new FeatureNotSupportedException (this.Name, Features.AddInterface);

            base.AddInterface (iface);
        }

        /// <summary>
        ///   ��������� ���������� ��������� � ����������.
        /// </summary>
        /// <param name = "iface">���������� ���������, ������� ����� ��������.</param>
        /// <param name = "name">�������� ����������.</param>
        /// <remarks>
        ///   � ���������� "����" ����� ��������� ��������� ������ �� ����� �������������.
        ///   ����� �������� ���������� ������� ����������� �����������.
        /// </remarks>
        /// <exception cref = "FeatureNotSupportedException">����������� �� ��������������.</exception>
        public override void AddInterface (IInterface iface, string name) {
            if (this.initialized)
                throw new FeatureNotSupportedException (this.Name, Features.AddInterface);

            base.AddInterface (iface, name);
        }

        /// <summary>
        ///   ��������� ���������� ��������� � ����������.
        /// </summary>
        /// <param name = "iface">���������� ���������, ������� ����� ��������.</param>
        /// <param name = "prefix">������� �������� ����������.</param>
        /// <remarks>
        ///   � ���������� "����" ����� ��������� ��������� ������ �� ����� �������������.
        ///   ����� �������� ���������� ������� ����������� �����������.
        /// </remarks>
        /// <exception cref = "FeatureNotSupportedException">����������� �� ��������������.</exception>
        public override void AddInterface_PrefixNamed (IInterface iface, string prefix) {
            if (this.initialized)
                throw new FeatureNotSupportedException (this.Name, Features.AddInterface);

            base.AddInterface_PrefixNamed (iface, prefix);
        }

        /// <summary>
        ///   ������� ���������� ��������� �� ����������.
        /// </summary>
        /// <remarks>
        ///   �� ���������� "����" ����� ������� ��������� ������ �� ����� �������������.
        ///   ����� �������� ���������� ������� ����������� �����������.
        /// </remarks>
        /// <exception cref = "FeatureNotSupportedException">����������� �� ��������������.</exception>
        public override void RemoveInterface (string name) {
            if (this.initialized)
                throw new FeatureNotSupportedException (this.Name, Features.RemoveInterface);

            base.RemoveInterface (name);
        }

        /// <summary>
        ///   ������� ��� ���������� ���������� �� ����������.
        /// </summary>
        /// <remarks>
        ///   �� ���������� "����" ����� ������� ��������� ������ �� ����� �������������.
        ///   ����� �������� ���������� ������� ����������� �����������.
        /// </remarks>
        /// <exception cref = "FeatureNotSupportedException">����������� �� ��������������.</exception>
        public override void RemoveInterfaces () {
            if (this.initialized)
                throw new FeatureNotSupportedException (this.Name, Features.RemoveInterface);

            base.RemoveInterfaces ();
        }

        /// <inheritdoc />
        public override void Load (XmlNode data) {
            this.initialized = false;

            base.RemoveInterfaces ();
            base.Load (data);

            this.initialized = true;
        }

        /// <inheritdoc />
        public override void ProcessPacket (IPacket packet, IInterfaceView from) {
            if (packet == null)
                throw new ArgumentNullException ("packet");

            if (from == null)
                throw new ArgumentNullException ("packet");

            if (from != this.HostInterface)
                throw new InterfaceNotAttachedToDeviceException (from.Name, this.Name);

            if (this.Engine == null)
                return;

            this.Engine.DispatchPacket (packet, from);
        }
        }
}
