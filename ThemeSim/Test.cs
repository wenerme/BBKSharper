using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml.Serialization;
using System.IO;

using BBK.Device;
using BBK.FileType;

using ThemeSim.ThemeSettings;
namespace ThemeSim
{
	interface ICreatable
	{
		//ICreatable(string ok);
	}

	abstract class CA
    {
         static CA()
        {
            TypeList.Add(typeof(CA), "CA");
        }
        public static Dictionary<Type, string> TypeList = new Dictionary<Type, string>();
		
    }
    class CB:CA ,ICreatable
    {
        static CB()
        {
            TypeList.Add(typeof(CB), "CB");
        }

		public CB()
		{
		}
		
    }
    class CC : CA
    {
        static CC()
        {
            TypeList.Add(typeof(CC), "CC");
        }
    }
    class CBA : CB
    {
        static CBA()
        {
            TypeList.Add(typeof(CBA), "CBA");
        }
    }
    class Test
    {
        static void Main()
        {
            object c = new CB();
            Console.WriteLine(CA.TypeList[c.GetType()]);
            c = new CBA();
            Console.WriteLine(CA.TypeList[c.GetType()]);

			//PType(typeof(CA));
			PType(c as CB);
            //typeof(CA).GetMember("TypList")[0].;
            //
            var setting = new ThemeSimSetting("wener", "first theme", DeviceInfo.LM_9688.Name);
            var screen = new ScreenSetting() { Name="MainScreen"};
            var image = new ImageSetting("BGImage", "con:1") {Width=240,Height=320};

            setting.ScreenList.Add(screen);
            setting.ResourceList.Add(new ImageContainerResourcesSetting(){Name="main",Path= "./wen/x.dlx"});
            setting.ControlList.Add(image);

			setting.NameMappingList.Add(new NameMappingSetting() { Name="RefA", Refer="RefTo" });
            image.PressedImage = "pressed:1";

            var serializer = new XmlSerializer(typeof(ThemeSimSetting));
            
            serializer.Serialize(Console.Out, setting);

            Console.WriteLine("\n\nPress any key to continue ...");
            Console.ReadKey();
        }

		static void PType<T>(T type)
			where T : new()
		{
			Console.WriteLine("Type is {0}",typeof(T));
			T s = new T();
			if(typeof(T).IsSubclassOf(typeof(CA)))
			{
				Console.WriteLine("Good CA");
			}
		}
    }
}
