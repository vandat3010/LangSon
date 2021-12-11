using Namek.Admin.AttributeCustom;
using Namek.Entity.EntityModel;
using Namek.Library.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Namek.Admin.Utilities;
using Namek.Core.ActionResult;
using Namek.Core.Utility;
using Namek.Resources.MultiLanguage;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Action = Namek.Library.Enums.Action;
using System.Web.Mvc;
using Namek.Admin.CustomFilters;

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class ChartController : BaseController
    {
        // GET: Chart
        [LogTracker(Action.View, PageId.Chart)]
        public async Task<ActionResult> Index()
        {
            Chart ag = await ApiService.ChartService.GetValue(1, 1);
            int agriculture = ag.Value;
            Chart ta = await ApiService.ChartService.GetValue(2, 1);
            int tax = ta.Value;
            Chart ser = await ApiService.ChartService.GetValue(3, 1);
            int service = ser.Value;
            Chart indus = await ApiService.ChartService.GetValue(4, 1);
            int industry = indus.Value;
            int total = agriculture + tax + service + industry;
            double percentOfAg = Math.Round(((double)agriculture / (double)total) * 100, 1);
            double percentOfTa = Math.Round(((double)tax / (double)total) * 100, 1);
            double percentOfSer = Math.Round(((double)service / (double)total) * 100, 1);
            double percentOfIndus = (double)100 - percentOfAg - percentOfTa - percentOfSer;

            List<int> a = await ApiService.ChartService.GetByIdProperty(5);
            List<int> t = await ApiService.ChartService.GetByIdProperty(6);
            List<int> s = await ApiService.ChartService.GetByIdProperty(7);
            List<int> i = await ApiService.ChartService.GetByIdProperty(8);

            List<int> tGVN = await ApiService.ChartService.GetByIdProperty(51);
            List<int> thGVN = await ApiService.ChartService.GetByIdProperty(52);
            List<int> fGVN = await ApiService.ChartService.GetByIdProperty(53);
            List<int> tGFP = await ApiService.ChartService.GetByIdProperty(57);
            List<int> thGFP = await ApiService.ChartService.GetByIdProperty(58);
            List<int> fGFP = await ApiService.ChartService.GetByIdProperty(59);
            List<int> tGViet = await ApiService.ChartService.GetByIdProperty(54);
            List<int> thGViet = await ApiService.ChartService.GetByIdProperty(55);
            List<int> fGViet = await ApiService.ChartService.GetByIdProperty(56);
            List<int> y = await ApiService.ReporterService.GetByYear(5, 2);
            var report = new Report()
            {
                Agriculture = percentOfAg.ToString("F2", new CultureInfo("en-US")),
                Tax = percentOfTa.ToString("F2", new CultureInfo("en-US")),
                Service = percentOfSer.ToString("F2", new CultureInfo("en-US")),
                Industry = percentOfIndus.ToString("F2", new CultureInfo("en-US")),
                LstAgriculture = a,
                LstTax = t,
                LstService = s,
                lstIndustry = i,
                Lst2GVNPT = tGVN,
                Lst3GVNPT = thGVN,
                Lst4GVNPT = fGVN,
                Lst2GFPT = tGFP,
                Lst3GFPT = thGFP,
                Lst4GFPT = fGFP,
                Lst2GViettel = tGViet,
                Lst3GViettel = thGViet,
                Lst4GViettel = fGViet,
                LstYear = y
            }; return View(report);
        }
    }
    public class Report
    {
        public string Agriculture { get; set; }
        public string Tax { get; set; }
        public string Service { get; set; }
        public string Industry { get; set; }
        public List<int> LstAgriculture { get; set; }
        public List<int> LstTax { get; set; }
        public List<int> LstService { get; set; }
        public List<int> lstIndustry { get; set; }
        public List<int> Lst2GVNPT { get; set; }
        public List<int> Lst3GVNPT { get; set; }
        public List<int> Lst4GVNPT { get; set; }
        public List<int> Lst2GFPT { get; set; }
        public List<int> Lst3GFPT { get; set; }
        public List<int> Lst4GFPT { get; set; }
        public List<int> Lst2GViettel { get; set; }
        public List<int> Lst3GViettel { get; set; }
        public List<int> Lst4GViettel { get; set; }
        public List<int> LstYear { get; set; }
    }
}