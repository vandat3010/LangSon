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
    [RoutePrefix("api/Document")]
    public class DocumentController : BaseController
    {
        private readonly IDocumentRepository _DocumentRepo = EngineContext.Current.Resolve<IDocumentRepository>();
        private readonly IDocumentCategoryRepository _DocumentCategoryRepo = EngineContext.Current.Resolve<IDocumentCategoryRepository>();

        // GET:  
        [HttpPost]
        [Route("Get")]
        public PagedList<Document> Get(DocumentRequestModel model)
        {
            PagedList<Document> pagedResult = new PagedList<Document>();
            int totalCount = 0;
            var rs = _DocumentRepo.Get(model, out totalCount);
            pagedResult.PageSize = model.PageSize;
            pagedResult.PageNumber = model.Page;
            pagedResult.TotalRecords = totalCount;
            pagedResult.Data = rs;
            return pagedResult;
        }

        [HttpPost]
        [Route("Add")]
        public int Add(DocumentInfo model)
        {
            try
            {
                var rs = _DocumentRepo.Add(model);
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
        public int Update(DocumentInfo model)
        {
            try
            {
                var rs = _DocumentRepo.Update(model);
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
        public int Delete(DocumentInfo model)
        {
            var rs = _DocumentRepo.Delete(model.Id);
            return rs;
        }

        [HttpGet]
        [Route("GetInfo")]
        public Document GetInfo(int id)
        {
            var rs = _DocumentRepo.GetInfo(id);
            return rs;
        }


        [HttpGet]
        [Route("GetDocumentBySeName")]
        public Document GetDocumentBySeName(string SeName)
        {
            var rs = _DocumentRepo.GetDocumentBySeName(SeName);

            return rs;
        }


        [HttpGet]
        [Route("GetAll")]
        public List<Document> GetAll()
        {
            var rs = _DocumentRepo.GetAll();
            return rs;
        }
        [HttpGet]
        [Route("GetDocumentCategory")]
        public List<DocumentCategory> GetDocumentCategory()
        {
            var rs = _DocumentCategoryRepo.GetAll();
            return rs;
        }

        [HttpGet]
        [Route("GetDocumentByDocumentCategoryId")]
        public List<Document> GetDocumentByDocumentCategoryId(string id)
        {
            var lstId = JsonConvert.DeserializeObject<List<int>>(id);
            var rs = _DocumentRepo.GetDocumentByDocumentCategoryId(lstId);
            return rs;
        }

        [HttpGet]
        [Route("GetDocumentByDocumentCategoryParentId")]
        public List<Document> GetDocumentByDocumentCategoryParentId(string parentId)
        {
            var rs = _DocumentRepo.GetDocumentByDocumentCategoryParentId(parentId);
            return rs;
        }
        

        [HttpPost]
        [Route("KhoiPhucDocument")]
        public int KhoiPhucDocument(DocumentInfo model)
        {
            var rs = _DocumentRepo.Delete(model.Id);
            return rs;
        }
    }
}
