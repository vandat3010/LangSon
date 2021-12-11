using Namek.Resources.MultiLanguage;
using Foolproof;

namespace Namek.Core.LocalizedValidationAttributes
{
    public class LocalizedRequiredIfNotAttribute : Foolproof.RequiredIfNotAttribute
    {
        public LocalizedRequiredIfNotAttribute(string dependentProperty, object dependentValue)
           : base(dependentProperty, dependentValue)
        {
            Register.Attribute(typeof(LocalizedRequiredIfNotAttribute));
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
