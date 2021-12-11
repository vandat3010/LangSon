using System;
using System.Collections.Generic;
using System.Linq;
using Automation.Library.Data;
using Automation.Library.Entity.Orders;
using Automation.Library.Entity.Services;
using Automation.Library.Entity.Users;

namespace Automation.LogServices.Quantities
{
    /// <summary>
    /// Báo cáo số lượng
    /// </summary>
    public class QuantityReportService : IQuantityReportService
    {
        #region Members
        private readonly IDataRepository<Order> _orderDataRepository;
        private readonly IDataRepository<OrderDetail> _orderDetailRepository;
        private readonly IDataRepository<ServiceInit> _serviceInitRepository;
        #endregion

        #region Ctors
        public QuantityReportService(IDataRepository<Order> orderDataRepository, 
            IDataRepository<OrderDetail> orderDetailRepository, IDataRepository<ServiceInit> serviceInitRepository)
        {
            _orderDataRepository = orderDataRepository;
            _orderDetailRepository = orderDetailRepository;
            _serviceInitRepository = serviceInitRepository;
        }
        #endregion

        #region Methods Quantity
        public IPagedList<CustomerReportQuantity> GetCustomerReportQuantities(
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
            int pageSize)
        {
            var getInfo = (from order in _orderDataRepository.Table
                join serviceInit in _serviceInitRepository.Table on order.Id equals serviceInit.OrderId
                join orderDetail in _orderDetailRepository.Table on order.Id equals orderDetail.OrderId into g
                from h in g.Where(x => x.ServicePackId == serviceInit.ServicePackId).DefaultIfEmpty()
                where (!fromDate.HasValue || fromDate.Value == serviceInit.ServerStartDate)
                    && (!toDate.HasValue || toDate.Value == serviceInit.ServerEndDate)
                    && (!orderType.HasValue || orderType.Value == order.OrderType)
                    && (!orderStatus.Any() || orderStatus.Contains(order.Status))
                    && (!serviceId.HasValue || serviceId.Value == h.ServiceId)
                    && (string.IsNullOrEmpty(servicePackName) || h.ServicePackName.Contains(servicePackName))
                    && (!servicePackStatus.HasValue || orderType.Value == order.OrderType)
                    && (string.IsNullOrEmpty(saleName) || order.SalesName.Contains(saleName))
                    && (!paymentType.HasValue || orderType.Value == order.OrderType)
                select new
                {
                    order,
                    serviceInit,
                    h
                });

            //var a = getInfo.GroupBy(n => new
            //{
            //    n.h.OrderId
            //}).Select(g => new
            //{
            //    g.Key.OrderId
            //});

            var orderInfo = from info in getInfo
                group info by info.h.OrderId
                into infoAfterGroup
                orderby infoAfterGroup.Key descending
                select new
                {
                    Code = infoAfterGroup.Select(x=>x.order.Code),
                    CustomerEmail = infoAfterGroup.Select(x => x.order.CustomerEmail),
                    SalesName = infoAfterGroup.Select(x => x.order.SalesName),
                    ServiceName = infoAfterGroup.Select(x => x.h.ServiceName),
                    ServicePackName = infoAfterGroup.Select(x => x.h.ServicePackName),
                    serviceCount = infoAfterGroup.Count(),
                    VoucherCode = infoAfterGroup.Select(x => x.h.VoucherCode),
                    RegionName = infoAfterGroup.Select(x => x.serviceInit.RegionName),
                    OrderType = infoAfterGroup.Select(x => x.order.OrderType),
                    PaymentTypeName = infoAfterGroup.Select(x => x.order.PaymentTypeName),
                    Status = infoAfterGroup.Select(x => x.order.Status),
                    ServicePackStatus = infoAfterGroup.Select(x => x.serviceInit.ServicePackStatus),
                    ServiceInitDate = infoAfterGroup.Select(x => x.serviceInit.CreateDate),
                    ServerStartDate = infoAfterGroup.Select(x => x.serviceInit.ServerStartDate),
                    ServerEndDate = infoAfterGroup.Select(x => x.serviceInit.ServerEndDate)
                };

            var outInfo = new PagedList<dynamic>(orderInfo, pageIndex, pageSize);
            return new PagedList<CustomerReportQuantity>(outInfo.Select(x => new CustomerReportQuantity
            {
                Code = x.Code,
                CustomerEmail = x.CustomerEmail,
                SalesName = x.SalesName,
                ServiceName = x.ServiceName,
                ServicePackName = x.ServicePackName,
                Quantity = x.serviceCount,
                VoucherCode = x.VoucherCode,
                RegionName = x.RegionName,
                OrderType = x.OrderType,
                PaymentTypeName = x.PaymentTypeName,
                Status = x.Status,
                ServicePackStatus = x.ServicePackStatus,
                ServiceInitDate = x.CreateDate,
                ServerStartDate = x.ServerStartDate,
                ServerEndDate = x.ServerEndDate
            }), outInfo.PageIndex, outInfo.PageSize, outInfo.TotalCount);
        }
        #endregion
    }
}

