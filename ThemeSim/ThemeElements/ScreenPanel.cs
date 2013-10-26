using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ThemeSim.ThemeElements
{
	public class ScreenPanel : Panel
	{
		ScreenControl currentScreen = null;
		ScreenControl lastScreen = null;
		public void ShowScreen(ScreenControl screen)
		{
			if (screen == null)
				throw new NullReferenceException("显示的界面为 null");

			if(currentScreen != null)
			{
				Controls.Remove(currentScreen);
				currentScreen.OnStateChanged(false);
			}

			screen.Load();
			Controls.Add(screen);

			screen.OnStateChanged(true);

			lastScreen = currentScreen;
			currentScreen = screen;
			
		}

		public void ShowLastScreen()
		{
			ShowScreen(lastScreen);
		}

		public bool HaveLastScreen()
		{
			return lastScreen != null;
		}
	}
}
