using Namek.Entity.EntityModel;
using Namek.Entity.InfoModel;
using Namek.Entity.RequestModel;
using Namek.Interface.Repository;
using Namek.Library.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Namek.Api.Controllers
{
    [RoutePrefix("api/DichVu")]
    public class DichVuController : BaseController
    {
        // GET: DichVu
        private readonly IDichVuRepository _dichVuRepo = EngineContext.Current.Resolve<IDichVuRepository>();
        [HttpPost]
        [Route("GetData")]
        public PagedList<DichVu> GetData(DichVuRequestModel model)
        {
            PagedList<DichVu> pagedResult = new PagedList<DichVu>();
            int totalCount = 0;
            var rs = _dichVuRepo.Search(model,model.Page,model.PageSize,out totalCount);
            pagedResult.PageSize = model.PageSize;
            pagedResult.PageNumber = model.Page;
            pagedResult.TotalRecords = totalCount;
            pagedResult.Data = rs;
            return pagedResult;
        }
        //[HttpGet]
        //[Route("GetAll")]
        //public List<DichVu> GetAll()
        //{
        //    List<DichVu> pagedResult = new List<DichVu>();
        //    var rs = _dichVuRepo.GetAll();
        //    return rs;
        //}

        [HttpGet]
        [Route("GetInfo")]
        public DichVu GetInfo(int id)
        {
            var rs = _dichVuRepo.GetInfo(id);
            return rs;
        }

        [HttpGet]
        [Route("DeleteItem")]
        public int DeleteItem(int id)
        {
            var rs = _dichVuRepo.Delete(id);
            return rs;
        }


        [HttpPost]
        [Route("UpdateItem")]
        public int UpdateItem(DichVu Item)
        {
            return _dichVuRepo.Update(Item);
        }

        [HttpPost]
        [Route("AddItem")]
        public int AddItem(DichVu Item)
        {
            return _dichVuRepo.Add(Item);
        }
    }
}