using Namek.Resources.MultiLanguage;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Namek.Admin.AttributeCustom;
using Namek.Admin.Models;
using Namek.Library.Entity.Modules;
using Namek.Library.Enums;
using Namek.Library.Helpers;
using Namek.LogServices.Modules;
using Action = Namek.Library.Enums.Action;
using StringHelper = Namek.Core.StringHelper;
using Namek.Entity.InfoModel;

namespace Namek.Admin.Controllers
{
    public class ModuleController : AdminBaseController
    {
        //private readonly IModuleService _moduleService;

        public ModuleController()
        {
            //_moduleService = moduleService;
        }

        // GET: Module
        [LogTracker(Action.View, PageId.Module)]
        public ActionResult Index()
        {
            return View();
        }

        [LogTracker(Action.View, PageId.Module)]
        public ActionResult Edit(int id = 0)
        {
            ViewBag.id = id;
            return View();
        }

        [Route("getAsync")]
        [HttpGet]
        public async Task<JsonResult> GetAsync()
        {
            var roles = await ApiService.ModuleService.GetAsync();
            var roleModels = roles.ToList(); //.Select(x => x.ToModel());
            return RespondSuccess(roleModels);
        }

        [HttpGet]
        [Route("get")]
        public async Task<JsonResult> Get(Namek.Admin.Models.RootRequestModel requestModel)
        {


            var modules = await ApiService.ModuleService.GetPagedList(requestModel);
            if (modules == null)
                return RespondFailure();
            var model = modules;

            return RespondSuccess(model, 1000);
        }

        [HttpGet]
        [Route("GetAllModule")]
        public async Task<JsonResult> GetAllModule(Namek.Entity.InfoModel.ModuleRequest requestModel)
        {

            var modules = await ApiService.ModuleService.GetAllModule(requestModel);
            if (modules == null)
                return RespondFailure();
            var model = (ModuleRequest)modules;

            return RespondSuccess(model.Data, model.TotalRecords);
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<JsonResult> GetAll()
        {
            Expression<Func<Module, bool>> where = x => true;

            RootRequestModel requestModel = new RootRequestModel()
            {
                Page = 1,
                Count = 99999
            };

            var modules = await ApiService.ModuleService.GetPagedList(requestModel);
            if (modules == null)
                return RespondFailure();
            modules.Insert(0, new Module
            {
                Id = 0,
                Name = ""
            });
            var model = modules;

            return RespondSuccess(model, 1000);
        }

        [Route("get/{id:short}")]
        [HttpGet]
        public JsonResult GetById(short id)
        {
            var role = ApiService.ModuleService.GetInfo(id);
            return RespondSuccess(role);
        }

        [Route("post")]
        [HttpPost]
        public async Task<JsonResult> Post(Module model)
        {
            var parent = model.ParentId == null ? null : ApiService.ModuleService.GetInfo(model.ParentId ?? 0);

            //if (!ModelState.IsValid)
            //    return BadRequest();
            if (model.ParentId == 0)
                model.ParentId = null;
            model.ParentName = parent != null ? parent.Name : null;
            model.NamePath = model.Name;
            model.CreateDate = DateTime.Now;
            model.ModifyDate = model.CreateDate;
            model.UnsignedName = StringHelper.RejectMarks(model.Name);
            //save it
            var rs = await ApiService.ModuleService.Insert(model);

            //VerboseReporter.ReportSuccess("Tạo module thành công", "post");
            return RespondSuccess(model);
        }

        [Route("put")]
        [HttpPut]
        public async Task<JsonResult> Put(Module entityModel)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();
            //get

            var parent = entityModel.ParentId == null
                ? null
                : ApiService.ModuleService.GetInfo(entityModel.ParentId ?? 0);

            var module = ApiService.ModuleService.GetInfo(entityModel.Id);
            if (module == null)
                return RespondFailure();

            module.Name = entityModel.Name;
            module.Description = entityModel.Description;
            module.OrderNo = entityModel.OrderNo;
            module.ParentId = parent != null ? entityModel.ParentId : null;
            module.ParentName = parent != null ? parent.Name : null;
            module.Level = entityModel.Level;
            module.NamePath = module.Name;
            module.ModifyDate = DateTime.Now;
            module.UnsignedName = StringHelper.RejectMarks(module.Name);
            //save it
            var rs = await ApiService.ModuleService.Update(module);

            //VerboseReporter.ReportSuccess("Sửa module thành công", "put");
            return RespondSuccess(module);
        }

        [Route("delete/{id:int}")]
        [HttpDelete]
        public async Task<JsonResult> Delete(int id)
        {
            var rs = await ApiService.ModuleService.Delete(id);
            //VerboseReporter.ReportSuccess("Xóa module thành công", "delete");
            return RespondSuccess();
        }
    }
}