using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
namespace ThemeSim
{
	class Logger
	{
		public static readonly ILog logger = LogManager.GetLogger(typeof(ThemeSimulator));
		static Logger()
		{
			XmlConfigurator.Configure();
		}
	}
}
