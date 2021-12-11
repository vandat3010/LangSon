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
    [RoutePrefix("api/FAQ")]
    public class FAQController : BaseController
    {
        private readonly IFAQRepository _faqRepo = EngineContext.Current.Resolve<IFAQRepository>();
        private readonly IFaqCategoryRepository _faqCategoryRepo = EngineContext.Current.Resolve<IFaqCategoryRepository>();

        // GET:  
        [HttpPost]
        [Route("Get")]
        public PagedList<Faq> Get(FAQRequestModel model)
        {
            PagedList<Faq> pagedResult = new PagedList<Faq>();
            int totalCount = 0;
            var rs = _faqRepo.Get(model, out totalCount);
            pagedResult.PageSize = model.PageSize;
            pagedResult.PageNumber = model.Page;
            pagedResult.TotalRecords = totalCount;
            pagedResult.Data = rs;
            return pagedResult;
        }
        [HttpPost]
        [Route("Add")]
        public int Add(FaqInfo model)
        {
            try
            {
                var rs = _faqRepo.Add(model);
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
        public int Update(FaqInfo model)
        {
            try
            {
                var rs = _faqRepo.Update(model);
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
        public int Delete(FaqInfo model)
        {
            var rs = _faqRepo.Delete(model.Id);
            return rs;
        }

        [HttpGet]
        [Route("GetInfo")]
        public Faq GetInfo(int id)
        {
            var rs = _faqRepo.GetInfo(id);
            return rs;
        }


        [HttpGet]
        [Route("GetFaqBySeName")]
        public Faq GetFaqBySeName(string UrlSegment)
        {
            var rs = _faqRepo.GetFaqBySeName(UrlSegment);

            return rs;
        }


        [HttpGet]
        [Route("GetAll")]
        public List<Faq> GetAll()
        {
            var rs = _faqRepo.GetAll();
            return rs;
        }
        [HttpGet]
        [Route("GetFaqCategory")]
        public List<FaqCategory> GetFaqCategory()
        {
            var rs = _faqCategoryRepo.GetAll();
            return rs;
        }

        [HttpGet]
        [Route("GetFAQByFaqCategoryId")]
        public List<Faq> GetFAQByFaqCategoryId(int id)
        {
            var rs = _faqRepo.GetFAQByFaqCategoryId(id);
            return rs;
        }
        [HttpGet]
        [Route("GetFAQByFaqCategoryIdV2")]
        public List<FaqInfo> GetFAQByFaqCategoryIdV2(int id)
        {
            var rs = _faqRepo.GetFAQByFaqCategoryIdV2(id);
            return rs;
        }
        [HttpGet]
        [Route("GetFAQByLstFaqCategoryId")]
        public List<Faq> GetFAQByLstFaqCategoryId(string LstCategoryId)
        {
            List<int> CategoryId = JsonConvert.DeserializeObject<List<int>>(LstCategoryId);
            var rs = _faqRepo.GetFAQByLstFaqCategoryId(CategoryId);
            return rs;
        }
        [HttpGet]
        [Route("GetFAQByFaqCategoryParentId")]
        public List<Faq> GetFAQByFaqCategoryParentId(string parentId)
        {
            var rs = _faqRepo.GetFAQByFaqCategoryParentId(parentId);
            return rs;
        }

        [HttpPost]
        [Route("KhoiPhucFaq")]
        public int KhoiPhucFaq(FaqInfo model)
        {
            var rs = _faqRepo.Delete(model.Id);
            return rs;
        }
    }
}
