using System;
using System.Configuration;
using System.Web;

namespace Namek.Core
{
    public class Config
    {
        #region General config

        public static string UrlDomain => ConfigurationManager.AppSettings["UrlDomain"];

        public static string ConnectionString => ConfigurationManager.AppSettings["connectstring"];

        public static string[] MemcacheUserInfo => ConfigurationManager.AppSettings["MemcacheUserInfo"].Split(':');

        public static string UploadUrl => ConfigurationManager.AppSettings["UploadUrl"];

        public static string UploadPath
        {
            get
            {
                var x = ConfigurationManager.AppSettings["UploadPath"];
                return x.Contains(":\\") ? x : HttpContext.Current.Server.MapPath(x);
            }
        }

        #endregion

        #region Log config

        public static string ApplicationId
        {
            get
            {
                var str = ConfigurationManager.AppSettings["ApplicationId"];
                return string.IsNullOrEmpty(str) ? "None" : str;
            }
        }

        public static string LogErrorFolder =>
            HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["LogErrorFolder"]);

        #endregion

        #region Category config

        public static string CateIdMenu => ConfigurationManager.AppSettings["categoryIDMenu"];
        public static string CateIdIntroduction => ConfigurationManager.AppSettings["categoryIDIntroduction"];
        public static string CateIdServices => ConfigurationManager.AppSettings["categoryIDServices"];
        public static string CateIdInvestorRelations => ConfigurationManager.AppSettings["categoryIDInvestorRelations"];
        public static string CateIdNews => ConfigurationManager.AppSettings["categoryIDNews"];

        #endregion

        #region Mail config

        public static string MailServer => ConfigurationManager.AppSettings["MailServer"] ?? "smtp.gmail.com";

        public static int MailServerPort
        {
            get
            {
                int result;
                try
                {
                    result = Convert.ToInt32(ConfigurationManager.AppSettings["MailServerPort"]);
                }
                catch (Exception exception)
                {
                    result = 25;
                    ExceptionHandler.Handle(exception);
                }
                if (result < 25) result = 25;
                return result;
            }
        }

        public static bool MailServerEnableSsl
        {
            get
            {
                bool result;
                try
                {
                    result = Convert.ToBoolean(ConfigurationManager.AppSettings["MailServerEnableSsl"]);
                }
                catch (Exception exception)
                {
                    result = false;
                    ExceptionHandler.Handle(exception);
                }
                return result;
            }
        }

        public static string MailAccountNoReply => ConfigurationManager.AppSettings["MailAccountNoReply"] ?? "no_reply";

        public static string MailAccountNoReplyAddress =>
            ConfigurationManager.AppSettings["MailAccountNoReply"] ?? "tvdung83@gmail.com";

        public static string MailAccountNoReplyPassword =>
            ConfigurationManager.AppSettings["MailAccountNoReplyPassword"] ?? "111111";

        #endregion

        #region Hangfire config

        public static string BackupStorageCronExpression => "0,15,30,45 * * * *";

        /// <summary>
        ///     Thời gian chạy Hangfire recruiting cron job
        /// </summary>
        public static int CronMinuteInterval
        {
            get
            {
                var result = Convert.ToInt32(ConfigurationManager.AppSettings["CronMinuteInterval"] ?? "5");
                return result;
            }
        }

        /// <summary>
        ///     Thời gian chạy Hangfire recruiting cron job
        /// </summary>
        public static int CheckNganluongOrderCronMinuteInterval
        {
            get
            {
                var result = Convert.ToInt32(ConfigurationManager.AppSettings["CheckNganluongOrderCronMinuteInterval"] ?? "15");
                return result;
            }
        }

        /// <summary>
        ///     Thời gian chạy Hangfire recruiting cron job
        /// </summary>
        public static int GetVmPerformanceCronDaily
        {
            get
            {
                var result = Convert.ToInt32(ConfigurationManager.AppSettings["GetVmPerformanceCronDaily"] ?? "17");
                return result;
            }
        }

        /// <summary>
        ///     Số lần Hangfire tự động retry
        /// </summary>
        public static int HangfireAutomaticRetry
        {
            get
            {
                var result = Convert.ToInt32(ConfigurationManager.AppSettings["HangfireAutomaticRetry"] ?? "0");

                return result;
            }
        }

        public static int HangfireWorkersRatio =>
            Convert.ToInt32(ConfigurationManager.AppSettings["HangfireWorkersRatio"] ?? "5");

        public static bool IsAddVmHistoryJob
        {
            get
            {
                var result =
                    Convert.ToBoolean(ConfigurationManager.AppSettings["IsAddVmHistoryJob"] ?? "false");

                return result;
            }
        }

        /// <summary>
        ///     Số lần Hangfire tự động retry khi call API Viettel bị time out
        /// </summary>
        public static int HangfireAutomaticRetryCallApiTimeout
        {
            get
            {
                var result =
                    Convert.ToInt32(ConfigurationManager.AppSettings["HangfireAutomaticRetryCallApiTimeout"] ?? "1");

                return result;
            }
        }

        /// <summary>
        ///     NameKContext Hangfire Connection
        /// </summary>
        public static string NamekContextHangfireConnection =>
            ConfigurationManager.AppSettings["NameKContextHangfire"] ??
            "NameKContextHangfire";

        /// <summary>
        ///     Use HangfireServer
        /// </summary>
        public static bool UseHangfireServer
        {
            get
            {
                bool result;
                try
                {
                    result = Convert.ToBoolean(ConfigurationManager.AppSettings["UseHangfireServer"] ?? "true");
                }
                catch (Exception exception)
                {
                    result = false;
                    ExceptionHandler.Handle(exception);
                }
                return result;
            }
        }

        #endregion

        #region Other

        public static int NumberOfDaysBeforeExpiration =>
            Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfDaysBeforeExpiration"] ?? "7");

        #endregion
    }
}