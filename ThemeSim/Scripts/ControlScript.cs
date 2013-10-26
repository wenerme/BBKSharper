using System;
using System.Windows.Forms;

namespace ThemeSim.Scripts
{
	/// <summary>
	/// 控件绑定脚本
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ControlScript<T> : RuntimeScript
		where T : Control
	{
		protected T control;
		public virtual void BindControl(T ctr)
		{
			if(control != null)
				UnBindControl();
			control = ctr;

			control.MouseMove += OnMouseMove;
			control.Click += OnClick;
			

		}
		public virtual void UnBindControl()
		{
			if(control == null)
				return;

			control.MouseMove -= OnMouseMove;
			control.Click -= OnClick;
			//
			UnBindExtraEvent();
		}
		protected virtual void BindExtraEvent()
		{
			{
				Control eventCtr = control;
				// 如果控件不能接受焦点,则将事件绑定到主窗口上
				if(false == control.CanFocus)
					eventCtr = MainForm;
				eventCtr.KeyDown += OnKeyDown;
				eventCtr.KeyUp += OnKeyUp;
				eventCtr.KeyPress += OnKeyPress;
			}
		}
		protected virtual void UnBindExtraEvent()
		{
			{
				Control eventCtr = control;
				// 如果控件不能接受焦点,则将事件绑定到主窗口上
				if(false == control.CanFocus)
					eventCtr = MainForm;
				eventCtr.KeyDown -= OnKeyDown;
				eventCtr.KeyUp -= OnKeyUp;
				eventCtr.KeyPress -= OnKeyPress;
			}
		}
		protected virtual void OnKeyUp(object sender, KeyEventArgs e)
		{
		}

		protected virtual void OnKeyDown(object sender, KeyEventArgs e)
		{ }

		protected virtual void OnKeyPress(object sender, KeyPressEventArgs e)
		{ }

		protected virtual void OnClick(object sender, EventArgs e)
		{ }
		protected virtual void OnMouseMove(object sender, MouseEventArgs e)
		{ }
	}
}
