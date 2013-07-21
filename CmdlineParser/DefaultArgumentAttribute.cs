#region

using System;
using System.Diagnostics.CodeAnalysis;

#endregion

namespace CmdlineParser {
    /// <summary>
    ///   Indicates that this argument is the default argument.
    ///   '/' or '-' prefix only the argument value is specified.
    ///   The ShortName property should not be set for DefaultArgumentAttribute
    ///   instances. The LongName property is used for usage text only and
    ///   does not affect the usage of the argument.
    /// </summary>
    [SuppressMessage ("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes")]
    [AttributeUsage (AttributeTargets.Field)]
    public class DefaultArgumentAttribute : ArgumentAttribute {
        /// <summary>
        ///   Indicates that this argument is the default argument.
        /// </summary>
        /// <param name = "type"> Specifies the error checking to be done on the argument. </param>
        public DefaultArgumentAttribute (ArgumentType type)
            : base (type) {}
    }
}
