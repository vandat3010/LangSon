using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namek.Entity.EntityModel
{
   public class ServicePack
    {
        public ServicePack()
        {
            UnsignedName = "";
            PriorityNo = 0;
            IsSystem = false;
            IsCustomizable = false;
            TrialAllow = 0;
            TrialHour = 72;
            DisplayMode = 0;
            IsDeleted = false;
        }
        /// <summary>
        ///     Mã gói dịch vụ
        /// </summary>
        public string Code { get; set; } // Code (length: 50)

        /// <summary>
        ///     Tên gói dịch vụ hoặc Tên gói dịch vụ mở rộng
        /// </summary>
        public string Name { get; set; } // Name (length: 300)
        public int Id { get; set; } // Id (Primary key)

        /// <summary>
        ///     Thông tin cho tìm kiếm nhân viên (gồm: email, tên đăng nhập, họ và tên) -&gt; Dạng tiếng việt không dấu
        /// </summary>
        public string UnsignedName { get; set; } // UnsignedName


        /// <summary>
        ///     Id của dịch vụ
        /// </summary>
        public int ServiceId { get; set; } // SerivceId

        /// <summary>
        ///     Tên dịch vụ
        /// </summary>
        public string ServiceName { get; set; } // ServiceName (length: 300)

        /// <summary>
        ///     Id gói dịch vụ (Chỉ sử dụng cho gói dịch vụ bổ xung, NULL thì gói này là gói dịch vụ)
        /// </summary>
        public int? ServicePackId { get; set; } // ServicePackId

        /// <summary>
        ///     Tên gói dịch vụ (Chỉ dùng cho gói dịch vụ bổ xung, NULL gói này là gói dịch vụ)
        /// </summary>
        public string ServicePackName { get; set; } // ServicePackName (length: 300)

        /// <summary>
        ///     Được update hay được tạo từ gói dịch vụ nào
        /// </summary>
        public int? FromServicePackId { get; set; } // FromServicePackId

        /// <summary>
        ///     Tên gói dịch vụ base
        /// </summary>
        public string FromServicePackName { get; set; } // FromServicePackName (length: 300)

        /// <summary>
        ///     Cho phép tùy chỉnh hay không (Sử dụng cho gói VPS tùy chỉnh)
        /// </summary>
        public bool IsSystem { get; set; } // IsSystem

        /// <summary>
        ///     Là gói dịch vụ cấu hình của hệ thống
        /// </summary>
        public bool IsCustomizable { get; set; } // IsCustomizable

        /// <summary>
        ///     Ngày bắt đầu của gói cưới
        /// </summary>
        public DateTime BeginDate { get; set; } // BeginDate

        /// <summary>
        ///     Ngày kết thúc của gói cước
        /// </summary>
        public DateTime EndDate { get; set; } // EndDate

        /// <summary>
        ///     Id đơn vị tính của gói dịch vụ (vd: 0: Theo giờ, 1: Theo tháng)
        /// </summary>
        public byte UnitId { get; set; } // UnitId

        /// <summary>
        ///     Tên đơn vị tính của gói dịch vụ (vd: Giờ, Tháng)
        /// </summary>
        public string UnitName { get; set; } // UnitName (length: 300)

        /// <summary>
        ///     Id loại hình (vd: 0: Trả trước, 1: Trả sau)
        /// </summary>
        public byte? TypeId { get; set; } // TypeId

        /// <summary>
        ///     Tên loại hình (vd: Trả trước, trả sau)
        /// </summary>
        public string TypeName { get; set; } // TypeName (length: 300)

        public int Time { get; set; } // Time

        /// <summary>
        ///     Đơn giá của dịch vụ (IsSystem = 1) hoặc tổng tiền dịch vụ (IsSystem = 0)
        /// </summary>
        public decimal? TotalPrice { get; set; } // TotalPrice

        public int Quantity { get; set; } // Quantity
        public decimal Vat { get; set; } // Vat
        public decimal VatSetting { get; set; } // VatSetting
        public decimal? SettingPrice { get; set; } // SettingPrice
        public decimal? TotalServicePrice { get; set; } // Phí dịch vụ duy trì,
        public decimal? SettingServicePrice { get; set; } // Phí dịch vụ khởi tạo,
        public decimal? VatService { get; set; } // Vat dịch vụ duy trì,
        public decimal? VatSettingService { get; set; } // VAT dịch vụ khởi tạo,

        public string DescriptionEn { get; set; }
        /// <summary>
        ///     Trạng thái của gói cước: (0: Không hiệu lực, 1: Hiệu lực)
        /// </summary>
        public byte Status { get; set; } // Status

        /// <summary>
        ///     Cho phép dùng thử, mặc định 0: Không cho phép, 1: Cho phép
        /// </summary>
        public byte TrialAllow { get; set; } // TrialAllow

        /// <summary>
        ///     Số giờ cho dùng thử
        /// </summary>
        public int TrialHour { get; set; } // TrialHour

        /// <summary>
        ///     Tổng tiền của gói dịch vụ
        /// </summary>
        public string Description { get; set; } // Description (length: 200)

        /// <summary>
        ///     Loại giao dịch áp dụng: (0: Đăng ký, 1: Nâng cấp, 2: Gia hạn)
        /// </summary>
        public byte? TransactionType { get; set; } // TransactionType

        /// <summary>
        ///     Kiểu hiển thị của gói mở rộng (0: Bình thường, 1: Radio, 2: Drowdown -&gt; vd: gói dịch vụ mở rộng storage -&gt;
        ///     tùy chọn SSD, HDD, SSD MIX HDD,...)
        /// </summary>
        public byte? DisplayMode { get; set; } // DisplayMode

        /// <summary>
        ///     Đơn giá của dịch vụ (IsSystem = 1) hoặc tổng tiền dịch vụ (IsSystem = 0)
        /// </summary> 

        /// <summary>
        ///     Trạng thái là xóa hay không (Mặc định là 0~False)
        /// </summary>
        public bool IsDeleted { get; set; } // IsDeleted

        /// <summary>
        ///     Thời gian tạo
        /// </summary>
        public DateTime CreateDate { get; set; } // CreateDate

        /// <summary>
        ///     Thời gian cập nhật
        /// </summary>
        public DateTime ModifyDate { get; set; } // ModifyDate
                                                 /// <summary>
                                                 ///     Vùng, miền cấu hình
                                                 /// </summary>
        public byte? RegionId { get; set; } // RegionId

        /// <summary>
        ///     Id hệ điều hành
        /// </summary>
        public int? OsId { get; set; } // OsId

        /// <summary>
        ///     Khách hàng của gói dịch vụ
        /// </summary>
        public int? CustomerId { get; set; } // CustomerId

        /// <summary>
        ///     Tên khách hàng
        /// </summary>
        public string CustomerName { get; set; } // CustomerName (length: 300)

        /// <summary>
        ///     Email khách hàng
        /// </summary>
        public string CustomerEmail { get; set; } // CustomerEmail (length: 300)

        /// <summary>
        ///     Số điện thoại khác hàng
        /// </summary>
        public string CustomerPhone { get; set; } // CustomerPhone (length: 300)

        /// <summary>
        ///     Nhân viên Sale của khách hàng
        /// </summary>
        public int? SalesId { get; set; } // SalesId

        /// <summary>
        ///     Tên nhân viên sale của khách hàng
        /// </summary>
        public string SalesName { get; set; } // SalesName (length: 300)

        /// <summary>
        ///     Chứa thông tin cấu hình của các máy VPS vd: hệ điều hành,...
        /// </summary>
        public string SetupConfigurationJson { get; set; } // SetupConfigurationJson

        public int? MinPeriodPayment { get; set; } // MinPeriodPayment
        public decimal ActualPrice { get; set; } // ActualPrice

        /// <summary>
        ///     SecondEmail khách hàng dự phòng
        /// </summary>
        public string CustomerSecondEmail { get; set; } // CustomerSecondEmail (length: 300)

        public bool IsForAgency { get; set; }

        public int? GroupId { get; set; } // GroupId

        public string Note { get; set; }
        public string NoteVoucher { get; set; }
        public int PriorityNo { get; set; } // PriorityN
    }
}
