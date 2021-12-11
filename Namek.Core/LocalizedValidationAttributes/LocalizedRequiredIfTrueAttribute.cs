using Namek.Resources.MultiLanguage;
using Foolproof;

namespace Namek.Core.LocalizedValidationAttributes
{
    public class LocalizedRequiredIfTrueAttribute : Foolproof.RequiredIfTrueAttribute
    {
        public LocalizedRequiredIfTrueAttribute(string dependentProperty)
           : base(dependentProperty)
        {
            Register.Attribute(typeof(LocalizedRequiredIfTrueAttribute));
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
