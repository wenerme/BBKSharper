using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThemeSim.ThemeSettings;
using BBK.Extension;
using System.Drawing;
namespace ThemeSim.ThemeElements
{
    /// <summary>
    /// 屏幕类型
    /// 
    /// 表示一个主题所包含的屏幕页面
    /// 页面可以包含 其他所有的control
    /// 页面的大小可以不固定
    /// </summary>
    public class ScreenControl : Panel, IThemeControl
    {
		/// <summary>
		/// 屏幕显示或隐藏,true为显示,false为隐藏
		/// </summary>
		public event Action<object, bool> StateChanged;

        public ThemeRefer Refer { get; set; }
		public ControlElementSetting Presetting { get; set; }

		IThemeSim simulator;

		public ScreenControl()
		{
			
		}

        public void LoadSetting(IThemeSim sim, ThemeElementSetting elementSetting)
        {
            ThemeControl.LoadSetting(this, sim, elementSetting);
			simulator = sim;
        }

		public void Load()
		{
			ScreenSetting setting = ThemeElement.ConvertSetting<ScreenSetting>(Presetting);

			if(setting.BackgroundImageSpecified)
			{
				BackgroundImage = simulator.GetObjectByRefer(setting.BackgroundImage) as Image;
			}

			foreach(var element in setting.Elements)
			{
				Control control = simulator.GetObjectByRefer(element.Refer) as Control;
				if(control == null)
					throw new NullReferenceException("Refer '{0}' is not control.".FormatMe(element.Refer));

				if((element.WidthSpecified || element.HeightSpecified) && control is Panel)
					(control as Panel).AutoSize = false;

				element.ApplyToControl(control);
				

				//control.Parent;
				Controls.Add(control);
				//control.Parent = sim.GetObjectByRefer("ctrMainBG") as Control;
				if(element.ParentSpecified)
					control.Parent = simulator.GetObjectByRefer(element.Parent) as Control;
				if(element.HrefSpecified)
				{
					control.Tag = element;
					control.Click += DoElementClick;
				}
					
			}
		}
		void DoElementClick(object sender, EventArgs e)
		{
			string href = ((sender as Control).Tag as ScreenElementSetting).Href;
			simulator.Screen.ShowScreen(simulator.GetObjectByRefer(href) as ScreenControl);
		}
        public ThemeElementSetting GetSetting()
        {
            ScreenSetting setting = ThemeControl.GetSetting<ScreenSetting>(this);

            return setting;
        }

		public void LoadSettingByRefer(ThemeRefer refer)
		{}

		internal void OnStateChanged(bool state)
		{
			if(StateChanged != null)
				StateChanged(this, state);
			if(state == false)
			{
				foreach(Control item in Controls)
				{
					item.Click -= DoElementClick;
				}
			}
		}
    }
}
