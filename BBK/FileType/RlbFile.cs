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
    public sealed class RlbFile : IBitmapContainerFile
    {
        /// <summary>
        /// 该类型的扩展名
        /// </summary>
        public const string Extension = ".rlb";

        public IList<Bitmap> ImageList { get; private set; }
        Dictionary<Bitmap, string> ImageName = new Dictionary<Bitmap, string>();

        public RlbFile()
        {
            // 初始化
            ImageList = new List<Bitmap>();
        }
        public RlbFile(string filepath)
            :this()
        {
            
            AppendFrom(filepath);
        }
        public RlbFile(Stream stream)
            : this()
        {
            AppendFrom(stream);
        }

        public void SetName(int i, string name)
        {
            if (i >= ImageList.Count)
                throw new IndexOutOfRangeException();

            SetName(ImageList[i], name);
        }
        public void SetName(Bitmap image, string name)
        {
            if (!ImageList.Contains(image))
                throw new ArgumentOutOfRangeException("所指定的图片不在图片列表内");

            if (ImageName.ContainsKey(image))
                ImageName[image] = name;
            else
                ImageName.Add(image, name);
        }
        public string GetName(int i)
        {
            if (i >= ImageList.Count)
                throw new IndexOutOfRangeException();
            return GetName(ImageList[i]);
        }
        public string GetName(Bitmap image)
        {
            if (!ImageList.Contains(image))
                throw new ArgumentOutOfRangeException("所指定的图片不在图片列表内");
            // 如果文件名不存在 则返回在 ImageList中 的索引号
            if (ImageName.ContainsKey(image))
                return ImageName[image];
            else
                return ""+ImageList.IndexOf(image);
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

        
#endregion

        public void AppendFrom(string filepath)
        {

            using (Stream stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                AppendFrom(stream);
            }

        }

        public void AppendFrom(Stream stream)
        {
            var lastPosition = stream.Position;
            if (!VerifyFile(stream))
            {
                throw new BadFileFormatException();
            }

            BinaryReader reader = new BinaryReader(stream);
            // 解析过程
            {
                Bitmap image;
                int imageCount = 0;
                IList<int> imageOffset = new List<int>();
                IList<string> imageName = new List<string>();
                //
                imageCount = reader.ReadInt32();
                // 读取偏移
                for (var i = 0; i < imageCount; i++)
                {
                    imageOffset.Add(reader.ReadInt32());
                }
                // 读取文件名 与lib不同的地方
                for (var i = 0; i < imageCount; i++)
                {
                    var fn = new string(reader.ReadChars(32));
                    // 去除结尾的 \0
                    fn = fn.Trim('\0');
                    imageName.Add(fn);
                }
                // 读取图片数据
                foreach (var offset in imageOffset)
                {
                    stream.Position = lastPosition + offset;
                    //
                    reader.ReadUInt32();// 数据长度

                    {
                        // 这里必须要保证 bitmap 的 stream 处于打开状态
                        // 所以不能使用using
                        Stream imageStream = File.Create(Path.GetTempFileName(), 4096, FileOptions.DeleteOnClose);
                        // 确保数据流从 0 开始
                        stream.CopyTo(imageStream);

                        image = new Bitmap(imageStream);

                    }

                    ImageList.Add(image);
                }
                // 绑定图片和图片名
                for (var i = 0; i < imageCount; i++)
                {
                    SetName(ImageList[i], imageName[i]);
                }
            }
        }

        public void SaveAs(string filename)
        {
            using (Stream stream = File.Open(filename, FileMode.Create, FileAccess.Write))
            {
                SaveAs(stream);
            }
        }
        public void SaveAs(Stream stream)
        {
            IList<int> imageOffset = new List<int>();
            IList<FileStream> fileList = new List<FileStream>();
            BinaryWriter writer = new BinaryWriter(stream);
            // 计算文件偏移
            {
                FileStream file;
                // 数量 + 偏移 + 名字
                int offset = 4 + ImageList.Count * (4 + 32);// 数据开始偏移
                foreach (var image in ImageList)
                {
                    imageOffset.Add(offset);
                    file = File.Create(Path.GetTempFileName(), 4096, FileOptions.DeleteOnClose);
                    image.Save(file,ImageFormat.Bmp);
                    fileList.Add(file);

                    offset += 4;// 文件长度
                    offset += (int)file.Length;
                }
            }
            //
            writer.Write(ImageList.Count);// 写入文件数量
            // 写入图片偏移
            foreach (int offset in imageOffset)
            {
                writer.Write(offset);
            }
            // 写入文件名
            foreach (var image in ImageList)
            {
                var startOffset = writer.BaseStream.Position;
                // 如果不转换为 charArray 会导致多写入一个字符~不知道为什么
                writer.Write(GetName(image).ToCharArray());
                // 确定写入的长度
                writer.BaseStream.Position = startOffset + 32;
            }
            // 写入每一个图片
            foreach (var file in fileList)
            {
                // 写入文件长
                writer.Write((int)file.Length);
                //
                file.Position = 0;
                file.CopyTo(stream);
            }
        }
    }
}
