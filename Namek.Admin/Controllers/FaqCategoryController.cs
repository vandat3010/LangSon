using Namek.Admin.AttributeCustom;
using Namek.Admin.CustomFilters;
using Namek.Admin.Models;
using Namek.Core.ActionResult;
using Namek.Core.Utility;
using Namek.Entity.EntityModel;
using Namek.Entity.InfoModel;
using Namek.Entity.RequestModel;
using Namek.Library.Enums;
using Namek.Library.Infrastructure;
using Namek.Resources.MultiLanguage;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Action = Namek.Library.Enums.Action;
using System.Linq;

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class FaqCategoryController : BaseController
    {
        // GET: Service
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.FaqCategory)]
        public async Task<ActionResult> Index(FaqCategoryRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10;
            var result = await ApiService.FaqCategoryService.GetAll();
            if (result != null && !string.IsNullOrEmpty(model.Keywords))
            {
                result = result.Where(t => t.Name.Contains(model.Keywords)).ToList();
            }
            result = result.OrderBy(t => t.Sequence).ToList();
            model.TotalRecords = result.Count;
            model.TotalPages = result.Count / model.PageSize + 1;
            ViewBag.Data = result;
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            return View(model);
        }

        [LogTracker(Action.Edit, PageId.FaqCategory)]
        public async Task<ActionResult> Edit(int Id)
        {
            var FaqCategory = await ApiService.FaqCategoryService.GetInfo(Id);
            var FaqCategoryinfo = new FaqCategoryInfo()
            {
                Id = FaqCategory.Id,
                Name = FaqCategory.Name,
                Sequence = FaqCategory.Sequence,
                CreatedDate = FaqCategory.CreatedDate,
                CreatedBy = FaqCategory.CreatedBy,
                ModifyDate = FaqCategory.ModifyDate,
                ModifyBy = FaqCategory.ModifyBy,
                ParentId = FaqCategory.ParentId,
                IsDeleted = FaqCategory.IsDeleted ?? 0,
                Name_en = FaqCategory.Name_en,
                Code = FaqCategory.Code
            };
            ViewBag.LstFaqCategory = await ApiService.FaqCategoryService.GetAll();

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View("Edit", FaqCategoryinfo);
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.FaqCategory)]
        public async Task<ActionResult> Update(FaqCategoryInfo model)
        {
            try
            {

                var FaqCategory = await ApiService.FaqCategoryService.GetInfo(model.Id);

                var newInfo = new FaqCategoryInfo()
                {
                    Id = FaqCategory.Id,
                    Name = model.Name,
                    Sequence = model.Sequence,
                    CreatedDate = FaqCategory.CreatedDate,
                    CreatedBy = FaqCategory.CreatedBy,
                    ModifyDate = DateTime.Now,
                    ModifyBy = CurrentUser.Id,
                    ParentId = model.ParentId,
                    IsDeleted = 0,
                    ServiceId = model.ServiceId,
                    Name_en = model.Name_en,
                    Code = model.Code
                };


                var result = await ApiService.FaqCategoryService.Update(newInfo);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ServiceManagementUpdateFailed) }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceUpdatedSuccessfully) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }


        [LogTracker(Action.Add, PageId.FaqCategory)]
        public async Task<ActionResult> Add()
        {
            FaqCategoryInfo item = new FaqCategoryInfo();
            FaqCategoryRequestModel model = new FaqCategoryRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;
            ViewBag.LstFaqCategory = await ApiService.FaqCategoryService.GetAll();

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View("Edit", item);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.FaqCategory)]
        public async Task<ActionResult> Add(FaqCategoryInfo model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }
                model.CreatedBy = CurrentUser.Id;
                model.CreatedDate = DateTime.Now;

                var newInfo = new FaqCategoryInfo()
                {

                    Name = model.Name,
                    Sequence = model.Sequence,
                    CreatedDate = DateTime.Now,
                    CreatedBy = CurrentUser.Id,
                    ModifyDate = DateTime.Now,
                    ModifyBy = CurrentUser.Id,
                    ParentId = model.ParentId,
                    IsDeleted = 0,
                    ServiceId = model.ServiceId,
                    Name_en = model.Name_en,
                    Code = model.Code
                };


                var result = await ApiService.FaqCategoryService.Add(newInfo);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thất bại" }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Delete, PageId.FaqCategory)]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {

                FaqCategoryInfo FaqCategoryInfo = new FaqCategoryInfo();
                FaqCategoryInfo.Id = Id;
                var result = await ApiService.FaqCategoryService.Delete(FaqCategoryInfo);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }
         
    }
}