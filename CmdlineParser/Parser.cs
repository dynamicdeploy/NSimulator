#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

#endregion

namespace CmdlineParser {
    ///<summary>
    ///  Parser for command line arguments.
    ///
    ///  The parser specification is infered from the instance fields of the object
    ///  specified as the destination of the parse.
    ///  Valid argument types are: int, uint, string, bool, enums
    ///  Also argument types of Array of the above types are also valid.
    /// 
    ///  Error checking options can be controlled by adding a ArgumentAttribute
    ///  to the instance fields of the destination object.
    ///
    ///  At most one field may be marked with the DefaultArgumentAttribute
    ///  indicating that arguments without a '-' or '/' prefix will be parsed as that argument.
    ///
    ///  If not specified then the parser will infer default options for parsing each
    ///  instance field. The default long name of the argument is the field name. The
    ///  default short name is the first character of the long name. Long names and explicitly
    ///  specified short names must be unique. Default short names will be used provided that
    ///  the default short name does not conflict with a long name or an explicitly
    ///  specified short name.
    ///
    ///  Arguments which are array types are collection arguments. Collection
    ///  arguments can be specified multiple times.
    ///</summary>
    ///<remarks>
    ///  <para>
    ///    Код данного парсера взят из проекта NModel (http://nmodel.codeplex.com/).
    ///  </para>
    ///  <para>
    ///    Изменения, сделанные в коде:
    ///    <list type = "number">
    ///      <item>
    ///        <description>Добавлено это замечание.</description>
    ///      </item>
    ///      <item>
    ///        <description>Код разделён на несколько файлов (в соответствии с классами).</description>
    ///      </item>
    ///      <item>
    ///        <description>Форматирование кода приведено к стандарту, принятому в проекте NSimualtor.</description>
    ///      </item>
    ///      <item>
    ///        <description>Произведён рефакторинг кода (код приведён к стандарту, принятому в проекте).</description>
    ///      </item>
    ///      <item>
    ///        <description>Добавлен README.</description>
    ///      </item>
    ///    </list>
    ///  </para>
    ///</remarks>
    public sealed partial class Parser {
        /// <summary>
        ///   The System Defined new line string.
        /// </summary>
        public const string NewLine = "\r\n";

        private const int spaceBeforeParam = 2;
        private readonly Hashtable argumentMap;
        private readonly ArrayList arguments;
        private readonly Argument defaultArgument;
        private readonly ErrorReporter reporter;

        /// <summary>
        ///   Creates a new command line argument parser.
        /// </summary>
        /// <param name = "argumentSpecification"> The type of object to  parse. </param>
        /// <param name = "reporter"> The destination for parse errors. </param>
        public Parser (Type argumentSpecification, ErrorReporter reporter) {
            if (argumentSpecification == null)
                throw new ArgumentNullException ("argumentSpecification");

            this.reporter = reporter;
            this.arguments = new ArrayList ();
            this.argumentMap = new Hashtable ();

            foreach (
                var field in
                    argumentSpecification.GetFields (BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                                     BindingFlags.NonPublic)) {
                if (field.IsInitOnly || field.IsLiteral)
                    continue;
                var attribute = GetAttribute (field);
                if (attribute is DefaultArgumentAttribute) {
                    Debug.Assert (this.defaultArgument == null);
                    this.defaultArgument = new Argument (attribute, field, reporter);
                }
                else if (attribute != null)
                    this.arguments.Add (new Argument (attribute, field, reporter));
            }

            foreach (Argument argument in this.arguments) {
                Debug.Assert (!this.argumentMap.ContainsKey (argument.LongName));
                this.argumentMap [argument.LongName] = argument;
                if (!argument.ExplicitShortName)
                    continue;

                if (!string.IsNullOrEmpty (argument.ShortName)) {
                    Debug.Assert (!this.argumentMap.ContainsKey (argument.ShortName));
                    this.argumentMap [argument.ShortName] = argument;
                }
                else
                    argument.ClearShortName ();
            }

            foreach (var argument in this.arguments.Cast <Argument> ().Where (argument => !argument.ExplicitShortName)) {
                if (!string.IsNullOrEmpty (argument.ShortName) && !this.argumentMap.ContainsKey (argument.ShortName))
                    this.argumentMap [argument.ShortName] = argument;
                else
                    argument.ClearShortName ();
            }
        }

