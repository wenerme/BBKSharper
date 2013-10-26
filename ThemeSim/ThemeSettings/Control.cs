using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Drawing;


namespace ThemeSim.ThemeSettings
{
    /// <summary>
    /// 控件类元素配置
    /// </summary>
    abstract public class ControlElementSetting : ThemeElementSetting
    {
        /// <summary>
        /// 引用
        /// </summary>
        [XmlAttribute]
        public string Refer;

		#region 控件属性

		/// <summary>
		/// 控件坐标 X
		/// </summary>
		[XmlAttribute]
		public int Left;

		/// <summary>
		/// 控件坐标 Y
		/// </summary>
		[XmlAttribute]
		public int Top;

		/// <summary>
		/// 控件宽
		/// </summary>
		[XmlAttribute]
		public int Width;

		/// <summary>
		/// 控件高
		/// </summary>
		[XmlAttribute]
		public int Height;

		/// <summary>
		/// 字体
		/// </summary>
		[XmlAttribute]
		public string Font;
		public bool FontSpecified { get { return Font != null && Font.Length > 0; } }
		
		/// <summary>
		/// 字体尺寸
		/// </summary>
		[XmlAttribute]
		public int FontSize;
		public bool FontSizeSpecified { get { return FontSize > 0; } }
		/// <summary>
		/// 字体
		/// </summary>
		[XmlAttribute]
		public string FontFamily;
		public bool FontFamilySpecified { get { return FontFamily != null && FontFamily.Length > 0; } }

		/// <summary>
		/// 文字内容
		/// </summary>
		[XmlAttribute]
		public string Text;
		[XmlIgnore]
		public bool TextSpecified { get { return Text != null && Text.Length > 0; } }
		/// <summary>
		/// 标签 可用来放置自定义数据
		/// </summary>
		[XmlAttribute]
		public string Tag;
		[XmlIgnore]
		public bool TagSpecified { get { return Tag != null && Tag.Length > 0; } }
		/// <summary>
		/// 背景色
		/// </summary>
		[XmlAttribute]
		public string BackColor;
		[XmlIgnore]
		public bool BackColorSpecified { get { return BackColor != null && BackColor.Length > 0; } }
		/// <summary>
		/// 前景色
		/// </summary>
		[XmlAttribute]
		public string ForeColor;
		[XmlIgnore]
		public bool ForeColorSpecified { get { return ForeColor != null && ForeColor.Length > 0; } }


		[XmlIgnore]
		public bool WidthSpecified { get { return Width > 0; } }
		[XmlIgnore]
		public bool HeightSpecified { get { return Height > 0; } }
		[XmlIgnore]
		public bool LeftSpecified { get { return Left > 0; } }
		[XmlIgnore]
		public bool TopSpecified { get { return Top > 0; } }
		#endregion

