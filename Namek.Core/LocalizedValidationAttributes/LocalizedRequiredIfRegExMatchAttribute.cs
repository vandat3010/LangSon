﻿using Namek.Resources.MultiLanguage;
using Foolproof;

namespace Namek.Core.LocalizedValidationAttributes
{
    public class LocalizedRequiredIfRegExMatchAttribute : Foolproof.RequiredIfRegExMatchAttribute
    {
        public LocalizedRequiredIfRegExMatchAttribute(string dependentProperty, string pattern)
           : base(dependentProperty, pattern)
        {
            Register.Attribute(typeof(LocalizedRequiredIfRegExMatchAttribute));
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
