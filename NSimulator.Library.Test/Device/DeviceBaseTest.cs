#region

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.Kernel;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public abstract class DeviceBaseTest <T>
        where T : IDevice {
        protected T device;

        [TestMethod]
        public void CheckSetName () {
            const string name = "new device name";

            Assert.AreNotEqual (name, this.device.Name);
            this.device.SetName (name);
            Assert.AreEqual (name, this.device.Name);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSetNullName () {
            this.device.SetName (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAttachNullInterface () {
            this.device.AddInterface (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAttachNullInterfaceWithName () {
            this.device.AddInterface (null, "name");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAttachNullInterfaceWithNamePrefix () {
            this.device.AddInterface_PrefixNamed (null, "name");
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAttachInterfaceWithNullName () {
            this.device.AddInterface (new InterfaceMock (), null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAttachInterfaceWithNameNullPrefix () {
            this.device.AddInterface_PrefixNamed (new InterfaceMock (), null);
        }

        [TestMethod]
        public void CheckAttachInterface () {
            var iface = new InterfaceMock ();
            iface.SetName (string.Empty);

            this.device.AddInterface (iface);
            Assert.AreEqual (this.device, iface.Device);
            Assert.IsTrue (this.device.Interfaces.Contains (iface));
            Assert.IsTrue (iface.Name.StartsWith (InterfacePrefixAttribute.GetPrefix (iface)));
        }

        [TestMethod]
        public void CheckAttachInterface_NoPrefix () {
            var iface = new InterfaceMock_NoPrefix ();
            iface.SetName (string.Empty);

            this.device.AddInterface (iface);
            Assert.AreEqual (this.device, iface.Device);
            Assert.IsTrue (this.device.Interfaces.Contains (iface));
            Assert.IsTrue (iface.Name.StartsWith ("iface"));
        }

        [TestMethod]
        public void CheckAttachInterface_NameIsUnique () {
            var iface1 = new InterfaceMock ();
            var iface2 = new InterfaceMock ();

            iface1.SetName (string.Empty);
            iface2.SetName (string.Empty);

            this.device.AddInterface (iface1);
            this.device.AddInterface (iface2);

            Assert.AreNotEqual (iface1.Name, iface2.Name);
        }

        [TestMethod]
        public void CheckAttachInterfacePrefixNamed () {
            var iface = new InterfaceMock ();
            iface.SetName (string.Empty);

            const string prefix = "prefix";

            this.device.AddInterface_PrefixNamed (iface, prefix);
            Assert.IsTrue (this.device.Interfaces.Contains (iface));
            Assert.AreEqual (this.device, iface.Device);
            Assert.IsTrue (iface.Name.StartsWith (prefix));
        }

        [TestMethod]
        public void CheckAttachInterfacePrefixNamed_NameIsUnique () {
            var iface1 = new InterfaceMock ();
            var iface2 = new InterfaceMock ();

            iface1.SetName (string.Empty);
            iface2.SetName (string.Empty);

            const string prefix = "prefix";

            this.device.AddInterface_PrefixNamed (iface1, prefix);
            this.device.AddInterface_PrefixNamed (iface2, prefix);

            Assert.AreNotEqual (iface1.Name, iface2.Name);
        }

        [TestMethod]
        public void CheckAttachInterfaceDirectlyNamed () {
            var iface = new InterfaceMock ();
            iface.SetName (string.Empty);

            const string name = "iface name";

            this.device.AddInterface (iface, name);
            Assert.IsTrue (this.device.Interfaces.Contains (iface));
            Assert.AreEqual (this.device, iface.Device);
            Assert.AreEqual (name, iface.Name);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNameMustBeUniqueException))]
        public void CheckCannotAttachTwoInterfacesWithSomeName () {
            var iface1 = new InterfaceMock ();
            var iface2 = new InterfaceMock ();

            const string name = "name";

            iface1.SetName (name);
            iface2.SetName (name);

            this.device.AddInterface (iface1, name);
            this.device.AddInterface (iface2, name);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceAlreadyAttachedToDeviceException))]
        public void CheckCannotAttachAlreadyAttachedInterface1 () {
            var iface = new InterfaceMock ();

            this.device.AddInterface (iface);
            this.device.AddInterface (iface);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceAlreadyAttachedToDeviceException))]
        public void CheckCannotAttachAlreadyAttachedInterface2 () {
            var iface = new InterfaceMock ();

            const string name1 = "name1";
            const string name2 = "name2";

            this.device.AddInterface (iface, name1);
            this.device.AddInterface (iface, name2);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceAlreadyAttachedToDeviceException))]
        public void CheckCannotAttachAlreadyAttachedInterface3 () {
            var iface = new InterfaceMock ();

            const string prefix = "prefix";

            this.device.AddInterface_PrefixNamed (iface, prefix);
            this.device.AddInterface_PrefixNamed (iface, prefix);
        }

        private static void Check (object o) {}

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotFoundException))]
        public void CheckCannotGetUnknownInterface () {
            Check (this.device ["unknown-iface"]);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckGetNullInterface () {
            Check (this.device [null]);
        }

        [TestMethod]
        public void CheckGetInterface () {
            const string name1 = "iface1";
            const string name2 = "iface2";

            var iface1 = new InterfaceMock ();
            var iface2 = new InterfaceMock ();

            this.device.AddInterface (iface1, name1);
            this.device.AddInterface (iface2, name2);

            Assert.AreEqual (iface1, this.device [name1]);
            Assert.AreEqual (iface2, this.device [name2]);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotFoundException))]
        public void CheckCannotDetachUnknownInterface () {
            this.device.RemoveInterface ("unknown-name");
        }

        [TestMethod]
        public void CheckDetachInterface () {
            var name = "iface name";

            for (var i = 0; i < int.MaxValue; ++ i) {
                var name1 = name;
                var i1 = i;

                if (this.device.Interfaces.Count (_ => _.Name == string.Format ("{0}{1}", name1, i1)) != 0)
                    continue;

                name = string.Format ("{0}{1}", name, i);
                break;
            }

            Assert.AreEqual (0, this.device.Interfaces.Count (_ => _.Name == name));

            var iface = new InterfaceMock ();

            this.device.AddInterface (iface, name);
            Assert.AreEqual (1, this.device.Interfaces.Count (_ => _.Name == name));

            this.device.RemoveInterface (name);
            Assert.IsNull (iface.Device);
            Assert.AreEqual (0, this.device.Interfaces.Count (_ => _.Name == name));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckDetachNullInterface () {
            this.device.RemoveInterface (null);
        }

        [TestMethod]
        public void CheckDetachAllInterfaces () {
            for (var i = 0; i < 4; ++ i)
                this.device.AddInterface (new InterfaceMock ());

            Assert.IsTrue (this.device.InterfacesCount >= 4);

            this.device.RemoveInterfaces ();
            Assert.AreEqual (0, this.device.InterfacesCount);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckProcessPacketForNullPacket () {
            const string name = "iface";

            var iface = new InterfaceMock ();
            this.device.AddInterface (iface, name);

            this.device.ProcessPacket (null, iface);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckProcessPacketForNullInterface () {
            this.device.ProcessPacket (new PacketMock (1), null);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotAttachedToDeviceException))]
        public void CheckProcessPacketForNotAttachedInterface () {
            var iface = new InterfaceMock ();
            this.device.ProcessPacket (new PacketMock (1), iface);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAttachBackboneNullInterface () {
            this.device.AttachBackbone (null, new UnicastBackboneMock ("1", 2, 1, new ClockMock (1)));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAttachBackboneNullBackbone () {
            this.device.AttachBackbone (string.Empty, null);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotFoundException))]
        public void CheckAttachBackboneUnknownInterface () {
            const string name = "name";

            if (this.device.Interfaces.Count (_ => _.Name == name) > 0)
                this.device.DetachBackbone (name);

            this.device.AttachBackbone (name, new UnicastBackboneMock ("1", 2, 1, new ClockMock (1)));
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceAlreadyAttachedToBackboneException))]
        public void CheckAttachBackboneAlreadyAttached () {
            const string name = "name";

            var backbone = new UnicastBackboneMock ("1", 2, 1, new ClockMock (1));
            this.device.RemoveInterfaces ();

            this.device.AddInterface (new InterfaceMock (), name);

            this.device.AttachBackbone (name, backbone);
            this.device.AttachBackbone (name, backbone);
        }

        [TestMethod]
        [ExpectedException (typeof (EndPointsOverflowException))]
        public void CheckAttachBackboneWithAllBusyEndpoints () {
            const string name = "name";

            var backbone = new UnicastBackboneMock ("1", 2, 1, new ClockMock (1));
            for (var i = 0; i < backbone.EndPointsCapacity; ++ i)
                backbone.AttachEndPoint (new InterfaceMock ());

            this.device.RemoveInterfaces ();
            this.device.AddInterface (new InterfaceMock (), name);
            this.device.AttachBackbone (name, backbone);
        }

        [TestMethod]
        public void CheckAttachBackbone () {
            const string name = "name";

            var iface = new InterfaceMock ();

            var backbone = new UnicastBackboneMock ("1", 2, 1, new ClockMock (1));
            Assert.AreEqual (0, backbone.EndPointsCount);

            this.device.RemoveInterfaces ();
            this.device.AddInterface (iface, name);
            this.device.AttachBackbone (name, backbone);

            Assert.AreEqual (1, backbone.EndPointsCount);
            Assert.AreEqual (iface, backbone [0]);
            Assert.AreEqual (backbone, iface.Backbone);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckDetachBackboneNull () {
            this.device.DetachBackbone (null);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotFoundException))]
        public void CheckDetachBackboneFromUnknownInterface () {
            this.device.RemoveInterfaces ();

            this.device.DetachBackbone ("name");
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotAttachedToBackboneException))]
        public void CheckDetachBackboneFromFreeInterface () {
            const string name = "name";

            this.device.RemoveInterfaces ();
            this.device.AddInterface (new InterfaceMock (), name);

            this.device.DetachBackbone (name);
        }

        [TestMethod]
        public void CheckDetachBackbone () {
            const string name = "name";

            var iface = new InterfaceMock ();

            var backbone = new UnicastBackboneMock ("1", 2, 1, new ClockMock (1));
            Assert.AreEqual (0, backbone.EndPointsCount);

            this.device.RemoveInterfaces ();
            this.device.AddInterface (iface, name);
            this.device.AttachBackbone (name, backbone);

            Assert.AreEqual (1, backbone.EndPointsCount);

            this.device.DetachBackbone (name);

            Assert.AreEqual (0, backbone.EndPointsCount);
            Assert.IsNull (iface.Backbone);
        }

        [TestMethod]
        public void CheckEnableDisable () {
            if (this.device.InterfacesCount == 0)
                Assert.Inconclusive ();

            this.device.Enable ();
            this.device.Disable ();

            Assert.IsFalse (this.device.Enabled);
            foreach (var iface in this.device.Interfaces)
                Assert.IsFalse (iface.Enabled);
        }

        [TestMethod]
        public void CheckDisableEnable () {
            if (this.device.InterfacesCount == 0)
                Assert.Inconclusive ();

            this.device.Disable ();
            this.device.Enable ();

            Assert.IsTrue (this.device.Enabled);
            foreach (var iface in this.device.Interfaces)
                Assert.IsTrue (iface.Enabled);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSetNullEngine () {
            this.device.SetEngine (null);
        }

        [TestMethod]
        public void CheckSetEngine () {
            var engine = new EngineMock1 ();
            this.device.SetEngine (engine);

            Assert.AreEqual (engine, this.device.Engine);
        }

        [TestMethod]
        public void CheckOnBeforeEnableFired () {
            var fired = false;

            this.device.OnBeforeEnable += (d, s) => {
                                              fired = true;
                                              Assert.AreEqual (this.device, d);
                                              Assert.IsFalse (this.device.Enabled);
                                              Assert.IsTrue (s);
                                          };

            this.device.Disable ();
            Assert.IsFalse (fired);
            this.device.Enable ();
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeDisableFired () {
            var fired = false;

            this.device.OnBeforeDisable += (d, s) => {
                                               fired = true;
                                               Assert.AreEqual (this.device, d);
                                               Assert.IsTrue (this.device.Enabled);
                                               Assert.IsFalse (s);
                                           };

            this.device.Enable ();
            Assert.IsFalse (fired);
            this.device.Disable ();
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeChangeNameFired () {
            var fired = false;

            const string NAME = "new-name";
            this.device.SetName (string.Empty);

            this.device.OnBeforeChangeName += (e, n) => {
                                                  fired = true;
                                                  Assert.AreEqual (this.device, e);
                                                  Assert.AreEqual (NAME, n);
                                                  Assert.AreNotEqual (NAME, this.device.Name);
                                              };

            this.device.SetName (NAME);
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeAddInterface1Fired () {
            var fired = false;

            var iface = new InterfaceMock ();
            iface.SetName (string.Empty);

            this.device.OnBeforeAddInterface += (d, i) => {
                                                    fired = true;
                                                    Assert.AreEqual (this.device, d);
                                                    Assert.AreEqual (iface, i);
                                                    Assert.IsFalse (this.device.Interfaces.Contains (iface));
                                                };

            this.device.AddInterface (iface);
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeAddInterface2Fired () {
            var fired = false;

            var iface = new InterfaceMock ();
            iface.SetName (string.Empty);

            const string prefix = "prefix";

            this.device.OnBeforeAddInterface += (d, i) => {
                                                    fired = true;
                                                    Assert.AreEqual (this.device, d);
                                                    Assert.AreEqual (iface, i);
                                                    Assert.IsFalse (this.device.Interfaces.Contains (iface));
                                                };

            this.device.AddInterface_PrefixNamed (iface, prefix);
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeAddInterface3Fired () {
            var fired = false;

            var iface = new InterfaceMock ();
            iface.SetName (string.Empty);

            const string name = "iface--name";

            this.device.OnBeforeAddInterface += (d, i) => {
                                                    fired = true;
                                                    Assert.AreEqual (this.device, d);
                                                    Assert.AreEqual (iface, i);
                                                    Assert.IsFalse (this.device.Interfaces.Contains (iface));
                                                };

            this.device.AddInterface (iface, name);
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeRemoveInterfaceFired () {
            var fired = false;

            var name = "iface name";

            for (var i = 0; i < int.MaxValue; ++i) {
                var name1 = name;
                var i1 = i;

                if (this.device.Interfaces.Count (_ => _.Name == string.Format ("{0}{1}", name1, i1)) != 0)
                    continue;

                name = string.Format ("{0}{1}", name, i);
                break;
            }

            Assert.AreEqual (0, this.device.Interfaces.Count (_ => _.Name == name));

            var iface = new InterfaceMock ();

            this.device.AddInterface (iface, name);
            Assert.AreEqual (1, this.device.Interfaces.Count (_ => _.Name == name));

            this.device.OnBeforeRemoveInterface += (d, i) => {
                                                       fired = true;
                                                       Assert.AreEqual (this.device, d);
                                                       Assert.AreEqual (iface, i);
                                                       Assert.AreEqual (1,
                                                                        this.device.Interfaces.Count (
                                                                            _ => _.Name == name));
                                                   };

            this.device.RemoveInterface (name);
            Assert.IsTrue (fired);
        }

        [TestMethod]
        public void CheckOnBeforeEnableFired_Exception () {
            this.device.OnBeforeEnable += (d, s) => { throw new Exception (); };

            this.device.Disable ();
            Assert.IsFalse (this.device.Enabled);
            this.device.Enable ();
            Assert.IsTrue (this.device.Enabled);
        }

        [TestMethod]
        public void CheckOnBeforeDisableFired_Exception () {
            this.device.OnBeforeDisable += (d, s) => { throw new Exception (); };

            this.device.Enable ();
            Assert.IsTrue (this.device.Enabled);
            this.device.Disable ();
            Assert.IsFalse (this.device.Enabled);
        }

        [TestMethod]
        public void CheckOnBeforeChangeNameFired_Exception () {
            const string NAME = "new-name";
            this.device.SetName (string.Empty);
            Assert.AreEqual (string.Empty, this.device.Name);

            this.device.OnBeforeChangeName += (e, n) => { throw new Exception (); };

            this.device.SetName (NAME);
            Assert.AreEqual (NAME, this.device.Name);
        }

        [TestMethod]
        public void CheckOnBeforeAddInterface1Fired_Exception () {
            var iface = new InterfaceMock ();
            iface.SetName (string.Empty);

            this.device.OnBeforeAddInterface += (d, i) => { throw new Exception (); };

            Assert.IsFalse (this.device.Interfaces.Contains (iface));
            this.device.AddInterface (iface);
            Assert.IsTrue (this.device.Interfaces.Contains (iface));
        }

        [TestMethod]
        public void CheckOnBeforeAddInterface2Fired_Exception () {
            var iface = new InterfaceMock ();
            iface.SetName (string.Empty);

            const string prefix = "prefix";

            this.device.OnBeforeAddInterface += (d, i) => { throw new Exception (); };

            Assert.IsFalse (this.device.Interfaces.Contains (iface));
            this.device.AddInterface_PrefixNamed (iface, prefix);
            Assert.IsTrue (this.device.Interfaces.Contains (iface));
        }

        [TestMethod]
        public void CheckOnBeforeAddInterface3Fired_Exception () {
            var iface = new InterfaceMock ();
            iface.SetName (string.Empty);

            const string name = "iface--name";

            this.device.OnBeforeAddInterface += (d, i) => { throw new Exception (); };

            Assert.IsFalse (this.device.Interfaces.Contains (iface));
            this.device.AddInterface (iface, name);
            Assert.IsTrue (this.device.Interfaces.Contains (iface));
        }

        [TestMethod]
        public void CheckOnBeforeRemoveInterfaceFired_Exception () {
            var name = "iface name";

            for (var i = 0; i < int.MaxValue; ++i) {
                var name1 = name;
                var i1 = i;

                if (this.device.Interfaces.Count (_ => _.Name == string.Format ("{0}{1}", name1, i1)) != 0)
                    continue;

                name = string.Format ("{0}{1}", name, i);
                break;
            }

            Assert.AreEqual (0, this.device.Interfaces.Count (_ => _.Name == name));

            var iface = new InterfaceMock ();

            this.device.AddInterface (iface, name);

            this.device.OnBeforeRemoveInterface += (d, i) => { throw new Exception (); };

            Assert.AreEqual (1, this.device.Interfaces.Count (_ => _.Name == name));
            this.device.RemoveInterface (name);
            Assert.AreEqual (0, this.device.Interfaces.Count (_ => _.Name == name));
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckLoadNullXml () {
            this.device.Load (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSaveNullXml () {
            this.device.Store (null);
        }
        }
}
