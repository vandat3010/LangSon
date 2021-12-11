using System;
using System.Collections.Generic;
using System.Globalization;
using Namek.Library.Enums;

namespace Namek.Library.Helpers
{
    public static class NamekHelpers
    {
        private static readonly Random _rng = new Random();
        private static readonly string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        ///     Chuyển đổi giá tiền theo đơn vị tính
        /// </summary>
        /// <param name="price">Giá dịch vụ</param>
        /// <param name="fromUnit">Đơn vị tính giá dịch vụ</param>
        /// <param name="toUnit">Đơn vị giá đích</param>
        /// <returns></returns>
        public static decimal ConvertPrice(decimal price, TimeUnit fromUnit, TimeUnit toUnit, int round = 4)
        {
            var dic = new Dictionary<byte, int[]>
            {
                {0, new[] {1, 24}},
                {1, new[] {1, 1}},
                {2, new[] {365, 12}},
                {3, new[] {365, 1}}
            };

            //return Math.Round(price / dic[(byte)from] * dic[(byte)to], 4);
            return Math.Round(
                price * dic[(byte) fromUnit][1] / dic[(byte) fromUnit][0] * dic[(byte) toUnit][0] /
                dic[(byte) toUnit][1], round);
        }

        public static float ConvertTime(int time, TimeUnit fromUnit, TimeUnit toUnit)
        {
            if ((byte) fromUnit == (byte) toUnit)
                return time;
            var dic = new Dictionary<byte, int[]>
            {
                {0, new[] {1, 24}},
                {1, new[] {1, 1}},
                {2, new[] {365, 12}},
                {3, new[] {365, 1}}
            };
            return time * dic[(byte) fromUnit][0] / (float) dic[(byte) fromUnit][1] * dic[(byte) toUnit][1] /
                   dic[(byte) toUnit][0];
        }

        public static float ConvertTimeByBlock(int time, TimeUnit fromUnit, TimeUnit toUnit)
        {
            if ((byte)fromUnit == (byte)toUnit)
                return time;
            var dic = new Dictionary<byte, int[]>
            {
                {0, new[] {1, 24}},
                {1, new[] {1, 1}},
                {2, new[] {360, 12}},
                {3, new[] {360, 1}}
            };
            return time * dic[(byte)fromUnit][0] / (float)dic[(byte)fromUnit][1] * dic[(byte)toUnit][1] /
                   dic[(byte)toUnit][0];
        }
        /// <summary>
        ///     @TuanDV: Tính thời gian kết thúc của gói dịch vụ
        /// </summary>
        /// <param name="beginDateTime">Bắt đầu</param>
        /// <param name="timeUnit">Đơn vị thời gian</param>
        /// <param name="time">Thời gian</param>
        /// <returns></returns>
        public static DateTime GetEndDateTime(this DateTime beginDateTime, TimeUnit timeUnit, int time)
        {
            DateTime endDateTime;

            if (timeUnit == TimeUnit.Hourly)
                endDateTime = beginDateTime.AddHours(time);
            else if (timeUnit == TimeUnit.Daily)
                endDateTime = beginDateTime.AddDays(time);
            else if (timeUnit == TimeUnit.Monthly)
                endDateTime = beginDateTime.AddMonths(time);
            else
                endDateTime = beginDateTime.AddYears(time);

            return endDateTime;
        }

        public static int GetTotalTime(DateTime beginDateTime, DateTime endDateTime, TimeUnit timeUnit)
        {
            if (timeUnit == TimeUnit.Hourly)
                return (int) (endDateTime - beginDateTime).TotalHours;

            return (int) (endDateTime - beginDateTime).TotalDays;
        }

        public static string FormatValueDisplay(int quantity)
        {
            if (quantity == int.MaxValue)
                return "Unlimited";

            var culture = CultureInfo.CreateSpecificCulture("vi-VN");

            return string.Format(culture, "{0:N0}", quantity);
        }

        public static string FormatValueDisplay(int quantity, string unitName, int attrId)
        {
            if (quantity == int.MaxValue)
                return "Unlimited";

            var culture = CultureInfo.CreateSpecificCulture("vi-VN");

            return attrId == 1
                ? string.Format(culture, "{0:N0}", quantity)
                : string.Format(culture, "{0:N0} {1}", quantity, unitName);
        }
 

        public static string ConvertDateTimeForExtend(int time)
        {
            var totalDays = (double) time / 24;
            var totalYears = Math.Truncate(totalDays / 365);
            var totalMonths = Math.Truncate(totalDays % 365 / 30);
            var remainingDays = Math.Truncate(totalDays % 365 % 30);

            return string.Format("{0} tháng và {1} ngày", totalMonths, remainingDays);
        }

        public static string RandomString(int size)
        {
            var buffer = new char[size];

            for (var i = 0; i < size; i++)
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            return new string(buffer);
        }
    }
}