        /// <summary>
        ///   Does this parser have a default argument.
        /// </summary>
        /// <value> Does this parser have a default argument. </value>
        public bool HasDefaultArgument {
            get { return this.defaultArgument != null; }
        }

        /// <summary>
        ///   Parses Command Line Arguments. Displays usage message to Console.Out
        ///   if /?, /help or invalid arguments are encounterd.
        ///   Errors are output on Console.Error.
        ///   Use ArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name = "arguments"> The actual arguments. </param>
        /// <param name = "destination"> The resulting parsed arguments. </param>
        /// <returns> true if no errors were detected. </returns>
        public static bool ParseArgumentsWithUsage (IEnumerable <string> arguments, object destination) {
            if (destination == null)
                throw new ArgumentNullException ("destination");

            if (ParseHelp (arguments)) {
                Console.Write (ArgumentsUsage (destination.GetType ()));
                return false;
            }
            return ParseArguments (arguments, destination);
        }

        /// <summary>
        ///   Parses Command Line Arguments. Displays usage message to Console.Out
        ///   if /?, /help or invalid arguments are encounterd.
        ///   Errors are output on Console.Error.
        ///   Use ArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name = "arguments"> The actual arguments. </param>
        /// <param name = "destination"> The resulting parsed arguments. </param>
        /// <returns> true if no errors were detected. </returns>
        public static bool ParseArgumentsWithUsage (IEnumerable <string> arguments, Type destination) {
            return !ParseHelp (arguments) && ParseArguments (arguments, destination);
        }

        /// <summary>
        ///   Parses Command Line Arguments. 
        ///   Errors are output on Console.Error.
        ///   Use ArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name = "arguments"> The actual arguments. </param>
        /// <param name = "destination"> The resulting parsed arguments. </param>
        /// <returns> true if no errors were detected. </returns>
        public static bool ParseArguments (IEnumerable <string> arguments, object destination) {
            return ParseArguments (arguments, destination, Console.Error.WriteLine);
        }

        /// <summary>
        ///   Parses Command Line Arguments. 
        ///   Errors are output on Console.Error.
        ///   Use ArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name = "arguments"> The actual arguments. </param>
        /// <param name = "destination"> The resulting parsed arguments. </param>
        /// <returns> true if no errors were detected. </returns>
        public static bool ParseArguments (IEnumerable <string> arguments, Type destination) {
            return ParseArguments (arguments, destination, Console.Error.WriteLine);
        }

        /// <summary>
        ///   Parses Command Line Arguments. 
        ///   Use ArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name = "arguments"> The actual arguments. </param>
        /// <param name = "destination"> The resulting parsed arguments. </param>
        /// <param name = "reporter"> The destination for parse errors. </param>
        /// <returns> true if no errors were detected. </returns>
        public static bool ParseArguments (IEnumerable <string> arguments, object destination, ErrorReporter reporter) {
            if (destination == null)
                throw new ArgumentNullException ("destination");

            var parser = new Parser (destination.GetType (), reporter);
            return parser.Parse (arguments, destination);
        }

        /// <summary>
        ///   Parses Command Line Arguments. 
        ///   Use ArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name = "arguments"> The actual arguments. </param>
        /// <param name = "destination"> The resulting parsed arguments. </param>
        /// <param name = "reporter"> The destination for parse errors. </param>
        /// <returns> true if no errors were detected. </returns>
        public static bool ParseArguments (IEnumerable <string> arguments, Type destination, ErrorReporter reporter) {
            var parser = new Parser (destination, reporter);
            return parser.Parse (arguments, null);
        }

