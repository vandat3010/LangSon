using Namek.Resources.MultiLanguage;

namespace Namek.Core.LocalizedAttributes
{
    public class LocalizedDisplayNameAttribute : System.ComponentModel.DisplayNameAttribute
    {
        private string _displayName;
        public LocalizedDisplayNameAttribute(string displayName)
        {
            _displayName = displayName;
        }

        public override string DisplayName
        {
            get
            {
                return LanguageHelper.GetLabel(_displayName);
            }
        }

    }
}
