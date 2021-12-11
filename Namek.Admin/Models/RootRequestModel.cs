using Namek.Admin.Models;
using System.Web.Mvc;

namespace Namek.Entity.RequestModel
{
    public class RootRequestModel : BaseModel
    {
        [AllowHtml]
        public int Count { get; set; }

        public int Page { get; set; }

        public bool? Ascending { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public int? CategoryId { get; set; }
    }
}