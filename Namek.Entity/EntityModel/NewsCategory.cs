using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namek.Entity.EntityModel
{
    public class NewsCategory
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public string Picture { get; set; }
        public bool? IsSystem { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? DisplayOrder { get; set; }
        public string NameEn { get; set; }
        public bool? IsHomePage { get; set; }
    }
}
