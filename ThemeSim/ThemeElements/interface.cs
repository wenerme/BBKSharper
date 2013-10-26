using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThemeSim.ThemeSettings;
using System.Windows.Forms;
using System.ComponentModel;

namespace ThemeSim.ThemeElements
{
    public interface IThemeSim
    {
		/// <summary>
		/// 根据Refer获取元素
		/// </summary>
		/// <param name="refer"></param>
		/// <returns></returns>
        object GetObjectByRefer(ThemeRefer refer);
		object GetObjectByRefer(string refer);

		/// <summary>
		/// 显示的主屏幕
		/// </summary>
		ScreenPanel Screen { get; }
    }
    public interface IThemeElement
    {
        string Name { get; set; }

		/// <summary>
		/// 加载配置
		/// </summary>
		/// <param name="setting"></param>
		/// <returns></returns>
		void LoadSetting(IThemeSim sim, ThemeElementSetting setting);

		/// <summary>
		/// 获取配置
		/// </summary>
		/// <returns></returns>
		ThemeElementSetting GetSetting();
    }
	public interface IReferableElement
	{
		ThemeRefer Refer { get; set; }
		void LoadSettingByRefer(ThemeRefer refer);
	}
	public interface IScreenElement
	{
		int Top { get; set; }
		int Left { get; set; }
		int Width { get; set; }
		int Height { get; set; }
	}
    /// <summary>
    /// 控件接口
    /// 实现此接口即可在主题上显示
	/// 凡是控件,都是可加载和保存配置的
	/// 控件均可以Refer
    /// </summary>
	public interface IThemeControl : IThemeElement, IReferableElement, IScreenElement
    {
		ControlElementSetting Presetting { get; set; }
    }

    public interface IResourcesObject : IThemeElement
    {
        string ResPath { get; }
    }

	/// <summary>
	/// 可索引的资源
	/// </summary>
    public interface IIndexableResources : IResourcesObject
    {
		/// <summary>
		/// 根据索引获取图片
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
        object GetByIndex(string index);
    }


}
