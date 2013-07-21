#region

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.Kernel;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Library.Test {
    [TestClass]
    public abstract class BackboneBaseTest <T>
        where T : IBackbone {
        protected T backbone;

        protected ClockMock clock;

        [TestMethod]
        public void CheckSetName () {
            const string name = "bb-name";
            this.backbone.SetName (name);

            Assert.AreEqual (name, this.backbone.Name);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSetNullName () {
            this.backbone.SetName (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAttachNullEndPoint () {
            this.backbone.AttachEndPoint (null);
        }

        [TestMethod]
        [ExpectedException (typeof (EndPointsOverflowException))]
        public void CheckAttachEndPoint_OverflowCapacity () {
            for (var i = this.backbone.EndPointsCount; i <= this.backbone.EndPointsCapacity; ++ i)
                this.backbone.AttachEndPoint (new InterfaceMock ());
        }

        [TestMethod]
        public void CheckAttachEndPoint () {
            var iface = new InterfaceMock ();
            var count = this.backbone.EndPointsCount;

            this.backbone.AttachEndPoint (iface);

            Assert.AreEqual (count + 1, this.backbone.EndPointsCount);
            Assert.IsTrue (this.backbone.EndPoints.Contains (iface));
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceAlreadyAttachedToBackboneException))]
        public void CheckAttachEndPoint2 () {
            var iface = new InterfaceMock ();

            this.backbone.AttachEndPoint (iface);
            this.backbone.AttachEndPoint (iface);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckDetachNullEndPoint () {
            this.backbone.DetachEndPoint (null);
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotAttachedToBackboneException))]
        public void CheckDetachNotAttachedEndPoint () {
            this.backbone.DetachEndPoint (new InterfaceMock ());
        }

        [TestMethod]
        public void CheckDetachEndPoint () {
            var endpoint = new InterfaceMock ();

            var count = this.backbone.EndPointsCount;
            endpoint.SetBackbone (this.backbone);
            this.backbone.DetachEndPoint (endpoint);

            Assert.AreEqual (count, this.backbone.EndPointsCount);
            Assert.IsFalse (this.backbone.EndPoints.Contains (endpoint));
        }

        [TestMethod]
        [ExpectedException (typeof (InterfaceNotAttachedToBackboneException))]
        public void CheckDetachEndPoint2 () {
            var endpoint = new InterfaceMock ();

            endpoint.SetBackbone (this.backbone);
            this.backbone.DetachEndPoint (endpoint);
            this.backbone.DetachEndPoint (endpoint);
        }

        [TestMethod]
        public void CheckChangeSpeed () {
            ulong speed = 0;
            this.backbone.ChangeSpeed (speed);
            Assert.AreEqual (speed, this.backbone.Speed);

            speed = 1;
            this.backbone.ChangeSpeed (speed);
            Assert.AreEqual (speed, this.backbone.Speed);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckChangeLossPercent_LessThan0 () {
            this.backbone.ChangeLossPercent (-1);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentOutOfRangeException))]
        public void CheckChangeLossPercent_GreaterThan1 () {
            this.backbone.ChangeLossPercent (2);
        }

        [TestMethod]
        public void CheckChangeLossPercent_Property () {
            double percent = 0;
            this.backbone.ChangeLossPercent (percent);
            Assert.AreEqual (percent, this.backbone.LossPercent);

            percent = 0.5;
            this.backbone.ChangeLossPercent (percent);
            Assert.AreEqual (percent, this.backbone.LossPercent);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckLoadNullXml () {
            this.backbone.Load (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSaveNullXml () {
            this.backbone.Store (null);
        }

        [TestMethod]
        public virtual void CheckLoadXml () {
            // todo
        }

        [TestMethod]
        public virtual void CheckSaveXml () {
            // todo
        }
        }
}
