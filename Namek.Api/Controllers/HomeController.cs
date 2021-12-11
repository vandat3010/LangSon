using System.Net;
using System.Web.Mvc;
using Namek.Entity.RequestModel;
using Namek.Infrastructure.Repository;
using Namek.Interface.Repository;
using Namek.Library.Infrastructure;
using Newtonsoft.Json;

namespace Namek.Api.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }


       
    }
}