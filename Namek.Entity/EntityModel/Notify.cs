using System;

namespace Namek.Entity.EntityModel
{
    // Notify
    public class Notify
    {
        public Notify()
        {
            FromUserId = 0;
            FromAvatar = "0";
            Mode = 0;
            Type = 0;
        }

        public long Id { get; set; } // Id (Primary key)
        public int? FromUserId { get; set; } // FromUserId
        public string FromFullName { get; set; } // FromFullName (length: 300)
        public string FromAvatar { get; set; } // FromAvatar

        /// <summary>
        ///     0: Thông báo message, 1: Thông báo email, 2: Hoạt động dịch vụ
        /// </summary>
        public byte Mode { get; set; } // Mode

        public int ToUserId { get; set; } // ToUserId
        public string ToFullName { get; set; } // ToFullName (length: 300)
        public string ToAvatar { get; set; } // ToAvatar (length: 500)
        public string Title { get; set; } // Title (length: 300)
        public string Content { get; set; } // Content

        /// <summary>
        ///     0: cảnh báo, 1: thông tin, 2: Cảnh báo của khách hàng, 3: Thông tin khách hàng
        /// </summary>
        public byte Type { get; set; } // Type

        public string Url { get; set; } // Url
        public DateTime SendTime { get; set; } // SendTime
        public DateTime? ReadTime { get; set; } // ReadTime
    }
}