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
    [RoutePrefix("api/menu")]
    public class MenuController : BaseController
    {
        private readonly IMenuRepository _menuRepo = EngineContext.Current.Resolve<IMenuRepository>();

        // GET:  
        [HttpPost]
        [Route("Get")]
        public PagedList<Menu> Get(MenuRequestModel model)
        {
            PagedList<Menu> pagedResult = new PagedList<Menu>();
            int totalCount = 0;
            var rs = _menuRepo.Get(model, out totalCount);
            pagedResult.PageSize = model.PageSize;
            pagedResult.PageNumber = model.Page;
            pagedResult.TotalRecords = totalCount;
            pagedResult.Data = rs;
            return pagedResult;
        }
        [HttpPost]
        [Route("Add")]
        public int Add(MenuInfo model)
        {
            var rs = _menuRepo.Add(model);
            return rs;
        }

        [HttpPost]
        [Route("Update")]
        public int Update(MenuInfo model)
        {
            var rs = _menuRepo.Update(model);
            return rs;
        }

        [HttpPost]
        [Route("Delete")]
        public int Delete(MenuInfo model)
        {
            var rs = _menuRepo.Delete(model.Id);
            return rs;
        }

        [HttpGet]
        [Route("GetInfo")]
        public MenuInfo GetInfo(int id)
        {
            var rs = _menuRepo.GetInfo(id);
            return rs;
        }

        [HttpGet]
        [Route("GetPositionId")]
        public List<MenuInfo> GetPositionId(int PositionId)
        {
            var rs = _menuRepo.GetPositionId(PositionId);

            return ToParentModels(rs);
        }

        [HttpGet]
        [Route("GetMenuBySeName")]
        public List<MenuInfo> GetMenuBySeName(string SeName)
        {
            var rs = _menuRepo.GetMenuBySeName(SeName);

            return rs;
        }


        public List<MenuInfo> ToParentModels(List<MenuInfo> menus)
        {
            var menuModels = menus;
            List<MenuInfo> menuParents = new List<MenuInfo>();
            foreach (var menuModel in menuModels)
            {
                var menuChildrents = menuModels.FindAll(x => menuModel.Id == x.ParentId).ToList();
                if (menuChildrents.Any())
                {
                    foreach (var childrent in menuChildrents)
                    {
                        var subChildrents = menuModels.FindAll(x => childrent.Id == x.ParentId).OrderBy(x => x.Sequence).ToList();
                        if (subChildrents.Any())
                        {
                            childrent.MenuChildrents = subChildrents;
                            childrent.HasChildrent = true;
                        }
                    }

                    menuModel.MenuChildrents = menuChildrents;
                    menuModel.HasChildrent = true;

                }

                if (!menuModel.ParentId.HasValue)
                {
                    menuModel.HasParent = true;
                    menuParents.Add(menuModel);
                }
            }
            return menuParents;
        }
        
        [HttpGet]
        [Route("GetAll")]
        public List<Menu> GetAll()
        {
            var rs = _menuRepo.GetAll();
            return rs;
        }
        
    }
}