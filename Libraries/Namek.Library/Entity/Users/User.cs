using System;
using Namek.Library.Data;

namespace Namek.Library.Entity.Users
{
    public class User : BaseEntity
    {
        /// <summary>
        ///     Tên tài khoản đăng nhập
        /// </summary>
        public string UserName { get; set; } // UserName (length: 50)

        /// <summary>
        ///     Mật khẩu đăng nhập hệ thống
        /// </summary>
        public string Password { get; set; } // Password (length: 50)

        /// <summary>
        ///     Tên nhân viên
        /// </summary>
        public string FirstName { get; set; } // FirstName (length: 30)

        /// <summary>
        ///     Tên đệm nhân viên
        /// </summary>
        public string MidleName { get; set; } // MidleName (length: 30)

        /// <summary>
        ///     Họ nhân viên
        /// </summary>
        public string LastName { get; set; } // LastName (length: 30)

        /// <summary>
        ///     Tên đầy đủ nhân viên
        /// </summary>
        public string FullName { get; set; } // FullName (length: 100)

        /// <summary>
        ///     Thông tin cho tìm kiếm nhân viên (gồm: email, tên đăng nhập, họ và tên) -&gt; Dạng tiếng việt không dấu
        /// </summary>
        public string UnsignedName { get; set; } // UnsignedName

        /// <summary>
        ///     Giới tính: 0: Nữ, 1: Nam, 2: Bí mật
        /// </summary>
        public byte Gender { get; set; } // Gender

        /// <summary>
        ///     Email của tài khoản
        /// </summary>
        public string Email { get; set; } // Email (length: 50)

        /// <summary>
        ///     0: Chưa kích hoạt, 1: Đã kích hoạt (Trạng thái hoạt động của tài khoản)
        /// </summary>
        public byte Status { get; set; } // Status

        /// <summary>
        ///     Avatar của tài khoản
        /// </summary>
        public string Avatar { get; set; } // Avatar (length: 2000)

        /// <summary>
        ///     Trạng thái đã xóa khỏi hệ thống
        /// </summary>
        public bool IsDelete { get; set; } // IsDelete

        /// <summary>
        ///     Tài khoản đang bị khóa (Sử dụng khi đăng nhập sai quá nhiều)
        /// </summary>
        public bool IsLocked { get; set; } // IsLocked

        /// <summary>
        ///     Thời gian hết hạn khóa tài khoản
        /// </summary>
        public DateTime? LockedToDateTime { get; set; } // LockedToDateTime

        /// <summary>
        ///     Thời gian hết hạn khóa tài khoản
        /// </summary>
        public DateTime? LockedDateTimeLast { get; set; } // LockedDateTimeLast

        /// <summary>
        ///     Lần đăng nhập sai đầu tiên của tài khoản
        /// </summary>
        public DateTime? LoginFailureFirstDateTime { get; set; } // LoginFailureFirstDateTime

        /// <summary>
        ///     Số lần đăng nhập sai của tài khoản
        /// </summary>
        public byte LoginFailureNo { get; set; } // LoginFailureNo

        /// <summary>
        ///     Id nhóm quyền truy cập của nhân viên
        /// </summary>
        public short? GroupPermissionId { get; set; } // GroupPermissionId

        /// <summary>
        ///     Tên nhóm quyền truy cập của nhân viên
        /// </summary>
        public string GroupPermissionName { get; set; } // GroupPermissionName (length: 300)

        /// <summary>
        ///     Vai trò của tài khoản: 0: Quản trị hệ thống, 1: Sale, 2: Quản trị kinh doanh, 3: Kế toán
        /// </summary>
        public byte RoleId { get; set; } // RoleId

        /// <summary>
        ///     Tên vai trò của tài khoản (vd: Nhân viên Sale, Quản trị hệ thống, Quản trị kinh doanh,...)
        /// </summary>
        public string RoleName { get; set; } // RoleName (length: 300)

        /// <summary>
        ///     Tài khoản khách hàng của nhân viên kinh doanh
        /// </summary>
        public int? SalesId { get; set; } // SalesId

        /// <summary>
        ///     Tên đầy dủ của nhân viên kinh doanh
        /// </summary>
        public string SalesFullName { get; set; } // SalesFullName (length: 300)

        /// <summary>
        ///     Số dư của khách hàng
        /// </summary>
        public decimal BalanceAvailable { get; set; } // BalanceAvailable

        /// <summary>
        ///     Kích hoạt tài khoản 1. Kích hoạt, 0. Chưa kích hoạt
        /// </summary>
        public bool IsActive { get; set; } // IsActive

        public string IdentityCard { get; set; } // IdentityCard (length: 50)
        public string Fax { get; set; } // Fax (length: 50)
        public int? CountryId { get; set; } // CountryId
        public string CountryCode { get; set; } // CountryCode (length: 10)
        public string CountryName { get; set; } // CountryName (length: 300)
        public int? ProvinceId { get; set; } // ProvinceId
        public string ProvinceName { get; set; } // ProvinceName (length: 300)
        public string Address { get; set; } // Address

        /// <summary>
        ///     Thời gian tạo
        /// </summary>
        public DateTime CreateDate { get; set; } // CreateDate

        /// <summary>
        ///     Thời gian cập nhật
        /// </summary>
        public DateTime ModifyDate { get; set; } // ModifyDate

        public string TaxCode { get; set; } // TaxCode (length: 50)
        public string CompanyName { get; set; } // CompanyName
        public string BillingAddress { get; set; } // BillingAddress

        public byte Level { get; set; }

        public string VdcInfoJSON { get; set; }

        /// <summary>
        ///     SecondEmail của tài khoản du phong
        /// </summary>
        public string SecondEmail { get; set; } // SecondEmail (length: 50)
    }
}