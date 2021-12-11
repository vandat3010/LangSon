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
    public class MediaFileController : BaseController
    {
        // GET: Service
        [Authorize]
        [AuthenticateUser]
        [LogTracker(Action.View, PageId.Media)]
        public async Task<ActionResult> Index(GroupPageRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 1000;

            model.ActionName = SelectListItemExtension.GetEnums<ViewEnum>().Where(x => x.Id == (int)ViewEnum.HomeMedia).FirstOrDefault().Name;

            var result = await ApiService.GroupPageService.Get(model);
            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / result.PageSize + 1;
            ViewBag.Data = result.Data;

            var lstDistrict = ApiService.GroupPageService.GetAllDistrict();
            model.lstHuyen = lstDistrict.ConvertAll(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name.ToString(),
            });
            return View(model);
        }

        [LogTracker(Action.Edit, PageId.Media)]
        public async Task<ActionResult> Edit(int id)
        {
            GroupPageInfo item = await ApiService.GroupPageService.GetInfo(id);
            var lstDistrict = ApiService.GroupPageService.GetAllDistrict();
            item.lstHuyen = lstDistrict.ConvertAll(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name.ToString(),
            });
            return View("Edit", item);
        }

        [LogTracker(Action.Add, PageId.Media)]
        public async Task<ActionResult> Add()
        {
            //MediaFileInfo item = new MediaFileInfo();
            //item.IsEdit = 0;
            //MediaFileRequestModel model = new MediaFileRequestModel();
            //model.Page = model.Page == 0 ? 1 : model.Page;
            //model.PageSize = 20;
            //var lstMedia = await ApiService.MediaFileService.Get(model);
            //ViewBag.lstMedia = lstMedia.Data;
            //ViewBag.LstNewsCategory = await ApiService.NewsCategoryService.GetAll();
            GroupPageInfo item = new GroupPageInfo();
            var lstDistrict = ApiService.GroupPageService.GetAllDistrict();
            item.lstHuyen = lstDistrict.ConvertAll(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.Name.ToString(),
            });
            return View("Edit", item);
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Add, PageId.Media)]
        public async Task<ActionResult> Add(MediaFileInfo model)
        {
            try
            {
                MediaFileInfo mediaInfoNew = new MediaFileInfo();
                mediaInfoNew.FileName = model.FileName;

                var result = await ApiService.MediaFileService.Add(mediaInfoNew);
                var msg = "";
                if (result == 0)
                {
                    return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thất bại" }, JsonRequestBehavior.AllowGet);
                }
                return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return new JsonCamelCaseResult(new { status = -1, msg = LanguageHelper.GetLabel(ResourceConstants.Label.ResourceAnErrorOccurred) }, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Delete, PageId.Media)]
        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                var faq = await ApiService.MediaFileService.GetAll();
                MediaFileInfo faqInfo = new MediaFileInfo();
                faqInfo.Id = Id;
                var result = await ApiService.MediaFileService.Delete(faqInfo);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Delete, PageId.Media)]
        public async Task<ActionResult> DeleteMedia(GroupPageInfo model)
        {
            try
            {
                var result = await ApiService.GroupPageService.Delete(model);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogTracker(Action.Delete, PageId.Media)]
        public async Task<ActionResult> DeleteItemMedia(MediaFileInfo model)
        {
            try
            {
                var result = await ApiService.MediaFileService.Delete(model);
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
    }
}