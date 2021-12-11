using Namek.Admin.Utilities;
using Namek.Entity.EntityModel;
using Namek.Entity.InfoModel; 
using Namek.Library.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Namek.Admin.Models.Utility
{
    public static class ImageRenderingExtensions
    {
        public static MvcHtmlString RenderImage(this HtmlHelper helper,MediaFile file, string imageUrl, Size targetSize = default(Size),
             string alt = null, string title = null, object attributes = null)
        {
            return RenderImage1(helper, file, imageUrl, targetSize, alt, title, attributes);
            //return MvcHtmlString.Create("");
        }
        //public static string GetImageUrl(this HtmlHelper helper, string imageUrl, Size targetSize = default(Size))
        //{
        //    return  GetImageUrl(imageUrl, targetSize);
        //    //return "";
        //}

        public static MvcHtmlString RenderImage1(HtmlHelper helper, MediaFile file, string imageUrl, Size targetSize = new Size(), string alt = null,
            string title = null, object attributes = null)
        {
            ImageProcessor imageProcessor = new ImageProcessor();
            string FileImageUrl = imageProcessor.GetUrl(file,  targetSize, true);
            var ImageInfo = new ImageInfo
            {
                Title = title,
                Description = file.Description,
                ImageUrl = FileImageUrl
            };
            if (string.IsNullOrWhiteSpace(imageUrl))
                return MvcHtmlString.Empty;
            //var imageInfo = GetImageInfo(imageUrl, targetSize);
            if (ImageInfo == null)
                return MvcHtmlString.Empty;
            return ReturnTag(ImageInfo, alt, title, attributes);

            //var cachingInfo = _mediaSettings.GetImageTagCachingInfo(imageUrl, targetSize, alt, title, attributes);
            //return helper.GetCached(cachingInfo, htmlHelper =>
            //{
            //    using (new SiteFilterDisabler(_session))
            //    {
            //        if (string.IsNullOrWhiteSpace(imageUrl))
            //            return MvcHtmlString.Empty;

            //        var imageInfo = GetImageInfo(imageUrl, targetSize);
            //        if (imageInfo == null)
            //            return MvcHtmlString.Empty;

                   
            //    }
            //});
        }
        //public static ImageInfo GetImageInfo(string imageUrl, Size targetSize)
        //{
        //    ImageProcessor _imageProcessor = new ImageProcessor();
        //    MediaFile image = await ApiService.MediaFileService.GetMediaFileByUrl(imageUrl);
        //    /*_imageProcessor.GetImage(imageUrl);*/
        //    if (image != null)
        //    {
        //        return new ImageInfo
        //        {
        //            Title = image.Title,
        //            Description = image.Description,
        //            ImageUrl = GetFileImageUrl(image, targetSize)
        //        };
        //    }
        //    return null;
        //}
        //public static string GetFileImageUrl(MediaFile image, Size targetSize)
        //{
        //    IFileRepository _fileRepository = EngineContext.Current.Resolve<IFileRepository>();
        //    return _fileRepository.GetFileLocation(image, targetSize, true);
        //}

        //public static string GetImageUrl(string imageUrl, Size targetSize)
        //{ 
        //    return GetImageInfo(imageUrl, targetSize)?.ImageUrl;
        //}
        private static MvcHtmlString ReturnTag(ImageInfo imageInfo, string alt, string title, object attributes)
        {
            var tagBuilder = new TagBuilder("img");
            tagBuilder.Attributes.Add("src", imageInfo.ImageUrl);
            tagBuilder.Attributes.Add("alt", alt ?? imageInfo.Title);
            tagBuilder.Attributes.Add("title", title ?? imageInfo.Description);
            if (attributes != null)
            {
                var routeValueDictionary = MrCMSHtmlHelperExtensions.AnonymousObjectToHtmlAttributes(attributes);
                foreach (var kvp in routeValueDictionary)
                {
                    tagBuilder.Attributes.Add(kvp.Key, kvp.Value.ToString());
                }
            }
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

    }
}