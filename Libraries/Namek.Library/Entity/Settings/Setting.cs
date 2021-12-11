using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heliosys.Ecommerce.Core.Data;

namespace Heliosys.Ecommerce.Core.Entity
{
    public class Setting : BaseEntity
    {
        public string GroupName { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }

}
