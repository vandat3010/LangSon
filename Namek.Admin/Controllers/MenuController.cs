using Namek.Admin.AttributeCustom;
using Namek.Admin.CustomFilters;
using Namek.Admin.Models;
using Namek.Core.ActionResult;
using Namek.Core.Utility;
using Namek.Entity.InfoModel;
using Namek.Entity.RequestModel; 
using Namek.Library.Enums;
using Namek.Library.Infrastructure;
using Namek.Resources.MultiLanguage;
using Spire.Pdf.Exporting.XPS.Schema;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Action = Namek.Library.Enums.Action;
namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class MenuController : BaseController
    { 
        [LogTracker(Action.View, PageId.Menu)]
        [HttpGet]
        public async Task<ActionResult> Index(MenuRequestModel model)
        { 
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10000;
            var result = await ApiService.MenuService.Get(model);
            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / model.PageSize + 1;
            ViewBag.Data = result.Data;

            return View(model);
        } 
        
        [LogTracker(Action.Add, PageId.Menu)]
        public async Task<ActionResult> Add()
        {
            MenuInfo item = new MenuInfo(); 

            MenuRequestModel model = new MenuRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10000;
            var lstmenu = await ApiService.MenuService.Get(model);
            ViewBag.LstMenu = lstmenu.Data;

            ViewBag.LstWebsite = SelectListItemExtension.GetEnums<WebsitePositionEnum>();
            //ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            List<EnumResponseModel> lstListItem = new List<EnumResponseModel>();
            //var LstService = await ApiServiceAutomation.ServiceService.GetAll();
            //for (int i = 0; i < LstService.Count; i++)
            //{
            //    EnumResponseModel itemService = new EnumResponseModel();
            //    itemService.Id =  LstService[i].Id;
            //    itemService.Name = "" + LstService[i].Name;
            //    lstListItem.Add(itemService);
            //}
            //ViewBag.LstServiceType = lstListItem;

            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();
            return View("Edit", item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.Menu)]
        public async Task<ActionResult> Add(MenuInfo model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }
                model.CreatedBy = CurrentUser.Id;
                model.CreatedDate = DateTime.Now;
                var result = await ApiService.MenuService.Add(model);
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

        [LogTracker(Action.Edit, PageId.Menu)]
        public async Task<ActionResult> Edit(int Id)
        {
            MenuInfo item = await ApiService.MenuService.GetInfo(Id);

            MenuRequestModel model = new MenuRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10000;
            var lstmenu = await ApiService.MenuService.Get(model);
            
            ViewBag.LstMenu = lstmenu.Data;
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            ViewBag.LstWebsite = SelectListItemExtension.GetEnums<WebsitePositionEnum>();

            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();
            ViewBag.Id = item.Id;

            return View("Edit", item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.Menu)]
        public async Task<ActionResult> Update(MenuInfo model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }
                model.ModifyBy = CurrentUser.Id;
                model.ModifyDate = DateTime.Now;
                if (model.Sequence!=null)
                {
                    if (!Regex.IsMatch(model.Sequence.Value.ToString(), @"^\d{9}$"))
                    {
                        model.Sequence = 0;
                    }
                }
                model.Sequence = model.Sequence ?? 0;
                  var result = await ApiService.MenuService.Update(model);
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
        [LogTracker(Action.Edit, PageId.Menu)]
        public async Task<ActionResult> Delete(MenuInfo model)
        {
            try
            {

                var result = await ApiService.MenuService.Delete(model);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
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