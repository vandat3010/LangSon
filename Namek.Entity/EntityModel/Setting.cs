namespace Namek.Entity.EntityModel
{
    // Setting
    public class Setting
    {
        public long Id { get; set; } // Id (Primary key)

        /// <summary>
        ///     Key cấu hình
        /// </summary>
        public string SettingKey { get; set; } // SettingKey

        /// <summary>
        ///     Giá trị cấu hình
        /// </summary>
        public string SettingValue { get; set; } // SettingValue (length: 1073741823)
    }
}