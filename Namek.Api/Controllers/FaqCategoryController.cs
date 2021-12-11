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
using StringHelper = Namek.Core.StringHelper;

namespace Namek.Api.Controllers
{
    [RoutePrefix("api/faqcategory")]
    public class FaqCategoryController : BaseController
    {
        private readonly IFaqCategoryRepository _faqCategoryRepo = EngineContext.Current.Resolve<IFaqCategoryRepository>();

        // GET:  
        [HttpGet]
        [Route("GetAll")]
        public List<FaqCategory> GetAll()
        {
            return _faqCategoryRepo.GetAll();
        }
        [HttpGet]
        [Route("GetAllQLVB")]
        public List<FaqCategory> GetAllQLVB()
        {
            return _faqCategoryRepo.GetAllQLVB();
        }

        [HttpGet]
        [Route("GetInfo")]
        public FaqCategory GetInfo(int id)
        {
            var rs = _faqCategoryRepo.GetInfo(id);
            return rs;
        }
        [HttpPost]
        [Route("Update")]
        public int Update(FaqCategoryInfo model)
        {
            var rs = _faqCategoryRepo.Update(model);
            return rs;
        }

        [HttpPost]
        [Route("Delete")]
        public int Delete(FaqCategoryInfo model)
        {
            var rs = _faqCategoryRepo.Delete(model.Id);
            return rs;
        }
        [HttpPost]
        [Route("Add")]
        public int Add(FaqCategoryInfo model)
        {
            var rs = _faqCategoryRepo.Add(model);
            return rs;
        }

        [HttpGet]
        [Route("GetAllGroup")]
        public List<FaqCategoryInfo> GetAllGroup()
        {
            var rs = _faqCategoryRepo.GetAllFaqCategoryInfo();
            return ToParentModels(rs);
        }
        public List<FaqCategoryInfo> ToParentModels(List<FaqCategoryInfo> groupPages)
        {
            var groupPageModels = groupPages;
            List<FaqCategoryInfo> groupPageParents = new List<FaqCategoryInfo>();
            foreach (var groupPageModel in groupPageModels)
            {
                var groupPageChildrents = groupPageModels.FindAll(x => groupPageModel.Id == x.ParentId).ToList();
                if (groupPageChildrents.Any())
                {
                    foreach (var childrent in groupPageChildrents)
                    {
                        var subChildrents = groupPageModels.FindAll(x => childrent.Id == x.ParentId).OrderBy(x => x.Sequence).ToList();
                        if (subChildrents.Any())
                        {
                            childrent.FaqCategoryChildrents = subChildrents;
                            childrent.HasChildrent = true;
                        }
                    }

                    groupPageModel.FaqCategoryChildrents = groupPageChildrents;
                    groupPageModel.HasChildrent = true;

                }

                if (!groupPageModel.ParentId.HasValue)
                {
                    groupPageModel.HasParent = true;
                    groupPageParents.Add(groupPageModel);
                }
            }
            return groupPageParents;
        }
    }
}