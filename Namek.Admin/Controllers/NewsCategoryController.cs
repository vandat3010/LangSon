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
    public class NewsCategoryController : BaseController
    { 
        // GET: Service
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.NewsCategory)]
        public async Task<ActionResult> Index(NewsCategoryRequestModel model)
        {
            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();

            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10;
            var result = await ApiService.NewsCategoryService.GetAll();
            if (result != null && !string.IsNullOrEmpty(model.Keywords))
            {
                result = result.Where(t => t.Name.Contains(model.Keywords) || t.DisplayName.Contains(model.Keywords)).ToList();
            }
            
            model.TotalRecords = result.Count;
            model.TotalPages = result.Count / model.PageSize + 1;
            ViewBag.Data = result;

            return View(model);
        }

        [LogTracker(Action.Edit, PageId.NewsCategory)]
        public async Task<ActionResult> Edit(int Id)
        {
            var NewsCategory = await ApiService.NewsCategoryService.GetInfo(Id);
            var NewsCategoryinfo = new NewsCategoryInfo()
            {
                Id = NewsCategory.Id,
                Name = NewsCategory.Name,
                DisplayName = NewsCategory.DisplayName,
                NameEn=NewsCategory.NameEn,
                Description = NewsCategory.Description,
                ParentId = NewsCategory.ParentId,
                Picture = NewsCategory.Picture,
                IsSystem = NewsCategory.IsSystem,
                IsActive = NewsCategory.IsActive??false,
                CreatedDate = NewsCategory.CreatedDate,
                CreatedBy = NewsCategory.CreatedBy,
                DisplayOrder = NewsCategory.DisplayOrder,
                Code=NewsCategory.Code,
                IsHomePage = NewsCategory.IsHomePage??false,
            };
            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();
            return View("Edit", NewsCategoryinfo);
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.NewsCategory)]
        public async Task<ActionResult> Update(NewsCategoryInfo model)
        {
            try
            { 
               
                var NewsCategory = await ApiService.NewsCategoryService.GetInfo(model.Id);

                var newInfo = new NewsCategoryInfo()
                {
                    Id = NewsCategory.Id,
                    Name = model.Name,
                    DisplayName = model.DisplayName,
                    NameEn=model.NameEn,
                    Description = model.Description,
                    ParentId = model.ParentId,
                    Picture = model.Picture,
                    IsSystem = NewsCategory.IsSystem,
                    IsActive = model.IsActive,
                    CreatedDate = NewsCategory.CreatedDate,
                    CreatedBy = NewsCategory.CreatedBy,
                    DisplayOrder = model.DisplayOrder,
                    Code=model.Code,
                    IsHomePage = model.IsHomePage
                };


                var result = await ApiService.NewsCategoryService.Update(newInfo);
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


        [LogTracker(Action.Add, PageId.NewsCategory)]
        public async Task<ActionResult> Add()
        {
            NewsCategoryInfo item = new NewsCategoryInfo(); 
            NewsCategoryRequestModel model = new NewsCategoryRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20; 
            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();
            return View("Edit", item);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.NewsCategory)]
        public async Task<ActionResult> Add(NewsCategoryInfo model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }
                model.CreatedBy = CurrentUser.Id;
                model.CreatedDate = DateTime.Now;
                var newInfo = new NewsCategoryInfo()
                { 
                    Name = model.Name,
                    DisplayName = model.DisplayName,
                    NameEn=model.NameEn,
                    Description = model.Description,
                    ParentId = model.ParentId,
                    Picture = model.Picture, 
                    IsActive = model.IsActive,
                    CreatedDate = DateTime.Now,
                    CreatedBy = CurrentUser.Id,
                    DisplayOrder = model.DisplayOrder,
                    Code=model.Code,
                    IsHomePage = model.IsHomePage
                };
                var result = await ApiService.NewsCategoryService.Add(newInfo);
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
        [LogTracker(Action.Delete, PageId.NewsCategory)]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {

                NewsCategoryInfo NewsCategoryInfo = new NewsCategoryInfo();
                NewsCategoryInfo.Id = Id;
                var result = await ApiService.NewsCategoryService.Delete(NewsCategoryInfo);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = "Tạm ngưng thất bại" }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = "Tạm ngưng thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }

        #region affiliate danh mục tin tức
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.NewsCategory)]
        public async Task<ActionResult> ListAffiliate(NewsCategoryRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 50;
            var result = await ApiService.NewsCategoryService.GetAll();

            if (result != null && result.Count > 0)
            {
                result = result.Where(t => t.Code == "Affiliate").ToList();
            }

            if (result != null && !string.IsNullOrEmpty(model.Keywords))
            {
                result = result.Where(t => t.Name.Contains(model.Keywords) || t.DisplayName.Contains(model.Keywords)).ToList();
            }
            model.Active = true;
            model.TotalRecords = result.Count;
            model.TotalPages = result.Count / model.PageSize + 1;
            ViewBag.Data = result;

            return View(model);
        }

        [LogTracker(Action.Edit, PageId.NewsCategory)]
        public async Task<ActionResult> EditAffiliate(int Id=0)
        {
            var NewsCategory = await ApiService.NewsCategoryService.GetInfo(Id);
            if (NewsCategory == null)
            {
                NewsCategory = new NewsCategory();
            }

            var NewsCategoryinfo = new NewsCategoryInfo()
            {
                Id = NewsCategory.Id,
                Name = NewsCategory.Name,
                DisplayName = NewsCategory.DisplayName,
                NameEn = NewsCategory.NameEn,
                Description = NewsCategory.Description,
                ParentId = NewsCategory.ParentId,
                Picture = NewsCategory.Picture,
                IsSystem = NewsCategory.IsSystem,
                IsActive = NewsCategory.IsActive ?? false,
                CreatedDate = NewsCategory.CreatedDate,
                CreatedBy = NewsCategory.CreatedBy,
                DisplayOrder = NewsCategory.DisplayOrder,
                Code = NewsCategory.Code
            };
            NewsCategoryinfo.Code = "Affiliate";
            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();
            return View("EditAffiliate", NewsCategoryinfo);
        }


        #endregion
    }
}