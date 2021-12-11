using Namek.Admin.AttributeCustom;
using Namek.Admin.Controllers;
using Namek.Admin.Utilities;
using Namek.Entity.EntityBase;
using Namek.Entity.EntityModel;
using Namek.Entity.InfoModel;
using Namek.Entity.RequestModel; 
using Namek.Library.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Namek.Admin.Controllers
{
    public class MediaSelectorController : BaseController
    {
        public MediaSelectorController()
        {

        }
        [XFrameOptionsFilterAttribute]
        public async Task<ActionResult> Show(MediaSelectorSearchQuery searchQuery)
        {
            var lstnew = await ApiService.NewsCategoryService.GetAll();
            var lstNewsCategory = new List<SelectListItem>();
            for (int i = 0; i < lstnew.Count; i++)
            {
                lstNewsCategory.Add(new SelectListItem()
                {
                    Value = "" + lstnew[i].Id,
                    Text = lstnew[i].Name,
                });
            }
            ViewData["categories"] = lstNewsCategory;
            ImageProcessor imageProcessor = new ImageProcessor();
            var results = await ApiService.MediaFileService.Search(searchQuery);
            //for (int i = 0; i < results.Count; i++)
            //{
            //    string FileImageUrl = imageProcessor.GetUrl(results[i], new System.Drawing.Size(), true);
            //    //imageProcessor.get
            //    results[i].ImageInfo = new ImageInfo
            //    {
            //        Title = results[i].Title,
            //        Description = results[i].Description,
            //        ImageUrl = FileImageUrl
            //    };
            //}

            ViewData["results"] = results;


            return PartialView(searchQuery);
        }

        [HttpGet]
        public async Task<JsonResult> Alt(string url)
        {
            return Json(new { alt = await ApiService.MediaFileService.GetAlt(url) });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateAlt(UpdateMediaParams updateMediaParams)
        {
            return Json(await ApiService.MediaFileService.UpdateAlt(updateMediaParams));
        }

        [HttpGet]
        public async Task<JsonResult> Description(string url)
        {
            return Json(new { description = (await ApiService.MediaFileService.GetDescription(url)) });
        }

        [HttpPost]
        public async Task<JsonResult> UpdateDescription(UpdateMediaParams updateMediaParams)
        {
            return Json(await ApiService.MediaFileService.UpdateDescription(updateMediaParams));
        }

        public async Task<JsonResult> GetFileInfo(string value)
        {
            var mediafile = await ApiService.MediaFileService.GetMediaFileByUrl(value);
            if (mediafile != null)
            {
                var SelectedItemInfo = new SelectedItemInfo()
                {
                    Url = mediafile.FileUrl
                };
                //{
                //    Url = fileUrl
                //}
                return Json(SelectedItemInfo, JsonRequestBehavior.AllowGet);
            }
            string[] split = value.Split('-');
            int id = Convert.ToInt32(split[0]);
            var file = await ApiService.MediaFileService.GetInfo(id);
            if (file == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            ImageProcessor imageProcessor = new ImageProcessor();

             ImageSize imageSize = imageProcessor.GetImageSize(file, Convert.ToInt32(split[1]), Convert.ToInt32(split[2]));

            //GetSizes(file)
            //       .FirstOrDefault(size => size.Size == new Size(Convert.ToInt32(split[1]), Convert.ToInt32(split[2])));


            //return GetFileLocation(file, imageSize.Size, false);

            string FileImageUrl = imageProcessor.GetUrl(file, imageSize.Size, false);
            var SelectedItemInfo1 = new SelectedItemInfo()
            {
                Url = FileImageUrl
            };
            return Json(SelectedItemInfo1, JsonRequestBehavior.AllowGet);
        }
       

        //public async Task<JsonResult> GetFileInfo(string value)
        //{
        //    var mediafile = await ApiService.MediaFileService.GetMediaFileByUrl(value);
        //    if (mediafile == null)
        //    {
        //        return Json("", JsonRequestBehavior.AllowGet);
        //    }
        //    SelectedItemInfo info = new SelectedItemInfo();
        //    //ImageSize imageSize = GetSizes(file)
        //    //        .FirstOrDefault(size => size.Size == new Size(Convert.ToInt32(split[1]), Convert.ToInt32(split[2])));
        //    return Json(, JsonRequestBehavior.AllowGet);
        //}


    }
}