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
         * Тесты на LoadFromXml (...)
         * 
         * Типы "плохих" случаев:
         * 0. Неверный аргумент (невозможно открыть файл, ...)
         * 1. Не проходит валидацию
         * 2. Проходит валидацию, но неверная семантика
         * 3. Проходит валидацию, верная семантика, но что-то другое
         * 
         * -- Неверный аргумент
         * 1. Неверное имя файла
         * 2. Битый контейнер
         * 3. Внутри не xml
         * 
         * -- Не проходит валидацию
         * 1. id не уникальные:
         *    Устройство, интерфейс, СПД, модуль, прошивка:
         *      Класс, сущность
         *    Топология:
         *      Устройство:
         *        Устройство, интерфейс, прошивка, модуль (прошивка вообще уникальна)
         *      СПД:
         *        СПД, интерфейс
         * 2. id не числа:
         *    Устройство, интерфейс, СПД, модуль, прошивка:
         *      Класс, сущность
         *    Топология:
         *      Устройство:
         *        Устройство, интерфейс, модуль, прошивка
         *      СПД:
         *        СПД, интерфейс
         * 3. Лишние сущности:
         *      Элементы:
         *        Не класс
         *        Не элемент списка
         *        Лишний ребёнок
         *        Не сущность
         *      Топология:
         *        Не устройство и СПД
         *        Устройство:
         *          Не интерфейс, прошивка, модуль
         *          Лишний ребёнок
         *        СПД:
         *          Не интерфейс
         *          Лишний ребёнок
         *          
         * -- Проходит валидацию, неверная семантика
         * 1. В топологии неверная ссылка:
         *      Устройство:
         *        Устройство, интерфейс, модуль, прошивка
         *      СПД:
         *        СПД, интерфейс
         * 2. Неверная ссылка на класс:
         *      Интерфейс, СПД, устройство, прошивка, модуль
         *      
         * -- Другое
         * 1. Невозможно загрузить класс:
         *      Неверный assembly
         *      Неверный classname
         * 2. Ошибка загрузки сущности
         *      [требуется Mock-реализация!]
         *      
         * Замечания к реализации.
         * 1. Сделать well-formed файл, который коррекно работает.
         * 2. При внесении ошибки проверять, что ошибка исправлена и о ней сообщено.
         * 3. Сделать вначале набор .xml-файлов, сконвертировать в .t-файлы и добавить в ресурсы.
         * 4. Автоматически сгенерировать тест-методы:
         *    Название: по названию xml
         *    Реализация:
         *      0. Реализация в отдельном методе + 2 тест-метода - с 'string' и 'XmlDocument' формой
         *         (только в случае прохождения валидации xml)
         *      1. Ошибка валидации - проверка проброса исключения
         *      2. Код - загрузка из ресурсов, строка 'to do'
         *      
         * Оценка количества тестов.
         * п.1 - 42
         * п.2 - 11
         * п.3 - 2+
         * Итого: 55+
         */

        /*
         * Заготовка шаблона для генерации тестов
         * $ makecode.bat >tests.cs
         * 
         * .\valid\ - Валидный xml
         * .\nonvalid\ - Невалидные xml
         * 
         */

        [TestCleanup]
        public void Done () {}
    }
}
