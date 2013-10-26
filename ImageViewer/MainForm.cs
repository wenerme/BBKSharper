using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//using Ookii.Dialogs;
using Ookii.Dialogs;
using BBK.FileType;
using BBK.Extension;

namespace ImageViewer
{
    public partial class MainForm : Form
    {

        IList<Bitmap> ImageList = new List<Bitmap>();
        
        int currentImageIndex = 0;
        Dictionary<string, ImageFormat> ExtensionToImageFormat = new Dictionary<string, ImageFormat>();
        private Dictionary<Keys,string> KeyCodeToAction = new Dictionary<Keys,string>();

        string imageFilter = "图像文件(*.jpg; *.jpeg; *.gif; *.bmp;*.png)|*.jpg; *.jpeg; *.gif; *.bmp;*.png";
        string BBKImageFilter = "步步高图像文件(*.dlx;*.rlb;*.lib)|*.dlx; *.rlb; *.lib;";
        string lastSelectPath = Directory.GetCurrentDirectory();
		string lastAppendFilepath = "";
		/// <summary>
		/// 添加时自动进行清除
		/// </summary>
		bool ClearWhenAppend = false;
        public MainForm()
        {
            InitializeComponent();
        }
        //
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBoxMain.ContextMenuStrip = cmsPictureBoxMain;

			// 设置后缀名对应的文件格式
            ExtensionToImageFormat[".png"] = ImageFormat.Png;
            ExtensionToImageFormat[".jpg"] = ImageFormat.Jpeg;
            ExtensionToImageFormat[".jpeg"] = ImageFormat.Jpeg;
            ExtensionToImageFormat[".bmp"] = ImageFormat.Bmp;
            ExtensionToImageFormat[".gif"] = ImageFormat.Gif;

			// 快捷键映射
            KeyCodeToAction[Keys.Left] = "PrevImage";
            KeyCodeToAction[Keys.Right] = "NextImage";
            KeyCodeToAction[Keys.O] = "OpenFile";
            KeyCodeToAction[Keys.Delete] = "RemoveImage";
			// 初始值
			ClearWhenAppend = tsmiClearWhenAppend.Checked;
        }
		// click 事件处理
        private void DoAction(object sender, EventArgs e)
        {
            string cmd = "";
            if (sender is Control)
            {
                cmd = (sender as Control).Tag as string;
            }
            else if (sender is ToolStripItem)
            {
                cmd = (sender as ToolStripItem).Tag as string;
            }

            DoAction(cmd);
        }

