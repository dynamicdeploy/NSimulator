#region

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Pcap-������ ������������ �������.
    /// </summary>
    /// <remarks>
    ///   Pcap-������ ������ �� ������ ����� �������� ������ ��������� ����,
    ///   � ������� ���������� � pcap-������� ������������ ������.
    /// </remarks>
    public sealed class PCAPPacketDumper : IPacketDumper {
        private readonly IClockView clock;
        private readonly Dictionary <IBackboneView, Triple <Object, PCAPFileWriter, bool>> dumpers;
        private readonly string location;
        private readonly object lock_obj;
        private bool isDisposed;

        /// <summary>
        ///   �������������� ������ ������������ �������.
        /// </summary>
        /// <param name = "clock">����, ��������������� ���������� � ������� ��������.</param>
        /// <remarks>
        ///   ����� � ������� ������������ ������� ��������� � ������� ����������.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "clock" /> �������� <c>null</c>.</exception>
        public PCAPPacketDumper (IClockView clock) {
            if (clock == null)
                throw new ArgumentNullException ("clock");

            this.lock_obj = new object ();
            this.clock = clock;
            this.dumpers = new Dictionary <IBackboneView, Triple <object, PCAPFileWriter, bool>> ();
            this.location = Directory.GetCurrentDirectory ();
        }

        /// <summary>
        ///   �������������� ������ ������������ �������.
        /// </summary>
        /// <param name = "clock">����, ��������������� ���������� � ������� ��������.</param>
        /// <param name = "directory">����������, � ������� ��������� ����� ������.</param>
        /// <remarks>
        ///   ����� � ������� ������������ ������� ��������� � ����������, �������� � <paramref name = "directory" />.
        ///   ���� ��������� ���������� �� ����������, �� ����� ����������� ������� � ��������.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "clock" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "directory" /> �������� <c>null</c>.</exception>
        public PCAPPacketDumper (IClockView clock, string directory)
            : this (clock) {
            if (directory == null)
                throw new ArgumentNullException ("directory");

            if (!Directory.Exists (directory))
                Directory.CreateDirectory (directory);

            this.location = directory;
        }

        #region IPacketDumper Members

        /// <summary>
        ///   ����������� ������� �������.
        /// </summary>
        public void Dispose () {
            this.Dispose (true);
            GC.SuppressFinalize (this);
        }

        /// <summary>
        ///   �������� ������������ �������, ������������ � ��������� ����� ��������.
        /// </summary>
        /// <param name = "backbone">����� ��������, � ������� ����� �������� ������������.</param>
        /// <remarks>
        ///   ��������������. ��� ����� �������� ������ <paramref name = "backbone" /> ��� ������������ ������
        ///   ������������ � ����, ��� �������� ��������� � ��������������� ����� ��������.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> �������� <c>null</c>.</exception>
        public void AttachToBackbone (IBackboneView backbone) {
            this.Attach (backbone,
                         new FileStream (string.Format (@"{0}\{1}", this.location, backbone.Name),
                                         FileMode.OpenOrCreate,
                                         FileAccess.Write),
                         true);
        }

        /// <summary>
        ///   �������� ������������ �������, ������������ � ��������� ����� ��������.
        /// </summary>
        /// <param name = "backbone">����� ��������, � ������� ����� �������� ������������.</param>
        /// <param name = "stream">�����, � ������� ��������� �������� ���.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> �������� <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "stream" /> �������� <c>null</c>.</exception>
        public void AttachToBackbone (IBackboneView backbone, Stream stream) {
            this.Attach (backbone, stream, false);
        }

        /// <summary>
        ///   ��������� ������������ �������, ������������ � ��������� ����� ��������.
        /// </summary>
        /// <param name = "backbone">����� ��������, � ������� ����� ��������� ������������.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> �������� <c>null</c>.</exception>
        public void DetachFromBackbone (IBackboneView backbone) {
            Triple <Object, PCAPFileWriter, bool> dumper_info;

            lock (this.lock_obj) {
                dumper_info = this.dumpers [backbone];
                this.dumpers.Remove (backbone);
            }

            lock (dumper_info.First)
                if (dumper_info.Third)
                    dumper_info.Second.Dispose ();
        }

        #endregion

        private void Attach (IBackboneView backbone, Stream stream, bool disposing) {
            lock (this.lock_obj) {
                if (!this.dumpers.ContainsKey (backbone)) {
                    this.dumpers.Add (backbone,
                                      new Triple <object, PCAPFileWriter, bool> (
                                          new object (),
                                          new PCAPFileWriter (stream, backbone.Type, disposing),
                                          disposing));
                }

                backbone.OnTransmit += (backbone1, packet) => {
                                           Triple <Object, PCAPFileWriter, bool> dumper_info;

                                           lock (this.lock_obj)
                                               dumper_info = this.dumpers [backbone1];

                                           lock (dumper_info.First)
                                               dumper_info.Second.Write (packet, this.clock.CurrentTime);
                                       };
            }
        }

        private void Dispose (bool disposing) {
            if (this.isDisposed)
                return;

            if (disposing) {
                lock (this.lock_obj) {
                    foreach (var item in this.dumpers.Values) {
                        lock (item.First)
                            if (item.Third)
                                item.Second.Dispose ();
                    }

                    this.dumpers.Clear ();
                }
            }

            this.isDisposed = true;
        }

        /// <summary>
        ///   ����������� �������.
        /// </summary>
        ~PCAPPacketDumper () {
            this.Dispose (false);
        }
    }
}