        private static void NullErrorReporter (string message) {}

        /// <summary>
        ///   Checks if a set of arguments asks for help.
        /// </summary>
        /// <param name = "args"> Args to check for help. </param>
        /// <returns> Returns true if args contains /? or /help. </returns>
        public static bool ParseHelp (IEnumerable <string> args) {
            var helpParser = new Parser (typeof (HelpArgument), NullErrorReporter);
            var helpArgument = new HelpArgument ();
            helpParser.Parse (args, helpArgument);
            return helpArgument.help;
        }

        /// <summary>
        ///   Returns a Usage string for command line argument parsing.
        ///   Use ArgumentAttributes to control parsing behaviour.
        ///   Formats the output to the width of the current console window.
        /// </summary>
        /// <param name = "argumentType"> The type of the arguments to display usage for. </param>
        /// <returns> Printable string containing a user friendly description of command line arguments. </returns>
        public static string ArgumentsUsage (Type argumentType) {
            var screenWidth = Console.WindowWidth;
            if (screenWidth == 0)
                screenWidth = 80;
            return ArgumentsUsage (argumentType, screenWidth);
        }

        /// <summary>
        ///   Returns a Usage string for command line argument parsing.
        ///   Use ArgumentAttributes to control parsing behaviour.
        /// </summary>
        /// <param name = "argumentType"> The type of the arguments to display usage for. </param>
        /// <param name = "columns"> The number of columns to format the output to. </param>
        /// <returns> Printable string containing a user friendly description of command line arguments. </returns>
        public static string ArgumentsUsage (Type argumentType, int columns) {
            return (new Parser (argumentType, null)).GetUsageString (columns);
        }

        /// <summary>
        ///   Searches a StringBuilder for a character
        /// </summary>
        /// <param name = "text"> The text to search. </param>
        /// <param name = "value"> The character value to search for. </param>
        /// <param name = "startIndex"> The index to stat searching at. </param>
        /// <returns> The index of the first occurence of value or -1 if it is not found. </returns>
        public static int IndexOf (StringBuilder text, char value, int startIndex) {
            if (text == null)
                throw new ArgumentNullException ("text");

            for (var index = startIndex; index < text.Length; index++) {
                if (text [index] == value)
                    return index;
            }

            return -1;
        }

        /// <summary>
        ///   Searches a StringBuilder for a character in reverse
        /// </summary>
        /// <param name = "text"> The text to search. </param>
        /// <param name = "value"> The character to search for. </param>
        /// <param name = "startIndex"> The index to start the search at. </param>
        /// <returns>The index of the last occurence of value in text or -1 if it is not found. </returns>
        public static int LastIndexOf (StringBuilder text, char value, int startIndex) {
            if (text == null)
                throw new ArgumentNullException ("text");

            for (var index = Math.Min (startIndex, text.Length - 1); index >= 0; index--) {
                if (text [index] == value)
                    return index;
            }

            return -1;
        }

        private static ArgumentAttribute GetAttribute (FieldInfo field) {
            var attributes = field.GetCustomAttributes (typeof (ArgumentAttribute), false);
            if (attributes.Length == 1)
                return (ArgumentAttribute) attributes [0];

            Debug.Assert (attributes.Length == 0);
            return null;
        }

        private void ReportUnrecognizedArgument (string argument) {
            this.reporter (string.Format ("Unrecognized command line argument '{0}'", argument));
        }