        #region DoAction
        private void DoAction(string cmd)
        {


            List<string> ValidWhenNoImage = new List<string>();
            ValidWhenNoImage.Add("OpenFile");
            ValidWhenNoImage.Add("AddImage");
            ValidWhenNoImage.Add("About");
            ValidWhenNoImage.Add("AtMe");
            ValidWhenNoImage.Add("OpenHomePage");

            if (ImageList.Count == 0 && !ValidWhenNoImage.Contains(cmd))
            {
                Report("尚无图片");
                return;
            }

            switch (cmd)
            {
                case "OpenFile":
                    {
                        OpenFileDialog open = new OpenFileDialog();
                        open.Filter = BBKImageFilter;
                        if (open.ShowDialog() == DialogResult.OK)
                        {
							// 自动清除
							if(ClearWhenAppend)
								DoAction("RemoveAllImage");
                            OpenFile(open.FileName);
                        }
                    }
                    break;
                case "RemoveImage":
                    if (ImageList.Count == 0)
                    {
                        Report("列表中没有图片");
                        break;
                    }
                    ImageList.RemoveAt(currentImageIndex);
                    ShowImage();// 校正当前显示的图像
                    break;
                case "RemoveAllImage":
                    ImageList.Clear();
                    ShowImage();// 校正当前显示的图像
                    break;
                case "AddImage":
                    {
                        OpenFileDialog open = new OpenFileDialog();
                        open.Multiselect = true;
                        open.Filter = "{0}|{1}".FormatMe(imageFilter, BBKImageFilter);
                        if (open.ShowDialog() == DialogResult.OK)
                        {
                            foreach (var fn in open.FileNames)
                            {
                                AppendFile(fn);
                            }
                        }
                        ShowImage();// 校正当前显示的图像
                    }
                    break;
                case "ReplaceImage":
                    {
                        OpenFileDialog open = new OpenFileDialog();
                        open.Multiselect = true;
                        open.Filter = imageFilter;
                        if (open.ShowDialog() == DialogResult.OK)
                        {
                            foreach (var fn in open.FileNames)
                            {
                                Image image = Image.FromFile(fn);
                                ImageList[currentImageIndex] = new Bitmap(image);
                            }
                        }
                        ShowImage();// 校正当前显示的图像
                    }
                    break;
                case "MoveForward":
                    if (ImageList.Count > 1 && currentImageIndex > 0)
                    {
                        var tmp = ImageList[currentImageIndex];
                        ImageList[currentImageIndex] = ImageList[currentImageIndex - 1];
                        ImageList[currentImageIndex - 1] = tmp;
                        ShowImage();// 校正当前显示的图像
                    }
                    else
                        Report("无法移动");
                    break;
                case "MoveBackward":
                    if (ImageList.Count > 1 && currentImageIndex < ImageList.Count - 1)
                    {
                        var tmp = ImageList[currentImageIndex];
                        ImageList[currentImageIndex] = ImageList[currentImageIndex + 1];
                        ImageList[currentImageIndex + 1] = tmp;
                        ShowImage();// 校正当前显示的图像
                    }
                    else
                        Report("无法移动");
                    break;
                case "SaveAs":
                    {
                        SaveFileDialog save = new SaveFileDialog();
                        //save.AddExtension = true;
                        //save.DefaultExt = ".lib";
                        save.Filter = "步步高图像文件(*.dlx;*.rlb;*.lib)|*.dlx;*.rlb;*.lib;";
                        if (save.ShowDialog() == DialogResult.OK)
                        {
                            string ext = Path.GetExtension(save.FileName).ToLower();
                            IBitmapContainerFile file;
                            switch (ext)
                            {
                                case ".dlx":
                                    file = new DlxFile();
                                    break;
                                case ".lib":
                                    file = new LibFile();
                                    break;
                                case ".rlb":
                                    file = new RlbFile();
                                    break;
                                default:
                                    Report("不支持的导出格式 " + ext);
                                    return;
                            }
                            foreach (var image in ImageList)
                            {
                                file.ImageList.Add(image);
                            }
                            file.SaveAs(save.FileName);
                        }
                    }
                    break;
                case "ExportImage":
                    {
                        if (ImageList.Count == 0)
                        {
                            Report("列表中没有图片导出.");
                            break;
                        }
                        SaveFileDialog save = new SaveFileDialog();
                        save.AddExtension = true;
                        save.DefaultExt = ".png";
                        save.Filter = "图像文件(*.jpg; *.jpeg; *.gif; *.bmp;*.png)|*.jpg;*.jpeg;*.gif;*.bmp;*.png";
                        if (save.ShowDialog() == DialogResult.OK)
                        {
                            ImageFormat format = ExtensionToImageFormat[Path.GetExtension(save.FileName).ToLower()];
                            ImageList[currentImageIndex].Save(save.FileName, format);
                        }
                    }
                    break;
                case "ExportAllImage":
                    {

						VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
                        dialog.Description = "选择导出的文件夹:";
                        dialog.ShowNewFolderButton = true;
						dialog.SelectedPath = Path.Combine(Path.GetDirectoryName(lastSelectPath), Path.GetFileNameWithoutExtension(lastAppendFilepath));
						dialog.UseDescriptionForTitle = true;
						
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            var path = dialog.SelectedPath;
                            lastSelectPath = path;
                            Directory.CreateDirectory(path);
                            var i = 1;

                            foreach (var image in ImageList)
                            {
                                image.Save(Path.Combine(path,i++ +".png"),ImageFormat.Png);
                            }
                        }
                    }
                    break;
                case "PrevImage":
                    ShowImage(--currentImageIndex);
                    break;
                case "NextImage":
                    ShowImage(++currentImageIndex);
                    break;
                case "About":
                    var box = new AboutBox();
                    box.ShowDialog();
                    break;
                case "AtMe":
                    System.Diagnostics.Process.Start("http://weibo.com/u/2705020605");
                    break;
                case "OpenHomePage":
                    System.Diagnostics.Process.Start("http://blog.wener.me");
                    break;
				case "ClearWhenAppend":
					tsmiClearWhenAppend.Checked = !tsmiClearWhenAppend.Checked;
					ClearWhenAppend = tsmiClearWhenAppend.Checked;
					break;
                default:
                    Report("未定义的动作 '{0}'".FormatMe(cmd));
                    break;
            }

        }
