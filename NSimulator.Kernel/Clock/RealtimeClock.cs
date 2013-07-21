namespace NSimulator.Kernel {
    /// <summary>
    ///   Часы реального времени.
    /// </summary>
    /// <seealso cref = "IClock" />
    /// <seealso cref = "BaseClock" />
    /// <remarks>
    ///   В часах реального времени продолжительность каждого тика не меньше
    ///   реальной продолжительности. Если обработка действий в тике
    ///   завершилась раньше, чем должна, то процесс приостанавливается на
    ///   необходимое время.
    /// </remarks>
    public class RealtimeClock : BaseClock {
        /// <summary>
        ///   Инициализация нового экземляра часов.
        ///   В начальный момент номер тика равен нулю, часы приостановлены.
        /// </summary>
        /// <param name = "tickLength">Размер тика в наносекундах.</param>
        public RealtimeClock (ulong tickLength) {
            this.TickLength = tickLength;
        }
    }
}
