using System;

namespace Namek.Entity.EntityModel
{
    // PermissionAction
    public class PermissionAction
    {
        public PermissionAction()
        {
            IsDeleted = false;
        }

        /// <summary>
        ///     Bảng liên kết Quyền trong trang và Các hành dộng cho phép
        /// </summary>
        public int Id { get; set; } // Id (Primary key)

        /// <summary>
        ///     Id module của page
        /// </summary>
        public short ModuleId { get; set; } // ModuleId

        /// <summary>
        ///     Tên module của page
        /// </summary>
        public string ModuleName { get; set; } // ModuleName (length: 300)

        /// <summary>
        ///     Id của page
        /// </summary>
        public short PageId { get; set; } // PageId

        /// <summary>
        ///     Tên page
        /// </summary>
        public string PageName { get; set; } // PageName (length: 300)

        /// <summary>
        ///     Id của nhóm quyền (NULL cấu hình khai báo các action có trong page)
        /// </summary>
        public short? PermisionId { get; set; } // PermisionId

        /// <summary>
        ///     Tên nhóm quyền
        /// </summary>
        public string PermisionName { get; set; } // PermisionName (length: 300)

        /// <summary>
        ///     Id action
        /// </summary>
        public int ActionKey { get; set; } // ActionKey

        /// <summary>
        ///     Trạng thái đã xóa khỏi hệ thống
        /// </summary>
        public bool IsDeleted { get; set; } // IsDeleted

        /// <summary>
        ///     Ngày tạo chức vụ
        /// </summary>
        public DateTime CreateDate { get; set; } // CreateDate

        /// <summary>
        ///     Cập nhật gần nhất
        /// </summary>
        public DateTime ModifyDate { get; set; } // ModifyDate
    }
}