using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

using BBK.FileType;

namespace BBK.Imaging
{
    /// <summary>
    /// 提供了色彩格式的转换
    /// 扩展了Color类
    /// </summary>
    public static class ColorFormat
    {
        //
        public static Color ColorFromRGB565(UInt16 b)
        {
            Color color;
            color = Color.FromArgb((b >> 11 & 31) << 3, (b >> 5 & 63) << 2, (b & 31) << 3);

            return color;
        }
        public static Color ColorFromBGR565(UInt16 b)
        {
            Color color;
            color = Color.FromArgb((b & 31) << 3, (b >> 5 & 63) << 2, (b >> 11 & 31) << 3);

            return color;
        }
        //
        public static Color ColorFromRGB555(UInt16 b)
        {
            Color color;
            color = Color.FromArgb((b >> 11 & 0x1f) << 3, (b >> 5 & 0x1f) << 2, (b & 31) << 3);

            return color;
        }
        public static Color ColorFromBGR555(UInt16 b)
        {
            Color color;
            color = Color.FromArgb((b & 31) << 3, (b >> 5 & 31) << 2, (b >> 10 & 31) << 3);

            return color;
        }

        public static UInt16 ToRGB565(this Color color)
        {
            UInt16 r;
            r = Convert.ToUInt16((color.R >> 3) << 11 | (color.G >> 2) << 5 | color.B >> 3);
            return r;
        }
        public static UInt16 ToBGR565(this Color color)
        {
            UInt16 r;
            r = Convert.ToUInt16((color.B >> 3) << 11 | (color.G >> 2) << 5 | color.R >> 3);
            return r;
        }
    }


}
