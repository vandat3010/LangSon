using Namek.Admin.AttributeCustom;
using Namek.Admin.CustomFilters;
using Namek.Core.ActionResult;
using Namek.Core.Utility;
using Namek.Entity.EntityModel;
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
    public class DistrictController : BaseController
    {
        // GET: District
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.District)]
        public async Task<ActionResult> Index(DistrictRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 100;
            var result = await ApiService.GroupPageService.GetDistrict(model);
            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / result.PageSize + 1;
            ViewBag.Data = result.Data ?? new List<District>(); ;

            return View(model);
        }

        [LogTracker(Action.Add, PageId.District)]
        public async Task<ActionResult> Add()
        {
            return View("Edit", new District());
        }

        [LogTracker(Action.Edit, PageId.District)]
        public async Task<ActionResult> Edit(int id)
        {
            District item = await ApiService.GroupPageService.GetDistrictById(id);

            return View("Edit", item);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.District)]
        public async Task<ActionResult> Update(District model)
        {
            var result = await ApiService.GroupPageService.UpdateDistrict(model);
            var msg = "";
            if (result == 0)
            {
                return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ServiceManagementUpdateFailed) }, JsonRequestBehavior.AllowGet);
            }
            if(model.Id==0)
            {
                //thêm mới
                return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thành công" }, JsonRequestBehavior.AllowGet);
            }    
            return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceUpdatedSuccessfully) }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.District)]
        public async Task<ActionResult> AddItem(District model)
        {
            var result = await ApiService.GroupPageService.UpdateDistrict(model);
            var msg = "";
            if (result == 0)
            {
                return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ServiceManagementUpdateFailed) }, JsonRequestBehavior.AllowGet);
            }
            return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thành công" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Delete, PageId.District)]
        public async Task<ActionResult> DeleteDistrict(District model)
        {
            try
            {
                var result = await ApiService.GroupPageService.DeleteDistrict(model);
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
    }
}