#region

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.Kernel;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public abstract class InterfaceBaseTest <Iface, Backbone>
        where Iface : IInterface
        where Backbone : IBackbone {
        protected ClockMock clock;
        protected Backbone compatible_backbone;
        protected Iface iface;
        protected Iface iface_in_backbone;

        protected IBackbone noncompatible_backbone;
        protected Iface second_iface;

        [TestMethod]
        public void CheckSetName () {
            const string name = "iface-name";
            this.iface.SetName (name);

            Assert.AreEqual (name, this.iface.Name);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSetNullName () {
            this.iface.SetName (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSetNullBackbone () {
            this.iface.SetBackbone (null);
        }

        [TestMethod]
        public void CheckSetBackbone () {
            Assert.IsNull (this.iface.Backbone);
            this.iface.SetBackbone (this.compatible_backbone);
            Assert.AreEqual (this.compatible_backbone, this.iface.Backbone);
            Assert.IsTrue (this.compatible_backbone.EndPoints.Contains (this.iface));
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotCompatibleWithBackboneException))]
        public void CheckSetNoncompatibleBackbone () {
            this.iface.SetBackbone (this.noncompatible_backbone);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceAlreadyAttachedToBackboneException))]
        public void CheckSetBackbone2 () {
            this.iface.SetBackbone (this.compatible_backbone);
            this.iface.SetBackbone (this.compatible_backbone);
        }

        [TestMethod]
        public void CheckReleaseBackbone () {
            this.iface.SetBackbone (this.compatible_backbone);
            this.iface.ReleaseBackbone ();
            Assert.IsNull (this.iface.Backbone);
            Assert.IsFalse (this.compatible_backbone.EndPoints.Contains (this.iface));
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotAttachedToBackboneException))]
        public void CheckReleaseBackbone2 () {
            this.iface.SetBackbone (this.compatible_backbone);
            this.iface.ReleaseBackbone ();
            this.iface.ReleaseBackbone ();
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSetNullDevice () {
            this.iface.SetDevice (null);
        }

        [TestMethod]
        public void CheckSetDevice () {
            var device = new DeviceMock ();

            Assert.IsNull (this.iface.Device);
            device.AddInterface (this.iface);

            this.iface.SetDevice (device);

            Assert.AreEqual (device, this.iface.Device);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceAlreadyAttachedToDeviceException))]
        public void CheckSetDevice2 () {
            var device = new DeviceMock ();

            Assert.IsNull (this.iface.Device);
            device.AddInterface (this.iface);

            this.iface.SetDevice (device);
            this.iface.SetDevice (new DeviceMock ("device"));
        }

        [TestMethod]
        public void CheckReleaseDevice () {
            var device = new DeviceMock ();

            device.AddInterface (this.iface);
            this.iface.SetDevice (device);
            this.iface.ReleaseDevice ();

            Assert.IsNull (this.iface.Device);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotAttachedToDeviceException))]
        public void CheckReleaseDevice2 () {
            var device = new DeviceMock ();

            device.AddInterface (this.iface);
            this.iface.SetDevice (device);
            this.iface.ReleaseDevice ();
            this.iface.ReleaseDevice ();
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSendNullPacket () {
            this.iface.SendPacket (null);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotAttachedToBackboneException))]
        public void CheckSendPacket_NullBackbone () {
            if (this.iface.Backbone != null)
                this.iface.ReleaseBackbone ();

            this.iface.SendPacket (new PacketMock (1));
        }

        [TestMethod]
        public void CheckSendPacket () {
            var processed = false;

            var packet = new PacketMock (10, 2);

            this.compatible_backbone.OnTransmit += (b, p) => {
                                                       processed = true;
                                                       Assert.AreEqual (packet, p);
                                                   };

            this.second_iface.Enable ();
            this.second_iface.SetBackbone (this.compatible_backbone);
            this.iface_in_backbone.SendPacket (packet);
            this.clock.Start ();
            Assert.IsTrue (processed);
        }

        [TestMethod]
        public void CheckNotSendPacketFromDisabled () {
            var processed = false;

            var packet = new PacketMock (10, 2);

            this.compatible_backbone.OnTransmit += (b, p) => {
                                                       processed = true;
                                                       Assert.AreEqual (packet, p);
                                                   };

            this.second_iface.Enable ();
            this.second_iface.SetBackbone (this.compatible_backbone);
            this.iface_in_backbone.Disable ();
            this.iface_in_backbone.SendPacket (packet);
            this.clock.Start ();
            Assert.IsFalse (processed);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckReceiveNullPacket () {
            this.iface.ReceivePacket (null);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotAttachedToDeviceException))]
        public void CheckReceivePacket_NullDevice () {
            if (this.iface.Device != null)
                this.iface.ReleaseDevice ();

            this.iface.ReceivePacket (new PacketMock (1));
        }

        [TestMethod]
        public void CheckReceivePacket () {
            var processed = false;

            var packet = new PacketMock (10, 2);

            var device = new DeviceMock ();
            device.Enable ();
            device.OnProcessPacket += (p, i) => {
                                          processed = true;
                                          Assert.AreEqual (packet, p);
                                          Assert.AreEqual (this.iface, i);
                                      };

            device.AddInterface (this.iface);
            this.iface.SetDevice (device);
            this.iface.Enable ();
            this.iface.ReceivePacket (packet);
            Assert.IsTrue (processed);
        }

        [TestMethod]
        public void CheckNotReceivePacketOnDisabled () {
            var processed = false;

            var packet = new PacketMock (10, 2);

            var device = new DeviceMock ();
            device.Enable ();
            device.OnProcessPacket += (p, i) => {
                                          processed = true;
                                          Assert.AreEqual (packet, p);
                                          Assert.AreEqual (this.iface, i);
                                      };

            device.AddInterface (this.iface);
            this.iface.SetDevice (device);
            this.iface.Disable ();
            this.iface.ReceivePacket (packet);
            Assert.IsFalse (processed);
        }

        [TestMethod]
        public void CheckEnabled_EnableDisable () {
            this.iface.Enable ();
            this.iface.Disable ();
            Assert.IsFalse (this.iface.Enabled);
        }

        [TestMethod]
        public void CheckEnabled_DisableEnable () {
            this.iface.Disable ();
            this.iface.Enable ();
            Assert.IsTrue (this.iface.Enabled);
        }

        [TestMethod]
        public void CheckOnBeforeEnableFired () {
            var fired = false;

            this.iface.OnBeforeEnable += (i, s) => {
                                             fired = true;
                                             Assert.AreEqual (this.iface, i);
                                             Assert.IsFalse (this.iface.Enabled);
                                             Assert.IsTrue (s);
                                         };

            this.iface.Disable ();
            Assert.IsFalse (fired);
            this.iface.Enable ();
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeDisableFired () {
            var fired = false;

            this.iface.OnBeforeDisable += (i, s) => {
                                              fired = true;
                                              Assert.AreEqual (this.iface, i);
                                              Assert.IsTrue (this.iface.Enabled);
                                              Assert.IsFalse (s);
                                          };

            this.iface.Enable ();
            Assert.IsFalse (fired);
            this.iface.Disable ();
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeChangeNameFired () {
            var fired = false;

            const string NAME = "new-name";
            this.iface.SetName (string.Empty);

            this.iface.OnBeforeChangeName += (e, n) => {
                                                 fired = true;
                                                 Assert.AreEqual (this.iface, e);
                                                 Assert.AreEqual (NAME, n);
                                                 Assert.AreNotEqual (NAME, this.iface.Name);
                                             };

            this.iface.SetName (NAME);
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeSendFired () {
            var fired = false;

            var packet = new PacketMock (10, 2);

            this.iface_in_backbone.OnBeforeSend += i => {
                                                       fired = true;
                                                       Assert.AreEqual (this.iface_in_backbone, i);
                                                   };
            this.compatible_backbone.OnTransmit += (b, p) => Assert.IsTrue (fired);

            this.second_iface.Enable ();
            this.second_iface.SetBackbone (this.compatible_backbone);
            this.iface_in_backbone.SendPacket (packet);
            this.clock.Start ();
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeReceiveFired () {
            var fired = false;

            var packet = new PacketMock (10, 2);

            var device = new DeviceMock ();
            device.Enable ();
            this.iface.OnBeforeReceive += i => {
                                              fired = true;
                                              Assert.AreEqual (this.iface, i);
                                          };

            device.OnProcessPacket += (p, i) => Assert.IsTrue (fired);

            device.AddInterface (this.iface);
            this.iface.SetDevice (device);
            this.iface.Enable ();
            this.iface.ReceivePacket (packet);
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeAttachBackboneFired () {
            var fired = false;

            this.iface.OnBeforeAttachBackbone += (i, b) => {
                                                     fired = true;
                                                     Assert.AreEqual (this.iface, i);
                                                     Assert.AreEqual (this.compatible_backbone, b);
                                                     Assert.IsNull (this.iface.Backbone);
                                                 };

            this.iface.SetBackbone (this.compatible_backbone);
            Assert.IsTrue (fired);
            Assert.IsNotNull (this.iface.Backbone);
        }

        [TestMethod]
        public void CheckOnBeforeDetachBackboneFired () {
            var fired = false;

            this.iface.OnBeforeDetachBackbone += i => {
                                                     fired = true;
                                                     Assert.AreEqual (this.iface, i);
                                                     Assert.IsNotNull (this.iface.Backbone);
                                                 };

            this.iface.SetBackbone (this.compatible_backbone);
            this.iface.ReleaseBackbone ();
            Assert.IsTrue (fired);
            Assert.IsNull (this.iface.Backbone);
        }

        [TestMethod]
        public void CheckOnBeforeEnableFired_Exception () {
            this.iface.OnBeforeEnable += (i, s) => { throw new Exception (); };

            this.iface.Disable ();
            Assert.IsFalse (this.iface.Enabled);
            this.iface.Enable ();
            Assert.IsTrue (this.iface.Enabled);
        }

        [TestMethod]
        public void CheckOnBeforeDisableFired_Exception () {
            this.iface.OnBeforeDisable += (i, s) => { throw new Exception (); };

            this.iface.Enable ();
            Assert.IsTrue (this.iface.Enabled);
            this.iface.Disable ();
            Assert.IsFalse (this.iface.Enabled);
        }

        [TestMethod]
        public void CheckOnBeforeChangeNameFired_Exception () {
            const string NAME = "new-name";
            this.iface.SetName (string.Empty);
            Assert.AreEqual (string.Empty, this.iface.Name);

            this.iface.OnBeforeChangeName += (e, n) => { throw new Exception (); };

            this.iface.SetName (NAME);
            Assert.AreEqual (NAME, this.iface.Name);
        }

        [TestMethod]
        public void CheckOnBeforeSendFired_Exception () {
            var transmitted = false;
            var packet = new PacketMock (10, 2);

            this.iface_in_backbone.OnBeforeSend += i => { throw new Exception (); };

            this.compatible_backbone.OnTransmit += (b, p) => { transmitted = true; };
            this.second_iface.Enable ();
            this.second_iface.SetBackbone (this.compatible_backbone);
            this.iface_in_backbone.SendPacket (packet);
            this.clock.Start ();
            Assert.IsTrue (transmitted);
        }

        [TestMethod]
        public void CheckOnBeforeReceiveFired_Exception () {
            var received = false;
            var packet = new PacketMock (10, 2);

            var device = new DeviceMock ();
            device.Enable ();
            this.iface.OnBeforeReceive += i => { throw new Exception (); };

            device.OnProcessPacket += (p, i) => { received = true; };
            device.AddInterface (this.iface);
            this.iface.SetDevice (device);
            this.iface.Enable ();
            this.iface.ReceivePacket (packet);
            Assert.IsTrue (received);
        }

        [TestMethod]
        public void CheckOnBeforeAttachBackboneFired_Exception () {
            this.iface.OnBeforeAttachBackbone += (i, b) => { throw new Exception (); };

            Assert.IsNull (this.iface.Backbone);
            this.iface.SetBackbone (this.compatible_backbone);
            Assert.AreEqual (this.compatible_backbone, this.iface.Backbone);
        }

        [TestMethod]
        public void CheckOnBeforeDetachBackboneFired_Exception () {
            this.iface.OnBeforeDetachBackbone += i => { throw new Exception (); };

            this.iface.SetBackbone (this.compatible_backbone);
            Assert.IsNotNull (this.iface.Backbone);
            this.iface.ReleaseBackbone ();
            Assert.IsNull (this.iface.Backbone);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckLoadNullXml () {
            this.iface.Load (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSaveNullXml () {
            this.iface.Store (null);
        }
        }
}
