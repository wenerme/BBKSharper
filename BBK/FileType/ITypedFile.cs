using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.IO;

namespace BBK.FileType
{
    /// <summary>
    /// 类型文件接口
    /// </summary>
    public interface ITypedFile
    {
        /// <summary>
        /// 存储到文件
        /// </summary>
        /// <param name="filename"></param>
        void SaveAs(string filepath);
        /// <summary>
        /// 存储到流
        /// </summary>
        /// <param name="stream"></param>
        void SaveAs(Stream stream);
    }

    /// <summary>
    /// 图片类型文件
    /// </summary>
    public interface IImageTypeFile : ITypedFile
    {
        /// <summary>
        /// 获取 <see cref="Image"/> 对象
        /// </summary>
        /// <returns></returns>
        Image GetImage();
    }

    public interface IImageContainerFile: ITypedFile
    {
        /// <summary>
        /// 图片容器中包含的图片列表
        /// </summary>
        IList<Image> ImageList { get; }

    }

    public interface IBitmapContainerFile : ITypedFile
    {
        /// <summary>
        /// 图片容器中包含的图片列表
        /// </summary>
        IList<Bitmap> ImageList { get; }

        /// <summary>
        /// 从文件添加
        /// </summary>
        /// <param name="filepath">文件地址</param>
        void AppendFrom(string filepath);

        /// <summary>
        /// 从流添加
        /// </summary>
        /// <param name="stream"></param>
        void AppendFrom(Stream stream);
    }
}
