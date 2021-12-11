using System;

namespace Namek.Entity.InfoModel
{
    // LogAction
    public class LogActionInfo
    {
        public long Id { get; set; } // Id (Primary key)

        /// <summary>
        ///     Id trang
        /// </summary>
        public int? PageId { get; set; } // PageId

        /// <summary>
        ///     Tên trang
        /// </summary>
        public string PageName { get; set; } // PageName (length: 300)

        /// <summary>
        ///     Id thao tác
        /// </summary>
        public int? ActionId { get; set; } // ActionId

        /// <summary>
        ///     Tên thao tác
        /// </summary>
        public string ActionName { get; set; } // ActionName (length: 300)

        /// <summary>
        ///     Id nhân viên
        /// </summary>
        public int UserId { get; set; } // UserId

        /// <summary>
        ///     Tên nhân viên
        /// </summary>
        public string UserFullName { get; set; } // UserFullName (length: 300)

        /// <summary>
        ///     Ngày tạo
        /// </summary>
        public DateTime CreateDate { get; set; } // CreateDate

        /// <summary>
        ///     Id vai trò của tài khoản (vd: 0: Khách hàng, 1: Sale, 2: Admin, 3.Accouting)
        /// </summary>
        public int? RoleId { get; set; } // RoleId

        /// <summary>
        ///     Tên vai trò của tài khoản (vd: Khách hàng, Sale, Admin, Accouting)
        /// </summary>
        public string RoleName { get; set; } // RoleName (length: 50)

        /// <summary>
        ///     Nội dung
        /// </summary>
        public string Content { get; set; } // Content

        /// <summary>
        ///     Id phiên làm việc
        /// </summary>
        public string SessionId { get; set; } // SessionId (length: 50)

        /// <summary>
        ///     Đối tượng dữ liệu
        /// </summary>
        public string DataJson { get; set; } // DataJson

        public string UnsignedName { get; set; } // UnsignedName (length: 100)
    }
}