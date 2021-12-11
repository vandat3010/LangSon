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
    [RoutePrefix("api/MediaFile")]
    public class MediaFileController : BaseController
    {
        private readonly IMediaFileRepository _mediaFileRepo = EngineContext.Current.Resolve<IMediaFileRepository>();
        private readonly INewsCategoryRepository _newCategoryRepo = EngineContext.Current.Resolve<INewsCategoryRepository>();

        // GET:  
        [HttpPost]
        [Route("Get")]
        public PagedList<MediaFile> Get(MediaFileRequestModel model)
        {
            PagedList<MediaFile> pagedResult = new PagedList<MediaFile>();
            int totalCount = 0;
            var rs = _mediaFileRepo.Get(model, out totalCount);
            pagedResult.PageSize = model.PageSize;
            pagedResult.PageNumber = model.Page;
            pagedResult.TotalRecords = totalCount;
            pagedResult.Data = rs;
            return pagedResult;
        }
        [HttpPost]
        [Route("Add")]
        public int Add(MediaFileInfo model)
        {
            var rs = _mediaFileRepo.Add(model);
            return rs;
        }

        [HttpPost]
        [Route("Update")]
        public int Update(MediaFileInfo model)
        {
            var rs = _mediaFileRepo.Update(model);
            return rs;
        }

        [HttpPost]
        [Route("Delete")]
        public int Delete(MediaFileInfo model)
        {
            var rs = _mediaFileRepo.Delete(model.Id);
            return rs;
        }

        [HttpGet]
        [Route("GetInfo")]
        public MediaFile GetInfo(int id)
        {
            var rs = _mediaFileRepo.GetInfo(id);
            return rs;
        }

        [HttpGet]
        [Route("GetAll")]
        public List<MediaFile> GetAll()
        {
            var rs = _mediaFileRepo.GetAll();
            return rs;
        }
        [HttpGet]
        [Route("GetAllNewCategory")]
        public List<NewsCategory> GetAllNewCategory()
        {
            var rs = _newCategoryRepo.GetAll();
            return rs;
        }

        [HttpPost]
        [Route("Search")]
        public List<MediaFile> Search(MediaSelectorSearchQuery searchQuery)
        {
            var rs = _mediaFileRepo.Search(searchQuery);
            return rs;
        }
        [HttpGet]
        [Route("GetAlt")]
        public string GetAlt(string url)
        {
            var rs = _mediaFileRepo.GetAlt(url);
            return rs;
        }


        [HttpPost]
        [Route("UpdateAlt")]
        public bool UpdateAlt(UpdateMediaParams updateMediaParams)
        {
            var rs = _mediaFileRepo.UpdateAlt(updateMediaParams);
            return rs;
        }

        [HttpGet]
        [Route("GetDescription")]
        public string GetDescription(string url)
        {
            var rs = _mediaFileRepo.GetDescription(url);
            return rs;
        }
        [HttpPost]
        [Route("UpdateDescription")]
        public bool UpdateDescription(UpdateMediaParams updateMediaParams)
        {
            var rs = _mediaFileRepo.UpdateDescription(updateMediaParams);
            return rs;
        }

        [HttpGet]
        [Route("GetFileInfo")]
        public MediaFile GetFileInfo(string value)
        {
            var rs = _mediaFileRepo.GetFileInfo(value);
            return rs;
        }


        [HttpGet]
        [Route("GetMediaFileByUrl")]
        public MediaFile GetMediaFileByUrl(string originalImageUrl)
        {
            MediaFile fileByLocation = _mediaFileRepo.GetMediaFileByUrl(originalImageUrl);
            return fileByLocation;
        }
       
    }
}
