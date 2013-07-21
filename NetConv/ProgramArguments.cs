#region

using CmdlineParser;

#endregion

namespace NetConv {
    internal class ProgramArguments {
        [Argument (
            ArgumentType.AtMostOnce | ArgumentType.Required,
            HelpText = "Исходный файл.",
            ShortName = "i",
            LongName = "input")]
        public string from;

        [Argument (
            ArgumentType.AtMostOnce | ArgumentType.Required,
            HelpText = "Файл-результат.",
            ShortName = "o",
            LongName = "output")]
        public string to;

        [Argument (
            ArgumentType.AtMostOnce,
            HelpText = "Распаковать контейнер.",
            ShortName = "u",
            LongName = "unpack",
            DefaultValue = false)]
        public bool unpack;

        public ProgramArguments () {
            this.unpack = false;
            this.from = string.Empty;
            this.to = string.Empty;
        }
    }
}
