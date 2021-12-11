using Namek.Admin.AttributeCustom;
using Namek.Admin.CustomFilters;
using Namek.Admin.Models;
using Namek.Admin.Utilities;
using Namek.Core.ActionResult;
using Namek.Core.Utility;
using Namek.Entity.EntityModel;
using Namek.Entity.InfoModel;
using Namek.Entity.RequestModel; 
using Namek.Library.Enums;
using Namek.Library.Infrastructure;
using Namek.Resources.MultiLanguage;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Action = Namek.Library.Enums.Action;
namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class GroupPageController : BaseController
    { 

        [LogTracker(Action.View, PageId.GroupPage)]
        [HttpGet]
        public async Task<ActionResult> Index(GroupPageRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 1000;
            //if (!model.StartNewDate.HasValue)
            //{
            //    model.StartNewDate = FirstDayOfYear(DateTime.Now);
            //}
            //if (!model.EndNewDate.HasValue)
            //{
            //    model.EndNewDate = DateTime.Now;
            //}
            var result = await ApiService.GroupPageService.Get(model);
            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / result.PageSize + 1;
            ViewBag.Data = result.Data;
            ViewBag.LstActionName = SelectListItemExtension.GetEnums<ViewEnum>();

            return View(model);
        }

        [LogTracker(Action.Add, PageId.GroupPage)]
        public async Task<ActionResult> Add()
        {
            GroupPageInfo item = new GroupPageInfo();
            item.LstGroupPageMedia = new System.Collections.Generic.List<GroupPageMediaInfo>();
            GroupPageRequestModel model = new GroupPageRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10000;
            var lstGroupPage = await ApiService.GroupPageService.Get(model);
            ViewBag.LstGroupPage = lstGroupPage.Data;

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            ViewBag.LstView = SelectListItemExtension.GetEnums<ViewEnum>();

            ViewBag.LstControllerView = (SelectListItemExtension.GetEnums<ControllerEnum>());
            ViewBag.LstActionName = SelectListItemExtension.GetEnums<ViewEnum>();

            MenuRequestModel modelMenu = new MenuRequestModel();
            modelMenu.Page = modelMenu.Page == 0 ? 1 : modelMenu.Page;
            modelMenu.PageSize = 10000;
            var lstmenu = await ApiService.MenuService.Get(modelMenu);
            ViewBag.LstMenu = lstmenu.Data;



            var LstMediaType = Enum.GetValues(typeof(MediaType)).Cast<MediaType>().Select(u => new SelectListItem
            {
                Text = EnumMultiLanguageHelper.GetEnumDescription(u),
                Value = ((int)u).ToString()
            });
            ViewBag.LstMediaType = LstMediaType.ToList();
            return View("Edit", item);
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.GroupPage, PageId.Media, PageId.Layout, PageId.PartnerBrand, PageId.Menu)]
        public async Task<ActionResult> Add(GroupPageInfo model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }

                if (string.IsNullOrEmpty(model.Code)) //Nếu không có mã thì lấy từ parent
                {
                    if (model.ParentId.HasValue)
                    {
                        var groupPage = await ApiService.GroupPageService.GetInfo(model.ParentId.Value);
                        if (groupPage != null)
                        {
                            model.Code = groupPage.Code;
                        }
                    }
                }


                if (model.ParentId.HasValue)
                {
                    if (!model.MenuId.HasValue) //Nếu không có mã thì lấy từ parent
                    {
                        var groupPage = await ApiService.GroupPageService.GetInfo(model.ParentId.Value);
                        if (groupPage != null)
                        {
                            model.MenuId = groupPage.MenuId;
                        }
                    }
                }



                var setting = await ApiService.SettingService.GetGlobalSetting();
                model.CreateBy = CurrentUser.Id;
                model.CreateDate = DateTime.Now;
                var result = await ApiService.GroupPageService.Add(model);
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

        [LogTracker(Action.Edit, PageId.GroupPage)]
        public async Task<ActionResult> Edit(int Id)
        {
            GroupPageInfo item = await ApiService.GroupPageService.GetInfo(Id);

            GroupPageRequestModel model = new GroupPageRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10000;
            var lstGroupPage = await ApiService.GroupPageService.Get(model);
            ViewBag.LstGroupPage = lstGroupPage.Data;

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            ViewBag.LstView = SelectListItemExtension.GetEnums<ViewEnum>();

            ViewBag.LstControllerView = (SelectListItemExtension.GetEnums<ControllerEnum>());
            ViewBag.LstActionName = SelectListItemExtension.GetEnums<ViewEnum>();

            MenuRequestModel modelMenu = new MenuRequestModel();
            modelMenu.Page = modelMenu.Page == 0 ? 1 : modelMenu.Page;
            modelMenu.PageSize = 10000;
            var lstmenu = await ApiService.MenuService.Get(modelMenu);
            ViewBag.LstMenu = lstmenu.Data;

            var LstMediaType = Enum.GetValues(typeof(MediaType)).Cast<MediaType>().Select(u => new SelectListItem
            {
                Text = EnumMultiLanguageHelper.GetEnumDescription(u),
                Value = ((int)u).ToString()
            });
            ViewBag.LstMediaType = LstMediaType.ToList();

            return View("Edit", item);
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.GroupPage, PageId.Media, PageId.Layout, PageId.PartnerBrand, PageId.Menu, PageId.Emergency)]
        public async Task<ActionResult> Update(GroupPageInfo model)
        {
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
            {
                return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
            }
            var setting = await ApiService.SettingService.GetGlobalSetting();
            model.ModifyBy = CurrentUser.Id;
            model.ModifyDate = DateTime.Now;


            if (string.IsNullOrEmpty(model.Code)) //Nếu không có mã thì lấy từ parent
            {
                if (model.ParentId.HasValue)
                {
                    var groupPage = await ApiService.GroupPageService.GetInfo(model.ParentId.Value);
                    if (groupPage != null)
                    {
                        model.Code = groupPage.Code;
                    }
                }
            }

            if (model.ParentId.HasValue)
            {
                if (!model.MenuId.HasValue) //Nếu không có mã thì lấy từ parent
                {
                    var groupPage = await ApiService.GroupPageService.GetInfo(model.ParentId.Value);
                    if (groupPage != null)
                    {
                        model.MenuId = groupPage.MenuId;
                    }
                }
            }


            var result = await ApiService.GroupPageService.Update(model);
            var msg = "";
            if (result == 0)
            {
                return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ServiceManagementUpdateFailed) }, JsonRequestBehavior.AllowGet);
            }
            return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceUpdatedSuccessfully) }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Delete, PageId.GroupPage)]
        public async Task<ActionResult> Delete(GroupPageInfo model)
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
        public static DateTime FirstDayOfYear(DateTime y)
        {
            return new DateTime(y.Year, 1, 1);
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.GroupPage)]
        public async Task<ActionResult> Copy(GroupPageInfo info)
        {
            try
            { 
                var result = await ApiService.GroupPageService.Copy(info);
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


        [LogTracker(Action.Add, PageId.GroupPage)]
        public async Task<ActionResult> CloneService()
        {
            GroupPageInfo item = new GroupPageInfo();
            item.LstGroupPageMedia = new System.Collections.Generic.List<GroupPageMediaInfo>();
            GroupPageRequestModel model = new GroupPageRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10000;
            var lstGroupPage = await ApiService.GroupPageService.Get(model);
            ViewBag.LstGroupPage = lstGroupPage.Data;

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            ViewBag.LstView = SelectListItemExtension.GetEnums<ViewEnum>();

            ViewBag.LstControllerView = (SelectListItemExtension.GetEnums<ControllerEnum>());
            ViewBag.LstActionName = SelectListItemExtension.GetEnums<ViewEnum>();

            MenuRequestModel modelMenu = new MenuRequestModel();
            modelMenu.Page = modelMenu.Page == 0 ? 1 : modelMenu.Page;
            modelMenu.PageSize = 10000;
            var lstmenu = await ApiService.MenuService.Get(modelMenu);
            ViewBag.LstMenu = lstmenu.Data;



            var LstMediaType = Enum.GetValues(typeof(MediaType)).Cast<MediaType>().Select(u => new SelectListItem
            {
                Text = EnumMultiLanguageHelper.GetEnumDescription(u),
                Value = ((int)u).ToString()
            });
            ViewBag.LstMediaType = LstMediaType.ToList();
            return View("CloneService", item);
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.GroupPage)]
        public async Task<ActionResult> CloneService(GroupPageInfo info)
        {
            try
            {
                var result = await ApiService.GroupPageService.CloneService(info);
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


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.GroupPage)]
        public async Task<ActionResult> RenderTemplate(GroupPageInfo info)
        {
            try
            {
                var result = await ApiService.GroupPageService.RenderTemplate(info);
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
    }
}