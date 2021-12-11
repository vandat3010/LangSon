using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Namek.Admin.Models.ActivityLog
{
    public class ApiLogRequestModel
    {
        public string UserKeywords { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }

        #region Paging

        public List<ApiLogModel> ApiLogs { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int PageSize { get; set; }

        #endregion
    }
}