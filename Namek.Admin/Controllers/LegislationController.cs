using Namek.Admin.AttributeCustom;
using Namek.Admin.CustomFilters;
using Namek.Admin.Models;
using Namek.Core.ActionResult;
using Namek.Core.Utility;
using Namek.Entity.EntityModel;
using Namek.Entity.InfoModel;
using Namek.Entity.RequestModel;
using Namek.Library.Enums;
using Namek.Library.Infrastructure;
using Namek.Resources.MultiLanguage;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Action = Namek.Library.Enums.Action;
using System.Linq;
using System.IO;
using Namek.Entity.Result;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class LegislationController : BaseController
    {
        // GET: Document 
        // GET: Service
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.Legislation)]
        public async Task<ActionResult> Index(LegislationRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10;
            model.Active = null;
            var result = await ApiService.LegislationService.Get(model);
            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / model.PageSize + 1;
            ViewBag.Data = result.Data;
            ViewBag.LstLegislationCategory = await ApiService.LegislationService.GetLegislationCategory();
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View(model);
        }
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.LegislationAffiliateBanner)]
        public async Task<ActionResult> ListAffiliateBanner(LegislationRequestModel model)
        {

            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;
            model.LegislationCategoryCode = "AffiliateBanner";
            var result = await ApiService.LegislationService.Get(model);
            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / model.PageSize + 1;
            ViewBag.Data = result.Data;
            var LegislationCategory = await ApiService.LegislationService.GetLegislationCategory();
            if (LegislationCategory.Count > 0)
            {
                LegislationCategory = LegislationCategory.Where(t => t.Code == "AffiliateBanner").ToList();
            }
            ViewBag.LstLegislationCategory = LegislationCategory;
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View(model);
        }
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.LegislationAffiliateEmail)]
        public async Task<ActionResult> ListAffiliateEmail(LegislationRequestModel model)
        {

            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;
            model.LegislationCategoryCode = "AffiliateEmail";
            var result = await ApiService.LegislationService.Get(model);
            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / model.PageSize + 1;
            ViewBag.Data = result.Data;
            var LegislationCategory = await ApiService.LegislationService.GetLegislationCategory();
            if (LegislationCategory.Count > 0)
            {
                LegislationCategory = LegislationCategory.Where(t => t.Code == "AffiliateBanner").ToList();
            }
            ViewBag.LstLegislationCategory = LegislationCategory;
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View(model);
        }

        [LogTracker(Action.Edit, PageId.Legislation)]
        public async Task<ActionResult> Edit(int Id)
        {
            var Legislation = await ApiService.LegislationService.GetInfo(Id);
            var Legislationtinfo = new LegislationInfo()
            {
                Id = Legislation.Id,
                Answer = Legislation.Answer,
                Name = Legislation.Name,
                legislation_status = Legislation.legislation_status,
                LegislationCategoryId = Legislation.LegislationCategoryId,
                ModifyDate = Legislation.ModifyDate,
                Short = Legislation.Short,
                MetaTitle = Legislation.MetaTitle,
                MetaDescription = Legislation.MetaDescription,
                MetaKeywords = Legislation.MetaKeywords,
                //ServiceId = Document.ServiceId,
                OrderNo = Legislation.OrderNo,
                Name_en = Legislation.Name_en,
                answer_en = Legislation.answer_en,
                Type = Legislation.Type,
                IsActive = Legislation.IsActive.Value,
                MetaTitle_En = Legislation.MetaTitle_En,
                MetaDescription_En = Legislation.MetaDescription_En,
                MetaKeywords_En = Legislation.MetaKeywords_En,
                UrlSegment = Legislation.UrlSegment,
                DateIssued= Legislation.DateIssued,
                DateEffect= Legislation.DateEffect
            };
            Legislationtinfo.IsEdit = 1;
            var LegislationCategory = await ApiService.LegislationService.GetLegislationCategory();
            ViewBag.LstLegislationCategory = LegislationCategory;
            return View("Edit", Legislationtinfo);
        }

        [LogTracker(Action.Edit, PageId.LegislationAffiliateBanner)]
        public async Task<ActionResult> EditAffiliateBanner(int Id)
        {
            var Legislation = await ApiService.LegislationService.GetInfo(Id);
            var Legislationinfo = new LegislationInfo()
            {
                Id = Legislation.Id,
                Answer = Legislation.Answer,
                Name = Legislation.Name,
                legislation_status = Legislation.legislation_status,
                LegislationCategoryId = Legislation.LegislationCategoryId,
                ModifyDate = Legislation.ModifyDate,
                Short = Legislation.Short,
                MetaTitle = Legislation.MetaTitle,
                MetaDescription = Legislation.MetaDescription,
                MetaKeywords = Legislation.MetaKeywords,
                //ServiceId = Document.ServiceId,
                OrderNo = Legislation.OrderNo,
                Name_en = Legislation.Name_en,
                answer_en = Legislation.answer_en,
                Type = Legislation.Type,
                IsActive = Legislation.IsActive.Value,
                MetaTitle_En = Legislation.MetaTitle_En,
                MetaDescription_En = Legislation.MetaDescription_En,
                MetaKeywords_En = Legislation.MetaKeywords_En,
                UrlSegment = Legislation.UrlSegment
            };
            Legislationinfo.IsEdit = 1;
            var LegislationCategory = await ApiService.LegislationService.GetLegislationCategory();
            if (LegislationCategory.Count > 0)
            {
                LegislationCategory = LegislationCategory.Where(t => t.Code == "AffiliateBanner").ToList();
            }
            ViewBag.LstLegislationCategory = LegislationCategory;

            return View("EditAffiliateBanner", Legislationinfo);
        }
        [LogTracker(Action.Edit, PageId.LegislationAffiliateBanner)]
        public async Task<ActionResult> EditAffiliateEmail(int Id)
        {
            var Legislation = await ApiService.LegislationService.GetInfo(Id);
            var Legislationinfo = new LegislationInfo()
            {
                Id = Legislation.Id,
                Answer = Legislation.Answer,
                Name = Legislation.Name,
                legislation_status = Legislation.legislation_status,
                LegislationCategoryId = Legislation.LegislationCategoryId,
                ModifyDate = Legislation.ModifyDate,
                Short = Legislation.Short,
                MetaTitle = Legislation.MetaTitle,
                MetaDescription = Legislation.MetaDescription,
                MetaKeywords = Legislation.MetaKeywords,
                //ServiceId = Document.ServiceId,
                OrderNo = Legislation.OrderNo,
                Name_en = Legislation.Name_en,
                answer_en = Legislation.answer_en,
                Type = Legislation.Type,
                IsActive = Legislation.IsActive.Value,
                MetaTitle_En = Legislation.MetaTitle_En,
                MetaDescription_En = Legislation.MetaDescription_En,
                MetaKeywords_En = Legislation.MetaKeywords_En,
                UrlSegment = Legislation.UrlSegment
            };
            Legislationinfo.IsEdit = 1;
            var LegislationCategory = await ApiService.LegislationService.GetLegislationCategory();
            if (LegislationCategory.Count > 0)
            {
                LegislationCategory = LegislationCategory.Where(t => t.Code == "AffiliateEmail").ToList();
            }
            ViewBag.LstLegislationCategory = LegislationCategory;

            return View("EditAffiliateEmail", Legislationinfo);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.Legislation)]
        public async Task<ActionResult> Update()
        {
            try
            {
                //DocumentInfo model

                var jsonData = Request.Form["jsonData"];

                var model = JsonConvert.DeserializeObject<LegislationInfo>(jsonData, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

                var setting = await ApiService.SettingService.GetGlobalSetting();
                if (string.IsNullOrEmpty(model.UrlSegment))
                {
                    model.UrlSegment = "/van-ban-phap-luat/" + SeoExtensions.GetSeName(model.Name);
                }

                for (int i = 0; i < Request.Files.AllKeys.Length; i++)
                {
                    if (Request.Files.AllKeys[i] == "filepdf")
                    {
                        //var file0 = Request.Files["filepdf"];
                        var file0 = Request.Files[i];
                        if (file0 != null)
                        {
                            //ghi file
                            var kq0 = GhiFile(file0);
                            if (kq0.Status < 1)
                            {
                                return new JsonCamelCaseResult(new { status = 0, msg = kq0.Msg }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                model.MetaDescription_En = kq0.Msg;
                            }

                        }

                    }
                }

                model.ModifyDate = DateTime.Now;
                var newInfo = new LegislationInfo();
                newInfo.Id = model.Id;
                newInfo.Answer = model.Answer;
                newInfo.LegislationCategoryId = model.LegislationCategoryId;
                newInfo.Name = model.Name;
                newInfo.ServiceId = model.ServiceId;
                newInfo.OrderNo = model.OrderNo;
                newInfo.Name_en = model.Name_en;
                newInfo.answer_en = model.answer_en;
                newInfo.Type = model.Type;
                newInfo.IsActive = model.IsActive;

                newInfo.MetaTitle = model.MetaTitle;
                newInfo.MetaDescription = model.MetaDescription;
                newInfo.MetaKeywords = model.MetaKeywords;
                newInfo.MetaTitle_En = model.MetaTitle_En;
                newInfo.MetaDescription_En = model.MetaDescription_En;
                newInfo.MetaKeywords_En = model.MetaKeywords_En;
                newInfo.UrlSegment = model.UrlSegment;
                newInfo.DateIssued = model.DateIssued;

                //người ký
                newInfo.MetaTitle = model.MetaTitle;
                // cơ quan ban hành
                newInfo.MetaDescription = model.MetaDescription;

                newInfo.DateEffect = model.DateEffect;

                var result = await ApiService.LegislationService.Update(newInfo);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ServiceManagementUpdateFailed) }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceUpdatedSuccessfully) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [LogTracker(Action.Add, PageId.Legislation)]
        public async Task<ActionResult> Add()
        {
            LegislationInfo item = new LegislationInfo();
            item.IsEdit = 0;
            LegislationRequestModel model = new LegislationRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;

            ViewBag.LstLegislationCategory = await ApiService.LegislationService.GetLegislationCategory();
            return View("Edit", item);
        }
        [LogTracker(Action.Add, PageId.LegislationAffiliateBanner)]
        public async Task<ActionResult> AddAffiliateBanner()
        {
            LegislationInfo item = new LegislationInfo();
            item.IsEdit = 0;
            LegislationRequestModel model = new LegislationRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;

            var LegislationCategory = await ApiService.LegislationService.GetLegislationCategory();
            if (LegislationCategory.Count > 0)
            {
                LegislationCategory = LegislationCategory.Where(t => t.Code == "AffiliateBanner").ToList();
            }
            ViewBag.LstLegislationCategory = LegislationCategory;
            return View("EditAffiliateBanner", item);
        }
        [LogTracker(Action.Add, PageId.LegislationAffiliateEmail)]
        public async Task<ActionResult> AddAffiliateEmail()
        {
            LegislationInfo item = new LegislationInfo();
            item.IsEdit = 0;
            LegislationRequestModel model = new LegislationRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;
            var LegislationCategory = await ApiService.LegislationService.GetLegislationCategory();
            if (LegislationCategory.Count > 0)
            {
                LegislationCategory = LegislationCategory.Where(t => t.Code == "AffiliateEmail").ToList();
            }
            ViewBag.LstLegislationCategory = LegislationCategory;
            return View("EditAffiliateEmail", item);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.Legislation)]
        [ValidateInput(false)]

        public async Task<ActionResult> Insert()
        {

            var jsonData = Request.Params["jsonData"];
            //DocumentInfo model
            var model = JsonConvert.DeserializeObject<LegislationInfo>(jsonData, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });

            try
            {

                for (int i = 0; i < Request.Files.AllKeys.Length; i++)
                {
                    if (Request.Files.AllKeys[i] == "filepdf")
                    {
                        //var file0 = Request.Files["filepdf"];
                        var file0 = Request.Files[i];
                        if (file0 != null)
                        {
                            //ghi file
                            var kq0 = GhiFile(file0);
                            if (kq0.Status < 1)
                            {
                                return new JsonCamelCaseResult(new { status = 0, msg = kq0.Msg }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                model.MetaDescription_En = kq0.Msg;
                            }

                        }

                    }
                }


                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.UrlSegment))
                {
                    model.UrlSegment = "/van-ban-phap-luat/" + SeoExtensions.GetSeName(model.Name);
                }

                model.CreatedBy = CurrentUser.Id;
                model.CreatedDate = DateTime.Now;
                LegislationInfo LegislationInfoNew = new LegislationInfo();
                LegislationInfoNew.Answer = model.Answer;
                LegislationInfoNew.Name = model.Name;
                LegislationInfoNew.Name_en = model.Name_en;
                LegislationInfoNew.answer_en = model.answer_en;
                LegislationInfoNew.legislation_status = 1;
                LegislationInfoNew.Type = model.Type;
                LegislationInfoNew.LegislationCategoryId = model.LegislationCategoryId;
                LegislationInfoNew.ServiceId = model.ServiceId;
                LegislationInfoNew.OrderNo = model.OrderNo;
                LegislationInfoNew.IsActive = model.IsActive;
                LegislationInfoNew.MetaDescription_En = model.MetaDescription_En;
                LegislationInfoNew.DateIssued = model.DateIssued;
                LegislationInfoNew.UrlSegment = model.UrlSegment;
                //người ký
                LegislationInfoNew.MetaTitle = model.MetaTitle;
                // cơ quan ban hành
                LegislationInfoNew.MetaDescription = model.MetaDescription;

                LegislationInfoNew.DateEffect = model.DateEffect;
                var result = await ApiService.LegislationService.Add(LegislationInfoNew);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thất bại" }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Delete, PageId.Legislation)]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {

                LegislationInfo LegislationInfo = new LegislationInfo();
                LegislationInfo.Id = Id;
                var result = await ApiService.LegislationService.Delete(LegislationInfo);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }


        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.Legislation)]
        public async Task<ActionResult> KhoiPhucLegislation(int Id)
        {
            try
            {

                LegislationInfo LegislationInfo = new LegislationInfo();
                LegislationInfo.Id = Id;
                var result = await ApiService.LegislationService.KhoiPhucLegislation(LegislationInfo);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }

       
        #region ghi file 
        public MesssageInfo GhiFile(HttpPostedFileBase file)
        {
            try
            {

                string filename = "";
                var sFullPath = "";

                if ((file.FileName.Contains(".pdf") || file.FileName.Contains(".jpeg") || file.FileName.Contains(".png") || file.FileName.Contains(".jdf")) && file.ContentLength >= 838860800)
                {
                    return new MesssageInfo() { Status = 0, Msg = "File có dung lượng quá lớn không thể Import (>100mb)" };
                }
                else if ((file.FileName.Contains(".mpeg-4") || file.FileName.Contains(".mpeg") || file.FileName.Contains(".avi") || file.FileName.Contains(".wmv")) && file.ContentLength >= 12288000)
                {
                    return new MesssageInfo() { Status = 0, Msg = "File có dung lượng quá lớn không thể Import (>1500 kbps)" };
                }
                else if ((file.FileName.Contains(".mp3") || file.FileName.Contains(".wma")) && file.ContentLength >= 1048576)
                {
                    return new MesssageInfo() { Status = 0, Msg = "File có dung lượng quá lớn không thể Import (>128 kbps)" };
                }
                else if ((file.FileName.Contains(".doc")|| file.FileName.Contains(".docx")))
                {
                    return new MesssageInfo() { Status = 0, Msg = "Không hỗ trợ lưu file này trên hệ thống" };
                }    
                //else if (file.ContentLength >= 209715200)
                //{
                //    return new MesssageInfo() { code = 0, message = "File có dung lượng quá lớn không thể Import (>20mb)" };

                //}
                string fileName = file.FileName.Replace("<", "").Replace(">", "").Replace("'", "");

                //if (!(fileName.ToLower().EndsWith(".pdf") || fileName.ToLower().EndsWith(".xls") || fileName.ToLower().EndsWith(".xlsx")) || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)//kiểm tra định dạng file
                //    return new MesssageInfo() { code = 0, message = "Định dạng file không hợp lệ!" };

                // copy file vào folder
                string thuMucLuuTru = System.Configuration.ConfigurationManager.AppSettings["RootFolderUpload"] + "";
                if (thuMucLuuTru == "")
                {
                    //lay giatrimacdinh
                    thuMucLuuTru = "D:/FilesManager";
                    // chưa cấu hình thư mục lưu trữ
                }

                string sFileFolder = Path.Combine(thuMucLuuTru);
                var folderInfo = new DirectoryInfo(sFileFolder);
                if (!folderInfo.Exists)
                {
                    folderInfo.Create();
                }
                string newName = "/" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileName;
                newName = LocDau(newName);
                filename = LocDau(newName);
                //sFullPath = Path.Combine(sFileFolder, newName);
                sFullPath = sFileFolder + newName;
                if (System.IO.File.Exists(sFullPath))
                    System.IO.File.Delete(sFullPath);
                file.SaveAs(sFullPath);//luu file lên server
                return new MesssageInfo() { Status = 1, Msg = newName };
            }
            catch (Exception ex)
            {
                return new MesssageInfo() { Status = 0, Msg = "Lỗi trong quá trình ghi file" };
            }
        }
        private readonly string[] VietNamChar = new string[]
        {
        "aAeEoOuUiIdDyY",
        "áàạảãâấầậẩẫăắằặẳẵ",
        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        "éèẹẻẽêếềệểễ",
        "ÉÈẸẺẼÊẾỀỆỂỄ",
        "óòọỏõôốồộổỗơớờợởỡ",
        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        "úùụủũưứừựửữ",
        "ÚÙỤỦŨƯỨỪỰỬỮ",
        "íìịỉĩ",
        "ÍÌỊỈĨ",
        "đ",
        "Đ",
        "ýỳỵỷỹ",
        "ÝỲỴỶỸ"
        };
        public string LocDau(string str)
        {
            //Thay thế và lọc dấu từng char      
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }

        public FileResult DownloadFileAttach(string FileName = "")
        {
            var sFullPath = "";
            string thuMucLuuTru = System.Configuration.ConfigurationManager.AppSettings["RootFolderUpload"] + "";
            if (thuMucLuuTru == "")
            {
                //lay giatrimacdinh
                thuMucLuuTru = "D:/FilesManager";
                // chưa cấu hình thư mục lưu trữ
            }

            string sFileFolder = Path.Combine(thuMucLuuTru);
            var folderInfo = new DirectoryInfo(sFileFolder);
            if (!folderInfo.Exists)
            {
                folderInfo.Create();
            }
            sFullPath = sFileFolder + FileName;
            string fileName = FileName;
            var fileBytes = System.IO.File.ReadAllBytes(sFullPath);
             
            if (sFullPath.Contains(".pdf"))
            {
                return File(fileBytes, "application/pdf");
            }
            else if (sFullPath.Contains(".jpeg"))
            {
                return File(fileBytes, "image/jpeg");
            }
            else if (sFullPath.Contains(".png"))
            {
                return File(fileBytes, "image/png");
            }
            else if (sFullPath.Contains(".jdf"))
            {
                return File(fileBytes, "image/jdf");
            }

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public FileResult DownloadFileAttach2(string FileName = "")
        {
            var sFullPath = "";
            string thuMucLuuTru = System.Configuration.ConfigurationManager.AppSettings["RootFolderUpload"] + "";
            if (thuMucLuuTru == "")
            {
                //lay giatrimacdinh
                thuMucLuuTru = "D:/FilesManager";
                // chưa cấu hình thư mục lưu trữ
            }

            string sFileFolder = Path.Combine(thuMucLuuTru);
            var folderInfo = new DirectoryInfo(sFileFolder);
            if (!folderInfo.Exists)
            {
                folderInfo.Create();
            }
            sFullPath = sFileFolder + FileName;
            string fileName = FileName;
            var fileBytes = System.IO.File.ReadAllBytes(sFullPath);

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        #endregion
    }
}