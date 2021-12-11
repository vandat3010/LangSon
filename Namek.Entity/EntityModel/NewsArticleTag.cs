using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namek.Entity.EntityModel
{
    public class NewsArticleTag
    { 
        public int Id { get; set; } 
        public int NewsArticleId { get; set; } 
        public int TagId { get; set; } 
        public int DisplayOrder { get; set; }
    }
}
