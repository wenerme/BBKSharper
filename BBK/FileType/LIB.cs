using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.IO;

using BBK.Imaging;

namespace BBK.FileType
{
    /// <summary>
    /// BBK lib格式图片文件
    /// </summary>
    public class LIB
    {
        /// <summary>
        /// 该类型的扩展名
        /// </summary>
        public const string Extension = ".lib";
        /// <summary>
        /// 每像素点的字节数
        /// </summary>
        private const int BytePrePixel = 2;
        static LIB()
        {
            
        }

        /// <summary>
        /// 根据文件名判断是否支持该文件
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static bool isSupport(string filename)
        {
            return Path.GetExtension(filename).ToLower() == Extension;
        }
        /// <summary>
        /// 验证文件有效性
        /// </summary>
        /// <returns></returns>
        public static bool VerifyFile(Stream stream)
        {
            var lastPosition = stream.Position;
            var dataLength = stream.Length - lastPosition;
            var result = false;
            // 数据过短
            if (dataLength < 4)
                goto END;

            BinaryReader reader = new BinaryReader(stream);
            int imageCount = 0;
            int imageOffset = 0;
            int imageLength = 0;
            //
            imageCount = reader.ReadInt32();
            // 不足偏移数据长
            if (dataLength < imageCount * 4 + 4)
                goto END;
            // 跳转到最后一个图像偏移数据位置
            stream.Position = lastPosition + imageCount * 4;
            // 最后一个图像的偏移位置
            imageOffset = reader.ReadInt32();
            //
            if (dataLength < imageOffset)
                goto END;
            // 跳转到最后一个图像的偏移位置
            stream.Position = lastPosition + imageOffset;
            // 获取文件长度
            imageLength = reader.ReadInt32();
            //
            if (dataLength < imageLength + imageOffset + 4)
                goto END;
            // 验证完毕
            result = true;
            END:
            stream.Position = lastPosition;
            return result;
        }
        /// <summary>
        /// 解析文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static IList<Bitmap> FromFile(string filepath)
        {
            IList<Bitmap> list;
            using(Stream stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                list = FromStream(stream);
            }
            return list;
        }
        /// <summary>
        /// 解析<see cref="Stream"/>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static IList<Bitmap> FromStream(Stream stream)
        {
            var lastPosition = stream.Position;
            if (!VerifyFile(stream))
            {
                throw new BadFileFormatException();
            }

            IList<Bitmap> imageList = new List<Bitmap>();
            BinaryReader reader = new BinaryReader(stream);
            // 在这里使用using会导致文件关闭
            //using(BinaryReader reader = new BinaryReader(stream))
            {
                Bitmap image;
                int width, height;
                int imageCount = 0;
                IList<int> imageOffset = new List<int>();
                //
                imageCount = reader.ReadInt32();
                //
                for (var i = 0; i < imageCount; i ++ )
                {
                    imageOffset.Add(reader.ReadInt32());
                }
                //
                foreach (var offset in imageOffset)
                {
                    stream.Position = lastPosition + offset;
                    //
                    reader.ReadUInt32();// 数据长度
                    width = reader.ReadUInt16();
                    height = reader.ReadUInt16();
                    reader.ReadUInt32();// 抛弃
                    reader.ReadUInt32();// 抛弃
                    image = ImageCreator.CreateBitmapFromDataUInt16(reader, width, height, ColorFormat.ColorFromRGB565);

                    imageList.Add(image);
                }
            }
            stream.Position = lastPosition;
            return imageList;
        }

        /// <summary>
        /// 根据文件列表计算文件尺寸
        /// </summary>
        /// <param name="imageList"></param>
        /// <returns></returns>
        public static int GetFileSize(IList<Bitmap> imageList)
        {
            int size = 0;
            size += 4;// 图片数量
            size += imageList.Count * 4;// 图片偏移位置
            size += imageList.Count * 16;// 图片头

            foreach (var image in imageList)
            {
                // 图片尺寸 * 每个点的字节数(UInt16 = 2)
                size += image.Width * image.Height * BytePrePixel;
            }
            return size;
        }

        /// <summary>
        /// 将文件列表存储为文件
        /// </summary>
        /// <param name="imageList"></param>
        /// <param name="filename"></param>
        public static void SaveFile(IList<Bitmap> imageList, string filename)
        {
            using (FileStream stream = File.Open(filename, FileMode.Create, FileAccess.Write))
            using(BinaryWriter writer = new BinaryWriter(stream))
            {
                IList<int> imageOffset = new List<int>();
                int dataOffset = 4 + imageList.Count * 4;// 数据开始偏移

                // 计算文件偏移
                {
                    int offset = dataOffset;
                    foreach (var image in imageList)
                    {
                        imageOffset.Add(offset);
                        offset += 16;// 图片头
                        offset += image.Width * image.Height * BytePrePixel;// 图片尺寸
                    }
                }
                //
                stream.SetLength(GetFileSize(imageList));// 设定文件大小
                writer.Write(imageList.Count);// 写入图片数量
                // 写入图片偏移
                foreach (int offset in imageOffset)
                {
                    writer.Write(offset);
                }
                // 写入每一个图片
                foreach (var image in imageList)
                {
                    // 写入文件头
                    writer.Write(image.Width * image.Height * BytePrePixel + 12);// 文件尺寸 + 文件头长度
                    writer.Write((ushort)image.Width);
                    writer.Write((ushort)image.Height);
                    writer.Write((int)65536);
                    writer.Write((int)0);
                    // 写入数据
                    ImageCreator.StoreBitmapToDataUInt16(writer, image, ColorFormat.ToRGB565);
                }
            }
            //
        }
    }
}
