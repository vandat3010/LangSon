using System.ComponentModel;
using Namek.Resources.MultiLanguage; 
namespace Namek.Core.LocalizedAttributes
{
    public class LocalizedEnumDescriptionAttribute : DescriptionAttribute
    {
        private readonly string _resourceKey;
        public LocalizedEnumDescriptionAttribute(string resourceKey)
        {
            _resourceKey = resourceKey;
        }

        public override string Description
        {
            get
            {
                var displayName = LanguageHelper.GetEnum(_resourceKey);

                return string.IsNullOrEmpty(displayName) ? $"[[{_resourceKey}]]" : displayName;
            }
        }
    }
}
