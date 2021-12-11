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
using Newtonsoft.Json;
using System.Linq;
using Namek.Infrastructure.DbContext;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class NewsController : BaseController
    {
        [LogTracker(Action.View, PageId.News)]
        [HttpGet]
        public async Task<ActionResult> Index(NewsArticleRequestModel model)
        {
            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();

            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10;

            //if (!model.StartNewDate.HasValue)
            //{
            //    model.StartNewDate = FirstDayOfYear(DateTime.Now);
            //}
            //if (!model.EndNewDate.HasValue)
            //{
            //    model.EndNewDate = DateTime.Now;
            //}
            model.UserId = CurrentUser.Id;
            var result = await ApiService.NewsArticleService.Get(model);
            var newsArrticleCategory = await ApiService.NewsArticleService.GetNewsArticleCategories();
            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / model.PageSize + 1;
            ViewBag.Data = result.Data;
            ViewBag.newsArrticleCategory = newsArrticleCategory;
            return View(model);
        }

        [LogTracker(Action.Add, PageId.News)]
        public async Task<ActionResult> Add()
        {
            NewsArticleInfo item = new NewsArticleInfo();

            NewsArticleRequestModel model = new NewsArticleRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;
            var lstNewsArticle = await ApiService.NewsArticleService.Get(model);
            ViewBag.LstNewsArticle = lstNewsArticle.Data;

            ViewBag.LstWebsite = SelectListItemExtension.GetEnums<WebsitePositionEnum>();
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();

            ViewBag.LstTag = await ApiService.TagService.GetAll();


            return View("Edit", item);
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.News)]
        public async Task<ActionResult> Add(NewsArticleInfo model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }
                var setting = await ApiService.SettingService.GetGlobalSetting();
                model.CreatedBy = CurrentUser.Id;
                model.CreatedDate = DateTime.Now;
                model.UrlSegment = "/tin-tuc/" + SeoExtensions.GetSeName(model.Name);
                int result = await ApiService.NewsArticleService.AddAndReturnId(model);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thất bại" }, JsonRequestBehavior.AllowGet);
                }
                if(!string.IsNullOrEmpty(model.Content_En))
                {
                    var _news = await ApiService.NewsArticleService.GetInfo(result);
                    var _link = await GetLinkAudioAsync(model.Content_En);
                    string DataJson = JsonConvert.SerializeObject(_link);
                    _news.AudioFile = DataJson;
                    var resul = await ApiService.NewsArticleService.UpdateAudio(_news);
                }    
                return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }

        [LogTracker(Action.Edit, PageId.News)]
        public async Task<ActionResult> Edit(int Id)
        {
            NewsArticleInfo item = await ApiService.NewsArticleService.GetInfo(Id);

            NewsArticleRequestModel model = new NewsArticleRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;
            var lstNewsArticle = await ApiService.NewsArticleService.Get(model);
            ViewBag.LstNewsArticle = lstNewsArticle.Data;
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            ViewBag.LstWebsite = SelectListItemExtension.GetEnums<WebsitePositionEnum>();

            //var dataNewsCategory = await ApiService.NewsCategoryService.GetAll();
            //var LstNewsCategory = dataNewsCategory.Select(t => new SelectListItem
            //{
            //    Value = t.Id.ToString(),
            //    Text = t.Name,
            //    Selected = item.LstNewsCategoryId.Contains(t.Id) ? true : false
            //});

            //ViewBag.LstNewsCategory = LstNewsCategory;

            //var dataTag = await ApiService.TagService.GetAll();
            //var LstTagId = dataTag.Select(t => new SelectListItem
            //{
            //    Value = t.Id.ToString(),
            //    Text = t.Name,
            //    Selected = item.LstTagId.Contains(t.Id) ? true : false
            //});

            //ViewBag.LstTag = LstTagId;
            ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();
            ViewBag.LstTag = await ApiService.TagService.GetAll();


            return View("Edit", item);
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.News)]
        public async Task<ActionResult> Update(NewsArticleInfo model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }
                var setting = await ApiService.SettingService.GetGlobalSetting();
                model.UrlSegment = "/tin-tuc/" + SeoExtensions.GetSeName(model.Name);
                model.ModifyBy = CurrentUser.Id;
                model.ModifyOn = DateTime.Now;
                //if (model.IsCreateFileAudio)
                //{

                //    var _news = await ApiService.NewsArticleService.GetInfo(model.Id);
                //    if (!string.IsNullOrEmpty(_news.Content_En))
                //    {
                //        var _link = await GetLinkAudioAsync(_news.Content_En);
                //        string DataJson = JsonConvert.SerializeObject(_link);
                //        _news.AudioFile = DataJson;
                //        var resul = await ApiService.NewsArticleService.UpdateAudio(_news);
                //    }


                //}
                //kiểm tra contet audio trong model có khác so với database hay không
                bool IsCreateFileAudio = false;
                if(!string.IsNullOrEmpty(model.Content_En))
                {
                    var _news = await ApiService.NewsArticleService.GetInfo(model.Id);
                    if (!string.IsNullOrEmpty(_news.Content_En))
                    {
                        //trong db đã có dữ liệu
                        if(!model.Content_En.Equals(_news.Content_En))
                        {
                            //tạo file âm thanh
                            IsCreateFileAudio = true;
                        }    
                    }
                    else
                    {
                        //trong db không có dữ liệu
                        //tạo file âm thanh
                        IsCreateFileAudio = true;
                    }
                    if (IsCreateFileAudio)
                    {
                        var _link = await GetLinkAudioAsync(model.Content_En);
                        string DataJson = JsonConvert.SerializeObject(_link);
                        _news.AudioFile = DataJson;
                        var resul = await ApiService.NewsArticleService.UpdateAudio(_news);
                    }
                }
                var result = await ApiService.NewsArticleService.Update(model);
                
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ServiceManagementUpdateFailed) }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceUpdatedSuccessfully) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Delete, PageId.News)]
        public async Task<ActionResult> Delete(NewsArticleInfo model)
        {
            try
            {

                var result = await ApiService.NewsArticleService.Delete(model);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.SystemResourcesSuccessfullyDeleted) }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }
        public static DateTime FirstDayOfYear(DateTime y)
        {
            return new DateTime(y.Year, 1, 1);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.NewsCategoryApprove)]
        public async Task<ActionResult> Approve(NewsArticleInfo model)
        {
            try
            {
                //lấy thông info
                var NewsArti = await ApiService.NewsArticleService.GetInfo(model.Id);
                if (NewsArti!=null && NewsArti.Id>0 &&NewsArti.Status==1 )
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bài viết đã được phê duyệt" }, JsonRequestBehavior.AllowGet);
                }
                var setting = await ApiService.SettingService.GetGlobalSetting(); 
                model.ApprovedBy = CurrentUser.Id;
                model.ModifyOn = DateTime.Now;
                model.Status = 1;
                model.ApprovedDate = DateTime.Now;
                var result = await ApiService.NewsArticleService.Approve(model);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ServiceManagementUpdateFailed) }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = "Phê duyệt bài viết thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public async Task<ActionResult> CreateAudio(int Id)
        {
            var _news = await ApiService.NewsArticleService.GetInfo(Id);
            if(string.IsNullOrEmpty(_news.Content_En))
            {
                return new JsonCamelCaseResult(new { status = 0, msg = "Nội dung tạo âm thanh không tồn tại" }, JsonRequestBehavior.AllowGet);
            }    
            var _link = await GetLinkAudioAsync(_news.Content_En);
            string DataJson = JsonConvert.SerializeObject(_link);
            _news.AudioFile = DataJson;
            var result = await ApiService.NewsArticleService.UpdateAudio(_news);
            if (result == 0)
            {
                return new JsonCamelCaseResult(new { status = result, msg = "Thêm âm thanh thất bại" }, JsonRequestBehavior.AllowGet);
            }
            return new JsonCamelCaseResult(new { status = result, msg = "Thêm âm thanh thành công" }, JsonRequestBehavior.AllowGet);
        }


        public async Task<List<AudioInfo>> GetLinkAudioAsync(string input_text)
        {
            string APPIDKEY = (string)ConfigurationManager.AppSettings["APPIDKEY"];
            string SECRETKEY = (string)ConfigurationManager.AppSettings["SECRETKEY"];
            string UrlTTS = (string)ConfigurationManager.AppSettings["UrlTTS"];
            List<AudioInfo> _lVoice = new List<AudioInfo>();
            _lVoice.Add(new AudioInfo() { Id = "sound_1", Name = "Giọng nam miền bắc", Voice = "hn_male_manhdung_news_48k-d" });
            //_lVoice.Add(new AudioInfo() { Id = "sound_2", Name = "Giọng nữ miền bắc", Voice = "hn_female_ngochuyen_news_48k-thg" });
            _lVoice.Add(new AudioInfo() { Id = "sound_3", Name = "Giọng nữ miền nam", Voice = "sg_female_thaotrinh_news_48k-d" });

            foreach (var item in _lVoice)
            {
                CookieContainer cookie = new CookieContainer();
                HttpClientHandler handler = new HttpClientHandler
                {
                    CookieContainer = cookie,
                    ClientCertificateOptions = ClientCertificateOption.Automatic,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    AllowAutoRedirect = true,
                    UseDefaultCredentials = false,
                };
                var client = new HttpClient(handler);
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, UrlTTS);
                MobifoneTTS _ttsObj = new MobifoneTTS("link", APPIDKEY, SECRETKEY, item.Voice, "1", "1", input_text);
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(_ttsObj), Encoding.UTF8, "application/json");
                var response = await client.SendAsync(requestMessage);
                var _dataResponse = await response.Content.ReadAsStringAsync();
                ResponseMobifoneTTS result = JsonConvert.DeserializeObject<ResponseMobifoneTTS>(_dataResponse);
                if (result.error.Equals("0"))
                {
                    item.DataReturn = result.link;
                }

            }
            return _lVoice;
        }



        #region affiliate tin tức
        [LogTracker(Action.View, PageId.News)]
        [HttpGet]
        public async Task<ActionResult> ListAffiliate(NewsArticleRequestModel model)
        {
            var LstNewsCategory = await ApiService.NewsCategoryService.GetAll();

            if (LstNewsCategory.Count > 0)
            {
                LstNewsCategory = LstNewsCategory.Where(t => t.Code == "Affiliate").ToList();


            }
            model.LstNewsCategoryId = LstNewsCategory.Select(t => t.Id).ToList();

            ViewBag.LstNewsCategory = LstNewsCategory;


            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;

            if (!model.StartNewDate.HasValue)
            {
                model.StartNewDate = FirstDayOfYear(DateTime.Now);
            }
            if (!model.EndNewDate.HasValue)
            {
                model.EndNewDate = DateTime.Now;
            }

            var result = await ApiService.NewsArticleService.Get(model);

            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / model.PageSize + 1;
            ViewBag.Data = result.Data;

            return View(model);
        }


        [LogTracker(Action.Edit, PageId.News)]
        public async Task<ActionResult> EditAffiliate(int Id = 0)
        {
            NewsArticleInfo item = await ApiService.NewsArticleService.GetInfo(Id);
            if (item == null)
            {
                item = new NewsArticleInfo();
            }


            NewsArticleRequestModel model = new NewsArticleRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            ViewBag.LstWebsite = SelectListItemExtension.GetEnums<WebsitePositionEnum>();



            var LstNewsCategory = await ApiService.NewsCategoryService.GetAll();

            if (LstNewsCategory.Count > 0)
            {
                LstNewsCategory = LstNewsCategory.Where(t => t.Code == "Affiliate").ToList();


            }

            ViewBag.LstNewsCategory = LstNewsCategory;
            ViewBag.LstTag = await ApiService.TagService.GetAll();


            return View("EditAffiliate", item);
        }
        #endregion
    }
}