        /// <summary>
        ///   Parses an argument list into an object
        /// </summary>
        /// <param name = "args"></param>
        /// <param name = "destination"></param>
        /// <returns> true if an error occurred </returns>
        private bool ParseArgumentList (IEnumerable <string> args, object destination) {
            var hadError = false;
            if (args != null) {
                foreach (var argument in args.Where (argument => argument.Length > 0)) {
                    switch (argument [0]) {
                        case '-' :
                        case '/' :
                            var endIndex = argument.IndexOfAny (new [] { ':', '+', '-' }, 1);
                            var option = argument.Substring (1, endIndex == -1 ? argument.Length - 1 : endIndex - 1);
                            string optionArgument;
                            if (option.Length + 1 == argument.Length)
                                optionArgument = null;
                            else if (argument.Length > 1 + option.Length && argument [1 + option.Length] == ':')
                                optionArgument = argument.Substring (option.Length + 2);
                            else
                                optionArgument = argument.Substring (option.Length + 1);

                            var arg = (Argument) this.argumentMap [option];
                            if (arg == null) {
                                this.ReportUnrecognizedArgument (argument);
                                hadError = true;
                            }
                            else
                                hadError |= !arg.SetValue (optionArgument, destination);
                            break;
                        case '@' :
                            string [] nestedArguments;
                            hadError |= this.LexFileArguments (argument.Substring (1), out nestedArguments);
                            hadError |= this.ParseArgumentList (nestedArguments, destination);
                            break;
                        default :
                            if (this.defaultArgument != null)
                                hadError |= !this.defaultArgument.SetValue (argument, destination);
                            else {
                                this.ReportUnrecognizedArgument (argument);
                                hadError = true;
                            }
                            break;
                    }
                }
            }

            return hadError;
        }

        /// <summary>
        ///   Parses an argument list.
        /// </summary>
        /// <param name = "args"> The arguments to parse. </param>
        /// <param name = "destination"> The destination of the parsed arguments. </param>
        /// <returns> true if no parse errors were encountered. </returns>
        public bool Parse (IEnumerable <string> args, object destination) {
            var hadError = this.ParseArgumentList (args, destination);

            hadError = this.arguments.Cast <Argument> ().Aggregate (hadError,
                                                                    (current, arg) => current | arg.Finish (destination));
            if (this.defaultArgument != null)
                hadError |= this.defaultArgument.Finish (destination);

            return !hadError;
        }

        /// <summary>
        ///   A user firendly usage string describing the command line argument syntax.
        /// </summary>
        public string GetUsageString (int screenWidth) {
            var strings = this.GetAllHelpStrings ();

            var maxParamLen = strings.Aggregate (0,
                                                 (current, helpString) => Math.Max (current, helpString.syntax.Length));

            const int minimumNumberOfCharsForHelpText = 10;
            const int minimumHelpTextColumn = 5;
            const int minimumScreenWidth = minimumHelpTextColumn + minimumNumberOfCharsForHelpText;

            var idealMinimumHelpTextColumn = maxParamLen + spaceBeforeParam;
            screenWidth = Math.Max (screenWidth, minimumScreenWidth);
            var helpTextColumn = screenWidth < (idealMinimumHelpTextColumn + minimumNumberOfCharsForHelpText)
                                     ? minimumHelpTextColumn
                                     : idealMinimumHelpTextColumn;

            const string newLine = "\n";
            var builder = new StringBuilder ();

            builder.AppendLine ();

            var assembly = Assembly.GetEntryAssembly ();
            if (assembly != null) {
                builder.AppendFormat ("{0} version {1}", GetTitle (assembly), assembly.GetName ().Version);
                builder.AppendLine ();

                var copyright = GetAssemblyAttribute <AssemblyCopyrightAttribute> (assembly);
                if (copyright != null && !string.IsNullOrEmpty (copyright.Copyright))
                    builder.AppendLine (copyright.Copyright.Replace ("©", "(C)"));
                builder.AppendLine ();

                builder.AppendFormat ("Usage: {0}", Path.GetFileNameWithoutExtension (assembly.Location));
                foreach (var helpStrings in strings)
                    builder.AppendFormat (" {0}", helpStrings.syntax);
                builder.AppendLine ();
                builder.AppendLine ();
            }

            foreach (var helpStrings in strings) {
                var syntaxLength = helpStrings.syntax.Length;
                builder.Append (helpStrings.syntax);

                var currentColumn = syntaxLength;
                if (syntaxLength >= helpTextColumn) {
                    builder.Append (newLine);
                    currentColumn = 0;
                }

                var charsPerLine = screenWidth - helpTextColumn;
                var index = 0;
                while (index < helpStrings.help.Length) {
                    builder.Append (' ', helpTextColumn - currentColumn);
                    currentColumn = helpTextColumn;

                    var endIndex = index + charsPerLine;
                    if (endIndex >= helpStrings.help.Length)
                        endIndex = helpStrings.help.Length;
                    else {
                        endIndex = helpStrings.help.LastIndexOf (' ',
                                                                 endIndex - 1,
                                                                 Math.Min (endIndex - index, charsPerLine));
                        if (endIndex <= index)
                            endIndex = index + charsPerLine;
                    }

                    builder.Append (helpStrings.help, index, endIndex - index);
                    index = endIndex;

                    AddNewLine (newLine, builder, ref currentColumn);

                    while (index < helpStrings.help.Length && helpStrings.help [index] == ' ')
                        index++;
                }

                if (helpStrings.help.Length == 0)
                    builder.Append (newLine);
            }

            return builder.ToString ();
        }

