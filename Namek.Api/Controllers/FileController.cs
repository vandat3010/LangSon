using System;
using System.Collections.Generic;
using System.IO;
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
    [RoutePrefix("api/File")]
    public class FileController : BaseController
    {
        private readonly IFileRepository _FileRepo = EngineContext.Current.Resolve<IFileRepository>();

        [HttpPost]
        [Route("AddFile")]
        public MediaFile AddFile(FileModel file)
        {

            var rs = _FileRepo.AddFile(file.FileUrl, file.FileName, file.ContentType, file.ContentLength, file.Width,file.Height, file.MediaCategoryId);
            return rs;
        }
        [HttpPost]
        [Route("Delete_POST")]
        public int Delete_POST(MediaFile file)
        {
            _FileRepo.DeleteFile(file);
            return 1;
        }

        [HttpPost]
        [Route("SaveFile")]
        public int SaveFile(MediaFile file)
        {
            _FileRepo.SaveFile(file);
            return 1;
        }

        //[HttpPost]
        //[Route("GetFileLocation")]
        //public string GetFileLocation(FileLocation file)
        //{
        //    return _FileRepo.GetFileLocation(file.Image, file.TargetSize, true);

        //}
    }
}
