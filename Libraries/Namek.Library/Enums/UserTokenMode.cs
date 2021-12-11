using System.ComponentModel;

namespace Namek.Library.Enums
{
    public enum UserTokenMode
    {
        /// <summary>
        ///     0: Token cho việc khôi phục mật khẩu
        /// </summary>
        [Description("Khôi phục mật khẩu")] PasswordRecovery = 0,

        /// <summary>
        ///     1: Token cho việc kích hoạt tài khoản
        /// </summary>
        [Description("Kích hoạt tài khoản")] AccountActivation = 1
    }
}