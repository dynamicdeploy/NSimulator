#region

using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

#endregion

namespace CmdlineParser {
    public sealed partial class Parser {
        #region Nested type: Argument

        private class Argument {
            private readonly ArrayList collectionValues;
            private readonly object defaultValue;
            private readonly Type elementType;
            private readonly bool explicitShortName;
            private readonly FieldInfo field;
            private readonly ArgumentType flags;
            private readonly bool hasHelpText;
            private readonly string helpText;
            private readonly bool isDefault;
            private readonly string longName;
            private readonly ErrorReporter reporter;
            private string shortName;

            public Argument (ArgumentAttribute attribute, FieldInfo field, ErrorReporter reporter) {
                this.longName = Parser.LongName (attribute, field);
                this.explicitShortName = Parser.ExplicitShortName (attribute);
                this.shortName = Parser.ShortName (attribute, field);
                this.hasHelpText = Parser.HasHelpText (attribute);
                this.helpText = Parser.HelpText (attribute);
                this.defaultValue = Parser.DefaultValue (attribute);
                this.elementType = ElementType (field);
                this.flags = Flags (attribute, field);
                this.field = field;
                this.SeenValue = false;
                this.reporter = reporter;
                this.isDefault = attribute != null && attribute is DefaultArgumentAttribute;

                if (this.IsCollection)
                    this.collectionValues = new ArrayList ();

                Debug.Assert (!String.IsNullOrEmpty (this.longName));
                Debug.Assert (!this.isDefault || !this.ExplicitShortName);
                Debug.Assert (!this.IsCollection || this.AllowMultiple, "Collection arguments must have allow multiple");
                Debug.Assert (!this.Unique || this.IsCollection, "Unique only applicable to collection arguments");
                Debug.Assert (IsValidElementType (this.Type) ||
                              IsCollectionType (this.Type));
                Debug.Assert ((this.IsCollection && IsValidElementType (this.elementType)) ||
                              (!this.IsCollection && this.elementType == null));
                Debug.Assert (!(this.IsRequired && this.HasDefaultValue), "Required arguments cannot have default value");
                Debug.Assert (!this.HasDefaultValue || (this.defaultValue.GetType () == field.FieldType),
                              "Type of default value must match field type");
            }

            public Type ValueType {
                get { return this.IsCollection ? this.elementType : this.Type; }
            }

            public string LongName {
                get { return this.longName; }
            }

            public bool ExplicitShortName {
                get { return this.explicitShortName; }
            }

            public string ShortName {
                get { return this.shortName; }
            }

            public bool HasShortName {
                get { return this.shortName != null; }
            }

            public bool HasHelpText {
                get { return this.hasHelpText; }
            }

            public string HelpText {
                get { return this.helpText; }
            }

            public object DefaultValue {
                get { return this.defaultValue; }
            }

            public bool HasDefaultValue {
                get { return null != this.defaultValue; }
            }

            public string FullHelpText {
                get {
                    var builder = new StringBuilder ();
                    if (this.HasHelpText)
                        builder.Append (this.HelpText);
                    if (this.HasDefaultValue) {
                        if (builder.Length > 0)
                            builder.Append (" ");
                        builder.Append ("Default value: '");
                        this.AppendValue (builder, this.DefaultValue);
                        builder.Append ('\'');
                    }
                    if (this.HasShortName) {
                        if (builder.Length > 0)
                            builder.Append (" ");
                        builder.Append ("(Short form: /");
                        builder.Append (this.ShortName);
                        builder.Append (")");
                    }
                    return builder.ToString ();
                }
            }

            public string SyntaxHelp {
                get {
                    var builder = new StringBuilder ();

                    if (this.IsDefault) {
                        builder.Append ("<");
                        builder.Append (this.LongName);
                        builder.Append (">");
                    }
                    else {
                        if (!this.IsRequired)
                            builder.Append ("[");

                        builder.Append ("/");
                        builder.Append (this.LongName);
                        var valueType = this.ValueType;
                        if (valueType == typeof (int))
                            builder.Append (":<int>");
                        else if (valueType == typeof (uint))
                            builder.Append (":<uint>");
                        else if (valueType == typeof (bool))
                            builder.Append ("[+|-]");
                        else if (valueType == typeof (string))
                            builder.Append (":<string>");
                        else {
                            Debug.Assert (valueType.IsEnum);

                            builder.Append (":{");
                            var first = true;
                            foreach (var fd in valueType.GetFields ()) {
                                if (fd.IsStatic) {
                                    if (first)
                                        first = false;
                                    else
                                        builder.Append ('|');
                                    builder.Append (fd.Name);
                                }
                            }
                            builder.Append ('}');
                        }

                        if (!this.IsRequired)
                            builder.Append ("]");
                    }

                    if (this.AllowMultiple)
                        builder.Append (this.IsRequired ? "+" : "*");

                    return builder.ToString ();
                }
            }

