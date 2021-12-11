﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Namek.Core
{
    public static class StringHelper
    {
        /// <summary>
        ///     hàm loại bỏ các nguy cơ gây ra SqlInjection của một chuỗi ký tự
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string RemoveSqlInjection(string inputString)
        {
            if (string.IsNullOrEmpty(inputString)) return string.Empty;
            //thay dấu "'" = "''"
            var result = inputString.Replace("'", "''");
            //loại bỏ những từ khóa nghuy hiểm
            return result;
        }

        /// <summary>
        ///     Compare str2 with str1
        ///     str1 and str2 is plain text
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns>Percent similarity</returns>
        public static double Compare2StringBySentence(string str1, string str2)
        {
            var lengthOfStr2Pre = 0;

            try
            {
                str1 = str1.Replace(" ", string.Empty).Replace("\n", ",").Replace("\t", string.Empty)
                    .Replace("\r", string.Empty).Replace(".", ",");
                str2 = str2.Replace(" ", string.Empty).Replace("\n", string.Empty).Replace("\t", string.Empty)
                    .Replace("\r", string.Empty).Replace(".", string.Empty).Replace(",", string.Empty);

                lengthOfStr2Pre = str2.Length;
                var str1Array = str1.Split(',');
                foreach (var t in str1Array)
                {
                    if (str2.Length < 1) return 100;

                    var strTemp = t.Trim();
                    if (string.IsNullOrEmpty(strTemp)) continue;
                    str2 = str2.Replace(strTemp, string.Empty);
                }
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }

            return (double)(lengthOfStr2Pre - str2.Length) / lengthOfStr2Pre * 100;
        }

        /// <summary>
        ///     Remove một biến querystring trong một url
        /// </summary>
        /// <param name="url">Chuỗi url</param>
        /// <param name="queryStringVarName">Tên biến cần remove</param>
        /// <returns></returns>
        public static string RemoveQueryStringVarFromUrl(string url, string queryStringVarName)
        {
            if (string.IsNullOrEmpty(url)) return url;

            var urlArray = url.Split('?');
            if (urlArray.Length == 2)
            {
                var mainUrl = urlArray[0];
                var queryStringArray = urlArray[1].Split('&');

                url = mainUrl;
                foreach (var t in queryStringArray)
                {
                    if (t.Contains(queryStringVarName + "=")) continue;
                    if (url == mainUrl)
                        url += "?" + t;
                    else
                        url += "&" + t;
                }
            }
            else
            {
                return url;
            }
            return url;
        }

        /// <summary>
        ///     Count Occurence of string in other string
        /// </summary>
        /// <param name="needle">string need find</param>
        /// <param name="haystack">String contain</param>
        /// <returns>Number of occurence</returns>
        public static int CountOccurences(string needle, string haystack)
        {
            if (string.IsNullOrEmpty(needle) || string.IsNullOrEmpty(haystack)) return 0;
            int result;
            try
            {
                haystack = haystack.ToLower();
                needle = needle.ToLower();
                result = (haystack.Length - haystack.Replace(needle, string.Empty).Length) / needle.Length;
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        public static int CountNumber(char c, string s)
        {
            var res = s.Split(c);
            return res.Length - 1;
        }

        /// <summary>
        ///     bỏ hết khoảng trống thừa trong chuỗi
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string TrimSpace(string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;

            var result = source.Trim();
            while (result.Contains("  "))
                result = result.Replace("  ", " ");
            return result;
        }

        /// <summary>
        ///     bỏ hết khoảng trống thừa trong chuỗi
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string TrimEnternTab(string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;

            source = source.Replace("\n", ",");
            source = source.Replace("\t", ",");
            source = source.Replace("\r", ",");
            source = source.Replace(".", ",");
            return source;
        }

        /// <summary>
        ///     Xóa bỏ \n và \r trong chuỗi
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveBreakLine(string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;

            source = source.Replace("\n", "");
            source = source.Replace("\r", "");

            return source;
        }

        public static string RemoveCr(string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;

            source = source.Replace("\r", "");

            return source;
        }

        public static string RemoveLf(string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;

            source = source.Replace("\n", "");

            return source;
        }

        public static string ReplaceLf(string source)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;

            source = source.Replace("\r\n", "\r");
            source = source.Replace("\n", "\r");

            return source;
        }

        /// <summary>
        ///     Hàm lấy substring có xử lý việc cắt ở giữa từ
        /// </summary>
        /// <param name="input">Chuỗi truyền vào</param>
        /// <param name="len">Số ký tự cần lấy</param>
        /// <returns></returns>
        public static string SubString(string input, int len)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            if (!input.Contains(" ")) return input;
            if (len > input.Length) return input;
            return input.Substring(0, input.Substring(0, len).LastIndexOf(" ", StringComparison.Ordinal)) + "...";
        }

        /// <summary>
        ///     Bỏ các tham số của url
        /// </summary>
        /// <param name="url">Url có tham số</param>
        /// <returns>Url gốc</returns>
        public static string CutUrlForFacebookLike(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                url = url.Replace("%3a8288", "").Replace(":8288", "");
                var list = url.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
                if (list.Any())
                    return list[0];
            }
            return url;
        }

        public static string CutUrlForFacebookCommentCount(string url)
        {
            if (!url.Contains("localhost"))
                return "";
            return "";
        }

        /// <summary>
        ///     Lấy 1 substring của chuỗi HTML
        ///     Xử lý các thẻ chưa đóng
        /// </summary>
        /// <param name="html">Chuối html gốc</param>
        /// <param name="length">Độ dài cần lấy</param>
        /// <returns>Chuỗi html con đã xử lý</returns>
        public static string HtmlSubstring(string html, int length)
        {
            try
            {
                if (html == null)
                    return string.Empty;

                var unclosedTags = new List<string>();

                if (html.Length >= length)
                    for (var i = 0; i < html.Length; i++)
                    {
                        var currentCharacter = html[i];

                        var nextCharacter = ' ';

                        if (i < html.Length - 1)
                            nextCharacter = html[i + 1];

                        // Check if quotes are on.
                        if (currentCharacter == '<' && nextCharacter != ' ' && nextCharacter != '>')
                            if (nextCharacter != '/') // Open tag.
                            {
                                var startIndex = i + 1;

                                if (startIndex < html.Length)
                                {
                                    var finishIndex = html.IndexOf(">", startIndex, StringComparison.Ordinal);

                                    if (finishIndex > 0)
                                    {
                                        if (html[finishIndex - 1] != '/')
                                        {
                                            var tag = html.Substring(startIndex, finishIndex - startIndex);

                                            if (tag.Contains(" "))
                                            {
                                                var temporaryFinishIndex = html.IndexOf(" ", startIndex,
                                                    StringComparison.Ordinal);

                                                tag = html.Substring(startIndex, temporaryFinishIndex - startIndex);
                                            }

                                            if (!tag.Equals("br", StringComparison.InvariantCultureIgnoreCase))
                                                unclosedTags.Add(tag);
                                        }

                                        var tagLength = finishIndex + 1 - i;

                                        length += tagLength;

                                        i = finishIndex;
                                    }
                                }
                            }
                            else if (nextCharacter == '/') // Close tag.
                            {
                                var startIndex = i + 2;

                                if (startIndex < html.Length)
                                {
                                    var finishIndex = html.IndexOf(">", startIndex, StringComparison.Ordinal);

                                    if (finishIndex > 0)
                                    {
                                        var tag = html.Substring(startIndex, finishIndex - startIndex);

                                        // FILO.
                                        var index = unclosedTags.LastIndexOf(tag);

                                        if (index >= 0)
                                        {
                                            unclosedTags.RemoveAt(index);

                                            var tagLength = finishIndex + 1 - i;

                                            length += tagLength;

                                            i = finishIndex;
                                        }
                                    }
                                }
                            }

                        if (i >= length)
                        {
                            html = $"{html.Substring(0, i)}...";

                            unclosedTags.Reverse();

                            return unclosedTags.Aggregate(html,
                                (current, unclosedTag) => current + $"</{unclosedTag}>");
                        }
                    }
            }
            catch (Exception exception)
            {
                Console.Write(exception.Message);
            }

            return html;
        }

        /// <summary>
        ///     take letters and number only
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string TakeLettersAndNumberOnly(string content)
        {
            const string pattern = "[^a-zA-Z0-9]"; //regex pattern
            var result = Regex.Replace(content, pattern, "");
            return result;
        }
        //public static double Compare(string str1, string str2)
        //{
        //    if (Utility.IsStringNullOrEmpty(str1))
        //        return 0;
        //    if (Utility.IsStringNullOrEmpty(str2))
        //        return 0;

        //    str1 = str1.Replace("&nbsp;", " ").ToLower().Trim();
        //    str2 = str2.Replace("&nbsp;", " ").ToLower().Trim();
        //    var kq = Fill2Array(str1, str2);

        //    var tu = Convert.ToInt32(kq[str1.Length, str2.Length]);
        //    var mau = Convert.ToInt32(str2.Length);

        //    return (tu / (double)mau) * 100;
        //}

        public static int[,] Fill2Array(string input1, string input2)
        {
            var max = Max(input1.Length, input2.Length);
            var kq = new int[max + 1, max + 1];
            for (var i = 0; i <= input1.Length; i++)
                kq[i, 0] = 0;
            for (var i = 0; i <= input2.Length; i++)
                kq[0, i] = 0;
            for (var i = 1; i <= input1.Length; i++)
                for (var j = 1; j <= input2.Length; j++)
                    if (input1[i - 1] == input2[j - 1])
                        kq[i, j] = Convert.ToInt32(kq[i - 1, j - 1]) + 1;
                    else kq[i, j] = Max(Convert.ToInt32(kq[i, j - 1]), Convert.ToInt32(kq[i - 1, j]));
            return kq;
        }

        public static int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        /// <summary>
        ///     Hàm loại bỏ các chữ sấu
        /// </summary>
        /// <param name="data">Doạn chữ cần loại bỏ chữ sấu</param>
        /// <param name="listRemoChar">Danh sách chữ sấu cần loại bỏ</param>
        /// <param name="dataSplipChar">kí tự phân biết giữa các chữ trong đoạn thường là ' '</param>
        /// <param name="replateString">Những chữ sâu sẽ được chuyển thành chuỗi này.</param>
        /// <returns>Mỗi chuỗi không chưa các chữ sấu</returns>
        public static string RemoveBadString(string data, string listRemoChar, char dataSplipChar, string replateString)
        {
            try
            {
                if (!string.IsNullOrEmpty(listRemoChar))
                {
                    var badSringList = listRemoChar.Split(';');
                    //Phần chia nội dung thành từng chữ để check.
                    var dataSplipText = data.Split(dataSplipChar);
                    var dataReturn = string.Empty;
                    //Lặp từ chữ một
                    foreach (var item in dataSplipText)
                    {
                        var isBad = badSringList.Any(itemCheck => item.ToLower().Contains(itemCheck));
                        //Với mỗi chữ thì lặp toàn bộ chữ cần loại bỏ
                        //Kiểm tra và thay thế chuỗi.
                        if (isBad)
                            dataReturn = dataReturn + dataSplipChar + replateString;
                        else
                            dataReturn = dataReturn + dataSplipChar + item;
                    }
                    return dataReturn;
                }
                return data;
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex, "StringHelper", "RemoveBadString", "StringHelper.cs");
                return data;
            }
        }

        public static string RatingAverage(int totalVote, double totalPoint)
        {
            if (totalVote == 0)
                return "0";
            var rs = totalPoint / totalVote;
            return Math.Round(rs, 1).ToString(CultureInfo.InvariantCulture);
            //return ((int)RatingCount > 0) ? ((int)RatingPoint + (int)RatingCount * 1 / 2) / (int)RatingCount : 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string ConvetSecondToString(double second)
        {
            try
            {
                var dt = TimeSpan.FromSeconds(second);
                return dt.ToString(); //string.Format("{0}:{1}:{2}", t.Hours, t.Minutes, t.Seconds);
            }
            catch (Exception)
            {
                return "00:00:00";
            }
        }

        #region Convert Unicode To Url

        public static string UnicodeToUrl(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            //cắt chuỗi tránh hiện tương url quá dài
            const int maxLengUrl = 120;
            if (s.Length > maxLengUrl) s = s.Substring(0, maxLengUrl).Trim();

            // xóa dấu tiếng việt
            var strReturn = RejectMarks(s);

            // giữ lại dấu '-' và '_'
            strReturn = strReturn.Replace("-", " ").Replace("_", " ");

            // xóa các ký tự đặc biệt
            strReturn = RemoveSpecialCharacters(strReturn);

            strReturn = strReturn.Replace(" ", "-");
            while (strReturn.Contains("--")) strReturn = strReturn.Replace("--", "-");

            return strReturn.ToLower();
        }

        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, @"[^a-zA-Z0-9\s]+", "", RegexOptions.Compiled).Trim();
        }

        public static string RejectMarks(string s)
        {
            var pattern = new string[14];

            pattern[0] = "a|(á|ả|à|ạ|ã|ă|ắ|ẳ|ằ|ặ|ẵ|â|ấ|ẩ|ầ|ậ|ẫ)";

            pattern[1] = "A|(Á|Ả|À|Ạ|Ã|Ă|Ắ|Ẳ|Ằ|Ặ|Ẵ|Â|Ấ|Ẩ|Ầ|Ậ|Ẫ)";

            pattern[2] = "o|(ó|ỏ|ò|ọ|õ|ô|ố|ổ|ồ|ộ|ỗ|ơ|ớ|ở|ờ|ợ|ỡ)";

            pattern[3] = "O|(Ó|Ỏ|Ò|Ọ|Õ|Ô|Ố|Ổ|Ồ|Ộ|Ỗ|Ơ|Ớ|Ở|Ờ|Ợ|Ỡ)";

            pattern[4] = "e|(é|è|ẻ|ẹ|ẽ|ê|ế|ề|ể|ệ|ễ)";

            pattern[5] = "E|(É|È|Ẻ|Ẹ|Ẽ|Ê|Ế|Ề|Ể|Ệ|Ễ)";

            pattern[6] = "u|(ú|ù|ủ|ụ|ũ|ư|ứ|ừ|ử|ự|ữ)";

            pattern[7] = "U|(Ú|Ù|Ủ|Ụ|Ũ|Ư|Ứ|Ừ|Ử|Ự|Ữ)";

            pattern[8] = "i|(í|ì|ỉ|ị|ĩ)";

            pattern[9] = "I|(Í|Ì|Ỉ|Ị|Ĩ)";

            pattern[10] = "y|(ý|ỳ|ỷ|ỵ|ỹ)";

            pattern[11] = "Y|(Ý|Ỳ|Ỷ|Ỵ|Ỹ)";

            pattern[12] = "d|(đ)";

            pattern[13] = "D|(Đ)";

            foreach (var t in pattern)
            {
                // kí tự sẽ thay thế
                var replaceChar = t[0];
                var matchs = Regex.Matches(s, t);

                s = matchs.Cast<Match>().Aggregate(s, (current, m) => current.Replace(m.Value[0], replaceChar));
            }

            return s;
        }

        public static string RemoveKeySpecial(object obj)
        {
            if (obj != null)
            {
                var ret = (string)obj;
                ret = ret.Replace("\"", "");
                ret = ret.Replace("\'", "");
                return ret;
            }
            return "";
        }

        #endregion

        //public static string Utf81252Convert(string input, bool flow)
        //{
        //    if (Utility.IsStringNullOrEmpty(input))
        //        return "";

        //    int intCount;

        //    const string strUtf8Literal = "" +
        //                                  "\" à á ả ã ạ À Á Ả Ã Ạ â ầ ấ ẩ ẫ ậ Â Ầ Ấ Ẩ Ẫ Ậ ă ằ ắ ẳ ẵ ặ Ă Ằ Ắ Ẳ Ẵ Ặ " +
        //                                  "ò ó ỏ õ ọ Ò Ó Ỏ Õ Ọ ô ồ ố ổ ỗ ộ Ô Ồ Ố Ổ Ỗ Ộ ơ ờ ớ ở ỡ ợ Ơ Ờ Ớ Ở Ỡ Ợ " +
        //                                  "è é ẻ ẽ ẹ È É Ẻ Ẽ Ẹ ê ề ế ể ễ ệ Ê Ề Ế Ể Ễ Ệ " +
        //                                  "ù ú ủ ũ ụ Ù Ú Ủ Ũ Ụ ư ừ ứ ử ữ ự Ư Ừ Ứ Ử Ữ Ự " +
        //                                  "ì í ỉ ĩ ị Ì Í Ỉ Ĩ Ị ỳ ý ỷ ỹ ỵ Ỳ Ý Ỷ Ỹ Ỵ đ Đ " +
        //                                  "Đ –";

        //    var arrUtf8Literal = strUtf8Literal.Split(' ');

        //    const string strDecimal = "" +
        //                              "&quot; &#224; &#225; &#7843; &#227; &#7841; &#192; &#193; &#7842; &#195; &#7840; &#226; &#7847; &#7845; &#7849; &#7851; &#7853; &#194; &#7846; &#7844; &#7848; &#7850; &#7852; &#259; &#7857; &#7855; &#7859; &#7861; &#7863; &#258; &#7856; &#7854; &#7858; &#7860; &#7862; " +
        //                              "&#242; &#243; &#7887; &#245; &#7885; &#210; &#211; &#7886; &#213; &#7884; &#244; &#7891; &#7889; &#7893; &#7895; &#7897; &#212; &#7890; &#7888; &#7892; &#7894; &#7896; &#417; &#7901; &#7899; &#7903; &#7905; &#7907; &#416; &#7900; &#7898; &#7902; &#7904; &#7906; " +
        //                              "&#232; &#233; &#7867; &#7869; &#7865; &#200; &#201; &#7866; &#7868; &#7864; &#234; &#7873; &#7871; &#7875; &#7877; &#7879; &#202; &#7872; &#7870; &#7874; &#7876; &#7878; " +
        //                              "&#249; &#250; &#7911; &#361; &#7909; &#217; &#218; &#7910; &#360; &#7908; &#432; &#7915; &#7913; &#7917; &#7919; &#7921; &#431; &#7914; &#7912; &#7916; &#7918; &#7920; " +
        //                              "&#236; &#237; &#7881; &#297; &#7883; &#204; &#205; &#7880; &#296; &#7882; &#7923; &#253; &#7927; &#7929; &#7925; &#7922; &#221; &#7926; &#7928; &#7924; &#273; &#272; " +
        //                              "&#208; &#45;";

        //    var arrDecimal = strDecimal.Split(' ');

        //    var strResult = input;
        //    var intTotal = arrDecimal.Length;

        //    if (flow)
        //    {
        //        //UTF8 Literal --> HTML Decimal
        //        for (intCount = 0; intCount < intTotal; intCount++)
        //            strResult = strResult.Replace(arrUtf8Literal[intCount], arrDecimal[intCount]);
        //    }
        //    else
        //    {
        //        //HTML Decimal --> UTF8 Literal
        //        for (intCount = 0; intCount < intTotal; intCount++)
        //            strResult = strResult.Replace(arrDecimal[intCount], arrUtf8Literal[intCount]);
        //    }

        //    return strResult;
        //}

        #region Format Search Keywords

        /// <summary>
        ///     Format chuỗi đầu vào khi search
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FormatSearchText(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            // Xóa thẻ HTML
            var rt = RemoveAllHtmlTags(input);
            rt = rt.Replace("+", " ");

            // Xóa thẻ BBCode
            rt = Regex.Replace(rt, @"[^\w-\'\s\""]+", "", RegexOptions.Compiled);

            // Xóa ký tự đặc biệt
            rt = RemoveSolrSpecialSymbols(rt);

            // Cắt chuỗi quá dài
            if (rt.Length > 200)
                rt = rt.Substring(0, 199);

            return rt;
        }

        public static string RemoveSolrSpecialSymbols(string input)
        {
            input = RemoveSqlInjectionSymbols(input);

            // Xóa các ký tự đặc biệt của Solr
            char[] removeChars =
                {'!', '@', '[', ']', ':', '-', '+', '(', ')', '{', '}', '~', '#', '$', '%', '^', '&', '*', '?', '\\'};

            input = removeChars.Aggregate(input,
                (current, removeChar) => current.Replace(removeChar.ToString(CultureInfo.InvariantCulture), " "));

            // Xóa ký tự đúp
            var doubleSymbol = new[] { "  ", "\"\"", "\'\'" };
            foreach (var sb in doubleSymbol)
                while (input.Contains(sb))
                    input = input.Replace(sb, " ");

            return input.Trim();
        }

        public static string RemoveAllHtmlTags(object input)
        {
            var htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);
            var output = input.ToString().Trim();
            return htmlRegex.Replace(output, string.Empty);
        }

        /// <summary>
        ///     hàm loại bỏ các nguy cơ gây ra SqlInjection của một chuỗi ký tự
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string RemoveSqlInjectionSymbols(string inputString)
        {
            if (string.IsNullOrEmpty(inputString)) return string.Empty;

            //thay dấu "'" = "''"
            var result = inputString.Replace("'", "''");

            // Thêm dấu đóng "
            if (CountNumber('"', result) % 2 == 1)
                result += "\"";

            return result;
        }

        /// <summary>
        ///     Bao từ khóa bằng dấu nháy kép
        /// </summary>
        /// <param name="input"></param>
        public static string WrapKeywordWithQuoteSymbols(string input)
        {
            // Xử lý từ khóa
            input = FormatSearchText(input);

            // Nếu từ khóa không chứa ký tự "
            if (!input.Contains("\"")) return "\"" + input + "\"";

            return input;
        }

        #endregion

        #region Danh sách hàm dùng để chỉnh sửa nội dung html

        /// <summary>
        ///     Hàm thay đổi tag nọ tành tág kia
        /// </summary>
        /// <param name="htmlContent">Nội dung html cần thay đổi</param>
        /// <param name="curetTagName">Tên Tag hiện tại cần thay đổi</param>
        /// <param name="newTagName">Tên Tag mới</param>
        /// <returns>Trả về string đã thay đổi.</returns>
        public static string ChangeTag(string htmlContent, string curetTagName, string newTagName)
        {
            var startTag = "<" + curetTagName;
            var endTag = "</" + curetTagName + ">";
            var newStartTag = "<" + newTagName;
            var newEndTag = "</" + newTagName + ">";
            htmlContent = htmlContent.Replace(startTag, newStartTag);
            htmlContent = htmlContent.Replace(endTag, newEndTag);
            htmlContent = htmlContent.Replace(startTag.ToUpper(), newStartTag);
            htmlContent = htmlContent.Replace(endTag.ToUpper(), newEndTag);
            return htmlContent;
        }

        /// <summary>
        ///     Hàm loại bỏ một thuộc tính của các element trong một html truỗi
        /// </summary>
        /// <param name="htmlContent">Nội dung html</param>
        /// <param name="propetieName">Tên thuộc tính</param>
        /// <returns>Chuỗi trả về</returns>
        public static string RemovePropetie(string htmlContent, string propetieName)
        {
            var notCssPropetie = propetieName + "=\"(.*?)\"";
            var notCssPropetie2 = propetieName + "=(.*?)&amp;";
            var notCssPropetie1 = propetieName + "=(.*?) ";
            var cssPropetie = propetieName + ":(.*?);";
            var cssPropetie1 = propetieName + ":(.*?)\"";
            htmlContent = Regex.Replace(htmlContent, notCssPropetie, "");
            htmlContent = Regex.Replace(htmlContent, notCssPropetie1, "");
            htmlContent = Regex.Replace(htmlContent, notCssPropetie2, "");
            htmlContent = Regex.Replace(htmlContent, cssPropetie, ";");
            htmlContent = Regex.Replace(htmlContent, cssPropetie1, "\"");
            htmlContent = Regex.Replace(htmlContent, notCssPropetie.ToUpper(), "");
            htmlContent = Regex.Replace(htmlContent, notCssPropetie1.ToUpper(), "");
            htmlContent = Regex.Replace(htmlContent, notCssPropetie2.ToUpper(), "");
            htmlContent = Regex.Replace(htmlContent, cssPropetie.ToUpper(), ";");
            htmlContent = Regex.Replace(htmlContent, cssPropetie1.ToUpper(), "\"");
            return htmlContent;
        }

        #endregion

        /// <summary>
        ///     Checking return url is same with the current host or not
        /// </summary>
        /// <param name="url">return url string</param>
        /// <returns></returns>
        public static bool IsLocalUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return false;
            return url[0] == '/' && (url.Length == 1 ||
                                     url[1] != '/' && url[1] != '\\') || // "/" or "/foo" but not "//" or "/\"
                   url.Length > 1 &&
                   url[0] == '~' && url[1] == '/'; // "~/" or "~/foo"
        }

        public static string StringFormat(string format, params object[] args)
        {
            try
            {
                return string.Format(format, args);
            }
            catch
            {
                return format;
            }
        }
    }
}