using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ThemeSim
{
	/// <summary>
	/// U5 = Utils
	/// </summary>
	public static class U5
	{
		public static IDisposable SetInterval(Action method, int delayInMilliseconds)
		{
			System.Timers.Timer timer = new System.Timers.Timer(delayInMilliseconds);
			timer.Elapsed += (source, e) =>
			{
				method();
			};

			timer.Enabled = true;
			timer.Start();
			//U5.SetInterval(()=>{},100).Dispose()
			// Returns a stop handle which can be used for stopping
			// the timer, if required
			return timer as IDisposable;
		}

		public static IDisposable SetTimeout(Action method, int delayInMilliseconds)
		{
			System.Timers.Timer timer = new System.Timers.Timer(delayInMilliseconds);
			timer.Elapsed += (source, e) =>
			{
				method();
			};

			timer.AutoReset = false;
			timer.Enabled = true;
			timer.Start();

			// Returns a stop handle which can be used for stopping
			// the timer, if required
			return timer as IDisposable;
		}

		/*
		public static IDisposable SetInterval(Action method, TimeSpan delay)
		{
			return Observable.Timer(dueTime: delay).Subscribe(_ => method());
		}

		public static IDisposable SetTimeOut(Action method, TimeSpan delay)
		{
			return Observable.Timer(dueTime: delay, period: delay).Subscribe(_ => method());
		} 
		 */

		/// <summary>
		/// 从字符串解析颜色
		/// </summary>
		/// <param name="val"></param>
		public static Color ColorFromString(string val)
		{
			Color color = Color.FromName(val);
			if(false == (color.A == 0 && color.R == 0 && color.G == 0 && color.B == 0))
				goto Parsed;

			color = ColorTranslator.FromHtml(val);

			Parsed:
			return color;
		}

		public static Font FontFromString(string val)
		{
			Font font = (new FontConverter()).ConvertFromString(val) as Font;

			return font;
		}
		
	}
}
