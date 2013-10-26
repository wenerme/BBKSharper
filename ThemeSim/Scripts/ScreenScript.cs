using ThemeSim.ThemeElements;

namespace ThemeSim.Scripts
{
	public abstract class ScreenScript : ControlScript<ScreenControl>
	{

		/// <summary>
		/// 绑定屏幕
		/// </summary>
		/// <param name="screen"></param>
		public override void BindControl(ScreenControl screen)
		{
			base.BindControl(screen);

			control.StateChanged += OnStateChanged;
		}
		/// <summary>
		/// 解除屏幕绑定
		/// </summary>
		public override void UnBindControl()
		{
			base.UnBindControl();
			if (control == null)
				return;

			control.StateChanged -= OnStateChanged;
		}
		/// <summary>
		/// 对事件绑定的控制,不能被重写
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="state"></param>
		void OnStateChanged(object sender, bool state)
		{
			if (state)
			{
				BindExtraEvent();
				OnDisplay();
			}
			else
			{
				UnBindExtraEvent();
				OnHide();
			}
		}

		protected virtual void OnDisplay()
		{ }

		protected virtual void OnHide()
		{ }
	}
}
