namespace NModel.Extension {
    /// <summary>
    ///   Операторы в формулах логики CTL.
    /// </summary>
    /// <seealso cref = "CTLFormula" />
    public enum CTLOperator {
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
        ///   Темпоральный "EX".
        /// </summary>
        CTL_EX,

        /// <summary>
        ///   Темпоральный "EG".
        /// </summary>
        CTL_EG,

        /// <summary>
        ///   Темпоральный "EU".
        /// </summary>
        CTL_EU
    }
}
