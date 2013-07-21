namespace CmdlineParser {
    public sealed partial class Parser {
        #region Nested type: ArgumentHelpStrings

        private struct ArgumentHelpStrings {
            public readonly string help;
            public readonly string syntax;

            public ArgumentHelpStrings (string syntax, string help) {
                this.syntax = syntax;
                this.help = help;
            }
        }

        #endregion
    }
}
