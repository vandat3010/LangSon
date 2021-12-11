using Namek.Resources.MultiLanguage;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Namek.Core.LocalizedValidationAttributes
{
    public class LocalizedCompareAttribute : System.ComponentModel.DataAnnotations.CompareAttribute, IClientValidatable
    {
        private string _displayName;
        private string _otherProperty;

        public LocalizedCompareAttribute(string otherProperty) : base(otherProperty)
        {
            _otherProperty = otherProperty;
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

        private string ErrorMessageTranslate(string displayName = null, string otherPropertyDisplayName = null)
        {
            var msg = LanguageHelper.GetValidation(ErrorMessage);
            try
            {
                if (string.Concat(msg, string.Empty).IndexOf("{0}") > -1 && string.Concat(msg, string.Empty).IndexOf("{1}") > -1)
                {
                    return string.Format(msg, string.IsNullOrEmpty(displayName) ? _displayName : displayName, string.IsNullOrEmpty(otherPropertyDisplayName) ? OtherPropertyDisplayName : otherPropertyDisplayName);
                }
                else
                {
                    if (string.Concat(msg, string.Empty).IndexOf("{0}") > -1)
                    {
                        return string.Format(msg, string.IsNullOrEmpty(displayName) ? _displayName : displayName);
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
            var parentType = metadata.ContainerType;
            var parentMetaData = ModelMetadataProviders.Current.GetMetadataForProperties(context.Controller.ViewData.Model, parentType);
            var otherProperty = parentMetaData.FirstOrDefault(p => p.PropertyName == _otherProperty);
            var otherPropertyDisplayName = _otherProperty;

            if (otherProperty != null)
            {
                otherPropertyDisplayName = string.IsNullOrEmpty(otherProperty.DisplayName) ? otherProperty.PropertyName : otherProperty.DisplayName;
            }

            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageTranslate(string.IsNullOrEmpty(metadata.DisplayName) ? metadata.PropertyName : metadata.DisplayName, otherPropertyDisplayName),
                ValidationType = "equalto"
            };

            rule.ValidationParameters.Add("other", string.Concat("*.", _otherProperty));

            return new[] {
                rule
            };
        }
    }
}
