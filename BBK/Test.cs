using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using BBK.FileType;

namespace BBK
{
	class CA
	{
		protected string val = "CA";
		public void DoIt()
		{
			Console.WriteLine(val);
		}
	}

	class CB:CA
	{
		protected int val = 100;

		public CB()
		{
			//val = "wen";
		}
	}

	class Test
	{
		static void Main()
		{

            //throw new Exception("test");
			//RlbFileTest("test.rlb");
            //LibFileTest("al.lib");
            DlxFileTest("tmp.dlx");
			//
			Console.WriteLine("按任意键继续...");
			Console.ReadKey();
		}
		static void SaveImage(IList<Bitmap> list, string filename = "{0}.png")
		{
			var i = 0;
			foreach (var image in list)
			{
				image.Save(string.Format(filename,i));
				i++;
			}
		}
        static void SaveImage(IList<Bitmap> list, IList<string> filename)
        {
            var i = 0;
            foreach (var image in list)
            {
                image.Save(string.Format(filename[i], i));
                i++;
            }
        }
        static void DlxFileTest(string filepath)
        {
            DlxFile dlx = new DlxFile(filepath);

            
            dlx.SaveAs("sec.dlx");
            dlx = new DlxFile("sec.dlx");
            SaveImage(dlx.ImageList);
        }
		static void RlbFileTest(string filepath)
		{
			RlbFile rlb = new RlbFile(filepath);
            foreach (var image in rlb.ImageList)
            {
                var fn = rlb.GetName(image);
                image.Save(fn);
            }
			//SaveImage(rlb.ImageList);
            rlb.SaveAs("sec.rlb");
		}
		static void LibFileTest(string filepath)
		{
            LibFile lib = new LibFile(filepath);
			{
				var i = 0;
				foreach (var image in lib.ImageList)
				{
					i++;
					image.Save(i + "a.png");
				}
			}
			//list.RemoveAt(1);
            lib.SaveAs("sec.lib");

            lib = new LibFile(filepath);
			{
				var i = 0;
                foreach (var image in lib.ImageList)
				{
					i++;
					image.Save(i + "b.png");
				}
			}
		}
	// end
	}
}
