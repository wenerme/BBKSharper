using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using ThemeSim.ThemeSettings;

using BBK.Extension;
using BBK.FileType;
using System.Text.RegularExpressions;

namespace ThemeSim.ThemeElements
{
	public abstract class ResourcesObject : IResourcesObject
	{
		public string Name { get; set; }
		public string ResPath { get; internal set; }

		public void LoadSetting(IThemeSim sim, ThemeElementSetting elementSetting)
		{
			ResourcesSetting setting = ThemeElement.ConvertSetting<ResourcesSetting>(elementSetting);
			ResPath = setting.Path;
			Name = setting.Name;
		}

		public ThemeElementSetting GetSetting()
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}

	/// <summary>
	/// 图像容器资源
	/// 一般为 dlx/rlb/lib等
	/// </summary>
	public class ImageContainerResources : ResourcesObject, IIndexableResources
	{
		protected List<Image> imageList;
		public ImageContainerResources()
		{}
		public ImageContainerResources(string path)
		{
			if(!File.Exists(path))
				throw new FileNotFoundException("{0} File not found.".FormatMe(path));

			ResPath = path;
			imageList = BBKFileType.OpenImageContainerFile(ResPath);

			if(imageList.Count == 0)
				throw new Exception("File {0} not contain any image.".FormatMe(ResPath));
		}

		public object GetByIndex(string index)
		{
			if(Regex.IsMatch(index, "^[0-9]+$"))
			{
				int n = int.Parse(index);
				if(n >= imageList.Count)
					goto Error;
				return imageList[n];
			}
			
			Error:
			throw new IndexOutOfRangeException();
		}
		/// <summary>
		/// 获取图像列表
		/// </summary>
		/// <returns></returns>
		public List<Image> GetImageList()
		{
			return imageList;
		}

		public void LoadSetting(IThemeSim sim, ThemeElementSetting elementSetting)
		{
			base.LoadSetting(sim, elementSetting);
			if(!File.Exists(ResPath))
				throw new DirectoryNotFoundException("{0} File not found.".FormatMe(ResPath));
		}

		public ThemeElementSetting GetSetting()
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
	/// <summary>
	/// 目录
	/// 可以直接使用目录,索引则为目录内文件名
	/// </summary>
	public class ImageDirectoryResources : ResourcesObject, IIndexableResources
	{

		public string DefaultExtension;

		public ImageDirectoryResources()
		{
			DefaultExtension = "";
		}
		public object GetByIndex(string index)
		{
			string path = index;

			if(false == Path.HasExtension(path))
				path = Path.ChangeExtension(path, DefaultExtension);

			path = Path.Combine(ResPath, path);
			if(!File.Exists(path))
				throw new FileNotFoundException("File '{0}' not found in {1}.".FormatMe(path, ResPath));
			Bitmap image = new Bitmap(Image.FromFile(path));
			//
			image.MakeTransparent(Color.FromArgb(0xF800F8));
			//
			image.MakeTransparent(Color.FromArgb(0xFF00FF));

			return image;
		}

		public void LoadSetting(IThemeSim sim, ThemeElementSetting elementSetting)
		{
			ImageDirectoryResourcesSetting setting = ThemeElement.ConvertSetting<ImageDirectoryResourcesSetting>(elementSetting);
			base.LoadSetting(sim, setting);
			if(!Directory.Exists(ResPath))
				throw new DirectoryNotFoundException("{0} Directory not found.".FormatMe(ResPath));

			DefaultExtension = setting.DefaultExtension;
		}

		public ThemeElementSetting GetSetting()
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
