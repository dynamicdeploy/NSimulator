#region

using System;
using System.Diagnostics.Contracts;
using System.IO;

#endregion

namespace NSimulator.Kernel {
    /// <summary>
    ///   Интерфейс логгирования передаваемых пакетов.
    /// </summary>
    [ContractClass (typeof (Contract_IPacketDumper))]
    public interface IPacketDumper : IDisposable {
        /// <summary>
        ///   Включает логгирование пакетов, передаваемых в указанной среде передачи.
        /// </summary>
        /// <param name = "backbone">Среда передачи, в которой нужно включить логгирование.</param>
        /// <remarks>
        ///   Логгирование передаваемых пакетов происходит в некоторое хранилище, зависящее
        ///   от конктретной реализации этого интерфейса.
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> является <c>null</c>.</exception>
        void AttachToBackbone (IBackboneView backbone);

        /// <summary>
        ///   Включает логгирование пакетов, передаваемых в указанной среде передачи.
        /// </summary>
        /// <param name = "backbone">Среда передачи, в которой нужно включить логгирование.</param>
        /// <param name = "stream">Поток, в который требуется выводить лог.</param>
        /// <remarks>
        ///   <para>
        ///     Логгирование передаваемых пакетов происходит в указанный в <paramref name = "stream" />
        ///     поток вывода.
        ///   </para>
        ///   <para>
        ///     При вызове <see cref = "IDisposable.Dispose" /> переданный поток освобождаться не должен.
        ///   </para>
        /// </remarks>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> является <c>null</c>.</exception>
        /// <exception cref = "ArgumentNullException"><paramref name = "stream" /> является <c>null</c>.</exception>
        void AttachToBackbone (IBackboneView backbone, Stream stream);

        /// <summary>
        ///   Отключает логгирование пакетов, передаваемых в указанной среде передачи.
        /// </summary>
        /// <param name = "backbone">Среда передачи, в которой нужно отключить логгирование.</param>
        /// <exception cref = "ArgumentNullException"><paramref name = "backbone" /> является <c>null</c>.</exception>
        // / <exception cref="DumperNotAttachedToBackboneException">Логгирование в среде передачи <paramref name="backbone"/> не включено.</exception>
        void DetachFromBackbone (IBackboneView backbone);
    }
}
