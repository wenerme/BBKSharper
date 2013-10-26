using System.Drawing;
using System.Windows.Forms;
using ThemeSim.ThemeSettings;

namespace ThemeSim.ThemeElements
{
	/// <summary>
	///     Text控件  用于在屏幕上显示文字
	/// </summary>
	public class TextControl : Label, IThemeControl
	{
		public TextControl()
		{
			BackColor = Color.Transparent;
			AutoSize = true;
		}

		public ThemeRefer Refer { get; set; }
		public ControlElementSetting Presetting { get; set; }

		public void LoadSetting(IThemeSim sim, ThemeElementSetting elementSetting)
		{
			ThemeControl.LoadSetting(this, sim, elementSetting);
			var setting = ThemeElement.ConvertSetting<TextSetting>(elementSetting);
		}


		public ThemeElementSetting GetSetting()
		{
			var setting = ThemeControl.GetSetting<TextSetting>(this);

			return setting;
		}

		public void LoadSettingByRefer(ThemeRefer refer)
		{
		}
	}
}