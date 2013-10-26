using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

using BBK.Device;
using BBK.FileType;
using System.Drawing;


namespace ThemeSim.ThemeSettings
{
    /// <summary>
    /// 虚拟主题的配置
    /// 可以根据该配置生成虚拟
    /// </summary>
    [Serializable, XmlRootAttribute("ThemeSetting")]
    [XmlInclude(typeof(ImageSetting)),
    XmlInclude(typeof(TextSetting)),
	XmlInclude(typeof(AreaSetting)),
    XmlInclude(typeof(ImageContainerResourcesSetting)),
	XmlInclude(typeof(ImageDirectoryResourcesSetting))
	]
    public class ThemeSimSetting
    {
        /// <summary>
        /// 主题的相关信息
        /// </summary>
        public ThemeInfo Info;
        /// <summary>
        /// 主题包含的屏幕列表
        /// </summary>
        [XmlArrayItem("Screen")]
        public List<ScreenSetting> ScreenList;
        /// <summary>
        /// 资源列表
        /// </summary>
        [XmlArrayItem("Resource")]
        public List<ResourcesSetting> ResourceList;
        /// <summary>
        /// 控件列表
        /// </summary>
        [XmlArrayItem("Control")]
        public List<ControlElementSetting> ControlList;
		/// <summary>
		/// 名字映射列表 Optional
		/// </summary>
		[XmlArrayItem("Mapping")]
		public List<NameMappingSetting> NameMappingList;

		public bool NameMappingListSpecified { get { return NameMappingList.Count > 0; } }

        public ThemeSimSetting()
        {
            Info = new ThemeInfo();
            ScreenList = new List<ScreenSetting>();
            ResourceList = new List<ResourcesSetting>();
            ControlList = new List<ControlElementSetting>();
			NameMappingList = new List<NameMappingSetting>();
        }
        public ThemeSimSetting(string createBy, string themeName, params string[] deviceName)
            : this()
        {
            Info.ThemeName = themeName;
            Info.CreateBy = createBy;
            Info.CompatibleDeviceName.AddRange(deviceName);
        }
        /// <summary>
        /// 从XML文件加载
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static ThemeSimSetting LoadFromXML(string filename)
        {
            ThemeSimSetting setting;
            using (Stream stream = File.OpenRead(filename))
            {
                setting = LoadFromXML(stream);
            }
            return setting;
        }
        public static ThemeSimSetting LoadFromXML(Stream stream)
        {
			try
			{
				ThemeSimSetting setting;
				var serializer = new XmlSerializer(typeof(ThemeSimSetting));
				setting = (ThemeSimSetting)serializer.Deserialize(stream);
				return setting;
			} catch(FileNotFoundException ex)
			{ }
			return null;
        }
        /// <summary>
        /// 序列化,保存为XML文件
        /// </summary>
        /// <param name="filename"></param>
        public void SaveAsXML(string filename)
        {
            using (Stream stream = File.Create(filename))
            {
                SaveAsXML(stream);
            }
        }
        public void SaveAsXML(Stream stream)
        {
			try
			{
				var serializer = new XmlSerializer(typeof(ThemeSimSetting));
				serializer.Serialize(stream, this);
			} catch(FileNotFoundException ex)
			{ }
            
        }


    }
    /// <summary>
    /// 主题信息
    /// </summary>
    public class ThemeInfo
    {
        /// <summary>
        /// 该主题配置的创建者
        /// </summary>
        [XmlAttribute]
        public string CreateBy;
        /// <summary>
        /// 该主题的名字
        /// </summary>
        [XmlAttribute]
        public string ThemeName;
        /// <summary>
        /// 主题的描述
        /// </summary>
        public string Description;
        /// <summary>
        /// 兼容的设备名字
        /// </summary>
        [XmlArrayItemAttribute("Name")]
        public List<string> CompatibleDeviceName;

        public ThemeInfo()
        {
            //Properties.Resources
            CreateBy = Properties.Resources.Author;
            ThemeName = Properties.Resources.DefaultThemeName;

            CompatibleDeviceName = new List<string>();
        }
    }
    /// <summary>
    /// 主题元素配置
    /// </summary>
    abstract public class ThemeElementSetting
    {
        /// <summary>
        /// 元素的名字
        /// </summary>
        [XmlAttribute]
        public string Name;
    }
	/// <summary>
	/// 名字映射
	/// </summary>
	public class NameMappingSetting : ThemeElementSetting
	{
		[XmlAttribute]
		public string Refer;
	}
}
