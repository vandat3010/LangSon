using Namek.Resources.MultiLanguage;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Namek.Core.LocalizedValidationAttributes
{
    public class LocalizedMaxLengthAttribute : System.ComponentModel.DataAnnotations.MaxLengthAttribute, IClientValidatable
    {
        private int _maximumLength;
        private string _displayName;

        public LocalizedMaxLengthAttribute(int maximumLength) : base(maximumLength)
        {
            _maximumLength = maximumLength;
        }

        protected override ValidationResult IsValid
       (object value, ValidationContext validationContext)
        {
            _displayName = validationContext.DisplayName;
            return base.IsValid(value, validationContext);
        }

        public override string FormatErrorMessage(string name)
        {
            return ErrorMessageTranslate();
        }

        private string ErrorMessageTranslate(string displayName = null)
        {
            var msg = LanguageHelper.GetValidation(ErrorMessage);
            try
            {
                if (string.Concat(msg, string.Empty).IndexOf("{0}") > -1 && string.Concat(msg, string.Empty).IndexOf("{1}") > -1)
                {
                    return string.Format(msg, string.IsNullOrEmpty(displayName) ? _displayName : displayName, _maximumLength);
                }
                else
                {
                    if (string.Concat(msg, string.Empty).IndexOf("{0}") > -1)
                    {
                        return string.Format(msg, _maximumLength);
                    }
                    else
                    {
                        return msg;
                    }
                }
            }
            catch
            {
                return msg;
            }

        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageTranslate(string.IsNullOrEmpty(metadata.DisplayName)? metadata.PropertyName: metadata.DisplayName),
                ValidationType = "maxlength",
            };
            rule.ValidationParameters.Add("max", _maximumLength);
            return new[] {
                rule
            };
        }

    }
}