        private static string GetTitle (Assembly assembly) {
            var prefix = "";
            var company = GetAssemblyAttribute <AssemblyCompanyAttribute> (assembly);
            if (company != null && !string.IsNullOrEmpty (company.Company))
                prefix = company.Company + " ";

            var title = GetAssemblyAttribute <AssemblyTitleAttribute> (assembly);
            if (title != null && !string.IsNullOrEmpty (title.Title))
                return title.Title.StartsWith (prefix) ? title.Title : prefix + title.Title;

            var product = GetAssemblyAttribute <AssemblyProductAttribute> (assembly);
            if (title != null && !string.IsNullOrEmpty (product.Product))
                return product.Product.StartsWith (prefix) ? product.Product : prefix + product.Product;

            var description = GetAssemblyAttribute <AssemblyDescriptionAttribute> (assembly);
            if (title != null && !string.IsNullOrEmpty (description.Description)) {
                return description.Description.StartsWith (prefix)
                           ? description.Description
                           : prefix + description.Description;
            }

            return prefix + assembly.GetName ().Name;
        }

        private static T GetAssemblyAttribute <T> (Assembly assembly) where T : Attribute {
            var attributes = assembly.GetCustomAttributes (typeof (T), true);
            if (attributes != null && attributes.Length > 0)
                return (T) attributes [0];
            return null;
        }

        private static void AddNewLine (string newLine, StringBuilder builder, ref int currentColumn) {
            builder.Append (newLine);
            currentColumn = 0;
        }

        private IEnumerable <ArgumentHelpStrings> GetAllHelpStrings () {
            var strings = new ArgumentHelpStrings[this.NumberOfParametersToDisplay ()];

            var index = 0;
            foreach (Argument arg in this.arguments)
                strings [index++] = GetHelpStrings (arg);
            if (this.defaultArgument != null)
                strings [index++] = GetHelpStrings (this.defaultArgument);
            strings [index++] = new ArgumentHelpStrings ("@<file>", "Read response file for more options.");

            return strings;
        }

        private static ArgumentHelpStrings GetHelpStrings (Argument arg) {
            return new ArgumentHelpStrings (arg.SyntaxHelp, arg.FullHelpText);
        }

        private int NumberOfParametersToDisplay () {
            var numberOfParameters = this.arguments.Count + 1;
            if (this.HasDefaultArgument)
                numberOfParameters += 1;
            return numberOfParameters;
        }

