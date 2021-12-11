using System;

namespace Namek.Entity.EntityModel
{
    public class Os
    {
        public Os()
        {
            Code = "";
            Status = 1;
            OrderNo = "2147483647";
        }

        public int Id { get; set; } // Id (Primary key)
        public string Code { get; set; } // Code (length: 50)
        public string Type { get; set; } // Type (length: 300)
        public string Name { get; set; } // Name (length: 300)
        public byte Status { get; set; } // Status
        public bool IsDelete { get; set; } // IsDelete

        /// <summary>
        ///     Thời gian tạo
        /// </summary>
        public DateTime CreateDate { get; set; } // CreateDate

        /// <summary>
        ///     Thời gian cập nhật
        /// </summary>
        public DateTime ModifyDate { get; set; } // ModifyDate

        public string OrderNo { get; set; } // OrderNo (length: 50)
    }
}