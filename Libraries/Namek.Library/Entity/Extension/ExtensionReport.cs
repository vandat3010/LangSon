using System;
using Namek.Library.Data;

namespace Namek.Library.Entity.Extension
{
    public class ExtensionReport : BaseEntity
    {
        /// <summary>
        ///     Tên VM
        /// </summary>
        public string VM_Name { get; set; }

        /// <summary>
        ///     Mã đơn hàng
        /// </summary>
        public string CodeName { get; set; }

        /// <summary>
        ///     Tên tài khoản
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Tên đầy dủ của nhân viên kinh doanh
        /// </summary>
        public string SalesFullName { get; set; }

        /// <summary>
        ///     Tên dịch vụ (vd: Web Hosting, Email Hosting, VPS)
        /// </summary>
        public string NameService { get; set; } //enum

        /// <summary>
        ///     Tên gói dịch vụ hoặc Tên gói dịch vụ mở rộng
        /// </summary>
        public string NameServicePack { get; set; } //enum

        /// <summary>
        ///     Thời gian tạo
        /// </summary>
        public DateTime? CreateDate { get; set; } // CreateDate

        /// <summary>
        ///     Ngày bắt đầu của gói cưới
        /// </summary>
        public DateTime? BeginDate { get; set; } // BeginDate

        /// <summary>
        ///     Ngày kết thúc của gói cước
        /// </summary>
        public DateTime? EndDate { get; set; } // EndDate

        /// <summary>
        ///     Phí duy trì trước VAT, trước KM
        /// </summary>
        public decimal? PriceBeforeVatBeforePromote { get; set; }

        /// <summary>
        ///     Phí duy trì sau VAT, sau KM
        /// </summary>
        public decimal? PriceAfterVatAfterPromote { get; set; }

        /// <summary>
        ///     Giá trị trước VAT
        /// </summary>
        public decimal? ValueBeforeVat { get; set; }

        /// <summary>
        ///     Giá trị sau VAT
        /// </summary>
        public decimal? ValueAfterVat { get; set; }

        /// <summary>
        ///     Mã khuyến mãi
        /// </summary>
        public string CodeVoucher { get; set; } //Voucher
    }
}