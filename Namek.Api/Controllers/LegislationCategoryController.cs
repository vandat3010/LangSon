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
    [RoutePrefix("api/LegislationCategory")]
    public class LegislationCategoryController : BaseController
    {
        private readonly ILegislationCategoryRepository _LegislationCategoryRepo = EngineContext.Current.Resolve<ILegislationCategoryRepository>();

        // GET:  
        [HttpGet]
        [Route("GetAll")]
        public List<LegislationCategory> GetAll()
        {
            return _LegislationCategoryRepo.GetAll();
        }
        [HttpGet]
        [Route("GetInfo")]
        public LegislationCategory GetInfo(int id)
        {
            var rs = _LegislationCategoryRepo.GetInfo(id);
            return rs;
        }

        [HttpGet]
        [Route("GetParentId")]
        public List<LegislationCategory> GetParentId(int parentId)
        {
            var rs = _LegislationCategoryRepo.GetParentId(parentId);
            return rs;
        }

        [HttpPost]
        [Route("Update")]
        public int Update(LegislationCategoryInfo model)
        {
            var rs = _LegislationCategoryRepo.Update(model);
            return rs;
        }

        [HttpPost]
        [Route("Delete")]
        public int Delete(LegislationCategoryInfo model)
        {
            var rs = _LegislationCategoryRepo.Delete(model.Id);
            return rs;
        }
        [HttpPost]
        [Route("Add")]
        public int Add(LegislationCategoryInfo model)
        {
            var rs = _LegislationCategoryRepo.Add(model);
            return rs;
        }

        [HttpGet]
        [Route("GetAllGroup")]
        public List<LegislationCategoryInfo> GetAllGroup()
        {
            var rs = _LegislationCategoryRepo.GetAllLegislationCategoryInfo();
            return ToParentModels(rs);
        }
        public List<LegislationCategoryInfo> ToParentModels(List<LegislationCategoryInfo> groupPages)
        {
            var groupPageModels = groupPages;
            List<LegislationCategoryInfo> groupPageParents = new List<LegislationCategoryInfo>();
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
                            childrent.LegislationCategoryChildrents = subChildrents;
                            childrent.HasChildrent = true;
                        }
                    }

                    groupPageModel.LegislationCategoryChildrents = groupPageChildrents;
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
        public LegislationCategory GetByCode(string Code)
        {
            var rs = _LegislationCategoryRepo.GetByCode(Code);
            return rs;
        }

        [HttpGet]
        [Route("GetByCodeByGroup")]
        public List<LegislationCategoryInfo> GetByCodeByGroup(string Code)
        {
            var rs = _LegislationCategoryRepo.GetAllLegislationCategoryInfo();
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