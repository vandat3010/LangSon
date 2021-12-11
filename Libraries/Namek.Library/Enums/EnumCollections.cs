using System.ComponentModel;
using Namek.Core.LocalizedAttributes;
using Namek.Resources.MultiLanguage;
namespace Namek.Library.Enums
{

    //Roles
    public enum Roles
    {
        [Description("Province")] Province = 1,

        [Description("Huyện")] District = 2,

        [Description("Người dân")] People = 3,


    }

    public enum GrouPermission
    {
        [Description("Tỉnh")] Provice = 1,
        [Description("Huyện")] District = 2,
        [Description("Người dân")] People = 3
    }
    //Group GroupPermissionName
    //public enum GroupPermissionName
    //{
    //    [LocalizedEnumDescription(ResourceConstants.Enum.GroupPermissionNameSuperAdmin)] SuperAdmin = 255,

    //    [LocalizedEnumDescription(ResourceConstants.Enum.GroupPermissionNameSales)] Sales = 6,

    //    [LocalizedEnumDescription(ResourceConstants.Enum.GroupPermissionNameTeachManagement)] TeachManagement = 4,

    //    [LocalizedEnumDescription(ResourceConstants.Enum.GroupPermissionNameAccounting)] Accounting = 3,

    //    [LocalizedEnumDescription(ResourceConstants.Enum.GroupPermissionNameAgency)] Agency = 1,
    //}


    /// <summary>
    ///     Đơn vị thời gian tính tiền trong gói dịch vụ
    /// </summary>
    public enum TimeUnit
    {
        [LocalizedEnumDescription(ResourceConstants.Enum.TimeUnitHourly)] Hourly = 0,

        [LocalizedEnumDescription(ResourceConstants.Enum.TimeUnitDaily)] Daily = 1,

        [LocalizedEnumDescription(ResourceConstants.Enum.TimeUnitMonthly)] Monthly = 2,

        [LocalizedEnumDescription(ResourceConstants.Enum.TimeUnitYearly)] Yearly = 3,

        [LocalizedEnumDescription(ResourceConstants.Enum.TimeUnitMinutely)] Minutely = 4
    }
    /// <summary>
    ///     Phân các loại Notify
    /// </summary>
    public enum NotifyMode
    {
        /// <summary>
        ///     Notify dạng thông báo mình thường
        /// </summary>
        [Description("Gửi thông báo")] Notify,

        /// <summary>
        ///     Notify gửi email
        /// </summary>
        [Description("Gửi email")] Message

        ///// <summary>
        ///// Notify liên quan đến các hoạt động
        ///// </summary>
        //[Description("Công việc")]
        //Task
    }
    /// <summary>
    ///     Các thao tác trong hệ thống
    /// </summary>
    public enum Action
    {
        [Description("Đọc")] View = 1,

        [Description("Ghi")] Add = 2,

        [Description("Sửa")] Edit = 4,

        [Description("Xóa")] Delete = 8
    }
    public enum WebsitePositionEnum
    {
        [Description("Header")]
        Header = 10,
        //[Description("LeftHeader")]
        //LeftHeader = 20,
        //[Description("RightHeader")]
        //RightHeader = 30,
        //[Description("TopNavigation")]
        //TopNavigation = 40,
        //[Description("MainNavigation")]
        //MainNavigation = 50,
        [Description("Footer")]
        Footer = 60,
        //[Description("Sidebar")]
        //Sidebar = 70
    }
    public enum ServiceType
    {

    }
    public enum ViewEnum
    {
        [Description("Header")]
        HomeHeader = 0,
        [Description("Footer")]
        HomeFooter = 1,

        [Description("Đối tác")]
        HomePartner = 2,
        [Description("Thương hiệu")]
        HomeTradeMark = 3,

        [Description("Banner")]
        HomeBanner = 4,

        [Description("Media")]
        HomeMedia = 5,

        [Description("Emergency")]
        Emergency = 6,

