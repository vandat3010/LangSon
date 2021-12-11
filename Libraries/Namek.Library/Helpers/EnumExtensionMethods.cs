using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Namek.Library.Helpers
{
    public static class EnumExtensionMethods
    {
        public static string GetDisplayName(this Enum value)
        {
            try
            {
                var type = value.GetType();
                if (!type.IsEnum) throw new ArgumentException(string.Format("Type '{0}' is not Enum", type));

                var members = type.GetMember(value.ToString());
                if (members.Length == 0)
                    throw new ArgumentException(string.Format("Member '{0}' not found in type '{1}'", value,
                        type.Name));

                var member = members[0];
                var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attributes.Length == 0)
                    throw new ArgumentException(string.Format("'{0}.{1}' doesn't have DisplayAttribute", type.Name,
                        value));

                var attribute = (DisplayAttribute) attributes[0];

                var temp = attribute.GetName();

                if (string.IsNullOrEmpty(temp)) temp = value.ToString();

                return temp;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string GetDescription(this Enum value)
        {
            try
            {
                var fi = value.GetType().GetField(value.ToString());

                var attributes =
                    (DescriptionAttribute[]) fi.GetCustomAttributes(
                        typeof(DescriptionAttribute),
                        false);

                if (attributes.Length > 0)
                    return attributes[0].Description;
                return value.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}