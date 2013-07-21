namespace NSimulator.Library {
    /// <summary>
    ///   Типы состояния среды передачи "провод".
    /// </summary>
    /// <seealso cref = "Wire" />
    // todo Адаптировать к full-duplex, half-duplex.
    public enum WireState {
        /// <summary>
        ///   Провод занят.
        /// </summary>
        WIRE_BUSY,

        /// <summary>
        ///   Провод свободен.
        /// </summary>
        WIRE_FREE
    }
}
