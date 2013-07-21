var xsd = new ActiveXObject ("MSXML2.XMLSchemaCache.6.0");
xsd.add ("", "schema.xsd");

var xml = new ActiveXObject ("MSXML2.DOMDocument.6.0");
xml.schemas = xsd;
xml.async = false;
xml.validateOnParse = true;
xml.resolveExternals = true;
xml.load (WScript.Arguments (0));

var err = xml.parseError;
if (err.errorCode != 0)
  WScript.Echo ("Validation failed." +
                "\nReason: " + err.reason +
                "\nSource: " + err.srcText +
                "\nLine: " + err.line + "\n");
else
  WScript.Echo ("OK");
