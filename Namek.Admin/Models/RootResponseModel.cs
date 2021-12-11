using System.Net;

namespace Namek.Admin.Models
{
    public class RootResponseModel
    {
        public bool Success { get; set; }

        public string ErrorMessages { get; set; }

        public string Messages { get; set; }

        public dynamic ResponseData { get; set; }

        public int Total { get; set; }

        public HttpStatusCode Code { get; set; }

        public string Url { get; set; }
    }
}