		public void ApplyToControl(Control ctr)
		{
			//ParentSpecified ? ctr.Parent
			//ForeColorSpecified? ctr.ForeColor = ForeColor: null;
			if(ForeColorSpecified)
				ctr.ForeColor = U5.ColorFromString(ForeColor);
			if(BackColorSpecified)
				ctr.ForeColor = U5.ColorFromString(BackColor);
			//
			if(TextSpecified)
				ctr.Text = Text;
			if(TagSpecified)
				ctr.Tag = Tag;
			//
			if(LeftSpecified)
				ctr.Left = Left;
			if(HeightSpecified)
				ctr.Height = Height;
			if(TopSpecified)
				ctr.Top = Top;
			if(WidthSpecified)
				ctr.Width = Width;
			//
			if(FontSpecified)
				ctr.Font = U5.FontFromString(Font);

			if(FontSizeSpecified)
			{
				Font font = ctr.Font;
				ctr.Font = new Font(FontFamilySpecified ? new FontFamily(FontFamily) : font.FontFamily,
					FontSize, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
			} else if(FontFamilySpecified)
			{
				Font font = ctr.Font;
				ctr.Font = new Font( new FontFamily(FontFamily), font.Size, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
			}
		}
	}
    /// <summary>
    /// 主题屏幕中的元素配置
    /// </summary>
    public class ScreenElementSetting : ControlElementSetting
    {
        

		/// <summary>
		/// 跳转
		/// </summary>
		[XmlAttribute]
		public string Href;

		/// <summary>
		/// 父级,影响透明 定位
		/// </summary>
		[XmlAttribute]
		public string Parent;

		[XmlIgnore]
		public bool HrefSpecified { get { return Href != null && Href.Length > 0; } }
		//
		[XmlIgnore]
		public bool ParentSpecified { get { return Parent != null && Parent.Length > 0; } }

        public ScreenElementSetting()
        { }
    }
    /// <summary>
    /// 主题屏幕的配置
    /// </summary>
    [XmlRootAttribute("Screen")]
    public class ScreenSetting : ControlElementSetting
    {
        /// <summary>
        /// 该主题包含的元素集
        /// </summary>
        [XmlArrayItem("Element")]
        public List<ScreenElementSetting> Elements;
		/// <summary>
		/// 背景图像
		/// </summary>
		[XmlAttribute]
		public string BackgroundImage;
		[XmlIgnore]
		public bool BackgroundImageSpecified { get { return BackgroundImage != null && BackgroundImage.Length > 0; } }

        public ScreenSetting()
        {
            Elements = new List<ScreenElementSetting>();
        }
    }
    /// <summary>
    /// 图像
    /// 主题显示的基本元素
    /// SX,SY和Width,Height用于图片的裁剪
    /// </summary>
    [XmlRootAttribute("Image"), XmlType("Image")]
    [Serializable]
    public class ImageSetting : ControlElementSetting
    {
        /// <summary>
        /// 图片左上角X坐标
        /// </summary>
        [XmlAttribute]
        public int SX;
        /// <summary>
        /// 图片左上角Y坐标
        /// </summary>
        [XmlAttribute]
        public int SY;
        /// <summary>
        /// 图片宽
        /// </summary>
        [XmlAttribute]
        public int Width;
        /// <summary>
        /// 图片高
        /// </summary>
        [XmlAttribute]
        public int Height;

		/// <summary>
		/// 正常显示的图片
		/// </summary>
		[XmlAttribute]
		public string NormalImage;
        /// <summary>
        /// 鼠标按下时的图片
        /// </summary>
        [XmlAttribute]
        public string PressedImage;
        /// <summary>
        /// 鼠标经过时的图片
        /// </summary>
        [XmlAttribute]
        public string HoverImage;

        [XmlIgnore]
        public bool SXSpecified { get { return SX > 0; } }
        [XmlIgnore]
        public bool SYSpecified { get { return SY > 0; } }
        [XmlIgnore]
        public bool WidthSpecified { get { return Width > 0; } }
        [XmlIgnore]
        public bool HeightSpecified { get { return Height > 0; } }
        [XmlIgnore]
		public bool PressedImageSpecified { get { return PressedImage != null && PressedImage.Length > 0; } }
        [XmlIgnore]
		public bool HoverImageSpecified { get { return HoverImage != null && HoverImage.Length > 0; } }

        public ImageSetting()
        { }
        public ImageSetting(string name, string refer)
            : this()
        {
            Name = name;
            Refer = refer;
        }
    }
    /// <summary>
    /// 文字
    /// 该项信息是额外的信息,或许在生成主题的时候会用上
    /// </summary>
    [XmlType("Text")]
    public class TextSetting : ControlElementSetting
    {
        public TextSetting()
        { }
    }

	/// <summary>
	/// 区域
	/// 只是简单的用来定义一个区域
	/// </summary>
	[XmlType("Area")]
	public class AreaSetting : ControlElementSetting
	{
	}
}
