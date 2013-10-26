using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThemeSim.ThemeSettings;
using BBK.Extension;

using log4net;

namespace ThemeSim.ThemeElements
{
	public delegate IThemeElement ThemeElementCreator(IThemeSim sim, ThemeElementSetting setting);
	/// <summary>
	/// 元素创建
	/// 
	/// 根据配置类型来转换为对应的元素类型
	/// </summary>
	public static class ThemeElement
	{
		/// <summary>
		/// 可转换类型
		/// </summary>
		public static Dictionary<Type,Type> TypeList;

		static ThemeElement()
        {
			TypeList = new Dictionary<Type,Type>();
        }
		/// <summary>
		/// 创建控件
		/// </summary>
		/// <param name="setting"></param>
		/// <returns></returns>
		public static IThemeElement CreateElement(IThemeSim sim, ThemeElementSetting setting)
		{
			if(false == CanCreateElementType(setting))
				throw new NotSupportedException("This setting type '{0}' not support yet".FormatMe(setting.GetType()));

			IThemeElement element;
			Type elementType = TypeList[setting.GetType()];
			element = (IThemeElement)Activator.CreateInstance(elementType);
			element.LoadSetting(sim, setting);

			return element;
		}
		/// <summary>
		/// 检测是否可以创建控件
		/// </summary>
		/// <param name="setting"></param>
		/// <returns></returns>
		public static bool CanCreateElementType(ThemeElementSetting setting)
		{
			return CanCreateElementType(setting.GetType());
		}
		public static bool CanCreateElementType(Type type)
		{
			return TypeList.ContainsKey(type);
		}
		/// <summary>
		/// 注册控件类型
		/// 
		/// 控件必须要实现 <see cref="IThemeElement"/> 接口
		/// </summary>
		/// <param name="elementType"></param>
		/// <param name="replace">如果存在则替换</param>
		public static void RegisterElementType(Type elementType, Type settingType, bool replace = false)
		{
			LogManager.GetLogger(typeof(ThemeElement)).Info("Regist {0} -> {1}.".FormatMe(settingType, elementType));

			// settingType: ThemeElementSetting
			if(false == settingType.IsSubclassOf(typeof(ThemeElementSetting)))
				throw new ArgumentException("The register settingType must inherit from ThemeElementSetting");

			// elementType: IThemeElement
			if(false == typeof(IThemeElement).IsAssignableFrom(elementType))
				throw new ArgumentException("The register elementType must implement IThemeElement interface");

			// elementType: new()
			if(null == elementType.GetConstructor(Type.EmptyTypes))
				throw new ArgumentException("The register elementType must have a parameterless constructor");

			LogManager.GetLogger(typeof(ThemeElement)).Info("Successful Regist {0} -> {1}.".FormatMe(settingType, elementType));
			TypeList.Add(settingType, elementType);
		}
		/// <summary>
		/// 安全转换Setting类型
		/// </summary>
		/// <typeparam name="TSetting"></typeparam>
		/// <param name="elementSetting"></param>
		/// <returns></returns>
		public static TSetting ConvertSetting<TSetting>(ThemeElementSetting elementSetting)
			where TSetting : ThemeElementSetting
		{
			if(false == (elementSetting is TSetting))
				throw new NotSupportedException("Setting type convert error.");

			TSetting setting = elementSetting as TSetting;

			return setting;
		}
	}
}
