using Automation.Interface.Repository;
using Namek.Entity.EntityModel;
using Namek.Library.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace Namek.Api.Controllers
{
    [RoutePrefix("api/Chart")]
    public class ChartController : BaseController
    {
        private readonly IChartRepository _chartRepo = EngineContext.Current.Resolve<IChartRepository>();
        [HttpGet]
        [Route("GetValue")]
        public Chart GetValue(int id_ChartProperties, int id_ChartCategory)
        {
            var rs = _chartRepo.GetValue(id_ChartProperties, id_ChartCategory);
            return rs;
        }
        [HttpGet]
        [Route("GetByIdProperty")]
        public List<int> GetByIdProperty(int id_ChartProperties)
        {
            var rs = _chartRepo.GetByIdProperty(id_ChartProperties);
            return rs;
        }
    }
}