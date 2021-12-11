using Namek.Core.ActionResult;
using Namek.Entity.EntityModel;
using Namek.Entity.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Namek.Admin.Controllers
{
    public class DichVuController : BaseController
    {
        // GET: DichVu
        public async Task<ActionResult>  Index(DichVuRequestModel model)
        {
            model.Page = model.Page == 0 ? 1 : model.Page;
            model.PageSize = 20;
            var result = await ApiServiceAutomation.DichVuService.GetData(model);
            model.TotalRecords = result.TotalRecords;
            model.TotalPages = result.TotalRecords / model.PageSize + 1;
            ViewBag.Data = result.Data;
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            DichVu item = await ApiServiceAutomation.DichVuService.GetInfo(id);

            return View("Edit", item);
        }

        public async Task<JsonCamelCaseResult> UpdateItem(DichVu Item)
        {
            int result = await ApiServiceAutomation.DichVuService.UpdateItem(Item);

            if (result == 0)
            {
                return new JsonCamelCaseResult(new { status = result, msg = "Cập nhật thất bại" }, JsonRequestBehavior.AllowGet);
            }

            return new JsonCamelCaseResult(new { status = result, msg = "Cập nhật thành công" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonCamelCaseResult> DeleteItem(int Id)
        {
            int result = await ApiServiceAutomation.DichVuService.DeleteItem(Id);

            if (result == 0)
            {
                return new JsonCamelCaseResult(new { status = result, msg = "Xóa thất bại" }, JsonRequestBehavior.AllowGet);
            }

            return new JsonCamelCaseResult(new { status = result, msg = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddItem(DichVu Item)
        {
            int result = await ApiServiceAutomation.DichVuService.AddItem(Item);

            if (result == 0)
            {
                return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thất bại" }, JsonRequestBehavior.AllowGet);
            }

            return new JsonCamelCaseResult(new { status = result, msg = "Thêm mới thành công" }, JsonRequestBehavior.AllowGet);
        }
    }
}