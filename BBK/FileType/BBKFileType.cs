using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace BBK.FileType
{
	/// <summary>
	/// BBK文件格式支持
	/// 
	/// 这里提供了很多便捷的操作函数
	/// </summary>
	public class BBKFileType
	{
		static Dictionary<string, ImageFormat> ExtensionToImageFormat = new Dictionary<string, ImageFormat>();


		/// <summary>
		/// 打开图片文件
		/// 
		/// 包括 jpg/jpeg/gif/png
		/// BBK文件: dlx/lib/rlb
		/// </summary>
		/// <param name="filepath"></param>
		/// <param name="openNormal">是否打开普通文件</param>
		public static List<Image> OpenImageContainerFile(string filepath, bool openNormal = false)
		{
			List<Image> imageList = new List<Image>();
			using(Stream stream = File.OpenRead(filepath))
			{
				// 打开 lib/dlx/rlb
				IBitmapContainerFile containerFile = null;
				if(LibFile.isSupport(filepath) && LibFile.VerifyFile(stream))
				{
					containerFile = new LibFile(stream);
				} else if(DlxFile.isSupport(filepath) && DlxFile.VerifyFile(stream))
				{
					containerFile = new DlxFile(stream);
				} else if(RlbFile.isSupport(filepath) && RlbFile.VerifyFile(stream))
				{
					containerFile = new RlbFile(stream);
				} else if(openNormal)
				{
					var image = OpenImageFile(filepath);
					if(image != null)
						imageList.Add(image);
				}
				// 如果打开成功,则合并文件列表
				if(containerFile != null)
				{
					imageList = imageList.Concat(containerFile.ImageList).ToList();
				}
			}// using

			return imageList;
		}

		/// <summary>
		/// 打开图片文件,如果打开失败则返回null
		/// </summary>
		/// <param name="path"></param>
		/// <returns>打开失败则返回null</returns>
		public static Image OpenImageFile(string path)
		{
			Image image = null;

			try
			{
				image = Image.FromFile(path);
			} catch
			{
				image = null;
			}

			return image;
		}
	}
}