            public bool IsRequired {
                get { return 0 != (this.flags & ArgumentType.Required); }
            }

            public bool SeenValue { get; private set; }

            public bool AllowMultiple {
                get { return 0 != (this.flags & ArgumentType.Multiple); }
            }

            public bool Unique {
                get { return 0 != (this.flags & ArgumentType.Unique); }
            }

            public Type Type {
                get { return this.field.FieldType; }
            }

            public bool IsCollection {
                get { return IsCollectionType (this.Type); }
            }

            public bool IsDefault {
                get { return this.isDefault; }
            }

            public bool Finish (object destination) {
                if (!this.SeenValue && this.HasDefaultValue)
                    this.field.SetValue (destination, this.DefaultValue);
                if (this.IsCollection)
                    this.field.SetValue (destination, this.collectionValues.ToArray (this.elementType));

                return this.ReportMissingRequiredArgument ();
            }

            private bool ReportMissingRequiredArgument () {
                if (this.IsRequired && !this.SeenValue) {
                    this.reporter (this.IsDefault
                                       ? string.Format ("Missing required argument '<{0}>'.", this.LongName)
                                       : string.Format ("Missing required argument '/{0}'.", this.LongName));
                    return true;
                }
                return false;
            }

            private void ReportDuplicateArgumentValue (string value) {
                this.reporter (string.Format ("Duplicate '{0}' argument '{1}'", this.LongName, value));
            }

            public bool SetValue (string value, object destination) {
                if (this.SeenValue && !this.AllowMultiple) {
                    this.reporter (string.Format ("Duplicate '{0}' argument", this.LongName));
                    return false;
                }
                this.SeenValue = true;

                object newValue;
                if (!this.ParseValue (this.ValueType, value, out newValue))
                    return false;
                if (this.IsCollection) {
                    if (this.Unique && this.collectionValues.Contains (newValue)) {
                        this.ReportDuplicateArgumentValue (value);
                        return false;
                    }
                    this.collectionValues.Add (newValue);
                }
                else
                    this.field.SetValue (destination, newValue);

                return true;
            }

            private void ReportBadArgumentValue (string value) {
                this.reporter (string.Format ("'{0}' is not a valid value for the '{1}' command line option",
                                              value,
                                              this.LongName));
            }

            [SuppressMessage ("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
            private bool ParseValue (Type type, string stringData, out object value) {
                if ((stringData != null || type == typeof (bool)) && (stringData == null || stringData.Length > 0)) {
                    try {
                        if (type == typeof (string)) {
                            value = stringData;
                            return true;
                        }
                        if (type == typeof (bool)) {
                            switch (stringData) {
                                case "+" :
                                case null :
                                    value = true;
                                    return true;
                                case "-" :
                                    value = false;
                                    return true;
                            }
                        }
                        else if (type == typeof (int)) {
                            value = int.Parse (stringData);
                            return true;
                        }
                        else if (type == typeof (uint)) {
                            value = uint.Parse (stringData);
                            return true;
                        }
                        else {
                            Debug.Assert (type.IsEnum);
                            value = Enum.Parse (type, stringData, true);
                            return true;
                        }
                    }
                    catch {}
                }

                this.ReportBadArgumentValue (stringData);
                value = null;
                return false;
            }

            private void AppendValue (StringBuilder builder, object value) {
                if (value is string || value is int || value is uint || value.GetType ().IsEnum)
                    builder.Append (value.ToString ());
                else if (value is bool)
                    builder.Append ((bool) value ? "+" : "-");
                else {
                    var first = true;
                    foreach (var o in (Array) value) {
                        if (!first)
                            builder.Append (", ");
                        this.AppendValue (builder, o);
                        first = false;
                    }
                }
            }

            public void ClearShortName () {
                this.shortName = null;
            }
        }

        #endregion
    }
}
