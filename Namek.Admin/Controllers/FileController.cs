using Namek.Admin.AttributeCustom;
using Namek.Admin.Controllers;
using Namek.Admin.Models.Utility;
using Namek.Admin.Utilities;
using Namek.Core.Utility;
using Namek.Entity.EntityBase;
using Namek.Entity.EntityBase.Media;
using Namek.Entity.EntityModel;
using Namek.Entity.RequestModel;
using Namek.Entity.Result; 
using Namek.Library.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Namek.Admin.Controllers
{
    public class FileController : BaseController
    {
        //private readonly IFileRepository _fileRepository = EngineContext.Current.Resolve<IFileRepository>();

        [HttpPost]
        [ActionName("Files")]
        public async Task<JsonResult> Files_Post(MediaCategory mediaCategory)
        {
            var list = new List<ViewDataUploadFilesResult>();
            ImageProcessor _imageProcessor = new ImageProcessor();
            FileSystem _fileSystem = new FileSystem();
            MediaSettings _mediaSettings = new MediaSettings();
           //var setting = await ApiService.SettingService.GetGlobalSetting();
            foreach (string files in Request.Files)
            {
                var file1 = Request.Files[files];
                var stream = file1.InputStream;


                var FileName = file1.FileName.Replace(" ","-");


                if (IsValidFileType(FileName))
                {
                    //byte[] byteStream = null;
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    file.InputStream.CopyTo(ms);
                    //    byteStream = ms.ToArray();
                    //}

                    //save file 
                    string MediaCategoryName = "";
                    if (mediaCategory.Id > 0)
                    {
                        var newcategory = await ApiService.NewsCategoryService.GetInfo(mediaCategory.Id);
                        if (newcategory != null)
                        {
                            MediaCategoryName = newcategory.Name;

                        }
                    }
                    MediaFile mediafile = new MediaFile()
                    { };

                    var FileExtension = Path.GetExtension(FileName);
                    if (_imageProcessor.IsImage(FileExtension))
                    {
                        _imageProcessor.EnforceMaxSize(ref stream,  _mediaSettings);
                       _imageProcessor.SetFileDimensions(mediafile, stream);
                    } 
                    FileModel filemodel = new FileModel()
                    {
                        
                        //byteStream = byteStream,
                      
                        FileName = FileName,
                        ContentType = file1.ContentType,
                        ContentLength = (long)stream.Length,
                        MediaCategoryId = mediaCategory.Id,
                        Width= mediafile.Width,
                        Height= mediafile.Height
                    };

                   

                  


                    string fileLocation = _imageProcessor.GetFileLocation(FileName, MediaCategoryName, mediaCategory.Id);
                    var FileUrl = _fileSystem.SaveFile(stream, fileLocation, "" + ((long)stream.Length));
                    filemodel.FileUrl = FileUrl;
                    stream.Dispose();

                   
                    var mediaFile = await ApiService.FileService.AddFile(filemodel);
                    ViewDataUploadFilesResult dbFile = new ViewDataUploadFilesResult(mediaFile);
                    dbFile.is_image = MediaFileExtensions.IsImage(mediaFile);
                    list.Add(dbFile);
                }
            }
            return Json(list.ToArray(), "text/html", Encoding.UTF8);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> Delete_POST(MediaFile file)
        {
            int categoryId = file.MediaCategoryId.Value;
            await ApiService.FileService.Delete_POST(file);

            FileSystem _fileSystem = new FileSystem();

            _fileSystem.Delete(file.FileUrl);


            return RedirectToAction("Show", "MediaCategory", new { Id = categoryId });
        }

        [HttpGet]
        public ActionResult Delete(MediaFile file)
        {
            return View("Delete", file);
        }

        [HttpPost]
        public async Task<string> UpdateSEO(MediaFile mediaFile, string title, string description)
        {
            try
            {
                mediaFile.Title = title;
                mediaFile.Description = description;
                await ApiService.FileService.SaveFile(mediaFile);

                return "Changes saved";
            }
            catch (Exception ex)
            {
                return string.Format("There was an error saving the SEO values: {0}", ex.Message);
            }
        }

        public ActionResult Edit(MediaFile file)
        {
            return View("Edit", file);
        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<ActionResult> Edit_POST(MediaFile file)
        {
            await ApiService.FileService.SaveFile(file);

            return file.MediaCategoryId != null
                ? RedirectToAction("Show", "MediaCategory", new { file.MediaCategoryId })
                : RedirectToAction("Index", "MediaCategory");
        }


        public bool IsValidFileType(string fileName)
        {

            string extension = Path.GetExtension(fileName);
            if (string.IsNullOrWhiteSpace(extension) || extension.Length < 1)
                return false;
            return Utils.FileTypeUploadSettings.AllowedFileTypeList.Contains(extension, StringComparer.OrdinalIgnoreCase);


            //string extension = Path.GetExtension(fileName);
            //if (string.IsNullOrWhiteSpace(extension) || extension.Length < 1)
            //    return false;
            ////Kiểm tra có phải đuôi ảnh không
            //ImageProcessor imageProcessor = new ImageProcessor();
            //if (!imageProcessor.IsImage(extension))
            //{
            //    return false;
            //}
            ////return true;
            //return Utils.FileTypeUploadSettings.AllowedFileTypeList.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
    }
}