using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Namek.Library.Helpers
{
    public class JsonSerialize
    {
        private readonly StringBuilder _output = new StringBuilder();

        public static string ToJSONDate(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ToJSON(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JavaScriptDateTimeConverter());
        }

        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public static object DeserializeObject(string value, string type)
        {
            return JsonConvert.DeserializeObject(value, Type.GetType(type));
        }

        public static object DeserializeObject(string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type);
        }

        private string ConvertToJSON(object obj)
        {
            WriteValue(obj);

            return _output.ToString();
        }

        private void WriteValue(object obj)
        {
            if (obj == null)
                _output.Append("null");
            else if (obj is sbyte || obj is byte || obj is short || obj is ushort || obj is int || obj is uint ||
                     obj is long || obj is ulong || obj is decimal || obj is double || obj is float)
                _output.Append(Convert.ToString(obj, NumberFormatInfo.InvariantInfo));
            else if (obj is bool)
                _output.Append(obj.ToString().ToLower());
            else if (obj is char || obj is Enum || obj is Guid)
                WriteString("" + obj);
            else if (obj is DateTime)
                _output.Append("new Date('" + ((DateTime) obj).ToString("dd MMMM yyyy HH:mm:ss") + "')");
            //_output.Append("new Date(" + ((DateTime)obj - new DateTime(1970, 1, 1)).TotalMilliseconds.ToString("0") + ")");
            else if (obj is string)
                WriteString((string) obj);
            else if (obj is IDictionary)
                WriteDictionary((IDictionary) obj);
            else if (obj is Array || obj is IList || obj is ICollection)
                WriteArray((IEnumerable) obj);
            else
                WriteObject(obj);
        }

        private void WriteObject(object obj)
        {
            _output.Append("{ ");

            var pendingSeparator = false;

            foreach (var field in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                if (pendingSeparator)
                    _output.Append(" , ");

                WritePair(field.Name, field.GetValue(obj));

                pendingSeparator = true;
            }

            foreach (var property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Length > 0)
                    continue;

                var name = property.Name;

                if (property.GetCustomAttributes(typeof(JsonFriendlyNameAttribute), true).Length > 0)
                {
                    var friendlyName =
                        (JsonFriendlyNameAttribute)
                        property.GetCustomAttributes(typeof(JsonFriendlyNameAttribute), true)[0];
                    name = friendlyName.Name;
                }

                if (!property.CanRead)
                    continue;

                if (pendingSeparator)
                    _output.Append(" , ");

                WritePair(name, property.GetValue(obj, null));

                pendingSeparator = true;
            }

            _output.Append(" }");
        }

        private void WritePair(string name, object value)
        {
            WriteString(name);

            _output.Append(" : ");

            WriteValue(value);
        }

        private void WriteArray(IEnumerable array)
        {
            _output.Append("[ ");

            var pendingSeperator = false;

            foreach (var obj in array)
            {
                if (pendingSeperator)
                    _output.Append(',');

                WriteValue(obj);

                pendingSeperator = true;
            }

            _output.Append(" ]");
        }

        private void WriteDictionary(IDictionary dic)
        {
            _output.Append("{ ");

            var pendingSeparator = false;

            foreach (DictionaryEntry entry in dic)
            {
                if (pendingSeparator)
                    _output.Append(" , ");

                WritePair(entry.Key.ToString(), entry.Value);

                pendingSeparator = true;
            }

            _output.Append(" }");
        }

        private void WriteString(string s)
        {
            _output.Append('\"');

            foreach (var c in s)
                switch (c)
                {
                    case '\t':
                        _output.Append("\\t");
                        break;
                    case '\r':
                        _output.Append("\\r");
                        break;
                    case '\n':
                        _output.Append("\\n");
                        break;
                    case '"':
                    case '\\':
                        _output.Append("\\" + c);
                        break;
                    default:
                    {
                        if (c >= ' ' && c < 128)
                            _output.Append(c);
                        else
                            _output.Append("\\u" + ((int) c).ToString("X4"));
                    }
                        break;
                }

            _output.Append('\"');
        }
    }
}