        //[Description("CustomHtml")]
        //CustomHtml = 0,
        //[Description("HomeNewsSlider")]
        //HomeNewsSlider = 10,
        //[Description("HomeServicePackDataCenter")]
        //HomeServicePackDataCenter = 20,
        //[Description("HomeServicePackCloud")]
        //HomeServicePackCloud = 30,
        //[Description("HomeOtherService")]
        //HomeOtherService = 40,
        //[Description("HomeCustomerType")]
        //HomeCustomerType = 50,
        //[Description("HomePartner")]
        //HomePartner = 60,
        //[Description("HomeWorkschedule")]
        //HomeWorkschedule = 70,
        //[Description("HomeReasonChoice")]
        //HomeReasonChoice = 80,
        //[Description("HomeMediaLibrary")]
        //HomeMediaLibrary = 90,
        //[Description("ServiceCloudServer")]
        //ServiceCloudServer = 100,
        //[Description("HomeServicePackSolution")]
        //HomeServicePackSolution = 110,
        //[Description("HomeWhyChooseUs")]
        //HomeWhyChooseUs = 120,
        //[Description("HomeStatistic")]
        //HomeStatistic = 130,
        //[Description("HomeCustomer")]
        //HomeCustomer = 140,
        //[Description("HomeTrial")]
        //HomeTrial = 150,
        //[Description("HomeSubscribe")]
        //HomeSubscribe = 160,
        //[Description("HomeAboutUs")]
        //HomeAboutUs = 170,
        //[Description("HomeAgency")]
        //HomeAgency = 180,
        //[Description("HomeContact")]
        //HomeContact = 190,
        //[Description("HomePriceList")]
        //HomePriceList = 200,
        //[Description("HomeSupport")]
        //HomeSupport = 210,

    }
    public enum ControllerEnum
    {
        [Description("Home")]
        Home = 10,
        [Description("NewsArticle")]
        NewsArticle = 20,
        [Description("Service")]
        Service = 30,
    }

    public enum CommunicationType
    {
        [Description("SocialMedia")]
        SocialMedia = 1, //truyền thông xã hội
        [Description("BasicInformation")]
        BasicInformation = 2, //thông tin cơ sở
        [Description("ExternalInformation")]
        ExternalInformation = 3, //thông tin đối ngoại
    }

    public enum Media
    {
        [LocalizedEnumDescription(ResourceConstants.Enum.BinhGia)] BinhGia = 1,
        [LocalizedEnumDescription(ResourceConstants.Enum.BacSon)] BacSon = 2,
        [LocalizedEnumDescription(ResourceConstants.Enum.CaoLoc)] CaoLoc = 3,
        [LocalizedEnumDescription(ResourceConstants.Enum.ChiLang)] ChiLang = 4,
        [LocalizedEnumDescription(ResourceConstants.Enum.DinhLap)] DinhLap = 5,
        [LocalizedEnumDescription(ResourceConstants.Enum.HuuLung)] HuuLung = 6,
        [LocalizedEnumDescription(ResourceConstants.Enum.LangSon)] LangSon = 7,
        [LocalizedEnumDescription(ResourceConstants.Enum.LocBinh)] LocBinh = 8,
        [LocalizedEnumDescription(ResourceConstants.Enum.TrangDinh)] TrangDinh = 9,
        [LocalizedEnumDescription(ResourceConstants.Enum.VanQuan)] VanQuan = 10,
        [LocalizedEnumDescription(ResourceConstants.Enum.VanLang)] VanLang = 11,
    }

    public enum PartnerBrand
    {
        [LocalizedEnumDescription(ResourceConstants.Enum.DoiTac)] DoiTac = 2,
        [LocalizedEnumDescription(ResourceConstants.Enum.ThuongHieu)] ThuongHieu = 3,
    }
    public enum PostFormat
    {
        [Description("ImageFormat")]
        ImageFormat = 0,
        [Description("VideoFormat")]
        VideoFormat = 1,
        [Description("SoundFormat")]
        SoundFormat = 2,
        [Description("DefaultFormat")]
        DefaultFormat = 3,
    }

    public enum TypeNews
    {
        [Description("Positive")]
        Positive = 0,
        [Description("Negative")]
        Negative = 1,
        [Description("Neutral")]
        Neutral = 2,
    }

}
