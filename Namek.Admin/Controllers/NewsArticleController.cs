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
    public class NewsArticleController : BaseController
    {
        [LogTracker(Action.View, PageId.NewsArticle)]
        [HttpGet]
        public async Task<ActionResult> Index(NewsArticleRequestModel model)
        {
            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();

            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10;

            if (!model.StartNewDate.HasValue)
            {
                model.StartNewDate = FirstDayOfYear(DateTime.Now);
            }
            if (!model.EndNewDate.HasValue)
            {
                model.EndNewDate = DateTime.Now;
            }

            var result = await ApiService.NewsArticleService.Get(model);

            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / model.PageSize + 1;
            ViewBag.Data = result.Data;

            return View(model);
        }

        [LogTracker(Action.Add, PageId.NewsArticle)]
        public async Task<ActionResult> Add()
        {
            NewsArticleInfo item = new NewsArticleInfo();

            NewsArticleRequestModel model = new NewsArticleRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;
            var lstNewsArticle = await ApiService.NewsArticleService.Get(model);
            ViewBag.LstNewsArticle = lstNewsArticle.Data;

            ViewBag.LstWebsite = SelectListItemExtension.GetEnums<WebsitePositionEnum>();
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();

            ViewBag.LstTag = await ApiService.TagService.GetAll();


            return View("Edit", item);
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.NewsArticle)]
        public async Task<ActionResult> Add(NewsArticleInfo model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }
                var setting = await ApiService.SettingService.GetGlobalSetting();
                model.CreatedBy = CurrentUser.Id;
                model.CreatedDate = DateTime.Now;
                model.UrlSegment = "/tin-tuc/" + SeoExtensions.GetSeName(model.Name);
                var result = await ApiService.NewsArticleService.Add(model);
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

        [LogTracker(Action.Edit, PageId.NewsArticle)]
        public async Task<ActionResult> Edit(int Id)
        {
            NewsArticleInfo item = await ApiService.NewsArticleService.GetInfo(Id);

            NewsArticleRequestModel model = new NewsArticleRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;
            var lstNewsArticle = await ApiService.NewsArticleService.Get(model);
            ViewBag.LstNewsArticle = lstNewsArticle.Data;
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            ViewBag.LstWebsite = SelectListItemExtension.GetEnums<WebsitePositionEnum>();

            //var dataNewsCategory = await ApiService.NewsCategoryService.GetAll();
            //var LstNewsCategory = dataNewsCategory.Select(t => new SelectListItem
            //{
            //    Value = t.Id.ToString(),
            //    Text = t.Name,
            //    Selected = item.LstNewsCategoryId.Contains(t.Id) ? true : false
            //});

            //ViewBag.LstNewsCategory = LstNewsCategory;

            //var dataTag = await ApiService.TagService.GetAll();
            //var LstTagId = dataTag.Select(t => new SelectListItem
            //{
            //    Value = t.Id.ToString(),
            //    Text = t.Name,
            //    Selected = item.LstTagId.Contains(t.Id) ? true : false
            //});

            //ViewBag.LstTag = LstTagId;
            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();
            ViewBag.LstTag = await ApiService.TagService.GetAll();


            return View("Edit", item);
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.NewsArticle)]
        public async Task<ActionResult> Update(NewsArticleInfo model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }
                var setting = await ApiService.SettingService.GetGlobalSetting();
                model.UrlSegment = "/tin-tuc/" + SeoExtensions.GetSeName(model.Name);
                model.ModifyBy = CurrentUser.Id;
                model.ModifyOn = DateTime.Now;


                //Nếu là tuyến  huyện thì cập nhật lại status =1
                if (CurrentUser.RoleId==(byte)Roles.District)
                {
                    model.Status = 0;
                }
                var result = await ApiService.NewsArticleService.Update(model);
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Delete, PageId.NewsArticle)]
        public async Task<ActionResult> Delete(NewsArticleInfo model)
        {
            try
            {

                var result = await ApiService.NewsArticleService.Delete(model);
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
        public static DateTime FirstDayOfYear(DateTime y)
        {
            return new DateTime(y.Year, 1, 1);
        }


        #region affiliate tin tức
        [LogTracker(Action.View, PageId.NewsArticle)]
        [HttpGet]
        public async Task<ActionResult> ListAffiliate(NewsArticleRequestModel model)
        {
            var LstNewsCategory = await ApiService.NewsCategoryService.GetAll();

            if (LstNewsCategory.Count > 0)
            {
                LstNewsCategory = LstNewsCategory.Where(t => t.Code == "Affiliate").ToList();


            }
            model.LstNewsCategoryId = LstNewsCategory.Select(t => t.Id).ToList();

            ViewBag.LstNewsCategory = LstNewsCategory;


            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;

            if (!model.StartNewDate.HasValue)
            {
                model.StartNewDate = FirstDayOfYear(DateTime.Now);
            }
            if (!model.EndNewDate.HasValue)
            {
                model.EndNewDate = DateTime.Now;
            }

            var result = await ApiService.NewsArticleService.Get(model);

            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / model.PageSize + 1;
            ViewBag.Data = result.Data;

            return View(model);
        }


        [LogTracker(Action.Edit, PageId.NewsArticle)]
        public async Task<ActionResult> EditAffiliate(int Id = 0)
        {
            NewsArticleInfo item = await ApiService.NewsArticleService.GetInfo(Id);
            if (item == null)
            {
                item = new NewsArticleInfo();
            }


            NewsArticleRequestModel model = new NewsArticleRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            ViewBag.LstWebsite = SelectListItemExtension.GetEnums<WebsitePositionEnum>();



            var LstNewsCategory = await ApiService.NewsCategoryService.GetAll();

            if (LstNewsCategory.Count > 0)
            {
                LstNewsCategory = LstNewsCategory.Where(t => t.Code == "Affiliate").ToList();


            }

            ViewBag.LstNewsCategory = LstNewsCategory;
            ViewBag.LstTag = await ApiService.TagService.GetAll();


            return View("EditAffiliate", item);
        }
        #endregion
    }
}