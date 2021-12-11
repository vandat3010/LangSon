using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web.Http;
using Namek.Api.Extensions;
using Namek.Core;
using Namek.Entity.Config;
using Namek.Entity.EntityModel;
using Namek.Entity.InfoModel;
using Namek.Entity.RequestModel;
using Namek.Entity.Result;
using Namek.Infrastructure.Repository;
using Namek.Interface.Repository;
using Namek.Library.Entity.Logging;
using Namek.Library.Enums;
using Namek.Library.Helpers;
using Namek.Library.Infrastructure;
using Namek.LogServices.Logging;
using Namek.Resources.MultiLanguage;
using Newtonsoft.Json;
using StringHelper = Namek.Core.StringHelper;

namespace Namek.Api.Controllers
{
    [RoutePrefix("api/Legislation")]
    public class LegislationController : BaseController
    {
        private readonly ILegislationRepository _LegislationRepo = EngineContext.Current.Resolve<ILegislationRepository>();
        private readonly ILegislationCategoryRepository _LegislationCategoryRepo = EngineContext.Current.Resolve<ILegislationCategoryRepository>();

        // GET:  
        [HttpPost]
        [Route("Get")]
        public PagedList<Legislation> Get(LegislationRequestModel model)
        {
            PagedList<Legislation> pagedResult = new PagedList<Legislation>();
            int totalCount = 0;
            var rs = _LegislationRepo.Get(model, out totalCount);
            pagedResult.PageSize = model.PageSize;
            pagedResult.PageNumber = model.Page;
            pagedResult.TotalRecords = totalCount;
            pagedResult.Data = rs;
            return pagedResult;
        }
        [HttpPost]
        [Route("Add")]
        public int Add(LegislationInfo model)
        {
            try
            {
                var rs = _LegislationRepo.Add(model);
                return rs;
            }
            catch (Exception ex)
            {
                var _activityLogService = EngineContext.Current.Resolve<IActivityLogService>();
                _activityLogService.Insert(new ActivityLog
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = LocationHelper.GetVisitorIPAddress(),
                    Comment = ex.Message,
                    AfterParams = ex.Message,
                    UserId = CurrentUser.Id
                }, ActivityLogTypeEnum.Exception);
                throw; 
            }
          
        }

        [HttpPost]
        [Route("Update")]
        public int Update(LegislationInfo model)
        {
            try
            {
                var rs = _LegislationRepo.Update(model);
                return rs;
            }
            catch (Exception ex)
            {
                var _activityLogService = EngineContext.Current.Resolve<IActivityLogService>();
                _activityLogService.Insert(new ActivityLog
                {
                    CreatedOnUtc = DateTime.UtcNow,
                    IpAddress = LocationHelper.GetVisitorIPAddress(),
                    Comment = ex.Message,
                    AfterParams = ex.Message,
                    UserId = CurrentUser.Id
                }, ActivityLogTypeEnum.Exception);
                throw;
            }

        }

        [HttpPost]
        [Route("Delete")]
        public int Delete(LegislationInfo model)
        {
            var rs = _LegislationRepo.Delete(model.Id);
            return rs;
        }

        [HttpGet]
        [Route("GetInfo")]
        public Legislation GetInfo(int id)
        {
            var rs = _LegislationRepo.GetInfo(id);
            return rs;
        }


        [HttpGet]
        [Route("GetLegislationBySeName")]
        public Legislation GetLegislationBySeName(string SeName)
        {
            var rs = _LegislationRepo.GetLegislationBySeName(SeName);

            return rs;
        }


        [HttpGet]
        [Route("GetAll")]
        public List<Legislation> GetAll()
        {
            var rs = _LegislationRepo.GetAll();
            return rs;
        }
        [HttpGet]
        [Route("GetLegislationCategory")]
        public List<LegislationCategory> GetLegislationCategory()
        {
            var rs = _LegislationCategoryRepo.GetAll();
            return rs;
        }

        [HttpGet]
        [Route("GetLegislationByLegislationCategoryId")]
        public Legislation GetLegislationByLegislationCategoryId(int id)
        {
        
            var rs = _LegislationRepo.GetLegislationByLegislationCategoryId(id);
            return rs;
        }

        [HttpGet]
        [Route("GetLegislationByLegislationCategoryParentId")]
        public List<Legislation> GetLegislationByLegislationCategoryParentId(string parentId)
        {
            var rs = _LegislationRepo.GetLegislationByLegislationCategoryParentId(parentId);
            return rs;
        }
        

        [HttpPost]
        [Route("KhoiPhucLegislation")]
        public int KhoiPhucLegislation(LegislationInfo model)
        {
            var rs = _LegislationRepo.Delete(model.Id);
            return rs;
        }
    }
}
