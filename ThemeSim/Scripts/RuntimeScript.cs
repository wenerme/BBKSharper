using System.Windows.Forms;
using log4net;

namespace ThemeSim.Scripts
{
	public class RuntimeScript
	{
		// 内部静态上下文对象
		static Form _MainForm;
		static ThemeSimulator _Simulator;
		static ILog _logger;

		// 实例中调用
		public Form MainForm { get { return _MainForm; } }
		public ThemeSimulator Simulator { get { return _Simulator; } }
		public ILog logger { get { return _logger; } }

		public static void BindRuntimeContext(Form mainForm, ThemeSimulator sim, ILog logger)
		{
			_MainForm = mainForm;
			_Simulator = sim;
			_logger = logger;
		}
	}
}
