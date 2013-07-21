namespace NModel.Extension {
    /// <summary>
    ///   Операторы в формулах логики LTL.
    /// </summary>
    /// <seealso cref = "LTLFormula" />
    public enum LTLOperator {
        /// <summary>
        ///   Пустой оператор (атомарное высказывание).
        /// </summary>
        NONE,

        /// <summary>
        ///   Тождественная истина.
        /// </summary>
        TRUE,

        /// <summary>
        ///   Тождественная ложь.
        /// </summary>
        FALSE,

        /// <summary>
        ///   Логическое "не".
        /// </summary>
        LOGIC_NOT,

        /// <summary>
        ///   Логическое "и".
        /// </summary>
        LOGIC_AND,

        /// <summary>
        ///   Логическое "или".
        /// </summary>
        LOGIC_OR,

        /// <summary>
        ///   Темпоральный "X".
        /// </summary>
        LTL_X,

        /// <summary>
        ///   Темпоральный "U".
        /// </summary>
        LTL_U
    }
}
