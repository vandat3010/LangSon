using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;
using Namek.Core.Utility;

namespace Namek.Core
{
    public class ExceptionHandler
    {
        /// <summary>
        ///     Handle Exception and write to log file
        /// </summary>
        /// <param name="ex">Exception object</param>
        public static void Handle(Exception ex)
        {
            Handle(ex, string.Empty, string.Empty);
        }

        /// <summary>
        ///     Handle Exception and write to log file
        /// </summary>
        /// <param name="ex">Exception object</param>
        /// <param name="className">Name of class, where the error occurred.</param>
        public static void Handle(Exception ex, string className)
        {
            Handle(ex, className, string.Empty);
        }

        /// <summary>
        ///     Handle Exception and write to log file
        /// </summary>
        /// <param name="ex">Exception object</param>
        /// <param name="className">Name of class, where the error occurred.</param>
        /// <param name="functionName">Name of function, where the error occurred.</param>
        public static void Handle(Exception ex, string className, string functionName)
        {
            Handle(ex, className, functionName, "Tinmoi_Logs.txt");
        }

        /// <summary>
        ///     Handle Exception and write to log file
        /// </summary>
        /// <param name="ex">Exception object</param>
        /// <param name="className">Name of class, where the error occurred.</param>
        /// <param name="functionName">Name of function, where the error occurred.</param>
        /// <param name="fileName">Name of file</param>
        public static void Handle(Exception ex, string className, string functionName, string fileName)
        {
            if (ex == null) return;
            var trace = new StackTrace(ex, true);
            var site = ex.TargetSite;
            var innerMethodName = site == null ? string.Empty : site.Name;
            var innerClassName = site == null ? string.Empty : site.DeclaringType.Name;
            var sb = new StringBuilder();

            sb.Append("Time:");
            sb.Append(DateTime.Now);

            //client ip
            //var clientIp = ClientHelper.GetClientIPAddress();
            //if (string.IsNullOrEmpty(clientIp))
            //    sb.Append(" |ClientIp:" + clientIp);

            sb.Append(" |AppId:" + Config.ApplicationId);
            sb.Append(" |Class:");
            sb.Append(className);
            sb.Append(" |Method:");
            sb.Append(functionName);
            if (!string.IsNullOrEmpty(innerMethodName) || !string.IsNullOrEmpty(innerClassName))
            {
                sb.Append(" |innerClass:");
                sb.Append(innerClassName);
                sb.Append(" |innerMethod:");
                sb.Append(innerMethodName);
            }
            if (trace != null && trace.GetFrame(0) != null)
            {
                sb.Append(" |Line:");
                sb.Append(trace.GetFrame(0).GetFileLineNumber());
            }
            sb.Append(" |Error: ");
            sb.Append(ex.Message);
            sb.AppendLine();
            if (HttpContext.Current != null)
            {
                sb.Append("URL:");
                sb.Append(HttpContext.Current.Request.RawUrl);
                sb.AppendLine();
                sb.Append(HttpContext.Current.Request.UserAgent);
                sb.AppendLine();
            }

            sb.Append("Trace:" + ex.StackTrace);
            sb.AppendLine();
            sb.Append("------------------------------------------");

            FileUtility.AppendToTextFile(sb.ToString(), Config.LogErrorFolder,
                DateTime.Now.ToString("yyyyMMdd_") + fileName);
            Debug.WriteLine(sb.ToString());
        }

        public static void LogMessage(string Message, string filePath, string fileName)
        {
            FileUtility.AppendToTextFile(Message, Path.Combine(Config.LogErrorFolder, filePath), fileName);
        }
    }
}