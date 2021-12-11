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
    public class LayoutController : BaseController
    {
        // GET: Layout
        [LogTracker(Action.View, PageId.Layout)]
        [HttpGet]
        public async Task<ActionResult> Index(GroupPageRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 1000;
            model.lstActionName = new System.Collections.Generic.List<string>();
            var header = SelectListItemExtension.GetEnums<ViewEnum>().Where(x => x.Id == (int)ViewEnum.HomeHeader).FirstOrDefault().Name;
            model.lstActionName.Add(header);
            var footer = SelectListItemExtension.GetEnums<ViewEnum>().Where(x => x.Id == (int)ViewEnum.HomeFooter).FirstOrDefault().Name;
            model.lstActionName.Add(footer);
            var banner = SelectListItemExtension.GetEnums<ViewEnum>().Where(x => x.Id == (int)ViewEnum.HomeBanner).FirstOrDefault().Name;
            model.lstActionName.Add(banner);

            var result = await ApiService.GroupPageService.Get(model);
            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / result.PageSize + 1;
            ViewBag.Data = result.Data;

            return View(model);
        }

        [LogTracker(Action.Edit, PageId.Layout)]
        public async Task<ActionResult> Edit(int id)
        {
            GroupPageInfo item = await ApiService.GroupPageService.GetInfo(id);

            if(item.ActionName == SelectListItemExtension.GetEnums<ViewEnum>().Where(x => x.Id == (int)ViewEnum.HomeBanner).FirstOrDefault().Name)
            {
                return View("BannerEdit", item);
            }

            return View("Edit", item);
        }


    }
}