using Namek.Entity.RequestModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Namek.Admin.Models
{
    public class ResourceRequestModel : BaseModel
    {
        [AllowHtml]
        public string Keywords { get; set; }
        public bool ExactlySearch { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
        public string ValueEn { get; set; }
        public string Description { get; set; }
        public override object RouteValues
        {
            get
            {
                return new
                {
                    this.Keywords,
                    this.ExactlySearch
                };
            }
        }
    }
}