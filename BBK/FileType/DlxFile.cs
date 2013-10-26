using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using BBK.Imaging;

namespace BBK.FileType
{
    /// <summary>
    /// Dlx类型文件
    /// </summary>
    public class DlxFile : IBitmapContainerFile
    {
        /// <summary>
        /// 该类型的扩展名
        /// </summary>
        public const string Extension = ".dlx";

        /// <summary>
        /// 文件签名,最长为16字节
        /// </summary>
        public string Signature {get;set;}

        public IList<Bitmap> ImageList { get; private set; }

        public DlxFile()
        {
            // 初始化
            ImageList = new List<Bitmap>();
            Signature = "www.wener.me";
        }
        public DlxFile(string filepath)
            :this()
        {
            
            AppendFrom(filepath);
        }
        public DlxFile(Stream stream)
            : this()
        {
            AppendFrom(stream);
        }

        #region static
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
        /// 验证文件
        /// </summary>
        /// <returns></returns>
        public static bool VerifyFile(string filepath)
        {
            var result = false;
            using (Stream stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                result = VerifyFile(stream);
            }

            return result;
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
            int baseOffset = 0;
            // 文件签名不匹配
            if (reader.ReadBytes(3).ToString() == "DLX")
                goto END;

            imageCount = reader.ReadByte();
            // 如果图片数量为0张,则不必再检测了
            if (imageCount == 0)
            {
                result = true;
                goto END;
            }

            reader.ReadBytes(8);// 固定信息
            baseOffset = reader.ReadInt32();// 第一张的基地址
            if (dataLength < baseOffset)
                goto END;

            reader.ReadInt32();// 数据长度 这个数据不可靠
            reader.ReadChars(16);// 签名
            // 读取偏移
            {
                int offset;
                int size;
                for (var i = 0; i < imageCount; i++)
                {
                    reader.ReadBytes(4);// 分割
                    offset = reader.ReadInt32() + baseOffset;
                    size = reader.ReadInt32();// 尺寸
                    if (dataLength < offset + size)
                        goto END;
                }
            }

            // 验证完毕
            result = true;
        END:
            stream.Position = lastPosition;
            return result;
        }


        #endregion

        public void SaveAs(string filepath)
        {
            using (Stream stream = File.Open(filepath, FileMode.Create, FileAccess.Write))
            {
                SaveAs(stream);
            }
        }

        public void SaveAs(Stream stream)
        {
            IList<int> imageOffset = new List<int>();
            IList<FileStream> fileList = new List<FileStream>();
            BinaryWriter writer = new BinaryWriter(stream);
            int baseOffset = 0;
            // 写入文件头
            {
                int offset = 0;
                writer.Write("DLX".ToCharArray());
                writer.Write((byte)ImageList.Count);
                writer.Write((UInt64)0x0103000008118119);
                writer.Write((int)0);// 第一张偏移的占位符
                writer.Write("wen\0".ToCharArray());// 文件尺寸位置 但是可以不必是文件尺寸
                
                offset = (int)writer.BaseStream.Position;
                writer.Write(Signature.ToCharArray());// 写入签名
                writer.BaseStream.Position = offset + 16;

                // 写入偏移
                offset = 0;
                int size = 0;
                foreach (var image in ImageList)
                {
                    writer.Write((uint)1);
                    writer.Write(offset);
                    // 单个图片数据长度
                    size = image.Width * image.Height * 2 + 24;
                    writer.Write(size);

                    offset += size;
                }
            }
            // 当前位置为第一张的偏移
            {
                baseOffset = (int)writer.BaseStream.Position;
                writer.BaseStream.Position = 0x0c;
                writer.Write(baseOffset);
                writer.BaseStream.Position = baseOffset;
            }
            // 写入图片数据
            {
                foreach (var image in ImageList)
                {
                    writer.Write("VX".ToCharArray());
                    writer.Write(new byte[] { 0xcc, 0xcc, 0xcc, 0xcc});
                    writer.Write(image.Width);
                    writer.Write(image.Height);
                    writer.Write(new byte[] { 0xcc, 0xcc, 0xcc, 0xcc, 0xcc, 0xcc, 0xff, 0xff,0xff,0xff });
                    // 写入数据
                    ImageCreator.StoreBitmapToDataUInt16(writer, image, ColorFormat.ToRGB565);
                }
            }
            

        }

        public void AppendFrom(string filepath)
        {
            using (Stream stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                AppendFrom(stream);
            }
        }

        public void AppendFrom(Stream stream)
        {
            var startPosition = stream.Position;
            if (!VerifyFile(stream))
            {
                throw new BadFileFormatException();
            }

            BinaryReader reader = new BinaryReader(stream);
            // 解析过程
            {
                Bitmap image;
                int imageCount = 0;
                int baseOffset = 0;
                IList<int> imageOffset = new List<int>();
                //
                reader.ReadBytes(3);// Magic Number
                imageCount = reader.ReadByte();
                reader.ReadBytes(8);// 固定信息
                baseOffset = reader.ReadInt32();// 第一张的地址
                reader.ReadInt32();// 数据长度
                Signature = new string(reader.ReadChars(16));// 签名
                // 读取偏移
                for (var i = 0; i < imageCount; i++)
                {
                    reader.ReadBytes(4);// 分割
                    imageOffset.Add(reader.ReadInt32() + baseOffset);
                    reader.ReadBytes(4);// 尺寸
                }

                // 读取图片数据
                foreach (var offset in imageOffset)
                {
                    stream.Position = startPosition + offset;
                    //
                    // 判断数据类型
                    string dataType = new string(reader.ReadChars(2));
                    if (dataType == "VX")
                    {
                        // cc cc cc cc
                        reader.ReadBytes(4);
                        int width = reader.ReadInt32();
                        int height = reader.ReadInt32();
                        // 补足数据 cc cc cc cc cc cc ff ff ff ff
                        reader.ReadBytes(10);
                        //image = new Bitmap(width, height);
                        image = ImageCreator.CreateBitmapFromDataUInt16(reader, width, height, ColorFormat.ColorFromRGB565);
                    }
                    else if (dataType == "BM")
                    {
                        // 回到 BM 前面
                        reader.BaseStream.Position -= 2;
                        {
                            // 这里必须要保证 bitmap 的 stream 处于打开状态
                            // 所以不能使用using
                            Stream imageStream = File.Create(Path.GetTempFileName(), 4096, FileOptions.DeleteOnClose);
                            // 确保数据流从 0 开始
                            stream.CopyTo(imageStream);

                            image = new Bitmap(imageStream);

                        }
                    }
                    else
                        throw new NotSupportedException("暂不支持类型为 '"+dataType+"' 的数据解析");

                    ImageList.Add(image);
                }

            }
        }
    }
}

