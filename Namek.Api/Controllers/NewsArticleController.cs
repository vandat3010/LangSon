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
    [RoutePrefix("api/newsarticle")]
    public class NewsArticleController : BaseController
    {
        private readonly INewsArticleRepository _newsArticleRepo = EngineContext.Current.Resolve<INewsArticleRepository>();

        // GET:  
        [HttpPost]
        [Route("Get")]
        public PagedList<NewsArticle> Get(NewsArticleRequestModel model)
        {
            PagedList<NewsArticle> pagedResult = new PagedList<NewsArticle>();
            int totalCount = 0;
            var rs = _newsArticleRepo.Get(model, out totalCount);
            pagedResult.PageSize = model.PageSize;
            pagedResult.PageNumber = model.Page;
            pagedResult.TotalRecords = totalCount;
            pagedResult.Data = rs;
            return pagedResult;
        }
        [HttpPost]
        [Route("Add")]
        public int Add(NewsArticleInfo model)
        {
            var rs = _newsArticleRepo.Add(model);
            return rs;
        }
        [HttpPost]
        [Route("AddAndReturnId")]
        public int AddAndReturnId(NewsArticleInfo model)
        {
            var rs = _newsArticleRepo.AddAndReturnId(model);
            return rs;
        }

        [HttpPost]
        [Route("Update")]
        public int Update(NewsArticleInfo model)
        {
            var rs = _newsArticleRepo.Update(model);
            return rs;
        }

        [HttpPost]
        [Route("Delete")]
        public int Delete(NewsArticleInfo model)
        {
            var rs = _newsArticleRepo.Delete(model.Id);
            return rs;
        }

        [HttpGet]
        [Route("GetInfo")]
        public NewsArticleInfo GetInfo(int id)
        {
            var rs = _newsArticleRepo.GetInfo(id);
            return rs;
        }
        [HttpPost]
        [Route("UpdateAudio")]
        public int UpdateAudio(NewsArticleInfo model)
        {
            var rs = _newsArticleRepo.UpdateAudio(model);
            return rs;
        }
        [HttpGet]
        [Route("GetNewsArticleCategories")]
        public List<NewsArticleCategory> GetNewsArticleCategories()
        {
            var rs = _newsArticleRepo.GetNewsArticleCategories();
            return rs;
        }

        [HttpPost]
        [Route("GetNewsCategoriesWithNewsArticles")]
        public PagedList<NewsArticleInfo> GetNewsCategoriesWithNewsArticles(NewsArticleRequestModel model)
        {
            PagedList<NewsArticleInfo> pagedResult = new PagedList<NewsArticleInfo>();
            int totalCount = 0;
            var rs = _newsArticleRepo.GetNewsCategoriesWithNewsArticles(model, out totalCount);
            pagedResult.PageSize = model.PageSize;
            pagedResult.PageNumber = model.Page;
            pagedResult.TotalRecords = totalCount;
            pagedResult.Data = rs;
            return pagedResult;
        }

        [HttpGet]
        [Route("GetNewsArticleByUrlSegment")]
        public NewsArticleInfo GetNewsArticleByUrlSegment(string UrlSegment)
        {
            var rs = _newsArticleRepo.GetNewsArticleByUrlSegment(UrlSegment);
            return rs;
        }


        [HttpGet]
        [Route("GetNewsArticleByHomePage")]
        public List<NewsArticleInfo> GetNewsArticleByHomePage()
        {
            var rs = _newsArticleRepo.GetNewsArticleByHomePage();
            return rs;
        }

        [HttpGet]
        [Route("GetAllNews")]
        public List<NewsArticle> GetAllNews()
        {
            var rs = _newsArticleRepo.GetAllNews();
            return rs;
        }
        [HttpGet]
        [Route("GetTypeHomeDashboard")]
        public List<int?> GetTypeHomeDashboard()
        {
            var reportInfo = _newsArticleRepo.GetTypeByHomeDashboard();


            return reportInfo;
        }

        [HttpGet]
        [Route("GetInfomationIsHot")]
        public List<NewsArticle> GetInfomationIsHot()
        {
            var rs = _newsArticleRepo.GetInfomationIsHot();
            return rs;
        }

        [HttpGet]
        [Route("GetInfomationHomePage")]
        public List<NewsArticle> GetInfomationHomePage()
        {
            var rs = _newsArticleRepo.GetInfomationHomePage();
            return rs;
        }

        [HttpPost]
        [Route("GetAllNewsByNewsCategoryId")]
        public List<TinTucInfo> GetAllNewsByNewsCategoryId(List<int> lstInt)
        {
            var rs = _newsArticleRepo.GetAllNewsByNewsCategoryId(lstInt);
            return rs;
        }

        [HttpPost]
        [Route("Approve")]
        public int Approve(NewsArticleInfo model)
        {
            var rs = _newsArticleRepo.Approve(model);
            return rs;
        }

    }
}