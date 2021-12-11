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
    public class LegislationCategoryController : BaseController
    {
        // GET: Service
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.LegislationCategory)]
        public async Task<ActionResult> Index(LegislationCategoryRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10;
            var result = await ApiService.LegislationCategoryService.GetAll();
            if (result != null && !string.IsNullOrEmpty(model.Keywords))
            {
                result = result.Where(t => t.Name.Contains(model.Keywords)).ToList();
            }
            model.TotalRecords = result.Count;
            model.TotalPages = result.Count / model.PageSize + 1;
            ViewBag.Data = result;
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            return View(model);
        }

        [LogTracker(Action.Edit, PageId.LegislationCategory)]
        public async Task<ActionResult> Edit(int Id)
        {
            var LegislationCategory = await ApiService.LegislationCategoryService.GetInfo(Id);
            var LegislationCategoryinfo = new LegislationCategoryInfo()
            {
                Id = LegislationCategory.Id,
                Name = LegislationCategory.Name,
                Code = LegislationCategory.Code,
                Sequence = LegislationCategory.Sequence,
                CreatedDate = LegislationCategory.CreatedDate,
                CreatedBy = LegislationCategory.CreatedBy,
                ModifyDate = LegislationCategory.ModifyDate,
                ModifyBy = LegislationCategory.ModifyBy,
                ParentId = LegislationCategory.ParentId,
                IsDeleted = LegislationCategory.IsDeleted ?? 0,
                Name_en = LegislationCategory.Name_en
            };
            ViewBag.LstLegislationCategory = await ApiService.LegislationCategoryService.GetAll();

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View("Edit", LegislationCategoryinfo);
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.LegislationCategory)]
        public async Task<ActionResult> Update(LegislationCategoryInfo model)
        {
            try
            {

                var LegislationCategory = await ApiService.LegislationCategoryService.GetInfo(model.Id);

                var newInfo = new LegislationCategoryInfo()
                {
                    Id = LegislationCategory.Id,
                    Code = model.Code,
                    Name = model.Name,
                    Sequence = model.Sequence,
                    CreatedDate = LegislationCategory.CreatedDate,
                    CreatedBy = LegislationCategory.CreatedBy,
                    ModifyDate = DateTime.Now,
                    ModifyBy = CurrentUser.Id,
                    ParentId = model.ParentId,
                    IsDeleted = 0,
                    ServiceId = model.ServiceId,
                    Name_en = model.Name_en
                };


                var result = await ApiService.LegislationCategoryService.Update(newInfo);
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


        [LogTracker(Action.Add, PageId.LegislationCategory)]
        public async Task<ActionResult> Add()
        {
            LegislationCategoryInfo item = new LegislationCategoryInfo();
            LegislationCategoryRequestModel model = new LegislationCategoryRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;
            ViewBag.LstLegislationCategory = await ApiService.LegislationCategoryService.GetAll();

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View("Edit", item);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.LegislationCategory)]
        public async Task<ActionResult> Add(LegislationCategoryInfo model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }
                //if (string.IsNullOrWhiteSpace(model.Code) || string.IsNullOrWhiteSpace(model.Code))
                //{
                //    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập mã" }, JsonRequestBehavior.AllowGet);
                //}
                model.CreatedBy = CurrentUser.Id;
                model.CreatedDate = DateTime.Now;

                var newInfo = new LegislationCategoryInfo()
                {
                    Code = model.Code,
                    Name = model.Name,
                    Sequence = model.Sequence,
                    CreatedDate = DateTime.Now,
                    CreatedBy = CurrentUser.Id,
                    ModifyDate = DateTime.Now,
                    ModifyBy = CurrentUser.Id,
                    ParentId = model.ParentId,
                    IsDeleted = 0,
                    ServiceId = model.ServiceId,
                    Name_en = model.Name_en
                };


                var result = await ApiService.LegislationCategoryService.Add(newInfo);
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
        [LogTracker(Action.Delete, PageId.LegislationCategory)]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {

                LegislationCategoryInfo LegislationCategoryInfo = new LegislationCategoryInfo();
                LegislationCategoryInfo.Id = Id;
                var rs = await ApiService.LegislationService.GetLegislationByLegislationCategoryId(Id);
                var msg = "";
                if (rs == null)
                {
                    var result = await ApiService.LegislationCategoryService.Delete(LegislationCategoryInfo);
                    var msg1 = "";
                    if (result == 0)
                    {
                        return new JsonCamelCaseResult(new { status = result, msg = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
                    }
                    return new JsonCamelCaseResult(new { status = result, msg = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = 0, msg = "Loại văn bản pháp luật đã chứa văn bản pháp luật bên trong không thể xóa được" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }
        //public async Task<ActionResult> Delete(int Id)
        //{
        //    try
        //    {

        //        LegislationCategoryInfo LegislationCategoryInfo = new LegislationCategoryInfo();
        //        LegislationCategoryInfo.Id = Id;
        //        var rs = await ApiService.LegislationService.GetLegislationByLegislationCategoryId(Id);
        //        var result = await ApiService.LegislationCategoryService.Delete(LegislationCategoryInfo);
        //        var msg = "";
        //        if (rs == null)
        //        {
        //            return new JsonCamelCaseResult(new { status = result, msg = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
        //        }
        //        return new JsonCamelCaseResult(new { status = result, msg = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch
        //    {
        //        return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
        //    }
        //}


        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.LegislationCategory)]
        public async Task<ActionResult> ListAffiliate(LegislationCategoryRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 50;
            var result = await ApiService.LegislationCategoryService.GetAll();
            if (result != null && !string.IsNullOrEmpty(model.Keywords))
            {
                result = result.Where(t => t.Name.Contains(model.Keywords)).ToList();
            }
            model.TotalRecords = result.Count;
            model.TotalPages = result.Count / model.PageSize + 1;
            ViewBag.Data = result;
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            return View(model);
        }

        [LogTracker(Action.Edit, PageId.LegislationCategory)]
        public async Task<ActionResult> EditAffiliate(int Id = 0)
        {
            var LegislationCategoryinfo = new LegislationCategoryInfo();
            var LegislationCategory = await ApiService.LegislationCategoryService.GetInfo(Id);

            if (LegislationCategory != null)
            {
                LegislationCategoryinfo = new LegislationCategoryInfo()
                {
                    Id = LegislationCategory.Id,
                    Name = LegislationCategory.Name,
                    Code = LegislationCategory.Code,
                    Sequence = LegislationCategory.Sequence,
                    CreatedDate = LegislationCategory.CreatedDate,
                    CreatedBy = LegislationCategory.CreatedBy,
                    ModifyDate = LegislationCategory.ModifyDate,
                    ModifyBy = LegislationCategory.ModifyBy,
                    ParentId = LegislationCategory.ParentId,
                    IsDeleted = LegislationCategory.IsDeleted ?? 0,
                    Name_en = LegislationCategory.Name_en
                };
            }

            ViewBag.LstLegislationCategory = await ApiService.LegislationCategoryService.GetAll();

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View("EditAffiliate", LegislationCategoryinfo);
        }

    }
}