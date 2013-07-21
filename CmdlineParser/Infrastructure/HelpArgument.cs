namespace CmdlineParser {
    public sealed partial class Parser {
        #region Nested type: HelpArgument

        private class HelpArgument {
            [Argument (ArgumentType.AtMostOnce, ShortName = "?")]
            public bool help;
        }

        #endregion
    }
}
