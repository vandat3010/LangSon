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
    [RoutePrefix("api/grouppage")]
    public class GroupPageController : BaseController
    {
        private readonly IGroupPageRepository _GroupPageRepo = EngineContext.Current.Resolve<IGroupPageRepository>();

        // GET:  
        [HttpPost]
        [Route("Get")]
        public PagedList<GroupPage> Get(GroupPageRequestModel model)
        {
            PagedList<GroupPage> pagedResult = new PagedList<GroupPage>();
            int totalCount = 0;
            var rs = _GroupPageRepo.Get(model, out totalCount);
            pagedResult.PageSize = model.PageSize;
            pagedResult.PageNumber = model.Page;
            pagedResult.TotalRecords = totalCount;
            pagedResult.Data = rs;
            return pagedResult;
        }

        [HttpPost]
        [Route("GetPartnerBrand")]
        public PagedList<GroupPage> GetPartnerBrand(GroupPageRequestModel model)
        {
            PagedList<GroupPage> pagedResult = new PagedList<GroupPage>();
            int totalCount = 0;
            var rs = _GroupPageRepo.GetPartnerBrand(model, out totalCount);
            pagedResult.PageSize = model.PageSize;
            pagedResult.PageNumber = model.Page;
            pagedResult.TotalRecords = totalCount;
            pagedResult.Data = rs;
            return pagedResult;
        }

        [HttpPost]
        [Route("Add")]
        public int Add(GroupPageInfo model)
        {
            var rs = _GroupPageRepo.Add(model);
            return rs;
        }

        [HttpPost]
        [Route("Update")]
        public int Update(GroupPageInfo model)
        {
            var rs = _GroupPageRepo.Update(model);
            return rs;
        }

        [HttpPost]
        [Route("Delete")]
        public int Delete(GroupPageInfo model)
        {
            var rs = _GroupPageRepo.Delete(model.Id);
            return rs;
        }

        [HttpGet]
        [Route("GetInfo")]
        public GroupPageInfo GetInfo(int id)
        {
            var rs = _GroupPageRepo.GetInfo(id);
            return rs;
        }


        [HttpGet]
        [Route("GetHomeInfo")]
        public List<GroupPageInfo> GetHomeInfo()
        {
            var rs = _GroupPageRepo.GetHomeInfo();

            return ToParentModels(rs);
        }

        public List<GroupPageInfo> ToParentModels(List<GroupPageInfo> groupPages)
        {
            var groupPageModels = groupPages;
            List<GroupPageInfo> groupPageParents = new List<GroupPageInfo>();
            foreach (var groupPageModel in groupPageModels)
            {
                var groupPageChildrents = groupPageModels.FindAll(x => groupPageModel.Id == x.ParentId).ToList();
                if (groupPageChildrents.Any())
                {
                    foreach (var childrent in groupPageChildrents)
                    {
                        var subChildrents = groupPageModels.FindAll(x => childrent.Id == x.ParentId).OrderBy(x => x.OrderNo).ToList();
                        if (subChildrents.Any())
                        {
                            childrent.GroupPageChildrents = subChildrents;
                            childrent.HasChildrent = true;
                        }
                    }

                    groupPageModel.GroupPageChildrents = groupPageChildrents;
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
        [Route("GetGroupPageByCode")]
        public List<GroupPageInfo> GetGroupPageByCode(string Code)
        {
            var rs = _GroupPageRepo.GetGroupPageByCode(Code);

            return ToParentModels(rs);
        }

        [HttpGet]
        [Route("GetGroupPageByMenuId")]
        public List<GroupPageInfo> GetGroupPageByMenuId(int MenuId)
        {
            var rs = _GroupPageRepo.GetGroupPageByMenuId(MenuId);

            return ToParentModels(rs);
        }

        [HttpPost]
        [Route("Copy")]
        public int Copy(GroupPageInfo model)
        {
            var rs = _GroupPageRepo.Copy(model.Id);
            return rs;
        }

        [HttpPost]
        [Route("CloneService")]
        public int CloneService(GroupPageInfo model)
        {
            var rs = _GroupPageRepo.CloneService(model);
            return rs;
        }
        [HttpPost]
        [Route("RenderTemplate")]
        public int RenderTemplate(GroupPageInfo model)
        {
            var rs = _GroupPageRepo.RenderView(model);
            return rs;
        }
        [HttpGet]
        [Route("GetGroupPageByActionName")]
        public List<GroupPageInfo> GetGroupPageByActionName(string ActionName)
        {
            var rs = _GroupPageRepo.GetGroupPageByActionName(ActionName);

            return ToParentModels(rs);
        }

        [HttpPost]
        [Route("GetDistrict")]
        public PagedList<District> GetDistrict(DistrictRequestModel model)
        {
            PagedList<District> pagedResult = new PagedList<District>();
            int totalCount = 0;
            var rs = _GroupPageRepo.GetDistrict(model, model.Page, model.PageSize, out totalCount);
            pagedResult.PageSize = model.PageSize;
            pagedResult.PageNumber = model.Page;
            pagedResult.TotalRecords = totalCount;
            pagedResult.Data = rs;
            return pagedResult;
        }


        [HttpPost]
        [Route("DeleteDistrict")]
        public int DeleteDistrict(District model)
        {
            var rs = _GroupPageRepo.DeleteDistrict(model.Id);
            return rs;
        }

        [HttpPost]
        [Route("UpdateDistrict")]
        public int UpdateDistrict(District model)
        {
            var rs = _GroupPageRepo.UpdateDistrict(model);
            return rs;
        }


        [HttpGet]
        [Route("GetAllDistrict")]
        public List<District> GetAllDistrict()
        {
            var rs = _GroupPageRepo.GetAllDistrict();
            return rs;
        }


        [HttpGet]
        [Route("GetLatLngByDistrictId")]
        public District GetLatLngByDistrictId(int id)
        {
            var rs = _GroupPageRepo.GetLatLngByDistrictId(id);

            return rs;
        }

        [HttpGet]
        [Route("GetDistrictById")]
        public District GetDistrictById(int id)
        {
            var rs = _GroupPageRepo.GetDistrictById(id);
            return rs;
        }
    }
}