#endregion


        private void OpenFile(string filepath)
        {
            Report("正在打开文件,请稍等...");
            AppendFile(filepath);
            Report("文件已打开");
        }
        private void AppendFile(string filepath)
        {
			lastAppendFilepath = filepath;
            IBitmapContainerFile containerFile = null;
            
            using (Stream stream = File.OpenRead(filepath))
            {
                string ext = Path.GetExtension(filepath).ToLower();
                if (ExtensionToImageFormat.ContainsKey(ext))
                {
                    var image = Image.FromFile(filepath);
                    ImageList.Add(new Bitmap(image));
                }else if (LibFile.isSupport(filepath) && LibFile.VerifyFile(stream))
                {
                    containerFile = new LibFile(stream);
                }
                else if (DlxFile.isSupport(filepath) && DlxFile.VerifyFile(stream))
                {
                    containerFile = new DlxFile(stream);
                }
                else if (RlbFile.isSupport(filepath) && RlbFile.VerifyFile(stream))
                {
                    containerFile = new RlbFile(stream);
                }else{
                        Report("打开文件失败");
                        MessageBox.Show("打开文件失败:'" + filepath + "'", "不支持的文件格式");
                        return;
                    }
                //
                if (containerFile != null)
                { 
                    ImageList = ImageList.Concat(containerFile.ImageList).ToList();
                }
            }

            //ShowImage(0);
        }

        private void ShowImage()
        {
            ShowImage(currentImageIndex);
        }
        private void ShowImage(int index)
        {
            if (ImageList.Count == 0)
            {
                Report("图片列表中已没有图片了");
                pictureBoxMain.Image = ImageViewer.Properties.Resources.OpenTip;
                tsStatusLabelImageCount.Text = "0/0";
                return;
            }
              
            if (index < 0)
                index = 0;
            else if (index >= ImageList.Count)
                index = ImageList.Count - 1;

            currentImageIndex = index;

            Image image = ImageList[index];
            pictureBoxMain.Image = image;
            tsStatusLabelImageCount.Text = "{0}/{1} {2}*{3}".FormatMe(index+1, ImageList.Count,image.Width,image.Height);
            Report("拖动添加图片..");
        }

        private void Report(string text)
        {
            tsStatusLabelReport.Text = text;
        }

        
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (KeyCodeToAction.ContainsKey(e.KeyCode))
            {
                DoAction(KeyCodeToAction[e.KeyCode]);
            }
        }
    #region 拖动实现 拖动显示时,自动切换到添加的图片
        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
			// 自动清除
			if(ClearWhenAppend)
				DoAction("RemoveAllImage");
			int before = ImageList.Count;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
                AppendFile(file);
			
			// 添加后显示添加的图片
			ShowImage(before);
        }
#endregion


    }
}

