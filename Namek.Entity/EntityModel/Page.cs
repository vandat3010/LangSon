using System;

namespace Namek.Entity.EntityModel
{
    // Page
    public class Page
    {
        public Page()
        {
            UnsignedName = "";
            ShowInMenu = true;
            OrderNo = 0;
            IsDeleted = false;
        }

        /// <summary>
        ///     Lưu các page được khai báo trong hệ thống
        /// </summary>
        public short Id { get; set; } // Id (Primary key)

        /// <summary>
        ///     Tên trang
        /// </summary>
        public string Name { get; set; } // Name (length: 300)

        /// <summary>
        ///     Thông tin cho tìm kiếm nhân viên (gồm: email, tên đăng nhập, họ và tên) -&gt; Dạng tiếng việt không dấu
        /// </summary>
        public string UnsignedName { get; set; } // UnsignedName

        /// <summary>
        ///     Mô tả về trang
        /// </summary>
        public string Description { get; set; } // Description (length: 500)

        /// <summary>
        ///     Id module (Trang thuộc module nào)
        /// </summary>
        public short ModuleId { get; set; } // ModuleId

        /// <summary>
        ///     Tên module
        /// </summary>
        public string ModuleName { get; set; } // ModuleName (length: 300)

        /// <summary>
        ///     Trạng thái trang có được hiển thị trong menu hay không
        /// </summary>
        public bool ShowInMenu { get; set; } // ShowInMenu

        /// <summary>
        ///     Thứ tự sắp xếp của trang
        /// </summary>
        public int OrderNo { get; set; } // OrderNo

        /// <summary>
        ///     Biểu tượng của trang trong menu
        /// </summary>
        public string Icon { get; set; } // Icon

        /// <summary>
        ///     Trạng thái đã xóa khỏi hệ thống của trang
        /// </summary>
        public bool IsDeleted { get; set; } // IsDeleted

        /// <summary>
        ///     Đường dẫn của trang
        /// </summary>
        public string Url { get; set; } // Url (length: 300)

        /// <summary>
        ///     Ngày tạo trang
        /// </summary>
        public DateTime CreateDate { get; set; } // CreateDate
    }
}