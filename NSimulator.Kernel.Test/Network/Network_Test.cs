#region

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSimulator.TestMocks;

#endregion

namespace NSimulator.Kernel.Test {
    [TestClass]
    public sealed class Network_Test {
        private IClock clock;
        private INetwork network;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void Init () {
            this.clock = new ClockMock (0);
            this.network = new Network (this.clock);
        }

        [TestMethod]
        public void CheckInitialize () {
            Assert.AreEqual (0, this.network.Interfaces.Count ());
            Assert.AreEqual (0, this.network.Backbones.Count ());
            Assert.AreEqual (0, this.network.Devices.Count ());
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckNullConstructor () {
            new Network (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAddNullBackbone () {
            this.network.AddBackbone (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAddNullInterface () {
            this.network.AddInterface (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckAddNullDevice () {
            this.network.AddDevice (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckRemoveNullBackbone () {
            this.network.RemoveBackbone (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckRemoveNullInterface () {
            this.network.RemoveInterface (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckRemoveNullDevice () {
            this.network.RemoveDevice (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckLoadNullXml () {
            this.network.LoadFromXml (null);
        }

        [TestMethod]
        [ExpectedException (typeof (ArgumentNullException))]
        public void CheckSaveToNull () {
            this.network.SaveToXml (null);
        }

        [TestMethod]
        [ExpectedException (typeof (EntityHandlerNotFoundException))]
        public void CheckDoubleRemoveBackbone () {
            var entity = this.network.AddBackbone (new FakeBackbone ());
            Assert.IsNotNull (entity);
            this.network.RemoveBackbone (entity);
            this.network.RemoveBackbone (entity);
        }

        [TestMethod]
        [ExpectedException (typeof (EntityHandlerNotFoundException))]
        public void CheckDoubleRemoveInterface () {
            var entity = this.network.AddInterface (new FakeInterface ());
            Assert.IsNotNull (entity);
            this.network.RemoveInterface (entity);
            this.network.RemoveInterface (entity);
        }

        [TestMethod]
        [ExpectedException (typeof (EntityHandlerNotFoundException))]
        public void CheckDoubleRemoveDevice () {
            var entity = this.network.AddDevice (new FakeDevice ());
            Assert.IsNotNull (entity);
            this.network.RemoveDevice (entity);
            this.network.RemoveDevice (entity);
        }

        /*
         * ����� �� LoadFromXml (...)
         * 
         * ���� "������" �������:
         * 0. �������� �������� (���������� ������� ����, ...)
         * 1. �� �������� ���������
         * 2. �������� ���������, �� �������� ���������
         * 3. �������� ���������, ������ ���������, �� ���-�� ������
         * 
         * -- �������� ��������
         * 1. �������� ��� �����
         * 2. ����� ���������
         * 3. ������ �� xml
         * 
         * -- �� �������� ���������
         * 1. id �� ����������:
         *    ����������, ���������, ���, ������, ��������:
         *      �����, ��������
         *    ���������:
         *      ����������:
         *        ����������, ���������, ��������, ������ (�������� ������ ���������)
         *      ���:
         *        ���, ���������
         * 2. id �� �����:
         *    ����������, ���������, ���, ������, ��������:
         *      �����, ��������
         *    ���������:
         *      ����������:
         *        ����������, ���������, ������, ��������
         *      ���:
         *        ���, ���������
         * 3. ������ ��������:
         *      ��������:
         *        �� �����
         *        �� ������� ������
         *        ������ ������
         *        �� ��������
         *      ���������:
         *        �� ���������� � ���
         *        ����������:
         *          �� ���������, ��������, ������
         *          ������ ������
         *        ���:
         *          �� ���������
         *          ������ ������
         *          
         * -- �������� ���������, �������� ���������
         * 1. � ��������� �������� ������:
         *      ����������:
         *        ����������, ���������, ������, ��������
         *      ���:
         *        ���, ���������
         * 2. �������� ������ �� �����:
         *      ���������, ���, ����������, ��������, ������
         *      
         * -- ������
         * 1. ���������� ��������� �����:
         *      �������� assembly
         *      �������� classname
         * 2. ������ �������� ��������
         *      [��������� Mock-����������!]
         *      
         * ��������� � ����������.
         * 1. ������� well-formed ����, ������� �������� ��������.
         * 2. ��� �������� ������ ���������, ��� ������ ���������� � � ��� ��������.
         * 3. ������� ������� ����� .xml-������, ��������������� � .t-����� � �������� � �������.
         * 4. ������������� ������������� ����-������:
         *    ��������: �� �������� xml
         *    ����������:
         *      0. ���������� � ��������� ������ + 2 ����-������ - � 'string' � 'XmlDocument' ������
         *         (������ � ������ ����������� ��������� xml)
         *      1. ������ ��������� - �������� �������� ����������
         *      2. ��� - �������� �� ��������, ������ 'to do'
         *      
         * ������ ���������� ������.
         * �.1 - 42
         * �.2 - 11
         * �.3 - 2+
         * �����: 55+
         */

        /*
         * ��������� ������� ��� ��������� ������
         * $ makecode.bat >tests.cs
         * 
         * .\valid\ - �������� xml
         * .\nonvalid\ - ���������� xml
         * 
         */

        [TestCleanup]
        public void Done () {}
    }
}
