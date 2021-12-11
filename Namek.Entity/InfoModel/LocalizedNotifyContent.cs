using System.Collections.Generic;

namespace Namek.Entity.InfoModel
{
    public class LocalizedNotifyContent
    {
        public string Key { get; set; }
        public string ResourceKey { get; set; }
        public Dictionary<int, string> Data { get; set; }
    }
}
