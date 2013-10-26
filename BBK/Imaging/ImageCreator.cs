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
    /// 从数据创建图象
    /// </summary>
    public static class ImageCreator
    {

        /// <summary>
        /// 从<see cref="Stream"/>创建图象
        /// </summary>
        /// <param name="reader">读取的文件流</param>
        /// <param name="w">图片宽</param>
        /// <param name="h">图片高</param>
        /// <param name="converter">颜色转换函数</param>
        /// <param name="resetPosition">是否重置位置</param>
        /// <returns></returns>
        public static Bitmap CreateBitmapFromDataUInt16(BinaryReader reader, int w, int h, Func<UInt16, Color> converter, bool resetPosition = false)
        {
            var lastPosition = reader.BaseStream.Position;
            //
            Bitmap image = new Bitmap(w, h);
            Color color;
            //
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    color = converter(reader.ReadUInt16());
                    image.SetPixel(x, y, color);
                }
            }

            //
            if (resetPosition)
                reader.BaseStream.Position = lastPosition;
            return image;
        }
        /// <summary>
        /// 从<see cref="Stream"/>创建图象
        /// </summary>
        /// <param name="reader">读取的文件流</param>
        /// <param name="w">图片宽</param>
        /// <param name="h">图片高</param>
        /// <param name="converter">颜色转换函数</param>
        /// <param name="resetPosition">是否重置位置</param>
        /// <returns></returns>
        public static Bitmap CreateBitmapFromReverseDataUInt16(BinaryReader reader, int w, int h, Func<UInt16, Color> converter, bool resetPosition = false)
        {
            var lastPosition = reader.BaseStream.Position;
            //
            Bitmap image = new Bitmap(w, h);
            Color color;
            //
            for (int y = h - 1; y >= 0; y--)
            {
                for (int x = 0; x < w; x++)
                {
                    color = converter(reader.ReadUInt16());
                    image.SetPixel(x, y, color);
                }
                //reader.BaseStream.Position += (w * 2) % 4;
            }

            //
            if (resetPosition)
                reader.BaseStream.Position = lastPosition;
            return image;
        }
 
        /// <summary>
        /// 将图片写入到流
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="image"></param>
        /// <param name="converter">颜色转换函数</param>
        /// <param name="resetPosition">是否重置位置</param>
        public static void StoreBitmapToDataUInt16(BinaryWriter writer, Bitmap image, Func<Color, UInt16> converter, bool resetPosition = false)
        {
            var lastPosition = writer.BaseStream.Position;

            UInt16 color;
            int w = image.Width;
            int h = image.Height;
            //
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    color = converter(image.GetPixel(x, y));
                    writer.Write(color);
                }
            }

            //
            if (resetPosition)
                writer.BaseStream.Position = lastPosition;
        }
    }
}
