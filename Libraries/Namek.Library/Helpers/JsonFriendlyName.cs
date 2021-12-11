using System;

namespace Namek.Library.Helpers
{
    public class JsonFriendlyNameAttribute : Attribute
    {
        public JsonFriendlyNameAttribute() { }

        public JsonFriendlyNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}