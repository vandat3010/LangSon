using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliosys.Ecommerce.Services.Security
{
    public interface IApplicationConfiguration
    {
        string GetSetting(string settingName);

        void SetSetting(string settingName, string value);
    }
}
