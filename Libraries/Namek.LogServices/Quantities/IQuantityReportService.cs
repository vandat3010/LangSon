using System;
using System.Collections.Generic;
using Automation.Library.Data;
using Automation.Library.Entity.Users;

namespace Automation.LogServices.Quantities
{
    public interface IQuantityReportService
    {
        IPagedList<CustomerReportQuantity> GetCustomerReportQuantities(
            DateTime? fromDate,
            DateTime? toDate,
            byte? orderType,
            List<byte> orderStatus,
            int? serviceId,
            string servicePackName,
            byte? servicePackStatus,
            string saleName,
            byte? paymentType,
            int pageIndex,
            int pageSize);
    }
}
