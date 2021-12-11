using Namek.Resources.MultiLanguage;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Namek.Admin.AttributeCustom;
using Namek.Admin.Models;
using Namek.Library.Entity.Pages;
using Namek.Library.Entity.PermissionActions;
using Namek.Library.Enums;
using Namek.Library.Helpers;
using Namek.LogServices.Modules;
using Namek.LogServices.Pages;
using Namek.LogServices.PermissionActions;
using Action = Namek.Library.Enums.Action;
using StringHelper = Namek.Core.StringHelper;
using Namek.Library.Infrastructure; 
using Namek.Entity.InfoModel;

namespace Namek.Admin.Controllers
{
    [Authorize]
    public class PageController : AdminBaseController
    {
        private readonly IModuleService _moduleService;
        private readonly IPageService _pageService;
        private readonly IPermissionActionService _permissionActionService;


        public PageController()
        {
        }

        // GET: Page
        [LogTracker(Action.View, PageId.Page)]
        public ActionResult Index()
        {
            return View();
        }

        [LogTracker(Action.View, PageId.Page)]
        public async Task<ActionResult> Edit(int id = 0)
        {
            ViewBag.id = id;
            Namek.Admin.Models.RootRequestModel requestModel = new Namek.Admin.Models.RootRequestModel()
            {
                Page = 1,
                Count = 100,
                Ascending = true
            };
            var modules = await ApiService.ModuleService.GetPagedList(requestModel);

            ViewBag.Module = modules;

            return View();
        }

        [Route("getAsync")]
        [HttpGet]
        public async Task<JsonResult> GetAsync()
        {
            var pages = await ApiService.PageService.GetAsync();
            var models = pages.ToList(); //.Select(x => x.ToModel());
            return RespondSuccess(models);
        }

        [HttpGet]
        [Route("get")]
        public async Task<JsonResult> Get(RootRequestModel requestModel)
        {
            Expression<Func<Page, bool>> where = x => !x.IsDeleted;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd(where, a => a.Name.Contains(requestModel.Name));

            ModuleRequest request = new ModuleRequest();
            request.Page = 1;
            request.PageSize = 999999;
            request.Name = string.Empty;

            var AllModule = await ApiService.ModuleService.GetAllModule(request);
            int total = AllModule.TotalRecords;
            //var modules = _moduleRepo.GetAllModule(1, 999999, out int total, string.Empty);
            var pages = await ApiService.PageService.GetPagedList(requestModel);
            if (pages == null)
                return RespondFailure();

            foreach (var p in pages)
            {
                //var permissionAction =
                //    _permissionActionService.FirstOrDefault(
                //        x => x.PageId == p.Id && !x.IsDeleted && x.PermisionId == null);

                var permissionAction = ApiService.PermissionActionService.GetInfoByPage(p.Id);
                p.Permission = permissionAction == null ? 0 : permissionAction.ActionKey;
                var mod = AllModule.Data.Where(m => m.Id == p.ModuleId).FirstOrDefault();
                p.UnsignedName = mod == null ? string.Empty : mod.Description;
            }
            var model = pages;

            return RespondSuccess(model, 1000);
        }

        [Route("get/{id:short}")]
        [HttpGet]
        public JsonResult GetById(short id)
        {
            var page = ApiService.PageService.GetInfo(id);
            //var permissionAction = _permissionActionService.FirstOrDefault(
            //    x => x.PageId == page.Id && !x.IsDeleted && x.PermisionId == null);

            var permissionAction = ApiService.PermissionActionService.GetInfoByPage(page.Id);


            page.Permission = permissionAction == null ? 0 : permissionAction.ActionKey;
            return RespondSuccess(page);
        }

        [Route("post")]
        [HttpPost]
        public async Task<JsonResult> Post(Page model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();

            var module = ApiService.ModuleService.GetInfo(model.ModuleId);

            model.CreateDate = DateTime.Now;
            model.Icon = "fa fa-file-o";
            model.ModuleName = module.Name;
            model.UnsignedName = StringHelper.RejectMarks(model.Name);
            model.Description = model.Name;
            //save it
            var rs = await ApiService.PageService.Insert(model);

            //thêm permission action mặc định
            var dateTimeNow = DateTime.Now;
            var permissionAction = new PermissionAction
            {
                IsDeleted = false,
                CreateDate = dateTimeNow,
                ModifyDate = dateTimeNow,
                ActionKey = model.Permission,
                PageId = rs,
                PageName = model.Name,
                ModuleId = model.ModuleId,
                ModuleName = model.ModuleName
            };

            var flag = await ApiService.PermissionActionService.Insert(permissionAction);

            //VerboseReporter.ReportSuccess(LanguageHelper.GetLabel(ResourceConstants.Label.PageCreateYourSuccess), "post");
            return RespondSuccess(model);
        }

        [Route("post")]
        [HttpPost]
        [LogTracker(Action.Edit, PageId.NoCheckPermisson)]
        public async Task<JsonResult> UpdatePage(Page entityModel)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            //get

            var module = ApiService.ModuleService.GetInfo(entityModel.ModuleId);

            var page = ApiService.PageService.GetInfo(entityModel.Id);
            if (page == null)
                return RespondFailure();

            page.Name = entityModel.Name;
            page.UnsignedName = StringHelper.RejectMarks(page.Name);
            page.Description = entityModel.Description;
            page.ModuleId = module.Id;
            page.ModuleName = module.Name;
            page.OrderNo = entityModel.OrderNo;
            page.ShowInMenu = entityModel.ShowInMenu;
            page.Url = entityModel.Url;
            //save it
            var rs = await ApiService.PageService.Update(page);

            //cập nhật thông tin permission
            //var permissionAction =
            //    _permissionActionService.FirstOrDefault(
            //        x => !x.IsDeleted && x.PageId == page.Id && x.PermisionId == null);
            var permissionAction = ApiService.PermissionActionService.GetInfoByPage(page.Id);
            if (permissionAction == null)
            {
                var dateTimeNow = DateTime.Now;
                permissionAction = new PermissionAction
                {
                    IsDeleted = false,
                    CreateDate = dateTimeNow,
                    ModifyDate = dateTimeNow,
                    ActionKey = entityModel.Permission,
                    PageId = page.Id,
                    PageName = page.Name,
                    ModuleId = page.ModuleId,
                    ModuleName = page.ModuleName
                };
                var flag = await ApiService.PermissionActionService.Insert(permissionAction);
            }
            else
            {
                permissionAction.ModifyDate = DateTime.Now;
                permissionAction.ActionKey = entityModel.Permission;
                permissionAction.PageName = page.Name;
                permissionAction.ModuleId = page.ModuleId;
                permissionAction.ModuleName = page.ModuleName;
                var flag=await ApiService.PermissionActionService.UpdateService(permissionAction);
            }

            //VerboseReporter.ReportSuccess(LanguageHelper.GetLabel(ResourceConstants.Label.PageEditSuccessPage), "put");
            return RespondSuccess(page);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            var rs = await ApiService.PageService.Delete(id);
            //VerboseReporter.ReportSuccess(LanguageHelper.GetLabel(ResourceConstants.Label.PageDeletePageSuccess), "delete");
            return RespondSuccess();
        }
    }
}