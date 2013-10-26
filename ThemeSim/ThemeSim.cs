using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThemeSim.ThemeSettings;
using ThemeSim.ThemeElements;
using System.IO;
using log4net;
using BBK.Extension;
using BBK.Device;
namespace ThemeSim
{
    /// <summary>
    /// 引用对象
    /// </summary>
    public class ThemeRefer
    {
        /// <summary>
        /// 引用的名字
        /// </summary>
        public string Name;
        /// <summary>
        /// 引用的索引
        /// </summary>
        public string Index;

		

        public ThemeRefer(string refer)
        {
            if (refer == null)
            {
                Name = string.Empty;
                Index = string.Empty;
				return;
            }
			
            string[] rel = refer.Split(':');

            Name = rel.Length > 0? rel[0]: "";
            Index = rel.Length > 1? rel[1]: "";
        }
        public ThemeRefer(string name, string index)
        {
            Name = name;
            Index = index;
        }
		/// <summary>
		/// 检测该引用是否有效
		/// </summary>
		/// <returns></returns>
        public bool IsValid()
        {
            return Name.Length > 0;
        }
		/// <summary>
		/// 转换引用为字符串
		/// </summary>
		/// <returns></returns>
        public override string ToString()
        {
            string str = string.Empty;
            if (IsValid())
            {
                str += Name;
            }
            return str;
        }

    }

    public class ThemeSimulator : IThemeSim
    {
		/// <summary>
		/// 当内容加载完时的事件
		/// </summary>
		public event EventHandler LoadComplete;

		/// <summary>
		/// 主题配置
		/// </summary>
		public ThemeSimSetting Setting { get; private set; }

		/// <summary>
		/// 以Name为索引的元素列表
		/// </summary>
		Dictionary<string, IThemeElement> ElementList;
		Dictionary<string, ThemeRefer> NameMappingList;
		/// <summary>
		/// 主题文件的基目录
		/// </summary>
		public string BaseDirectory;

		/// <summary>
		/// 虚拟的设备信息
		/// </summary>
		public DeviceInfo DeviceInfo;

		/// <summary>
		/// 显示的屏幕
		/// </summary>
		public ScreenPanel Screen { get; set; }

		internal static readonly ILog logger = LogManager.GetLogger(typeof(ThemeSimulator));

		#region 静态初始
		static ThemeSimulator()
		{
			// 注册元素类型
			ThemeElement.RegisterElementType(typeof(TextControl), typeof(TextSetting));
			ThemeElement.RegisterElementType(typeof(ImageControl), typeof(ImageSetting));
			ThemeElement.RegisterElementType(typeof(ScreenControl), typeof(ScreenSetting));
			ThemeElement.RegisterElementType(typeof(ImageContainerResources), typeof(ImageContainerResourcesSetting));
			ThemeElement.RegisterElementType(typeof(ImageDirectoryResources), typeof(ImageDirectoryResourcesSetting));
			ThemeElement.RegisterElementType(typeof(AreaControl), typeof(AreaSetting));
		}
		#endregion

