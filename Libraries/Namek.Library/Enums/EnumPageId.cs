using System.ComponentModel;

namespace Namek.Library.Enums
{
    /// <summary>
    ///     Khai báo các trang trong hệ thống
    /// </summary>
    public enum PageId
    {
        /// <summary>
        ///     Trang này không check quyền truy cập
        /// </summary>
        NoCheckPermisson = 0,
         

        /// <summary>
        ///     4. Quyền truy cập
        /// </summary>
        [Description("Quyền truy cập")] Permission = 4,

   
        /// <summary>
        ///     6. Nhân viên
        /// </summary>
        [Description("Nhân viên")] User = 6,

        /// <summary>
        ///     7. Khách hàng
        /// </summary>
        [Description("Khách hàng")] Customer = 7,

        /// <summary>
        ///  8.Phóng Viên
        /// </summary>
        [Description("Quản lí Phóng Viên")] Reporter = 498,
        /// <summary>
        ///     47. Quản lý log tác động
        /// </summary>
        [Description("Quản lý log tác động")] ActivityLog = 47,

        /// <summary>
        ///     47. Quản lý log API
        /// </summary>
        [Description("Quản lý log API")] ApiLog = 48,

       
        /// <summary>
        ///     60. Quản lý Module
        /// </summary>
        [Description("Quản lý Module")] Module = 60,

        /// <summary>
        ///     61. Quản lý Page
        /// </summary>
        [Description("Quản lý Page")] Page = 61,

        [Description("Quản lý Menu")] Menu = 157,

        [Description("Quản lý Tin tức")] NewsArticle = 158,
        [Description("Quản lý Widget")] GroupPage = 258,
        [Description("Quản lý dịch vụ")] Service = 358,
        [Description("Quản lý Media")] Media = 359,
        [Description("Quản lý câu hỏi")] FAQ = 360,
        [Description("Quản lý nội dung gói")] ServicePack = 361,

        [Description("Quản lý yêu cầu đại lý - đối tác")] TicketAgency = 362,
        [Description("Quản lý nội dung gói")] ServicePackCMS = 363,
        [Description("Quản lý loại tin tức")] NewsCategory = 364,
        [Description("Quản lý loại câu hỏi")] FaqCategory = 365,
        [Description("Quản lý các nhà cung cấp")] Manufacturer = 366,
        

        //Namnh thêm các pageID
        [Description("Cộng điểm cho khách hàng trung thành")] ImportLoyaltyUser = 367,
        [Description("Danh sách khách hàng trung thành")] LoyaltyManagement = 368, 
        [Description("Danh sách quà tặng")] LoyaltyGiftList = 369,
        [Description("Chi tiết lịch sử giao dịch của khách hàng")] LoyaltyTransactionDetail = 370,
        [Description("Chi tiết lịch sử đổi quà của khách hàng")] LoyaltyListChangeGift = 371,
        //End

        [Description("Quản lý văn bản pháp luật")] Legislation = 503,
        [Description("Quản lý loại văn bản pháp luật")] LegislationCategory = 502,
        [Description("Quản lý văn bản pháp luật affiliate banner")] LegislationAffiliateBanner = 462,
        [Description("Quản lý văn bản pháp luật affiliate email")] LegislationAffiliateEmail = 463,

        [Description("Quản lý biều đồ ")] Chart = 504,
        [Description("Quản lý báo cáo biều đồ ")] ReportChart = 505,
        [Description("Quản lý biểu đồ affiliate banner")] ChartAffiliateBanner = 492,




        [Description("Quản lý tài liệu")] Document = 495,
        [Description("Quản lý loại tài liệu")] DocumentCategory = 472,

        [Description("Quản lý thoả thuận hợp tác")] CooperationAgreement = 500,

        [Description("Quản lý tài liệu affiliate banner")] DocumentAffiliateBanner = 372,
        [Description("Quản lý tài liệu affiliate email")] DocumentAffiliateEmail = 373,
        [Description("Quản lý Layout")] Layout = 487,
        [Description("Quản lý dối tác thương hiệu")] PartnerBrand = 488,

        [Description("Quản lý tin bài")] News = 483,
        [Description("Quản lý thiên tai tình huống khẩn cấp")] Emergency = 490,
        [Description("Quản lý huyện")] District = 491,

        [Description("Quản lý phê duyệt")] NewsCategoryApprove = 496,
        
    }
}