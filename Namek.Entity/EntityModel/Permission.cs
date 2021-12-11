using System;

namespace Namek.Entity.EntityModel
{
    // GroupPermission
    public class Permission
    {
        public Permission()
        {
            IsSystem = false;
            UserNo = 0;
            IsDeleted = false;
        }

        public short Id { get; set; } // Id (Primary key)

        /// <summary>
        ///     Tên nhóm quyền
        /// </summary>
        public string Name { get; set; } // Name (length: 300)

        /// <summary>
        ///     Thông tin không dấu cho tìm kiếm nhóm quyền
        /// </summary>
        public string UnsignedName { get; set; } // UnsignedName (length: 1073741823)

        /// <summary>
        ///     Mô tả nhóm quyền
        /// </summary>
        public string Description { get; set; } // Description (length: 300)

        /// <summary>
        ///     Là nhóm quyền mặc định của hệ thống không thể xóa
        /// </summary>
        public bool IsSystem { get; set; } // IsSystem

        /// <summary>
        ///     Số lượng tài khoản đang có nhóm quyền này
        /// </summary>
        public int UserNo { get; set; } // UserNo

        /// <summary>
        ///     Ngày tạo nhóm quyền
        /// </summary>
        public DateTime CreateDate { get; set; } // CreateDate

        /// <summary>
        ///     Cập nhật gần nhất
        /// </summary>
        public DateTime ModifyDate { get; set; } // ModifyDate

        /// <summary>
        ///     Trạng thái đã xóa của nhóm quyền
        /// </summary>
        public bool IsDeleted { get; set; } // IsDeleted
    }
}