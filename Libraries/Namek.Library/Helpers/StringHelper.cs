using System.Text.RegularExpressions;

namespace Namek.Library.Helpers
{
    public class StringHelpers
    {
        private const string UnsignChars =
                "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU"
            ;

        private const string UnicodeChars =
                "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ"
            ;

        /// <summary>
        ///     Chuyển đổi chuỗi ký tự có dấu sang chuỗi ký tự không dấu.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnicodeToUnsign(string str)
        {
            var retVal = string.Empty;
            if (str == null)
                return retVal;
            for (var i = 0; i < str.Length; i++)
            {
                var pos = UnicodeChars.IndexOf(str[i].ToString());
                if (pos >= 0)
                    retVal += UnsignChars[pos];
                else
                    retVal += str[i];
            }
            return retVal.TrimEnd().Replace(" ", "-");
        }

        /// <summary>
        ///     Chuyển chuối tiếng việt về không dấu và thêm gạch - giữa hai từ
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UrlRewriting(string s)
        {
            var retVal = "";
            if (!string.IsNullOrEmpty(s))
            {
                //Chuyển thành không dấu và thêm ký tự '-' ở mỗi từ
                retVal = ConvertToUrlStandard(s);
                //Xóa ký tự đặc biệt
                retVal = ReplaceSpecialChar(retVal.TrimEnd().ToLower());
                //Lấy tối đa 100 ký tự
                retVal = GetSubString(retVal, 100, "");
            }
            if (retVal == "" || retVal == "con") retVal = "title";
            return retVal;
        }

        /// <summary>
        ///     Chuyển thành không dấu và thêm ký tự '-' ở mỗi từ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ConvertToUrlStandard(string input)
        {
            var tmp = UnicodeToUnsign(input);
            var re = new Regex("[^a-zA-Z0-9_]");
            tmp = re.Replace(tmp, "_");
            return tmp;
        }

        /// <summary>
        ///     Xóa ký tự đặc biệt trong chuỗi
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string ReplaceSpecialChar(string src)
        {
            var specialChars = " ,~,`,!,@,#,$,%,^,&,*,(,),<,>,?,:,|,+,{,},\\,/,\",;,.,\r,\n,\t,=,_,[,]";
            var arrSc = specialChars.Split(',');
            for (var i = 0; i < arrSc.Length; i++)
                try
                {
                    src = src.Replace(arrSc[i], "-");
                }
                catch { }
            while (src.Contains("--"))
                src = src.Replace("--", "-");
            if (src.EndsWith("-"))
                src = src.Substring(0, src.Length - 1);
            return src;
        }

        /// <summary>
        ///     Lấy chuỗi con trong 1 chuỗi
        /// </summary>
        /// <param name="source"></param>
        /// <param name="numberCharacter"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string GetSubString(string source, int numberCharacter = 150, string ext = "...")
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            var leng = 0;
            if (source.Trim().Length > numberCharacter)
            {
                leng = numberCharacter;
                return source.Substring(0, leng) + ext;
            }
            leng = source.Trim().Length;
            return source.Substring(0, leng);
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