        [SuppressMessage ("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private bool LexFileArguments (string fileName, out string [] arguments1) {
            string args;

            try {
                using (var file = new FileStream (fileName, FileMode.Open, FileAccess.Read)) {
                    var sreader = new StreamReader (file);
                    args = sreader.ReadToEnd ();
                }
            }
            catch (Exception e) {
                this.reporter (string.Format ("Error: Can't open command line argument file '{0}' : '{1}'",
                                              fileName,
                                              e.Message));
                arguments1 = null;
                return true;
            }

            var hadError = false;
            var argArray = new ArrayList ();
            var currentArg = new StringBuilder ();
            var inQuotes = false;
            var index = 0;

            try {
                while (true) {
                    while (char.IsWhiteSpace (args [index]))
                        index += 1;

                    if (args [index] == '#') {
                        index += 1;
                        while (args [index] != '\n')
                            index += 1;
                        continue;
                    }

                    do {
                        switch (args [index]) {
                            case '\\' : {
                                var cSlashes = 1;
                                index += 1;
                                while (index == args.Length && args [index] == '\\')
                                    cSlashes += 1;

                                if (index == args.Length || args [index] != '"')
                                    currentArg.Append ('\\', cSlashes);
                                else {
                                    currentArg.Append ('\\', (cSlashes >> 1));
                                    if (0 != (cSlashes & 1))
                                        currentArg.Append ('"');
                                    else
                                        inQuotes = !inQuotes;
                                }
                            }
                                break;
                            case '"' :
                                inQuotes = !inQuotes;
                                index += 1;
                                break;
                            default :
                                currentArg.Append (args [index]);
                                index += 1;
                                break;
                        }
                    } while (!char.IsWhiteSpace (args [index]) || inQuotes);
                    argArray.Add (currentArg.ToString ());
                    currentArg.Length = 0;
                }
            }
            catch (IndexOutOfRangeException) {
                if (inQuotes) {
                    this.reporter (string.Format ("Error: Unbalanced '\"' in command line argument file '{0}'", fileName));
                    hadError = true;
                }
                else if (currentArg.Length > 0)
                    argArray.Add (currentArg.ToString ());
            }

            arguments1 = (string []) argArray.ToArray (typeof (string));
            return hadError;
        }

        private static string LongName (ArgumentAttribute attribute, FieldInfo field) {
            return (attribute == null || attribute.DefaultLongName) ? field.Name : attribute.LongName;
        }

        private static string ShortName (ArgumentAttribute attribute, FieldInfo field) {
            if (attribute is DefaultArgumentAttribute)
                return null;
            if (!ExplicitShortName (attribute))
                return LongName (attribute, field).Substring (0, 1);
            return attribute.ShortName;
        }

        private static string HelpText (ArgumentAttribute attribute) {
            return attribute == null ? null : attribute.HelpText;
        }

        private static bool HasHelpText (ArgumentAttribute attribute) {
            return (attribute != null && attribute.HasHelpText);
        }

        private static bool ExplicitShortName (ArgumentAttribute attribute) {
            return (attribute != null && !attribute.DefaultShortName);
        }

        private static object DefaultValue (ArgumentAttribute attribute) {
            return (attribute == null || !attribute.HasDefaultValue) ? null : attribute.DefaultValue;
        }

        private static Type ElementType (FieldInfo field) {
            return IsCollectionType (field.FieldType) ? field.FieldType.GetElementType () : null;
        }

        private static ArgumentType Flags (ArgumentAttribute attribute, FieldInfo field) {
            if (attribute != null)
                return attribute.Type;
            return IsCollectionType (field.FieldType) ? ArgumentType.MultipleUnique : ArgumentType.AtMostOnce;
        }

        private static bool IsCollectionType (Type type) {
            return type.IsArray;
        }

        private static bool IsValidElementType (Type type) {
            return type != null && (
                                       type == typeof (int) ||
                                       type == typeof (uint) ||
                                       type == typeof (string) ||
                                       type == typeof (bool) ||
                                       type.IsEnum);
        }
    }
}
