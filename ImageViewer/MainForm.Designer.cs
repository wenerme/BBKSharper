namespace ImageViewer
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tsStatusLabelImageCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsStatusLabelReport = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsbtnOpenFile = new System.Windows.Forms.ToolStripButton();
			this.tsbtnSaveAs = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnPrevImage = new System.Windows.Forms.ToolStripButton();
			this.tsbtnNextImage = new System.Windows.Forms.ToolStripButton();
			this.tsbtnAddImage = new System.Windows.Forms.ToolStripButton();
			this.tsbtnRemoveImage = new System.Windows.Forms.ToolStripButton();
			this.tsbtnReplaceImage = new System.Windows.Forms.ToolStripButton();
			this.tsbtnExportImage = new System.Windows.Forms.ToolStripButton();
			this.tsbtnRemoveAllImage = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
			this.tsbtnAbout = new System.Windows.Forms.ToolStripButton();
			this.cmsPictureBoxMain = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.上一张ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.下一张ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.移除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.替换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolStripMenuItemExportAll = new System.Windows.Forms.ToolStripMenuItem();
			this.清除所有ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiClearWhenAppend = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.前移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.后移ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pictureBoxMain = new System.Windows.Forms.PictureBox();
			this.statusStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.cmsPictureBoxMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).BeginInit();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusLabelImageCount,
            this.tsStatusLabelReport});
			this.statusStrip1.Location = new System.Drawing.Point(0, 364);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(315, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tsStatusLabelImageCount
			// 
			this.tsStatusLabelImageCount.Name = "tsStatusLabelImageCount";
			this.tsStatusLabelImageCount.Size = new System.Drawing.Size(41, 17);
			this.tsStatusLabelImageCount.Text = "00/00";
			this.tsStatusLabelImageCount.ToolTipText = "图片数量";
			// 
			// tsStatusLabelReport
			// 
			this.tsStatusLabelReport.Name = "tsStatusLabelReport";
			this.tsStatusLabelReport.Size = new System.Drawing.Size(116, 17);
			this.tsStatusLabelReport.Text = "打开或拖拽打开文件";
			// 
			// toolStrip1
			// 
			this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
			this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnOpenFile,
            this.tsbtnSaveAs,
            this.toolStripSeparator4,
            this.tsbtnPrevImage,
            this.tsbtnNextImage,
            this.tsbtnAddImage,
            this.tsbtnRemoveImage,
            this.tsbtnReplaceImage,
            this.tsbtnExportImage,
            this.tsbtnRemoveAllImage,
            this.toolStripSeparator5,
            this.toolStripButton1,
            this.toolStripButton2,
            this.tsbtnAbout});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(315, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsbtnOpenFile
			// 
			this.tsbtnOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnOpenFile.Image = global::ImageViewer.Properties.Resources.OpenFile;
			this.tsbtnOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnOpenFile.Name = "tsbtnOpenFile";
			this.tsbtnOpenFile.Size = new System.Drawing.Size(23, 22);
			this.tsbtnOpenFile.Tag = "OpenFile";
			this.tsbtnOpenFile.Text = "toolStripButton1";
			this.tsbtnOpenFile.ToolTipText = "打开文件";
			this.tsbtnOpenFile.Click += new System.EventHandler(this.DoAction);
			// 
			// tsbtnSaveAs
			// 
			this.tsbtnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnSaveAs.Image = global::ImageViewer.Properties.Resources.SaveAs;
			this.tsbtnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSaveAs.Name = "tsbtnSaveAs";
			this.tsbtnSaveAs.Size = new System.Drawing.Size(23, 22);
			this.tsbtnSaveAs.Tag = "SaveAs";
			this.tsbtnSaveAs.Text = "toolStripButton4";
			this.tsbtnSaveAs.ToolTipText = "另存为";
			this.tsbtnSaveAs.Click += new System.EventHandler(this.DoAction);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbtnPrevImage
			// 
			this.tsbtnPrevImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnPrevImage.Image = global::ImageViewer.Properties.Resources.PrevImage;
			this.tsbtnPrevImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnPrevImage.Name = "tsbtnPrevImage";
			this.tsbtnPrevImage.Size = new System.Drawing.Size(23, 22);
			this.tsbtnPrevImage.Tag = "PrevImage";
			this.tsbtnPrevImage.Text = "toolStripButton2";
			this.tsbtnPrevImage.ToolTipText = "上一张图片";
			this.tsbtnPrevImage.Click += new System.EventHandler(this.DoAction);
			// 
			// tsbtnNextImage
			// 
			this.tsbtnNextImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnNextImage.Image = global::ImageViewer.Properties.Resources.NextImage;
			this.tsbtnNextImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnNextImage.Name = "tsbtnNextImage";
			this.tsbtnNextImage.Size = new System.Drawing.Size(23, 22);
			this.tsbtnNextImage.Tag = "NextImage";
			this.tsbtnNextImage.Text = "toolStripButton1";
			this.tsbtnNextImage.ToolTipText = "下一张图片";
			this.tsbtnNextImage.Click += new System.EventHandler(this.DoAction);
			// 
			// tsbtnAddImage
			// 
			this.tsbtnAddImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAddImage.Image = global::ImageViewer.Properties.Resources.AddImage;
			this.tsbtnAddImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAddImage.Name = "tsbtnAddImage";
			this.tsbtnAddImage.Size = new System.Drawing.Size(23, 22);
			this.tsbtnAddImage.Tag = "AddImage";
			this.tsbtnAddImage.Text = "toolStripButton1";
			this.tsbtnAddImage.ToolTipText = "添加图片";
			this.tsbtnAddImage.Click += new System.EventHandler(this.DoAction);
			// 
			// tsbtnRemoveImage
			// 
			this.tsbtnRemoveImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnRemoveImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRemoveImage.Image")));
			this.tsbtnRemoveImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnRemoveImage.Name = "tsbtnRemoveImage";
			this.tsbtnRemoveImage.Size = new System.Drawing.Size(23, 22);
			this.tsbtnRemoveImage.Tag = "RemoveImage";
			this.tsbtnRemoveImage.Text = "toolStripButton2";
			this.tsbtnRemoveImage.ToolTipText = "移除图片";
			this.tsbtnRemoveImage.Click += new System.EventHandler(this.DoAction);
			// 
			// tsbtnReplaceImage
			// 
			this.tsbtnReplaceImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnReplaceImage.Image = global::ImageViewer.Properties.Resources.ReplaceImage;
			this.tsbtnReplaceImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnReplaceImage.Name = "tsbtnReplaceImage";
			this.tsbtnReplaceImage.Size = new System.Drawing.Size(23, 22);
			this.tsbtnReplaceImage.Tag = "ReplaceImage";
			this.tsbtnReplaceImage.Text = "toolStripButton3";
			this.tsbtnReplaceImage.ToolTipText = "替换图片";
			this.tsbtnReplaceImage.Click += new System.EventHandler(this.DoAction);
			// 
			// tsbtnExportImage
			// 
			this.tsbtnExportImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnExportImage.Image = global::ImageViewer.Properties.Resources.ExportImage;
			this.tsbtnExportImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnExportImage.Name = "tsbtnExportImage";
			this.tsbtnExportImage.Size = new System.Drawing.Size(23, 22);
			this.tsbtnExportImage.Tag = "ExportImage";
			this.tsbtnExportImage.Text = "toolStripButton1";
			this.tsbtnExportImage.ToolTipText = "导出图像";
			this.tsbtnExportImage.Click += new System.EventHandler(this.DoAction);
			// 
			// tsbtnRemoveAllImage
			// 
			this.tsbtnRemoveAllImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnRemoveAllImage.Image = global::ImageViewer.Properties.Resources.RemoveAllImage;
			this.tsbtnRemoveAllImage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnRemoveAllImage.Name = "tsbtnRemoveAllImage";
			this.tsbtnRemoveAllImage.Size = new System.Drawing.Size(23, 22);
			this.tsbtnRemoveAllImage.Tag = "RemoveAllImage";
			this.tsbtnRemoveAllImage.Text = "tsbtnRemoveAllImage";
			this.tsbtnRemoveAllImage.ToolTipText = "清除所有图片";
			this.tsbtnRemoveAllImage.Click += new System.EventHandler(this.DoAction);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Image = global::ImageViewer.Properties.Resources.AtMe;
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Tag = "AtMe";
			this.toolStripButton1.Text = "toolStripButton1";
			this.toolStripButton1.ToolTipText = "@Me";
			this.toolStripButton1.Click += new System.EventHandler(this.DoAction);
			// 
			// toolStripButton2
			// 
			this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton2.Image = global::ImageViewer.Properties.Resources.HomePage;
			this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton2.Name = "toolStripButton2";
			this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton2.Tag = "OpenHomePage";
			this.toolStripButton2.Text = "toolStripButton2";
			this.toolStripButton2.ToolTipText = "打开主页";
			this.toolStripButton2.Click += new System.EventHandler(this.DoAction);
			// 
			// tsbtnAbout
			// 
			this.tsbtnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbtnAbout.Image = global::ImageViewer.Properties.Resources.About;
			this.tsbtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAbout.Name = "tsbtnAbout";
			this.tsbtnAbout.Size = new System.Drawing.Size(23, 22);
			this.tsbtnAbout.Tag = "About";
			this.tsbtnAbout.Text = "toolStripButton5";
			this.tsbtnAbout.ToolTipText = "关于";
			this.tsbtnAbout.Click += new System.EventHandler(this.DoAction);
			// 
			// cmsPictureBoxMain
			// 
			this.cmsPictureBoxMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开ToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.toolStripSeparator1,
            this.上一张ToolStripMenuItem,
            this.下一张ToolStripMenuItem,
            this.添加ToolStripMenuItem,
            this.移除ToolStripMenuItem,
            this.替换ToolStripMenuItem,
            this.导出ToolStripMenuItem,
            this.ToolStripMenuItemExportAll,
            this.清除所有ToolStripMenuItem,
            this.tsmiClearWhenAppend,
            this.toolStripSeparator2,
            this.前移ToolStripMenuItem,
            this.后移ToolStripMenuItem,
            this.toolStripSeparator3,
            this.关于ToolStripMenuItem});
			this.cmsPictureBoxMain.Name = "contextMenuStrip1";
			this.cmsPictureBoxMain.Size = new System.Drawing.Size(161, 352);
			// 
			// 打开ToolStripMenuItem
			// 
			this.打开ToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.OpenFile;
			this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
			this.打开ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.打开ToolStripMenuItem.Tag = "OpenFile";
			this.打开ToolStripMenuItem.Text = "打开";
			this.打开ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// 另存为ToolStripMenuItem
			// 
			this.另存为ToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.SaveAs;
			this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
			this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.另存为ToolStripMenuItem.Tag = "SaveAs";
			this.另存为ToolStripMenuItem.Text = "另存为";
			this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
			// 
			// 上一张ToolStripMenuItem
			// 
			this.上一张ToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.PrevImage;
			this.上一张ToolStripMenuItem.Name = "上一张ToolStripMenuItem";
			this.上一张ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.上一张ToolStripMenuItem.Tag = "PrevImage";
			this.上一张ToolStripMenuItem.Text = "上一张";
			this.上一张ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// 下一张ToolStripMenuItem
			// 
			this.下一张ToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.NextImage;
			this.下一张ToolStripMenuItem.Name = "下一张ToolStripMenuItem";
			this.下一张ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.下一张ToolStripMenuItem.Tag = "NextImage";
			this.下一张ToolStripMenuItem.Text = "下一张";
			this.下一张ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// 添加ToolStripMenuItem
			// 
			this.添加ToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.AddImage;
			this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
			this.添加ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.添加ToolStripMenuItem.Tag = "AddImage";
			this.添加ToolStripMenuItem.Text = "添加";
			this.添加ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// 移除ToolStripMenuItem
			// 
			this.移除ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("移除ToolStripMenuItem.Image")));
			this.移除ToolStripMenuItem.Name = "移除ToolStripMenuItem";
			this.移除ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.移除ToolStripMenuItem.Tag = "RemoveImage";
			this.移除ToolStripMenuItem.Text = "移除";
			this.移除ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// 替换ToolStripMenuItem
			// 
			this.替换ToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.ReplaceImage;
			this.替换ToolStripMenuItem.Name = "替换ToolStripMenuItem";
			this.替换ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.替换ToolStripMenuItem.Tag = "ReplaceImage";
			this.替换ToolStripMenuItem.Text = "替换";
			this.替换ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// 导出ToolStripMenuItem
			// 
			this.导出ToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.ExportImage;
			this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
			this.导出ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.导出ToolStripMenuItem.Tag = "ExportImage";
			this.导出ToolStripMenuItem.Text = "导出";
			this.导出ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// ToolStripMenuItemExportAll
			// 
			this.ToolStripMenuItemExportAll.Name = "ToolStripMenuItemExportAll";
			this.ToolStripMenuItemExportAll.Size = new System.Drawing.Size(160, 22);
			this.ToolStripMenuItemExportAll.Tag = "ExportAllImage";
			this.ToolStripMenuItemExportAll.Text = "导出所有";
			this.ToolStripMenuItemExportAll.Click += new System.EventHandler(this.DoAction);
			// 
			// 清除所有ToolStripMenuItem
			// 
			this.清除所有ToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.RemoveAllImage;
			this.清除所有ToolStripMenuItem.Name = "清除所有ToolStripMenuItem";
			this.清除所有ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.清除所有ToolStripMenuItem.Tag = "RemoveAllImage";
			this.清除所有ToolStripMenuItem.Text = "清除所有";
			this.清除所有ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// tsmiClearWhenAppend
			// 
			this.tsmiClearWhenAppend.AutoSize = false;
			this.tsmiClearWhenAppend.Checked = true;
			this.tsmiClearWhenAppend.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsmiClearWhenAppend.Name = "tsmiClearWhenAppend";
			this.tsmiClearWhenAppend.Size = new System.Drawing.Size(160, 22);
			this.tsmiClearWhenAppend.Tag = "ClearWhenAppend";
			this.tsmiClearWhenAppend.Text = "添加时自动清除";
			this.tsmiClearWhenAppend.Click += new System.EventHandler(this.DoAction);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
			// 
			// 前移ToolStripMenuItem
			// 
			this.前移ToolStripMenuItem.Name = "前移ToolStripMenuItem";
			this.前移ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.前移ToolStripMenuItem.Tag = "MoveForward";
			this.前移ToolStripMenuItem.Text = "前移";
			this.前移ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// 后移ToolStripMenuItem
			// 
			this.后移ToolStripMenuItem.Name = "后移ToolStripMenuItem";
			this.后移ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.后移ToolStripMenuItem.Tag = "MoveBackward";
			this.后移ToolStripMenuItem.Text = "后移";
			this.后移ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
			// 
			// 关于ToolStripMenuItem
			// 
			this.关于ToolStripMenuItem.Image = global::ImageViewer.Properties.Resources.About;
			this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
			this.关于ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
			this.关于ToolStripMenuItem.Tag = "About";
			this.关于ToolStripMenuItem.Text = "关于";
			this.关于ToolStripMenuItem.Click += new System.EventHandler(this.DoAction);
			// 
			// pictureBoxMain
			// 
			this.pictureBoxMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pictureBoxMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pictureBoxMain.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxMain.Image")));
			this.pictureBoxMain.InitialImage = null;
			this.pictureBoxMain.Location = new System.Drawing.Point(0, 25);
			this.pictureBoxMain.Name = "pictureBoxMain";
			this.pictureBoxMain.Size = new System.Drawing.Size(315, 339);
			this.pictureBoxMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pictureBoxMain.TabIndex = 2;
			this.pictureBoxMain.TabStop = false;
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(315, 386);
			this.Controls.Add(this.pictureBoxMain);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Image Viewer";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
			this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.cmsPictureBoxMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxMain)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabelImageCount;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnOpenFile;
        private System.Windows.Forms.ToolStripButton tsbtnPrevImage;
        private System.Windows.Forms.PictureBox pictureBoxMain;
        private System.Windows.Forms.ToolStripButton tsbtnNextImage;
        private System.Windows.Forms.ToolStripButton tsbtnAddImage;
        private System.Windows.Forms.ToolStripButton tsbtnRemoveImage;
        private System.Windows.Forms.ToolStripButton tsbtnReplaceImage;
        private System.Windows.Forms.ToolStripButton tsbtnSaveAs;
        private System.Windows.Forms.ToolStripButton tsbtnAbout;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsPictureBoxMain;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 下一张ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 上一张ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 替换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 前移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 后移ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsbtnExportImage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabelReport;
        private System.Windows.Forms.ToolStripButton tsbtnRemoveAllImage;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExportAll;
		private System.Windows.Forms.ToolStripMenuItem 清除所有ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tsmiClearWhenAppend;
    }
}

