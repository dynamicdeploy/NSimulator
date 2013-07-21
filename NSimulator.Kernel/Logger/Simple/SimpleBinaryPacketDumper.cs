#region

using System;
using System.Collections.Generic;
using System.IO;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Простой логгер передаваемых пакетов.
    /// </summary>
    /// <remarks>
    ///   Простой логгер создаёт на каждую среду передачи данных отдельный файл,
    ///   в который дописывает в бинарном виде передаваемые пакеты.
    /// </remarks>
    public sealed class SimpleBinaryPacketDumper : IPacketDumper {
        private readonly Dictionary <IBackboneView, Triple <Object, BinaryWriter, bool>> dumpers;
        private readonly string location;
        private readonly object lock_obj;
        private bool isDisposed;

        /// <summary>
        ///   Инициализирует логгер передаваемых пакетов.
        /// </summary>
        /// <remarks>
        ///   Файлы с дампами передаваемых пакетов создаются в текущей директории.
        /// </remarks>
        public SimpleBinaryPacketDumper () {
            this.lock_obj = new object ();
            this.dumpers = new Dictionary <IBackboneView, Triple <object, BinaryWriter, bool>> ();
            this.location = Directory.GetCurrentDirectory ();
        }

        /// <summary>
        ///   Инициализирует логгер передаваемых пакетов.
        /// </summary>
        /// <param name = "directory">Директория, в которой создаются файлы дампов.</param>
        /// <remarks>
        ///   Файлы с дампами передаваемых пакетов создаются в директории, заданной в <paramref name = "directory" />.
        ///   Если указанная директория не существует, то будет произведена попытка её создания.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "directory" /> является <c>null</c>.</exception>
        public SimpleBinaryPacketDumper (string directory)
            : this () {
            if (directory == null)
                throw new ArgumentNullException ("directory");

            if (!Directory.Exists (directory))
                Directory.CreateDirectory (directory);

            this.location = directory;
        }

        #region IPacketDumper Members

        /// <summary>
        ///   Освобождает занятые ресурсы.
        /// </summary>
        public void Dispose () {
            this.Dispose (true);
            GC.SuppressFinalize (this);
        }

        /// <summary>
        ///   Включает логгирование пакетов, передаваемых в указанной среде передачи.
        /// </summary>
        /// <param name = "backbone">Среда передачи, в которой нужно включить логгирование.</param>
        /// <remarks>
        ///   Предупреждение. Для среды передачи данных <paramref name = "backbone" /> все передаваемые пакеты
        ///   записываются в файл, имя которого совпадает с идентификатором среды передачи.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> является <c>null</c>.</exception>
        public void AttachToBackbone (IBackboneView backbone) {
            this.Attach (backbone,
                         new FileStream (string.Format (@"{0}\{1}", this.location, backbone.Name),
                                         FileMode.OpenOrCreate,
                                         FileAccess.Write),
                         true);
        }

        /// <summary>
        ///   Включает логгирование пакетов, передаваемых в указанной среде передачи.
        /// </summary>
        /// <param name = "backbone">Среда передачи, в которой нужно включить логгирование.</param>
        /// <param name = "stream">Поток, в который требуется выводить лог.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "stream" /> является <c>null</c>.</exception>
        public void AttachToBackbone (IBackboneView backbone, Stream stream) {
            if (stream == null)
                throw new ArgumentNullException ("stream");

            this.Attach (backbone, stream, false);
        }

        /// <summary>
        ///   Отключает логгирование пакетов, передаваемых в указанной среде передачи.
        /// </summary>
        /// <param name = "backbone">Среда передачи, в которой нужно отключить логгирование.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> является <c>null</c>.</exception>
        public void DetachFromBackbone (IBackboneView backbone) {
            Triple <Object, BinaryWriter, bool> dumper_info;

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
                                      new Triple <object, BinaryWriter, bool> (
                                          new object (),
                                          new BinaryWriter (stream),
                                          disposing));
                }

                backbone.OnTransmit += (backbone1, packet) => {
                                           Triple <Object, BinaryWriter, bool> dumper_info;

                                           lock (this.lock_obj)
                                               dumper_info = this.dumpers [backbone1];

                                           lock (dumper_info.First)
                                               dumper_info.Second.Write (packet.Data);
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
        ///   Финализатор логгера.
        /// </summary>
        ~SimpleBinaryPacketDumper () {
            this.Dispose (false);
        }
    }
}
