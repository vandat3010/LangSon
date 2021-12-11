using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDKTTCTN.Core.Entity;

namespace CDKTTCTN.Services.ExportImport
{
    public interface IExportManager
    {
        /// <summary>
        /// Export customers to XLSX
        /// </summary>
        /// <param name="entities">Products</param>
        byte[] ExportToXlsx<T>(IEnumerable<T> entities);

        byte[] ExportStudyResultsToXlsx(IEnumerable<Study_result> studyResults);

    }
}
