using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namek.Entity.EntityModel
{
    public class Service
    {
        public int Id { get; set; } // Id (Primary key)

        /// <summary>
        ///     Tên dịch vụ (vd: Web Hosting, Email Hosting, VPS)
        /// </summary>
        public string Name { get; set; } // Name (length: 300)

        /// <summary>
        ///     Thứ tự ưu tiên hiển thị của Service
        /// </summary>
        public int OrderNo { get; set; } // OrderNo

        /// <summary>
        ///     Trạng thái vd: 0: mới tạo, 1: Đang sử dụng,..
        /// </summary>
        public byte Status { get; set; } // Status

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

        public int Type { get; set; } // Type

        public string Description { get; set; } // Type
    }
}
