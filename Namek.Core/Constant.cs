using System.Configuration;

namespace Namek.Core
{
    public static class Constant
    {
        public const string KeyPassWord = "GKHTDGDS";
        public const string CaptchaPassWord = "SDGFASDF";

        public const string SessionUserId = "SessionUserId";
        public const string SessionUserName = "SessionUserName";

        public const string SessionUser = "SessionUser";
        public const string SessionTotalView = "SessionTotalView";

        public const string SessionLanguage = "CurrentCulture";

        public const string OlineUser = "OlineUser";
        public const string OlineUserInDay = "OlineUserInDay";
        public const string Day = "Day";

        public static int PageSize = ConfigurationManager.AppSettings["DefaultPageSize"] == null
            ? 10
            : int.Parse(ConfigurationManager.AppSettings["DefaultPageSize"]);

        public static string KeyNewsHot(int catid)
        {
            return string.Format("KeyNewsHot_{0}", catid);
        }
    }
}