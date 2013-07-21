#region

using System;
using System.Diagnostics.Contracts;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс среды передачи данных.
    ///   Предоставляется расширение интерфейса только для чтения до полного интерфейса.
    /// </summary>
    /// <seealso cref = "IBackboneView" />
    [ContractClass (typeof (Contract_IBackbone))]
    public interface IBackbone : IBackboneView {
        /// <summary>
        ///   Устанавливает название среды передачи данных.
        /// </summary>
        /// <param name = "name">Новое название среды передачи.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "name" /> является <c>null</c>.</exception>
        void SetName (string name);

        /// <summary>
        ///   Привязывает интерфейс к конечной точке среды передачи.
        /// </summary>
        /// <remarks>
        ///   Метод должен только выполнять проверку корректности привязки и выполнять её.
        ///   Обратную связь должна выполнять реализация <see cref = "IInterface.SetBackbone" />.
        /// </remarks>
        /// <param name = "iface">Интерфейс, который необходимо привязать к конечной точке.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceAlreadyAttachedToBackboneException">Интерфейс <paramref name = "iface" /> уже привязан к среде передачи.</exception>
        /// <exception cref = "EndPointsOverflowException">Количество конечных точек превысило лимит.</exception>
        void AttachEndPoint (IInterfaceView iface);

        /// <summary>
        ///   Отвязывает конечную точку среды передачи данных от интерфейса.
        /// </summary>
        /// <remarks>
        ///   Метод должен только выполнять проверку корректности отвязки и выполнять её.
        ///   Обратную связь должна уничтожать реализация <see cref = "IInterface.ReleaseBackbone" />.
        /// </remarks>
        /// <param name = "iface">Интерфейс, который необходимо отвязать от конечной точки.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "iface" /> является <c>null</c>.</exception>
        /// <exception cref = "InterfaceNotAttachedToBackboneException"><paramref name = "iface" /> не привязана к данной среде передачи.</exception>
        void DetachEndPoint (IInterfaceView iface);

        /// <summary>
        ///   Изменяет скорость среды передачи (бит/с).
        /// </summary>
        /// <param name = "speed">Скорость передачи данных.</param>
        void ChangeSpeed (ulong speed);

        /// <summary>
        ///   Изменяет долю потерь пакетов.
        /// </summary>
        /// <param name = "percent">Доля потерь пакетов.</param>
        /// <remarks>
        ///   Указывается 0, если потерь не должно быть, и 1, если должно теряться всё.
        /// </remarks>
        /// <exception cref = "ArgumentOutOfRangeException"><paramref name = "percent" /> не лежит в диапазоне [0..1].</exception>
        void ChangeLossPercent (double percent);
    }
}
