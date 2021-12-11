using System;
using System.Collections.Generic;

namespace Namek.Admin.Models.Home
{
    public class IndexModel
    {
        public IndexModel()
        {
        }

        public int ActiveWebHostingCount { get; set; }
        public int ActiveEmailHostingCount { get; set; }
        public int ActiveDomainCount { get; set; }
        public int ActiveCloudServerCount { get; set; }
        public int ActiveStartCloudCount { get; set; }
        public int ActiveOtherCount { get; set; }
        public int WebHostingAboutToExpire { get; set; }
        public int EmailHostingAboutToExpire { get; set; }
        public int DomainAboutToExpire { get; set; }
        public int CloudServerAboutToExpire { get; set; }
        public int StartCloudAboutToExpire { get; set; }
        public List<UnpaidOrder> UnpaidOrders { get; set; }
        public List<UnpaidOrder> UnfinishedPaymentOrders { get; set; }
    }

    public class UnpaidOrder
    {
        public int Total { get; set; }
        public DateTime Date { get; set; }
    }
}