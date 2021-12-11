using Namek.Resources.MultiLanguage;
using System.Web.Mvc;
using Namek.Admin.AttributeCustom;
using Namek.Admin.CustomFilters;
using Namek.Library.Enums;

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class ErrorController : Controller
    {
        // GET: Error
        [LogTracker(Action.View, PageId.NoCheckPermisson)]
        public ActionResult Index(string permission, string pages)
        {
            ViewBag.Permission = permission;
            ViewBag.Pages = pages;

            return View();
        }

        [LogTracker(Action.View, PageId.NoCheckPermisson)]
        public ActionResult NotFound()
        {
            return View();
        }

        [LogTracker(Action.View, PageId.NoCheckPermisson)]
        public ActionResult Error()
        {
            return View();
        }
    }
}