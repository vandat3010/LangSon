using Namek.Admin.AttributeCustom;
using Namek.Admin.CustomFilters;
using Namek.Admin.Models.Home;
using Namek.Core;
using Namek.Entity.EntityModel;
using Namek.Entity.RequestModel;
using Namek.Library.Enums;
using Namek.Library.Infrastructure;
using Namek.Resources.MultiLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Action = Namek.Library.Enums.Action;

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class HomeController : BaseController
    {
        [LogTracker(Action.View, PageId.NoCheckPermisson)]
        public ActionResult Index()
        {
            var model = new IndexModel();

            var theDateWillBeExpire = DateTime.Now.AddDays(Config.NumberOfDaysBeforeExpiration).Date;
          
            ViewBag.TheDateWillBeExpire = theDateWillBeExpire;
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult SetCurrentDisplayLanguage(string displayLanguage)
        {
            // Validate input
            displayLanguage = CultureHelper.GetImplementedCulture(displayLanguage);
            // Save culture in a cookie
            var cookie = Request.Cookies["_displayLanguage"];
            if (cookie != null)
                cookie.Value = displayLanguage;
            else
            {
                cookie = new HttpCookie("_displayLanguage")
                {
                    Value = displayLanguage,
                    Expires = DateTime.Now.AddYears(1)
                };
            }
            Response.Cookies.Add(cookie);
            var resource = ResourceHelper.GetListResource(displayLanguage);
            return Json(new { Resource = resource }, JsonRequestBehavior.AllowGet);
        }

      
    }
}