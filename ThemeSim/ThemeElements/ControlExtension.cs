using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ThemeSim.ThemeElements
{
	public interface IInvokeable
	{
		bool InvokeRequired { get; }
		Object Invoke(Delegate method);
		Object Invoke(Delegate method, params Object[] args);
	}

	public static class ControlExtension
	{
		#region 对控件的跨线程安全操作
		public static void SetTop(this Control control, int val)
		{
			if(control.InvokeRequired)
			{
				Action<Control, int> act = SetTop;
				control.Invoke(act, new object[] { control, val });
			}else
				control.Top = val;
		}
		public static void SetLeft(this Control control, int val)
		{
			if(control.InvokeRequired)
			{
				Action<Control, int> act = SetTop;
				control.Invoke(act, new object[] { control, val });
			} else
				control.Left = val;
		}
		public static void SetHeight(this Control control, int val)
		{
			if(control.InvokeRequired)
			{
				Action<Control, int> act = SetHeight;
				control.Invoke(act, new object[] { control, val });
			} else
				control.Height = val;
		}
		public static void SetWidth(this Control control, int val)
		{
			if(control.InvokeRequired)
			{
				Action<Control, int> act = SetWidth;
				control.Invoke(act, new object[] { control, val });
			} else
				control.Width = val;

		}
		public static void SetText(this Control control, string val)
		{
			if(control.InvokeRequired)
			{
				Action<Control, string> act = SetText;
				control.Invoke(act, new object[] { control, val });
			} else
				control.Text = val;
		}
		public static void SetTag(this Control control, object val)
		{
			if(control.InvokeRequired)
			{
				Action<Control, object> act = SetTag;
				control.Invoke(act, new object[] { control, val });
			} else
				control.Tag = val;
		}
		#endregion

	}
}
