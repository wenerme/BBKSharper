using System;
using System.Drawing;
using System.Windows.Forms;
using BBK.Extension;
using ThemeSim.ThemeSettings;

namespace ThemeSim.ThemeElements
{
	/// <summary>
	///     用于在屏幕上显示图片
	/// </summary>
	public class ImageControl : Panel, IThemeControl
	{
		private readonly PictureBox pictureBox;
		public Image HoverImage;
		public Image NormalImage;
		public Image PressedImage;

		public ImageControl()
		{
			BackColor = Color.Transparent;
			DoubleBuffered = true;
			AutoSize = true;

			pictureBox = new PictureBox();
			pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
			pictureBox.BackColor = Color.Transparent;
			Controls.Add(pictureBox);

			pictureBox.MouseDown += DoMouseDown;
			pictureBox.MouseEnter += DoMouseEnter;
			pictureBox.MouseLeave += DoMouseLeave;
			pictureBox.MouseUp += DoMouseUp;
			// 代理事件
			pictureBox.Click += (sender, e) => { OnClick(e); };
			pictureBox.MouseClick += (sender, e) => { OnMouseClick(e); };
			// 改变父级
			ControlAdded += DoControlAdded;
		}

		// proxy property
		public Image Image
		{
			get { return pictureBox.Image; }
			set { pictureBox.Image = value; }
		}

		public int OffsetTop
		{
			get { return pictureBox.Top; }
			set { pictureBox.Top = value; }
		}

		public int OffsetLeft
		{
			get { return pictureBox.Left; }
			set { pictureBox.Left = value; }
		}

		public ThemeRefer Refer { get; set; }
		public ControlElementSetting Presetting { get; set; }

		//

		public void LoadSetting(IThemeSim sim, ThemeElementSetting elementSetting)
		{
			ThemeControl.LoadSetting(this, sim, elementSetting);
			var setting = ThemeElement.ConvertSetting<ImageSetting>(elementSetting);

			//Image image = GetImageByRefer(sim, new ThemeRefer(setting.NormalImage));


			//if(setting.WidthSpecified)
			//this.Width = setting.Width;

			// 是否需要裁减
			if (setting.SXSpecified || setting.SYSpecified || setting.WidthSpecified || setting.HeightSpecified)
			{
				var image = new Bitmap(GetImageByRefer(sim, new ThemeRefer(setting.NormalImage)));
				var crop = new Rectangle();
				crop.X = setting.SXSpecified ? setting.SX : 0;
				crop.Y = setting.SYSpecified ? setting.SY : 0;
				crop.Width = setting.WidthSpecified ? setting.Width : image.Width;
				crop.Height = setting.HeightSpecified ? setting.Height : image.Height;

				NormalImage = image.Clone(crop, image.PixelFormat);
			} else
				NormalImage = GetImageByRefer(sim, new ThemeRefer(setting.NormalImage));

			if (setting.HoverImageSpecified)
				HoverImage = GetImageByRefer(sim, new ThemeRefer(setting.HoverImage));
			if (setting.PressedImageSpecified)
				PressedImage = GetImageByRefer(sim, new ThemeRefer(setting.PressedImage));

			ShowImage(NormalImage);
		}

		public ThemeElementSetting GetSetting()
		{
			var setting = ThemeControl.GetSetting<ScreenSetting>(this);

			return setting;
		}

		public void LoadSettingByRefer(ThemeRefer refer)
		{
		}

		private void ShowImage(Image image)
		{
			Image = image;
			var setting = Presetting as ImageSetting;
			//if(false == setting.WidthSpecified)
			Width = image.Width;
			//if(false == setting.HeightSpecified)
			Height = image.Height;
		}

		private static Image GetImageByRefer(IThemeSim sim, ThemeRefer refer)
		{
			object obj = sim.GetObjectByRefer(refer);

			if (obj is Image)
			{
				return obj as Image;
			}
			if (obj is ImageControl)
			{
				return (obj as ImageControl).NormalImage.Clone() as Image;
			}
			if (obj == null)
				throw new Exception("Image Refer '{0}' not found.".FormatMe(refer.ToString()));
			throw new Exception("Image Refer '{0}' type not match.".FormatMe(refer.ToString()));
		}

		protected void DoControlAdded(object sender, ControlEventArgs e)
		{
			//Controls.Remove(e.Control);
			e.Control.Parent = pictureBox;
			//pictureBox.Controls.Add(e.Control);
		}

		#region 图片状态切换

		protected void DoMouseEnter(object sender, EventArgs e)
		{
			if ((Presetting as ImageSetting).HoverImageSpecified)
			{
				if (HoverImage is Image)
					ShowImage(HoverImage);
				else
					throw new Exception("{0} 控件的 HoverImage 并不是 Image.配置错误.".FormatMe(Name));
			}
		}

		protected void DoMouseLeave(object sender, EventArgs e)
		{
			if ((Presetting as ImageSetting).HoverImageSpecified)
			{
				ShowImage(NormalImage);
			}
		}

		protected void DoMouseDown(object sender, MouseEventArgs e)
		{
			if ((Presetting as ImageSetting).PressedImageSpecified)
			{
				if (PressedImage is Image)
					ShowImage(PressedImage);
				else
					throw new Exception("{0} 控件的 HoverImage 并不是 Image.配置错误.".FormatMe(Name));
			}
		}

		protected void DoMouseUp(object sender, MouseEventArgs e)
		{
			if ((Presetting as ImageSetting).PressedImageSpecified)
			{
				if ((Presetting as ImageSetting).HoverImageSpecified)
					ShowImage(HoverImage);
				else
					ShowImage(NormalImage);
			}
		}

		#endregion
	}
}