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
    public class DocumentCategoryController : BaseController
    {
        // GET: Service
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.DocumentCategory)]
        public async Task<ActionResult> Index(DocumentCategoryRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10;
            var result = await ApiService.DocumentCategoryService.GetAll();
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

        [LogTracker(Action.Edit, PageId.DocumentCategory)]
        public async Task<ActionResult> Edit(int Id)
        {
            var DocumentCategory = await ApiService.DocumentCategoryService.GetInfo(Id);
            var DocumentCategoryinfo = new DocumentCategoryInfo()
            {
                Id = DocumentCategory.Id,
                Name = DocumentCategory.Name,
                Code = DocumentCategory.Code,
                Sequence = DocumentCategory.Sequence,
                CreatedDate = DocumentCategory.CreatedDate,
                CreatedBy = DocumentCategory.CreatedBy,
                ModifyDate = DocumentCategory.ModifyDate,
                ModifyBy = DocumentCategory.ModifyBy,
                ParentId = DocumentCategory.ParentId,
                IsDeleted = DocumentCategory.IsDeleted ?? 0,
                Name_en = DocumentCategory.Name_en
            };
            ViewBag.LstDocumentCategory = await ApiService.DocumentCategoryService.GetAll();

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View("Edit", DocumentCategoryinfo);
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.DocumentCategory)]
        public async Task<ActionResult> Update(DocumentCategoryInfo model)
        {
            try
            {

                var DocumentCategory = await ApiService.DocumentCategoryService.GetInfo(model.Id);

                var newInfo = new DocumentCategoryInfo()
                {
                    Id = DocumentCategory.Id,
                    Code = model.Code,
                    Name = model.Name,
                    Sequence = model.Sequence,
                    CreatedDate = DocumentCategory.CreatedDate,
                    CreatedBy = DocumentCategory.CreatedBy,
                    ModifyDate = DateTime.Now,
                    ModifyBy = CurrentUser.Id,
                    ParentId = model.ParentId,
                    IsDeleted = 0,
                    ServiceId = model.ServiceId,
                    Name_en = model.Name_en
                };


                var result = await ApiService.DocumentCategoryService.Update(newInfo);
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


        [LogTracker(Action.Add, PageId.DocumentCategory)]
        public async Task<ActionResult> Add()
        {
            DocumentCategoryInfo item = new DocumentCategoryInfo();
            DocumentCategoryRequestModel model = new DocumentCategoryRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;
            ViewBag.LstDocumentCategory = await ApiService.DocumentCategoryService.GetAll();

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View("Edit", item);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.DocumentCategory)]
        public async Task<ActionResult> Add(DocumentCategoryInfo model)
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

                var newInfo = new DocumentCategoryInfo()
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


                var result = await ApiService.DocumentCategoryService.Add(newInfo);
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
        [LogTracker(Action.Delete, PageId.DocumentCategory)]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {

                DocumentCategoryInfo DocumentCategoryInfo = new DocumentCategoryInfo();
                DocumentCategoryInfo.Id = Id;
                var result = await ApiService.DocumentCategoryService.Delete(DocumentCategoryInfo);
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


        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.DocumentCategory)]
        public async Task<ActionResult> ListAffiliate(DocumentCategoryRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 50;
            var result = await ApiService.DocumentCategoryService.GetAll();
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

        [LogTracker(Action.Edit, PageId.DocumentCategory)]
        public async Task<ActionResult> EditAffiliate(int Id = 0)
        {
            var DocumentCategoryinfo = new DocumentCategoryInfo();
            var DocumentCategory = await ApiService.DocumentCategoryService.GetInfo(Id);

            if (DocumentCategory != null)
            {
                DocumentCategoryinfo = new DocumentCategoryInfo()
                {
                    Id = DocumentCategory.Id,
                    Name = DocumentCategory.Name,
                    Code = DocumentCategory.Code,
                    Sequence = DocumentCategory.Sequence,
                    CreatedDate = DocumentCategory.CreatedDate,
                    CreatedBy = DocumentCategory.CreatedBy,
                    ModifyDate = DocumentCategory.ModifyDate,
                    ModifyBy = DocumentCategory.ModifyBy,
                    ParentId = DocumentCategory.ParentId,
                    IsDeleted = DocumentCategory.IsDeleted ?? 0,
                    Name_en = DocumentCategory.Name_en
                };
            }

            ViewBag.LstDocumentCategory = await ApiService.DocumentCategoryService.GetAll();

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View("EditAffiliate", DocumentCategoryinfo);
        }

    }
}