using System;
using System.Windows.Forms;
using ThemeSim.ThemeSettings;

namespace ThemeSim.ThemeElements
{
	public class AreaControl : Control, IThemeControl
	{
		public ThemeRefer Refer { get; set; }
		public ControlElementSetting Presetting { get; set; }

		public void LoadSetting(IThemeSim sim, ThemeElementSetting elementSetting)
		{
			ThemeControl.LoadSetting(this, sim, elementSetting);
			var setting = ThemeElement.ConvertSetting<AreaSetting>(elementSetting);
		}

		public ThemeElementSetting GetSetting()
		{
			var setting = ThemeControl.GetSetting<AreaSetting>(this);

			return setting;
		}

		public void LoadSettingByRefer(ThemeRefer refer)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}