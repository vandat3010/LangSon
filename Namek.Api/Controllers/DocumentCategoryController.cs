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
    [RoutePrefix("api/DocumentCategory")]
    public class DocumentCategoryController : BaseController
    {
        private readonly IDocumentCategoryRepository _DocumentCategoryRepo = EngineContext.Current.Resolve<IDocumentCategoryRepository>();

        // GET:  
        [HttpGet]
        [Route("GetAll")]
        public List<DocumentCategory> GetAll()
        {
            return _DocumentCategoryRepo.GetAll();
        }
        [HttpGet]
        [Route("GetInfo")]
        public DocumentCategory GetInfo(int id)
        {
            var rs = _DocumentCategoryRepo.GetInfo(id);
            return rs;
        }

        [HttpGet]
        [Route("GetParentId")]
        public List<DocumentCategory> GetParentId(int parentId)
        {
            var rs = _DocumentCategoryRepo.GetParentId(parentId);
            return rs;
        }

        [HttpPost]
        [Route("Update")]
        public int Update(DocumentCategoryInfo model)
        {
            var rs = _DocumentCategoryRepo.Update(model);
            return rs;
        }

        [HttpPost]
        [Route("Delete")]
        public int Delete(DocumentCategoryInfo model)
        {
            var rs = _DocumentCategoryRepo.Delete(model.Id);
            return rs;
        }
        [HttpPost]
        [Route("Add")]
        public int Add(DocumentCategoryInfo model)
        {
            var rs = _DocumentCategoryRepo.Add(model);
            return rs;
        }

        [HttpGet]
        [Route("GetAllGroup")]
        public List<DocumentCategoryInfo> GetAllGroup()
        {
            var rs = _DocumentCategoryRepo.GetAllDocumentCategoryInfo();
            return ToParentModels(rs);
        }
        public List<DocumentCategoryInfo> ToParentModels(List<DocumentCategoryInfo> groupPages)
        {
            var groupPageModels = groupPages;
            List<DocumentCategoryInfo> groupPageParents = new List<DocumentCategoryInfo>();
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
                            childrent.DocumentCategoryChildrents = subChildrents;
                            childrent.HasChildrent = true;
                        }
                    }

                    groupPageModel.DocumentCategoryChildrents = groupPageChildrents;
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

        [HttpGet]
        [Route("GetByCode")]
        public DocumentCategory GetByCode(string Code)
        {
            var rs = _DocumentCategoryRepo.GetByCode(Code);
            return rs;
        }

        [HttpGet]
        [Route("GetByCodeByGroup")]
        public List<DocumentCategoryInfo> GetByCodeByGroup(string Code)
        {
            var rs = _DocumentCategoryRepo.GetAllDocumentCategoryInfo();
            if (rs.Count>0)
            {
                var rs1 = rs.Where(t => t.Code == Code).FirstOrDefault();
                if (rs1!=null)
                {
                    rs = rs.Where(t => t.ParentNode.Contains(rs1.ParentNode)).ToList();
                }
             
            }
            return ToParentModels(rs);
        }
    }
}