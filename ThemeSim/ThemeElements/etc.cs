using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThemeSim.ThemeSettings;

namespace ThemeSim.ThemeElements
{
	/// <summary>
	/// 名字映射类型
	/// 只用来映射
	/// </summary>
	public class NameMapping : IThemeElement, IReferableElement
	{
		public string Name { get; set; }
		public ThemeRefer Refer{ get; set; }

		static NameMapping()
		{

		}

		public void LoadSetting(IThemeSim sim, ThemeElementSetting setting)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public ThemeElementSetting GetSetting()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void LoadSettingByRefer(ThemeRefer refer)
		{ }
	}
}
