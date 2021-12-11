using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Namek.Admin.Models;
using Namek.Api.Extensions;
using Namek.Core;
using Namek.Entity.Config;
using Namek.Entity.EntityModel;
using Namek.Entity.InfoModel;
using Namek.Entity.Result;
using Namek.Infrastructure.Repository;
using Namek.Interface.Repository;
using Namek.Library.Data;
using Namek.Library.Entity.Logging;
using Namek.Library.Enums;
using Namek.Library.Helpers;
using Namek.Library.Infrastructure;
using Namek.LogServices.Logging;
using Namek.LogServices.Modules;
using Namek.Resources.MultiLanguage;
using StringHelper = Namek.Core.StringHelper;

namespace Namek.Api.Controllers
{
    [RoutePrefix("api/module")]
    public class ModuleController : BaseController
    {
        private readonly IModuleRepository _moduleRepo =
            EngineContext.Current.Resolve<IModuleRepository>();

        private readonly IModuleService _moduleService =
          EngineContext.Current.Resolve<IModuleService>();

        [Route("GetAllModule")]
        [HttpPost]
        public ModuleRequest GetAllModule(ModuleRequest info)
        {
            var TotalRecords = 0;
            info.Data = _moduleRepo.GetAllModule(info.Page, info.PageSize, out TotalRecords, info.Name);
            info.TotalRecords = TotalRecords;
            return info;
        }
        [Route("GetInfo")]
        [HttpGet]
        public Namek.Library.Entity.Modules.Module GetInfo(short moduleId)
        {

            return _moduleService.FirstOrDefault(x => x.Id == moduleId);
        }

        [Route("GetAsync")]
        [HttpPost]
        public async Task<List<Namek.Library.Entity.Modules.Module>> GetAsync()
        {
            var pages = await _moduleService.GetAsync();
            return pages.ToList();
        }
        [Route("GetPagedList")]
        [HttpPost]
        public IPagedList<Namek.Library.Entity.Modules.Module> GetPagedList(RootRequestModel requestModel)
        {
            Expression<Func<Namek.Library.Entity.Modules.Module, bool>> where = x => !x.IsDeleted;

            if (!string.IsNullOrWhiteSpace(requestModel.Name))
                where = ExpressionHelpers.CombineAnd(where, a => a.Name.Contains(requestModel.Name));

            var pages = _moduleService.GetPagedList(
               where,
               null,
               false,
               requestModel.Page - 1,
               requestModel.Count);
            
            return pages;
        }
        [Route("Insert")]
        [HttpPost]
        public int Insert(Namek.Library.Entity.Modules.Module p)
        {
            _moduleService.Insert(p);
            return p.Id;
        }
        [Route("Update")]
        [HttpPost]
        public int Update(Namek.Library.Entity.Modules.Module p)
        {
            var module = _moduleService.Get(p.Id);
            if (module == null)
                return 0;

            module.Name = p.Name;
            module.Description = p.Description;
            module.OrderNo = p.OrderNo;
            module.ParentId = p.ParentId;
            module.ParentName = p.Name;
            module.Level = p.Level;
            module.NamePath = p.Name;
            module.ModifyDate = DateTime.Now;
            module.UnsignedName = StringHelper.RejectMarks(module.Name);

            _moduleService.Update(module);
            return 1;
        }
        [Route("Delete")]
        [HttpPost]
        public int Delete(int id)
        {
            _moduleService.Delete(x => x.Id == id);
            return 1;
        } 
    }
}