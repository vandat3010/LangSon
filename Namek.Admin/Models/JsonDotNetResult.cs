using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Namek.Admin.Models
{
    public class JsonDotNetResult : JsonResult
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            //DateFormatString = "dd/MM/yyyy"
            //Converters = new List<JsonConverter> { new StringEnumConverter() }
        };

        public override void ExecuteResult(ControllerContext context)
        {
            //if (this.JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
            //    string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            //{
            //    throw new InvalidOperationException("GET request not allowed");
            //}

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;

            if (Data == null)
                return;

            response.Write(JsonConvert.SerializeObject(Data, Settings));
        }
    }
}