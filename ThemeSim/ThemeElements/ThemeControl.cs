using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml.Serialization;
using System.IO;

using BBK.Device;
using BBK.FileType;
using BBK.Extension;
using ThemeSim.ThemeSettings;

namespace ThemeSim.ThemeElements
{
    public delegate IThemeControl ThemeControlCreator(ControlElementSetting setting);

    /// <summary>
    /// 主题显示需要的基础控件
    /// 从这里创建控件和注册控件类型
    /// </summary>
    public static class ThemeControl
    {

        public static void LoadSetting(IThemeControl control, IThemeSim sim, ThemeElementSetting elementSetting)
        {
			ControlElementSetting setting = ThemeElement.ConvertSetting<ControlElementSetting>(elementSetting);
            (control as Control).Name = setting.Name;
			
            control.Refer = new ThemeRefer(setting.Refer);
			control.Presetting = setting;

			control.Top = 0;
			control.Left = 0;
			//
			setting.ApplyToControl(control as Control);
        }

        public static TSetting GetSetting<TSetting>(IThemeControl control)
            where TSetting : ControlElementSetting, new()
        {
            TSetting setting = new TSetting();
            setting.Name = (control as Control).Name;
            setting.Refer = control.Refer.ToString();

            return setting;
        }

		public static void LoadSettingByRefer()
		{}
    }
    
}