        public ThemeSimulator(ThemeSimSetting setting)
        {
			ElementList = new Dictionary<string, IThemeElement>();
			NameMappingList = new Dictionary<string, ThemeRefer>();
			BaseDirectory = Directory.GetCurrentDirectory();
            Setting = setting;
			//
			DeviceInfo = DeviceInfo.LM_9688;
			//
			Screen = new ScreenPanel();
			Screen.Width = DeviceInfo.ScreenWidth;
			Screen.Height = DeviceInfo.ScreenHeight;
		}
		/// <summary>
		/// 加载配置
		/// </summary>
		public void Load()
		{
			if(false == Directory.Exists(BaseDirectory))
				throw new DirectoryNotFoundException("BaseDirectory '{0}' not found.".FormatMe(BaseDirectory));
			Directory.SetCurrentDirectory(BaseDirectory);
			if(false == Verify())
				throw new Exception("ThemeSimulator Verify failed.");
			// 加载是顺序有关的
			// 加载名字映射
			foreach(var item in Setting.NameMappingList)
			{
				NameMappingList.Add(item.Name, new ThemeRefer(item.Refer));
			}
			// 加载资源
			foreach(var setting in Setting.ResourceList)
			{
				var element = ThemeElement.CreateElement(this,setting);
				ElementList.Add(element.Name, element);
				logger.Info("Load Resource '{0}' -> {1}".FormatMe(setting.Name,setting.Path));
			}
			// 加载控件
			foreach(var setting in Setting.ControlList)
			{
				var element = ThemeElement.CreateElement(this, setting);
				ElementList.Add(element.Name, element);
				logger.Info("Load Control '{0}' -> {1}".FormatMe(setting.Name, setting.GetType()));
			}
			// 加载屏幕
			foreach(var setting in Setting.ScreenList)
			{
				ScreenControl element = ThemeElement.CreateElement(this, setting) as ScreenControl;
				element.Width = DeviceInfo.ScreenWidth;
				element.Height = DeviceInfo.ScreenHeight;
				ElementList.Add(element.Name, element);
				logger.Info("Load Screen '{0}' -> {1}".FormatMe(setting.Name, setting.GetType()));
			}

			//
		}
        /// <summary>
        /// 验证配置的有效性
        /// </summary>
        /// <returns></returns>
        public bool Verify()
        {
			bool result = false;
			if(Setting == null)
				goto VerifyFailed;
			// 检测设备兼容
			if(false == Setting.Info.CompatibleDeviceName.Contains(DeviceInfo.Name))
			{
				logger.Error("Device not compatible. target:'{0}'".FormatMe(DeviceInfo.Name));
				goto VerifyFailed;
			}
			// 验证资源配置, 必须要有名字 路径必须正确
			foreach(var item in Setting.ResourceList)
			{
				if(item.Name == null || item.Name.Length == 0 )
				{
					logger.Error("资源配置无 Name.");
					goto VerifyFailed;
				}

				if(item.Path == null || item.Path.Length == 0)
				{
					logger.Error("'{0}':资源配置无 Path.".FormatMe(item.Name));
					goto VerifyFailed;
				}

				//var path = Path.Combine(BaseDirectory, item.Path);
				var path = item.Path;
				if(false == (File.Exists(path) || Directory.Exists(path)))
				{
					logger.Error("'{0}':资源配置路径不存在.".FormatMe(path));
					goto VerifyFailed;
				}
			}
			// 控件必须要有 Name
			foreach(var item in Setting.ControlList)
			{
				if(item.Name == null || item.Name.Length == 0)
				{
					logger.Error("配置无 Name.");
					goto VerifyFailed;
				}
			}
			// 名字映射必须要有 Name 和 Refer
			foreach(var item in Setting.NameMappingList)
			{
				if(item.Name == null || item.Name.Length == 0)
				{
					logger.Error("名字映射配置无 Name.");
					goto VerifyFailed;
				}
				if(item.Refer == null || item.Refer.Length == 0)
				{
					logger.Error("名字映射配置无 Refer.");
					goto VerifyFailed;
				}
			}
			//
			result = true;
			VerifyFailed:
            return result;
        }
		ThemeRefer DeRefer(ThemeRefer refer)
		{
			if(refer.Index.Length > 0)
			{
				return refer;
			} else if(NameMappingList.ContainsKey(refer.Name))
			{
				return NameMappingList[refer.Name];
			} else
				return refer;
				
		}
        public object GetObjectByRefer(ThemeRefer refer)
        {
			refer = DeRefer(refer);

			if(!refer.IsValid())
				return null;

			object item = this.ElementList[refer.Name];

			if(item != null && refer.Index.Length > 0 && item is IIndexableResources)
			{
				item = (item as IIndexableResources).GetByIndex(refer.Index);
			}
			return item;
        }
		public object GetObjectByRefer(string refer)
		{
			return GetObjectByRefer(new ThemeRefer(refer));
		}
    }

	
}
