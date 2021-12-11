using System;
using Namek.Library.Data;

namespace Namek.Library.Entity.Modules
{
    // Module
    public class Module : BaseEntity
    {
        public Module()
        {
            OrderNo = 0;
            Level = 1;
            IsDeleted = false;
        }

        public new short Id { get; set; }

        /// <summary>
        ///     Tên module
        /// </summary>
        public string Name { get; set; } // Name (length: 300)

        /// <summary>
        ///     Icon hiển thị của module
        /// </summary>
        public string Icon { get; set; } // Icon (length: 300)

        /// <summary>
        ///     Mô tả về module
        /// </summary>
        public string Description { get; set; } // Description (length: 500)

        /// <summary>
        ///     Thứ tự của module
        /// </summary>
        public int OrderNo { get; set; } // OrderNo

        /// <summary>
        ///     Id của module cha
        /// </summary>
        public short? ParentId { get; set; } // ParentId

        /// <summary>
        ///     Tên module cha
        /// </summary>
        public string ParentName { get; set; } // ParentName (length: 300)

        /// <summary>
        ///     Cấp của module (bắt đầu từ 0)
        /// </summary>
        public byte Level { get; set; } // Level

        /// <summary>
        ///     Id path của module (Dạng: IdParent.MyId)
        /// </summary>
        public string IdPath { get; set; } // IdPath (length: 500)

        /// <summary>
        ///     Name path của module dạng: (ParentName / MyName)
        /// </summary>
        public string NamePath { get; set; } // NamePath (length: 1000)

        /// <summary>
        ///     Trạng thái xóa khỏi hệ thống của module
        /// </summary>
        public bool IsDeleted { get; set; } // IsDeleted

        /// <summary>
        ///     Ngày tạo
        /// </summary>
        public DateTime CreateDate { get; set; } // CreateDate

        public DateTime ModifyDate { get; set; } // ModifyDate
        public string UnsignedName { get; set; } // UnsignedName (length: 100)
    }
}