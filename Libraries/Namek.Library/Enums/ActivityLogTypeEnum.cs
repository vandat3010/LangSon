using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Namek.Library.Enums
{
    public enum ActivityLogTypeEnum
    {
        /// <summary>
        ///     Thêm gói dịch vụ mới
        /// </summary>
        [Description("Thêm gói dịch vụ mới")] [Display(Name = "AddNewServicePack")] AddNewServicePack,

        /// <summary>
        ///     Xem gói dịch vụ
        /// </summary>
        [Description("Xem gói dịch vụ")] [Display(Name = "ViewServicePack")] ViewServicePack,

        /// <summary>
        ///     Sửa gói dịch vụ
        /// </summary>
        [Description("Sửa gói dịch vụ")] [Display(Name = "EditServicePack")] EditServicePack,

        /// <summary>
        ///     Xóa gói dịch vụ
        /// </summary>
        [Description("Xóa gói dịch vụ")] [Display(Name = "DeleteServicePack")] DeleteServicePack,

        /// <summary>
        ///     Thêm gói dịch vụ bổ sung
        /// </summary>
        [Description("Thêm gói dịch vụ bổ sung")]
        [Display(Name = "AddNewExtensionServicePack")]
        AddNewExtensionServicePack,

        /// <summary>
        ///     Sửa gói dịch vụ bổ sung
        /// </summary>
        [Description("Sửa gói dịch vụ bổ sung")] [Display(Name = "EditExtensionServicePack")] EditExtensionServicePack,

        /// <summary>
        ///     Xóa gói dịch vụ bổ sung
        /// </summary>
        [Description("Xóa gói dịch vụ bổ sung")]
        [Display(Name = "DeleteExtensionServicePack")]
        DeleteExtensionServicePack,

        /// <summary>
        ///     Cập nhật cấu hình dịch vụ
        /// </summary>
        [Description("Cập nhật cấu hình dịch vụ")] [Display(Name = "UpdateConfigService")] UpdateConfigService,

        /// <summary>
        ///     Cập nhật quyền truy cập
        /// </summary>
        [Description("Cập nhật quyền truy cập")] [Display(Name = "UpdatePermission")] UpdatePermission,

        /// <summary>
        ///     Cập nhật nhóm quyền
        /// </summary>
        [Description("Cập nhật nhóm quyền")] [Display(Name = "UpdateGroupPermission")] UpdateGroupPermission,

        /// <summary>
        ///     Gán sales cho Khách hàng
        /// </summary>
        [Description("Gán sales cho Khách hàng")] [Display(Name = "AssignSalesForCustomer")] AssignSalesForCustomer,

        /// <summary>
        ///     Thêm mới khuyến mại
        /// </summary>
        [Description("Thêm mới khuyến mại")] [Display(Name = "AddNewVoucher")] AddNewVoucher,

        /// <summary>
        ///     Xóa khuyến mại
        /// </summary>
        [Description("Xóa khuyến mại")] [Display(Name = "DeleteVoucher")] DeleteVoucher,

        /// <summary>
        ///     Sửa khuyến mại
        /// </summary>
        [Description("Sửa khuyến mại")] [Display(Name = "EditVoucher")] EditVoucher,

        /// <summary>
        ///     Sao chép khuyến mại
        /// </summary>
        [Description("Sao chép khuyến mại")] [Display(Name = "CopyVoucher")] CopyVoucher,

        /// <summary>
        ///     Tạo mới tài khoản End-User
        /// </summary>
        [Description("Tạo mới tài khoản End-User")] [Display(Name = "AddNewCustomer")] AddNewCustomer,

        /// <summary>
        ///     Tạo mới đơn hàng
        /// </summary>
        [Description("Tạo mới đơn hàng")] [Display(Name = "AddNewOrder")] AddNewOrder,

        /// <summary>
        ///     Thay đổi trạng thái Cloud Server
        /// </summary>
        [Description("Thay đổi trạng thái Cloud Server")]
        [Display(Name = "ChangeCloudServerStatus")]
        ChangeCloudServerStatus,

        /// <summary>
        ///     Thay đổi mật khẩu Cloud Server
        /// </summary>
        [Description("Thay đổi mật khẩu Cloud Server")]
        [Display(Name = "ChangePasswordCloudServer")]
        ChangePasswordCloudServer,

        /// <summary>
        ///     Thay đổi mật khẩu Hosting
        /// </summary>
        [Description("Thay đổi mật khẩu Hosting")] [Display(Name = "ChangePasswordHosting")] ChangePasswordHosting,

        /// <summary>
        ///     Thay đổi trạng thái Web Hosting
        /// </summary>
        [Description("Thay đổi trạng thái Web Hosting")]
        [Display(Name = "ChangeWebHostingStatus")]
        ChangeWebHostingStatus,

        /// <summary>
        ///     Thay đổi trạng thái Email Hosting
        /// </summary>
        [Description("Thay đổi trạng thái Email Hosting")]
        [Display(Name = "ChangeEmailHostingStatus")]
        ChangeEmailHostingStatus,

        /// <summary>
        ///     Xác nhận đơn hàng
        /// </summary>
        [Description("Xác nhận đơn hàng")] [Display(Name = "ConfirmOrder")] ConfirmOrder,

        /// <summary>
        ///     Thanh toán online thành công
        /// </summary>
        [Description("Thanh toán online thành công")] [Display(Name = "PayOnlineSuccess")] PayOnlineSuccess,

        /// <summary>
        ///     Thay đổi số dư
        /// </summary>
        [Description("Thay đổi số dư")] [Display(Name = "ChangeBalance")] ChangeBalance,

        /// <summary>
        ///     Thanh toán thành công
        /// </summary>
        [Description("Thanh toán thành công")] [Display(Name = "PaymentSuccess")] PaymentSuccess,

        /// <summary>
        ///     Tạo mới service
        /// </summary>
        [Description("Khởi tạo dịch vụ")] [Display(Name = "CallApiCreateService")] CallApiCreateService,

        /// <summary>
        ///     Nâng cấp
        /// </summary>
        [Description("Nâng cấp dịch vụ")] [Display(Name = "CallApiUpgradeService")] CallApiUpgradeService,

        /// <summary>
        ///     Bổ sung
        /// </summary>
        [Description("Mua bổ sung dịch vụ")] [Display(Name = "CallApiExtendService")] CallApiExtendService,

        /// <summary>
        ///     Gia hạn
        /// </summary>
        [Description("Gia hạn dịch vụ")] [Display(Name = "CallApiRenewService")] CallApiRenewService,

        /// <summary>
        ///     Chuyển dùng thật
        /// </summary>
        [Description("Chuyển sang dùng thật dịch vụ")]
        [Display(Name = "CallApiSwithToRealService")]
        CallApiSwithToRealService,

        /// <summary>
        /// Duyệt số hợp đồng của đơn hàng
        /// </summary>
        [Description("Duyệt số hợp đồng của đơn hàng")]
        [Display(Name = "ApproveContractNumber")]
        ApproveContractNumber,

        /// <summary>
        /// Duyệt giá tùy chỉnh của đơn hàng
        /// </summary>
        [Description("Duyệt giá tùy chỉnh của đơn hàng")]
        [Display(Name = "ApproveCustomPrice")]
        ApproveCustomPrice,

        /// <summary>
        /// Hủy đơn hàng
        /// </summary>
        [Description("Hủy đơn hàng")] [Display(Name = "RejectOrder")] RejectOrder,

        /// <summary>
        /// Tạo bản Backup Snapshot Cloud Server
        /// </summary>
        [Description("Tạo Cloud Server Snapshot")]
        [Display(Name = "CreateCloudServerSnapshot")]
        CreateCloudServerSnapshot,

        /// <summary>
        /// Xóa bản Backup Snapshot Cloud Server
        /// </summary>
        [Description("Xóa Cloud Server Snapshot")]
        [Display(Name = "RemoveCloudServerSnapshot")]
        RemoveCloudServerSnapshot,

        /// <summary>
        /// Khôi phục Cloud Server từ bản Snapshot
        /// </summary>
        [Description("Khôi phục Cloud Server từ bản Snapshot")]
        [Display(Name = "RestoreCloudServerSnapshot")]
        RestoreCloudServerSnapshot,

        /// <summary>
        /// Duyệt đơn hàng trả sau
        /// </summary>
        [Description("Duyệt đơn hàng trả sau")]
        [Display(Name = "ApprovePayLaterOrder")]
        ApprovePayLaterOrder,

        /// <summary>
        /// Duyệt đơn hàng
        /// </summary>
        [Description("Duyệt đơn hàng")]
        [Display(Name = "ApproveOrder")]
        ApproveOrder,

        /// <summary>
        /// Thay đổi thông tin Vm
        /// </summary>
        [Description("Thay đổi thông tin VM")]
        [Display(Name = "ChangeVmInfo")]
        ChangeVmInfo,

        /// <summary>
        /// Xác nhận thanh toán Thông báo cước
        /// </summary>
        [Description("Xác nhận thanh toán Thông báo cước")]
        [Display(Name = "PaylaterNoticePaymentConfirm")]
        PaylaterNoticePaymentConfirm,

        /// <summary>
        /// Lấy lịch sử hiệu năng Vm
        /// </summary>
        [Description("Update lịch sử VM Performance")]
        [Display(Name = "UpdateVmPerformanceHistories")]
        UpdateVmPerformanceHistories,

        /// <summary>
        /// Khôi phục Cloud Server từ bản backup
        /// </summary>
        [Description("Khôi phục Cloud Server từ bản backup")]
        [Display(Name = "RestoreCloudServerFromBackup")]
        RestoreCloudServerFromBackup,

        /// <summary>
        /// Cập nhật thông tin khách hàng
        /// </summary>
        [Description("Cập nhật thông tin khách hàng")] [Display(Name = "UpdateInfoForCustomer")] UpdateInfoForCustomer,

        [Description("Lỗi hệ thống")] [Display(Name = "Exception")] Exception,

        [Description("Nạp tiền")] [Display(Name = "Nạp tiền")] NapTien
    }
}