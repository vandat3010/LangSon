namespace Namek.Admin.Models.Customer
{
    /// <summary>
    ///     Model dùng cho export danh sách khách hàng theo filter ra file excel
    /// </summary>
    public class CustomerExportModel
    {
        /// <summary>
        ///     Họ tên đầy đủ
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Số điện thoại
        /// </summary>
        public string PhoneNo { get; set; }

        /// <summary>
        ///     CMND/Hộ chiếu
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        ///     Mã số thuế
        /// </summary>
        public string TaxCode { get; set; }

        /// <summary>
        ///     Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     Họ tên NVKD
        /// </summary>
        public string Sale { get; set; }

        /// <summary>
        ///     Vùng miền
        /// </summary>
        public string DC { get; set; }

        /// <summary>
        ///     Thời điểm kích hoạt tài khoản
        /// </summary>
        public string AccCreatedDateTime { get; set; }

        /// <summary>
        ///     Số Lượng dịch vụ duy trì
        /// </summary>
        public int NumberOfUsingService { get; set; }

        /// <summary>
        ///     Số lượng dịch vụ phát sinh
        /// </summary>
        public int NumberOfUsedService { get; set; }

        /// <summary>
        ///     Số lượng gói dịch vụ duy trì
        /// </summary>
        public int NumberOfUsingServicePackage { get; set; }

        /// <summary>
        ///     Số lượng gói dịch vụ phát sinh
        /// </summary>
        public int NumberOfUsedServicePackage { get; set; }

        /// <summary>
        ///     Tổng phí duy trì hàng tháng
        /// </summary>
        public decimal TotalFeePerMonth { get; set; }

        /// <summary>
        ///     Tổng cước phí phát sinh
        /// </summary>
        public decimal TotalPaidValueFromStart { get; set; }

        /// <summary>
        ///     Tổng tiền nạp tài khoản
        /// </summary>
        public decimal TotalTopUpValueFromStart { get; set; }

        /// <summary>
        ///     Giá trị đơn hàng lớn nhất
        /// </summary>
        public decimal LargestOrderValue { get; set; }
    }
}