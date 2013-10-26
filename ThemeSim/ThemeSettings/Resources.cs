using System.Collections.Generic;
using System.Xml.Serialization;
using System.Drawing;
namespace ThemeSim.ThemeSettings
{

    /// <summary>
    /// 文件配置
    /// </summary>
    abstract public class ResourcesSetting : ThemeElementSetting
    {
        /// <summary>
        /// 文件相对路径
        /// </summary>
        [XmlAttribute]
        public string Path;
    }
    /// <summary>
    /// 可索引的文件
    /// </summary>
    abstract public class IndexableResourcesSetting : ResourcesSetting
    {}
    /// <summary>
    /// 图像容器文件配置
    /// </summary>
    [XmlType("ImageContainerResource")]
    public class ImageContainerResourcesSetting : IndexableResourcesSetting
    {}

    /// <summary>
    /// 图像文件夹配置
    /// </summary>
    [XmlType("ImageDirectoryResource")]
    public class ImageDirectoryResourcesSetting : IndexableResourcesSetting
	{
		/// <summary>
		/// 默认扩展名
		/// </summary>
		[XmlAttribute]
		public string DefaultExtension;

		[XmlIgnore]
		public bool DefaultExtensionSpecified { get { return DefaultExtension.Length > 0; } }
	}
}
