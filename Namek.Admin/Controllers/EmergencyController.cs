using Namek.Admin.AttributeCustom;
using Namek.Admin.CustomFilters;
using Namek.Core.ActionResult;
using Namek.Core.Utility;
using Namek.Entity.InfoModel;
using Namek.Entity.RequestModel;
using Namek.Library.Enums;
using Namek.Resources.MultiLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Action = Namek.Library.Enums.Action;

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class EmergencyController : BaseController
    {
        // GET: Emergency
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.Emergency)]
        public async Task<ActionResult> Index(GroupPageRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 100;

            model.ActionName = SelectListItemExtension.GetEnums<ViewEnum>().Where(x => x.Id == (int)ViewEnum.Emergency).FirstOrDefault().Name;

            var result = await ApiService.GroupPageService.Get(model);
            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / result.PageSize + 1;
            ViewBag.Data = result.Data;
            var lstDistrict = ApiService.GroupPageService.GetAllDistrict();
            model.lstHuyen = lstDistrict.ConvertAll(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name.ToString(),
            });
            return View(model);
        }

        [LogTracker(Action.Edit, PageId.Emergency)]
        public async Task<ActionResult> Edit(int id)
        {
            GroupPageInfo item = await ApiService.GroupPageService.GetInfo(id);
            var lstDistrict = ApiService.GroupPageService.GetAllDistrict();
            item.lstHuyen = lstDistrict.ConvertAll(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name.ToString(),
            });
            return View("Edit", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Delete, PageId.Emergency)]
        public async Task<ActionResult> DeleteEmergency(GroupPageInfo model)
        {
            try
            {
                var result = await ApiService.GroupPageService.Delete(model);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.SystemResourcesSuccessfullyDeleted) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetLatLngByDistrictId(int id)
        {
            var rs = await ApiService.GroupPageService.GetLatLngByDistrictId(id);
            return Json(rs, JsonRequestBehavior.AllowGet);
        }
    }
}