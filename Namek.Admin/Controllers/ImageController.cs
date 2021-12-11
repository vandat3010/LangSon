using Namek.Admin.AttributeCustom;
using Namek.Admin.Controllers;
using Namek.Admin.Models.Utility;
using Namek.Admin.Utilities;
using Namek.Entity.EntityModel;
using Namek.Entity.InfoModel;
using Namek.Entity.RequestModel;  
using Namek.Library.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Namek.Admin.Controllers
{
    public class ImageController : BaseController
    {
        public ImageController()
        {

        }
        public async Task<JsonResult> GetImageData(string url)
        {
            ImageProcessor _imageProcessor = new ImageProcessor();
            string originalImageUrl = _imageProcessor.GetOriginalImageUrl(url);
            MediaFile image = await ApiService.MediaFileService.GetMediaFileByUrl(originalImageUrl);

            var imageInfo = new ImageInfo();
            //ImageRenderingExtensions.GetImageInfo(url, ImageProcessor.GetRequestedSize(url).GetValueOrDefault());

            FileLocation filel = new FileLocation()
            {
                Image = image,
                TargetSize = ImageProcessor.GetRequestedSize(url).GetValueOrDefault()
            };
            ImageProcessor imageProcessor = new ImageProcessor();
            string FileImageUrl = imageProcessor.GetUrl(image, ImageProcessor.GetRequestedSize(url).GetValueOrDefault(), true);
            if (image != null)
            {

                imageInfo = new ImageInfo
                {
                    Title = image.Title,
                    Description = image.Description,
                    ImageUrl = FileImageUrl
                };
            }


            return imageInfo != null
                ? Json(new { alt = imageInfo.Title, title = imageInfo.Description, url = imageInfo.ImageUrl }, JsonRequestBehavior.AllowGet)
                : Json(new { alt = "", title = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}