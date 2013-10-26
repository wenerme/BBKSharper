using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ThemeSim.ThemeElements;
using ThemeSim.ThemeSettings;
using ThemeSim.Scripts;
using log4net;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
namespace ThemeSim
{
    public partial class TestForm : Form
    {

        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
			//logger.Info("TestForm loading...");
			Logger.logger.Info("TestForm loading...");
			if(typeof(IThemeElement).IsAssignableFrom(typeof(TextControl)))
				Console.WriteLine("Good TextControl");
			Type ta, tb;
			ta = typeof(TextSetting);
			tb = typeof(TextControl);
			if(ta.IsSubclassOf(typeof(ThemeElementSetting)))
				Console.WriteLine("Good ta");

			ThemeSimSetting setting = new ThemeSimSetting();
            
			//TextSetting textSetting = new TextSetting();
			//textSetting.Name = "text";
			////textSetting.Refer = "myref";
			//textSetting.Content = "Ok, This is Content";

			//ImageDirectoryResourcesSetting resSetting = new ImageDirectoryResourcesSetting();
			//resSetting.Name = "imageDir";
			//resSetting.Path = "./pic";

			//ImageSetting imageSetting = new ImageSetting();
			//imageSetting.Name = "image";
			//imageSetting.NormalImage = "bgimage";
			//imageSetting.HoverImage = "imageDir:2.bmp";
			//imageSetting.PressedImage = "imageDir:3.bmp";

			//ScreenSetting screenSetting = new ScreenSetting();
			//screenSetting.Name = "screen";
			//screenSetting.Elements.Add(new ScreenElementSetting() { Left = 20, Top = 10, Refer = "textA" });
			//screenSetting.Elements.Add(new ScreenElementSetting() { Left=10, Top=10,Refer="image"});

			//NameMappingSetting mappingSetting = new NameMappingSetting();
			//mappingSetting.Name = "textA";
			//mappingSetting.Refer = "text";
			//setting.NameMappingList.Add(mappingSetting);

			//mappingSetting = new NameMappingSetting();
			//mappingSetting.Name = "bgimage";
			//mappingSetting.Refer = "imageDir:1.bmp";
			//setting.NameMappingList.Add(mappingSetting);

			//setting.ScreenList.Add(screenSetting);
			//setting.ControlList.Add(imageSetting);
			//setting.ControlList.Add(textSetting);
			//setting.ResourceList.Add(resSetting);

			setting = ThemeSimSetting.LoadFromXML("setting.xml");

			ThemeSimulator sim = new ThemeSimulator(setting);
			sim.BaseDirectory = "images";


			this.KeyPreview = true;
			sim.Load();
			// use Lua
			//Lua lua = new Lua();
			//if(File.Exists("main.lua"))
			//{
			//    Logger.logger.Info("Find script 'main.lua'");
			//    ILog logger = LogManager.GetLogger(typeof(ThemeSim));
			//    lua["logger"] = logger;
			//    lua["themeSim"] = sim;
			//    lua["mainForm"] = this;
			//    //lua["U5"] = U5;
			//    try { 
			//        lua.DoFile("main.lua");
			//        var onLoad = lua.GetFunction("OnLoad");
			//        if(onLoad != null)
			//        {
			//            onLoad.Call();
			//        } else
			//            logger.Error("OnLoad function not found.");
			//    } 
			//    catch(LuaScriptException ex)
			//    { logger.Error("{0}:\n\t{1}".FormatMe(typeof(LuaScriptException), ex.Message)); }
			//}
			Console.WriteLine(Assembly.GetAssembly(typeof(log4net.LogManager)).FullName);


			Dictionary<string, string> providerOptions = new Dictionary<string, string>
                {
                    {"CompilerVersion", "v4.0"}
                };
			CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

			CompilerParameters compilerParams = new CompilerParameters
			{
				GenerateInMemory = true,
				GenerateExecutable = false,
				//IncludeDebugInformation = true,
				//TempFiles = new TempFileCollection(".",false)

			};

			List<Type> refType = new List<Type>();
			refType.Add(typeof(System.Drawing.Point));
			refType.Add(typeof(System.IO.Path));
			refType.Add(typeof(System.Windows.Forms.Keys));
			refType.Add(typeof(System.Net.Cookie));
			refType.Add(typeof(System.Linq.Enumerable));
			refType.Add(typeof(System.Text.Decoder));
			refType.Add(typeof(log4net.LogManager));
			refType.Add(typeof(ThemeSimulator));
			// 添加引用
			compilerParams.ReferencedAssemblies.AddRange(refType.ConvertAll((typ) => { return Assembly.GetAssembly(typ).Location; }).ToArray());
			CompilerResults results = provider.CompileAssemblyFromFile(compilerParams, "main.cs");


			if(results.Errors.Count > 0)
			{
				string txt = "";

				results.Errors.Cast<CompilerError>().ToList().ForEach(error =>
					Console.WriteLine("{3}:{0}:{1}\n\t{2}", error.Line, error.Column,error.ErrorText,error.FileName)
					);
				Console.WriteLine(txt);
				throw new Exception("Mission failed!");
			}

			//object o = results.CompiledAssembly.CreateInstance("Bar");
			//MethodInfo mi = o.GetType().GetMethod("SayHello");
			//mi.Invoke(o, null);
			RuntimeScript.BindRuntimeContext(this, sim, LogManager.GetLogger("Script"));
			Type t = results.CompiledAssembly.GetType("MainScript");
			MethodInfo info = t.GetMethod("OnLoad", BindingFlags.Public | BindingFlags.Static);
			info.Invoke(null,new object[] {this, sim, LogManager.GetLogger("Script")});

			//imageSetting.
			//ThemeElement
			
			//setting.SaveAsXML("setting.xml");
			sim.Screen.ShowScreen(sim.GetObjectByRefer("DefaultScreen") as ScreenControl);
			
            this.Controls.Add(sim.Screen);
			//Width = sim.DeviceInfo.ScreenWidth;
			//Height = sim.DeviceInfo.ScreenHeight;
			AutoSize = true;
			//this.Controls.Add(sim.GetObjectByRefer("image") as Control);
			//this.Controls.Add(sim.GetObjectByRefer("image") as Control);
        }
    }
}
