#region

using System;
using System.Diagnostics.Contracts;
using System.Xml;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс xml-сериализуемой сущности.
    /// </summary>
    /// <remarks>
    ///   <para>
    ///     Класс, реализующий данный интерфейс, должен иметь
    ///     конструктор по умолчанию (public или private), который может
    ///     быть вызван средствами динамической загрузки.
    ///   </para>
    ///   <para>
    ///     Исключение составляет сущность <see cref = "IBackbone" /> - она должна иметь конструктор,
    ///     принимающий ровно один аргумент типа <see cref = "IClock" />.
    ///   </para>
    /// </remarks>
    [ContractClass (typeof (Contract_IXMLSerializable))]
    public interface IXMLSerializable {
        /// <summary>
        ///   Выполняет загрузку информации из заданного xml-узла.
        /// </summary>
        /// <param name = "data">xml-узел, из которого загружать информацию.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "data" /> является <c>null</c>.</exception>
        void Load (XmlNode data);

        /// <summary>
        ///   Выполняет сохранение информации в заданный xml-узел.
        /// </summary>
        /// <param name = "node">xml-узел, в который нужно сохранить информацию.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "node" /> является <c>null</c>.</exception>
        void Store (XmlNode node);
    }
}
