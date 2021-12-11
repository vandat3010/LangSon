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

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class FAQController : BaseController
    {
        // GET: FAQ 
        // GET: Service
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.FAQ)]
        public async Task<ActionResult> Index(FAQRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 10;
            var result = await ApiService.FAQService.Get(model);
            model.TotalRecords = result.TotalRecords;

            model.TotalPages = result.TotalRecords / model.PageSize + 1;
            ViewBag.Data = result.Data;
            ViewBag.LstFAQCategory = await ApiService.FAQService.GetFaqCategory();
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();

            return View(model);
        }

        [LogTracker(Action.Edit, PageId.FAQ)]
        public async Task<ActionResult> Edit(int Id)
        {
            var faq = await ApiService.FAQService.GetInfo(Id);
            var faqinfo = new FaqInfo()
            {
                Id = faq.Id,
                Answer = faq.Answer,
                Name = faq.Name,
                faq_status = faq.faq_status,
                FaqCategoryId = faq.FaqCategoryId,
                ModifyDate = faq.ModifyDate,
                Short = faq.Short,
                Summary = faq.Summary,
                MetaTitle = faq.MetaTitle,
                MetaDescription = faq.MetaDescription,
                MetaKeywords = faq.MetaKeywords,
                ServiceId = faq.ServiceId,
                OrderNo = faq.OrderNo,
                Name_en = faq.Name_en,
                answer_en = faq.answer_en,
                Type = faq.Type,
                IsActive = faq.IsActive
            };

            faqinfo.IsEdit = 1;
            ViewBag.LstFAQCategory = await ApiService.FAQService.GetFaqCategory();
            return View("Edit", faqinfo);
        }

        [LogTracker(Action.Edit, PageId.FAQ)]
        public async Task<ActionResult> EditThuongGap(int Id)
        {
            var faq = await ApiService.FAQService.GetInfo(Id);
            var faqinfo = new FaqInfo()
            {
                Id = faq.Id,
                Answer = faq.Answer,
                Name = faq.Name,
                faq_status = faq.faq_status,
                FaqCategoryId = faq.FaqCategoryId,
                ModifyDate = faq.ModifyDate,
                Short = faq.Short,
                MetaTitle = faq.MetaTitle,
                MetaDescription = faq.MetaDescription,
                MetaKeywords = faq.MetaKeywords,
                ServiceId = faq.ServiceId,
                OrderNo = faq.OrderNo,
                Name_en = faq.Name_en,
                answer_en = faq.answer_en,
                Type = faq.Type,
                IsActive = faq.IsActive

            };
            faqinfo.IsEdit = 1;
            ViewBag.LstFAQCategory = await ApiService.FAQService.GetFaqCategory();
            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            return View("EditThuongGap", faqinfo);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Edit, PageId.FAQ)]
        public async Task<ActionResult> Update(FaqInfo model)
        {
            try
            {
                model.ModifyDate = DateTime.Now;
                var newInfo = new FaqInfo();
                newInfo.Id = model.Id;
                newInfo.Answer = model.Answer;
                newInfo.FaqCategoryId = model.FaqCategoryId;
                newInfo.Name = model.Name;
                newInfo.ServiceId = model.ServiceId;
                newInfo.OrderNo = model.OrderNo;
                newInfo.Name_en = model.Name_en;
                newInfo.Summary = model.Summary;
                newInfo.answer_en = model.answer_en;
                newInfo.Type = model.Type;
                newInfo.IsActive = model.IsActive;
                newInfo.MetaTitle = model.MetaTitle;
                var setting = await ApiService.SettingService.GetGlobalSetting();
                newInfo.UrlSegment = "/ho-tro/" + SeoExtensions.GetSeName(newInfo.Name);


                var result = await ApiService.FAQService.Update(newInfo);
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

        [LogTracker(Action.Add, PageId.FAQ)]
        public async Task<ActionResult> AddThuongGap()
        {
            FaqInfo item = new FaqInfo();
            item.IsEdit = 0;
            FAQRequestModel model = new FAQRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;

            ViewBag.LstServiceType = SelectListItemExtension.GetEnums<ServiceType>();
            ViewBag.LstFAQCategory = await ApiService.FAQService.GetFaqCategory();
            return View("EditThuongGap", item);
        }


        [LogTracker(Action.Add, PageId.FAQ)]
        public async Task<ActionResult> Add()
        {
            FaqInfo item = new FaqInfo();
            item.IsEdit = 0;
            FAQRequestModel model = new FAQRequestModel();
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;

            ViewBag.LstFAQCategory = await ApiService.FAQService.GetFaqCategory();
            return View("Edit", item);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.FAQ)]
        public async Task<ActionResult> Add(FaqInfo model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Name))
                {
                    return new JsonCamelCaseResult(new { status = 0, msg = "Bạn chưa nhập tên" }, JsonRequestBehavior.AllowGet);
                }
                model.CreatedBy = CurrentUser.Id;
                model.CreatedDate = DateTime.Now;
                FaqInfo faqInfoNew = new FaqInfo();
                faqInfoNew.Answer = model.Answer;
                faqInfoNew.Name = model.Name;
                faqInfoNew.Name_en = model.Name_en;
                faqInfoNew.answer_en = model.answer_en;
                faqInfoNew.faq_status = 1;
                faqInfoNew.Type = model.Type;
                faqInfoNew.FaqCategoryId = model.FaqCategoryId;
                faqInfoNew.ServiceId = model.ServiceId;
                faqInfoNew.OrderNo = model.OrderNo;
                faqInfoNew.IsActive = model.IsActive;
                faqInfoNew.Summary = model.Summary;
                faqInfoNew.MetaTitle = model.MetaTitle;

                var setting = await ApiService.SettingService.GetGlobalSetting();
                faqInfoNew.UrlSegment = "/ho-tro/" + SeoExtensions.GetSeName(faqInfoNew.Name);

                var result = await ApiService.FAQService.Add(faqInfoNew);
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
        [LogTracker(Action.Delete, PageId.FAQ)]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {

                FaqInfo faqInfo = new FaqInfo();
                faqInfo.Id = Id;
                var result = await ApiService.FAQService.Delete(faqInfo);
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
        [LogTracker(Action.Edit, PageId.FAQ)]
        public async Task<ActionResult> KhoiPhucFaq(int Id)
        {
            try
            {

                FaqInfo faqInfo = new FaqInfo();
                faqInfo.Id = Id;
                var result = await ApiService.FAQService.KhoiPhucFaq(faqInfo);
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

 
    }
}