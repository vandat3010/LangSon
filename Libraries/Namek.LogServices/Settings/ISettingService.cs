using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Entity.EntityModel;
using Automation.Library.Config;
using Automation.Library.Services;
using Heliosys.Ecommerce.Core.Config;
using Heliosys.Ecommerce.Core.Entity;
using Heliosys.Ecommerce.Core.Services;

namespace Heliosys.Ecommerce.Services.Settings
{
    public  interface ISettingService : IBaseEntityService<Setting>
    {
        Setting Get<T>(string keyName) where T : ISettingGroup;

        void Save<T>(string keyName, string keyValue) where T : ISettingGroup;

        void Save<T>(T settings) where T : ISettingGroup;

        T GetSettings<T>() where T : ISettingGroup;

        void LoadSettings<T>(T settingsObject) where T : ISettingGroup;
    }
}
