#region

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Топология сети.
    /// </summary>
    public sealed partial class Network : INetwork {
        private static readonly IDictionary <string, Assembly> assemblies;

        private IDictionary <ulong, IBackbone> backbones;
        private IDictionary <ulong, IDevice> devices;
        private IDictionary <ulong, IDeviceEngine> engines;

        private UniqueIdGenerator id_backbones;
        private UniqueIdGenerator id_devices;
        private UniqueIdGenerator id_engines;
        private UniqueIdGenerator id_interfaces;
        private UniqueIdGenerator id_modules;
        private IDictionary <ulong, IInterface> interfaces;
        private IDictionary <ulong, IModule> modules;

        static Network () {
            assemblies = new Dictionary <string, Assembly> ();
        }

        /// <summary>
        ///   Инициализирует топологию сети пустой сетью.
        /// </summary>
        /// <param name = "clock">Часы, используемые для инициализации сред передачи данных при загрузке топологии.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "clock" /> является <c>null</c>.</exception>
        public Network (IClock clock) {
            if (clock == null)
                throw new ArgumentNullException ("clock");

            this.Initialize (clock);
        }

        /// <summary>
        ///   Инициализирует топологию сети сетью, загруженной из файла <paramref name = "filename" />.
        /// </summary>
        /// <param name = "filename">Имя файла, из которого загружать топологию.</param>
        /// <param name = "clock">Часы, используемые для инициализации сред передачи данных при загрузке топологии.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "filename" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "clock" /> является <c>null</c>.</exception>
        /// <exception cref = "SchemaValidationException">Файл <paramref name = "filename" /> имеет неверный формат.</exception>
        public Network (string filename, IClock clock)
            : this (clock) {
            this.LoadFromXml (filename);
        }

        #region INetwork Members

        /// <inheritdoc />
        public IClock Clock { get; private set; }

        /// <inheritdoc />
        IClockView INetworkView.Clock {
            get { return this.Clock; }
        }

        /// <inheritdoc />
        public void LoadFromXml (string filename) {
            using (var streamReader = File.OpenRead (filename))
            using (var stream = new GZipStream (streamReader, CompressionMode.Decompress)) {
                var xml = new XmlDocument ();
                xml.Load (stream);

                var schema = XmlSchema.Read (new StreamReader (Strings.TopologySchema),
                                             (o, e) => { throw new SchemaValidationException (e.Exception); });
                xml.Schemas.Add (schema);
                xml.Validate ((o, e) => { throw new SchemaValidationException (e.Exception); });

                this.Initialize (this.Clock);
                this.LoadEntity (xml.DocumentElement ["devices"], this.devices);
                this.LoadEntity (xml.DocumentElement ["interfaces"], this.interfaces);
                this.LoadEntity (xml.DocumentElement ["backbones"], this.backbones);
                this.LoadEntity (xml.DocumentElement ["modules"], this.modules);
                this.LoadEntity (xml.DocumentElement ["engines"], this.engines);
                this.MakeTopology (xml.DocumentElement ["topology"]);
                this.InitializeGenerators ();
            }
        }

        /// <inheritdoc />
        public void SaveToXml (string filename) {
            var xml = new XmlDocument ();

            this.PrepareToSave ();

            xml.AppendChild (xml.CreateXmlDeclaration ("1.0", "utf-8", "yes"));
            xml.AppendChild (xml.CreateElement ("network"));
            foreach (var name in new [] { "devices", "interfaces", "backbones", "modules", "engines", "topology" })
                xml.DocumentElement.AppendChild (xml.CreateElement (name));

            this.SaveEntity (xml.DocumentElement ["devices"], "device", this.devices);
            this.SaveEntity (xml.DocumentElement ["interfaces"], "interface", this.interfaces);
            this.SaveEntity (xml.DocumentElement ["backbones"], "backbone", this.backbones);
            this.SaveEntity (xml.DocumentElement ["modules"], "module", this.modules);
            this.SaveEntity (xml.DocumentElement ["engines"], "engine", this.engines);
            this.SaveTopology (xml.DocumentElement ["topology"]);

            using (var streamWriter = File.OpenWrite (filename))
            using (var stream = new GZipStream (streamWriter, CompressionMode.Compress))
                xml.Save (stream);
        }

        /// <inheritdoc />
        public event LoadTopologyErrorEventHandler OnLoadError;

        #endregion

        private void Initialize (IClock clock) {
            this.devices = new Dictionary <ulong, IDevice> ();
            this.interfaces = new Dictionary <ulong, IInterface> ();
            this.backbones = new Dictionary <ulong, IBackbone> ();
            this.modules = new Dictionary <ulong, IModule> ();
            this.engines = new Dictionary <ulong, IDeviceEngine> ();
            this.Clock = clock;

            this.InitializeGenerators ();
        }

        private void InitializeGenerators () {
            this.id_devices = new UniqueIdGenerator (this.devices.Keys);
            this.id_interfaces = new UniqueIdGenerator (this.interfaces.Keys);
            this.id_backbones = new UniqueIdGenerator (this.backbones.Keys);
            this.id_modules = new UniqueIdGenerator (this.modules.Keys);
            this.id_engines = new UniqueIdGenerator (this.engines.Keys);
        }

        private void PrepareToSave () {
            this.engines = new Dictionary <ulong, IDeviceEngine> ();
            this.id_engines = new UniqueIdGenerator ();
            this.modules = new Dictionary <ulong, IModule> ();
            this.id_modules = new UniqueIdGenerator ();

            foreach (var device in this.devices.Values.Where (d => !this.engines.Values.Contains (d.Engine)))
                this.engines.Add (this.id_engines.GetNext (), device.Engine);

            foreach (var module in from engine in this.engines.Values
                                   from module in engine.Modules
                                   where !this.modules.Values.Contains (module)
                                   select module)
                this.modules.Add (this.id_modules.GetNext (), module);
        }

        private void FireOnLoadErrorEvent (ILoadTopologyError error) {
            if (this.OnLoadError != null) {
                try {
                    this.OnLoadError.Invoke (error);
                }
                catch {}
            }
        }
    }
}
