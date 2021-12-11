﻿using Namek.Resources.MultiLanguage;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Namek.Core.LocalizedValidationAttributes
{
    public class LocalizedRegularExpressionAttribute : System.ComponentModel.DataAnnotations.RegularExpressionAttribute, IClientValidatable
    {
        private string _displayName;
        private string _pattern;

        public LocalizedRegularExpressionAttribute(string pattern) : base(pattern)
        {
            _pattern = pattern;
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
                    return string.Format(msg, string.IsNullOrEmpty(displayName) ? _displayName : displayName);
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
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = ErrorMessageTranslate(string.IsNullOrEmpty(metadata.DisplayName) ? metadata.PropertyName : metadata.DisplayName),
                ValidationType = "regex"
            };

            rule.ValidationParameters.Add("pattern", _pattern);

            return new[] {
                rule
            };
        }
    }
}
