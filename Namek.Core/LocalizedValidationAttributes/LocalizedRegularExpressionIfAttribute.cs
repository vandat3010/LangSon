using Namek.Resources.MultiLanguage;
using Foolproof;

namespace Namek.Core.LocalizedValidationAttributes
{
    public class LocalizedRegularExpressionIfAttribute : Foolproof.RegularExpressionIfAttribute
    {
        public LocalizedRegularExpressionIfAttribute(string pattern, string dependentProperty, object dependentValue)
            : base(pattern, dependentProperty, dependentValue)
        {
            Register.Attribute(typeof(LocalizedRegularExpressionIfAttribute));
        }

        public LocalizedRegularExpressionIfAttribute(string pattern, string dependentProperty, Operator @operator, object dependentValue)
            : base(pattern, dependentProperty, @operator, dependentValue)
        {
            Register.Attribute(typeof(LocalizedRegularExpressionIfAttribute));
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessageTranslate(name);
        }

        private string ErrorMessageTranslate(string displayName = null)
        {
            var msg = LanguageHelper.GetValidation(ErrorMessage);
            try
            {
                if (string.Concat(msg, string.Empty).IndexOf("{0}") > -1)
                {
                    return string.Format(msg, displayName);
                }
                else
                {
                    return msg;
                }
            }
            catch
            {
                return msg;
            }
        }

    }
}
