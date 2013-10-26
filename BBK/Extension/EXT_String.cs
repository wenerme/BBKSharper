using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace BBK.Extension
{
    /// <summary>
    /// 扩展String
    /// </summary>
    public static class EXT_String
    {
        #region String
        /// <summary>
        /// string.Formath 的简写,以调用字符串作为格式化字符串
        /// </summary>
        /// <param name="str">格式字符串</param>
        /// <param name="args">参数</param>
        /// <returns>格式化后的内容</returns>
        public static string FormatMe(this string str, params object[] args)
        {
            return string.Format(str, args);
        }
        /// <summary>
        /// 使用正则替换
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ReplaceRegex(this string str, string pattern, string replacement = "")
        {
            string result;
            result = Regex.Replace(str, pattern, replacement);

            return result;
        }
        /// <summary>
        /// 使用正则替换,可使用 RegexOption 参数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <param name="opt"></param>
        /// <returns></returns>
        public static string ReplaceRegex(this string str, string pattern, string replacement, RegexOptions opt)
        {
            string result;
            result = Regex.Replace(str, pattern, replacement, opt);

            return result;
        }
        /// <summary>
        /// 将字符串修正为指定宽度,超出该宽度的会被替换为 appendStr
        /// </summary>
        /// <param name="str">待修正的字符串</param>
        /// <param name="width">修整到指定的宽度</param>
        /// <param name="appendStr">替换为该字符串,可以使用正则的引用,例如$0</param>
        /// <returns>修正后的字符串</returns>
        public static string TrimToWidth(this string str, int width, string appendStr = "...")
        {
            string regex = @"(?<=^.{" + width + "}).+";
            return Regex.Replace(str, regex, appendStr, RegexOptions.Multiline);
        }
        /// <summary>
        /// 重复字符串n次
        /// </summary>
        /// <param name="str">需要重复的字符串</param>
        /// <param name="n">重复的次数</param>
        /// <returns></returns>
        public static string Repeat(this string str, int n)
        {
            if (n < 0)
                throw new Exception("重复次数必须 >= 0");

            StringBuilder ret = new StringBuilder(str.Length * n);
            while (n-- > 0)
                ret.Append(str);

            return ret.ToString();
        }
        #endregion
    }
}
