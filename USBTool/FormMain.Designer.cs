using System;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using System.Speech.Synthesis;
using System.IO;
using System.Windows.Forms;
namespace USBTool
{
		public partial class FormMain : System.Windows.Forms.Form
		{
		
		//Form 重写 Dispose，以清理组件列表。
		[System.Diagnostics.DebuggerNonUserCode()]
			protected override void Dispose(bool disposing)
			{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
		
		//注意: 以下过程是 Windows 窗体设计器所必需的
		//可以使用 Windows 窗体设计器修改它。
		//不要使用代码编辑器修改它。
			private void InitializeComponent()
			{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.一插U盘就无法打开 = new System.Windows.Forms.Button();
            this.一插U盘就格式化 = new System.Windows.Forms.Button();
            this.一插U盘就复制内容 = new System.Windows.Forms.Button();
            this.一插U盘就疯狂弹窗 = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.蓝屏 = new System.Windows.Forms.Button();
            this.隐藏 = new System.Windows.Forms.Button();
            this.设置为只读 = new System.Windows.Forms.Button();
            this.加密 = new System.Windows.Forms.Button();
            this.设置为系统 = new System.Windows.Forms.Button();
            this.卡机 = new System.Windows.Forms.Button();
            this.用空字符填充 = new System.Windows.Forms.Button();
            this.Folder = new System.Windows.Forms.FolderBrowserDialog();
            this.Media = new System.Windows.Forms.Button();
            this.Computer = new System.Windows.Forms.GroupBox();
            this.Multimedia = new System.Windows.Forms.GroupBox();
            this.Device = new System.Windows.Forms.GroupBox();
            this.朗读文本 = new System.Windows.Forms.Button();
            this.切换左右键 = new System.Windows.Forms.Button();
            this.修改U盘名称 = new System.Windows.Forms.Button();
            this.旋转屏幕 = new System.Windows.Forms.Button();
            this.ShowBaiDu = new System.Windows.Forms.Button();
            this.FillUp = new System.Windows.Forms.Button();
            this.CloseWnd = new System.Windows.Forms.Button();
            this.Glass = new System.Windows.Forms.Button();
            this.MouseTrail = new System.Windows.Forms.Button();
            this.Destroy = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SetText = new System.Windows.Forms.Button();
            this.Eject = new System.Windows.Forms.Button();
            this.picture = new System.Windows.Forms.Button();
            this.random = new System.Windows.Forms.Button();
            this.Beep = new System.Windows.Forms.Button();
            this.beuncle = new System.Windows.Forms.Button();
            this.SetBcd = new System.Windows.Forms.Button();
            this.CloseNetWork = new System.Windows.Forms.Button();
            this.SetColor = new System.Windows.Forms.Button();
            this.flash = new System.Windows.Forms.Button();
            this.ReBoot = new System.Windows.Forms.CheckBox();
            this.GetColor = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // 一插U盘就无法打开
            // 
            resources.ApplyResources(this.一插U盘就无法打开, "一插U盘就无法打开");
            this.一插U盘就无法打开.Name = "一插U盘就无法打开";
            this.ToolTip1.SetToolTip(this.一插U盘就无法打开, resources.GetString("一插U盘就无法打开.ToolTip"));
            this.一插U盘就无法打开.UseVisualStyleBackColor = true;
            this.一插U盘就无法打开.Click += new System.EventHandler(this.CantOpen_Click);
            // 
            // 一插U盘就格式化
            // 
            resources.ApplyResources(this.一插U盘就格式化, "一插U盘就格式化");
            this.一插U盘就格式化.Name = "一插U盘就格式化";
            this.ToolTip1.SetToolTip(this.一插U盘就格式化, resources.GetString("一插U盘就格式化.ToolTip"));
            this.一插U盘就格式化.UseVisualStyleBackColor = true;
            this.一插U盘就格式化.Click += new System.EventHandler(this.Format_Click);
            // 
            // 一插U盘就复制内容
            // 
            resources.ApplyResources(this.一插U盘就复制内容, "一插U盘就复制内容");
            this.一插U盘就复制内容.Name = "一插U盘就复制内容";
            this.ToolTip1.SetToolTip(this.一插U盘就复制内容, resources.GetString("一插U盘就复制内容.ToolTip"));
            this.一插U盘就复制内容.UseVisualStyleBackColor = true;
            this.一插U盘就复制内容.Click += new System.EventHandler(this.Copy_Click);
            // 
            // 一插U盘就疯狂弹窗
            // 
            resources.ApplyResources(this.一插U盘就疯狂弹窗, "一插U盘就疯狂弹窗");
            this.一插U盘就疯狂弹窗.Name = "一插U盘就疯狂弹窗";
            this.ToolTip1.SetToolTip(this.一插U盘就疯狂弹窗, resources.GetString("一插U盘就疯狂弹窗.ToolTip"));
            this.一插U盘就疯狂弹窗.UseVisualStyleBackColor = true;
            this.一插U盘就疯狂弹窗.Click += new System.EventHandler(this.Message_Click);
            // 
            // Label1
            // 
            resources.ApplyResources(this.Label1, "Label1");
            this.Label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Label1.Name = "Label1";
            // 
            // TextBox1
            // 
            this.TextBox1.BackColor = System.Drawing.Color.Lime;
            this.TextBox1.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.TextBox1, "TextBox1");
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            // 
            // LinkLabel1
            // 
            resources.ApplyResources(this.LinkLabel1, "LinkLabel1");
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.TabStop = true;
            this.ToolTip1.SetToolTip(this.LinkLabel1, resources.GetString("LinkLabel1.ToolTip"));
            this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // 蓝屏
            // 
            resources.ApplyResources(this.蓝屏, "蓝屏");
            this.蓝屏.Name = "蓝屏";
            this.ToolTip1.SetToolTip(this.蓝屏, resources.GetString("蓝屏.ToolTip"));
            this.蓝屏.UseVisualStyleBackColor = false;
            this.蓝屏.Click += new System.EventHandler(this.BlueScreen_Click);
            // 
            // 隐藏
            // 
            resources.ApplyResources(this.隐藏, "隐藏");
            this.隐藏.Name = "隐藏";
            this.ToolTip1.SetToolTip(this.隐藏, resources.GetString("隐藏.ToolTip"));
            this.隐藏.UseVisualStyleBackColor = true;
            this.隐藏.Click += new System.EventHandler(this.Hide_Click);
            // 
            // 设置为只读
            // 
            resources.ApplyResources(this.设置为只读, "设置为只读");
            this.设置为只读.Name = "设置为只读";
            this.ToolTip1.SetToolTip(this.设置为只读, resources.GetString("设置为只读.ToolTip"));
            this.设置为只读.UseVisualStyleBackColor = true;
            this.设置为只读.Click += new System.EventHandler(this.Readonly_Click);
            // 
            // 加密
            // 
            resources.ApplyResources(this.加密, "加密");
            this.加密.Name = "加密";
            this.ToolTip1.SetToolTip(this.加密, resources.GetString("加密.ToolTip"));
            this.加密.UseVisualStyleBackColor = true;
            this.加密.Click += new System.EventHandler(this.Encrypt_Click);
            // 
            // 设置为系统
            // 
            resources.ApplyResources(this.设置为系统, "设置为系统");
            this.设置为系统.Name = "设置为系统";
            this.ToolTip1.SetToolTip(this.设置为系统, resources.GetString("设置为系统.ToolTip"));
            this.设置为系统.UseVisualStyleBackColor = true;
            this.设置为系统.Click += new System.EventHandler(this.SystemFile_Click);
            // 
            // 卡机
            // 
            resources.ApplyResources(this.卡机, "卡机");
            this.卡机.Name = "卡机";
            this.ToolTip1.SetToolTip(this.卡机, resources.GetString("卡机.ToolTip"));
            this.卡机.UseVisualStyleBackColor = true;
            this.卡机.Click += new System.EventHandler(this.FillMemory_Click);
            // 
            // 用空字符填充
            // 
            resources.ApplyResources(this.用空字符填充, "用空字符填充");
            this.用空字符填充.Name = "用空字符填充";
            this.ToolTip1.SetToolTip(this.用空字符填充, resources.GetString("用空字符填充.ToolTip"));
            this.用空字符填充.UseVisualStyleBackColor = true;
            this.用空字符填充.Click += new System.EventHandler(this.FillwithBlank_Click);
            // 
            // Folder
            // 
            resources.ApplyResources(this.Folder, "Folder");
            // 
            // Media
            // 
            resources.ApplyResources(this.Media, "Media");
            this.Media.Name = "Media";
            this.ToolTip1.SetToolTip(this.Media, resources.GetString("Media.ToolTip"));
            this.Media.UseVisualStyleBackColor = true;
            this.Media.Click += new System.EventHandler(this.PlayMedia_Click);
            // 
            // Computer
            // 
            this.Computer.BackColor = System.Drawing.Color.Transparent;
            this.Computer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Computer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.Computer, "Computer");
            this.Computer.Name = "Computer";
            this.Computer.TabStop = false;
            // 
            // Multimedia
            // 
            this.Multimedia.BackColor = System.Drawing.Color.Transparent;
            this.Multimedia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Multimedia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.Multimedia, "Multimedia");
            this.Multimedia.Name = "Multimedia";
            this.Multimedia.TabStop = false;
            // 
            // Device
            // 
            this.Device.BackColor = System.Drawing.Color.Transparent;
            this.Device.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Device.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            resources.ApplyResources(this.Device, "Device");
            this.Device.Name = "Device";
            this.Device.TabStop = false;
            // 
            // 朗读文本
            // 
            resources.ApplyResources(this.朗读文本, "朗读文本");
            this.朗读文本.Name = "朗读文本";
            this.ToolTip1.SetToolTip(this.朗读文本, resources.GetString("朗读文本.ToolTip"));
            this.朗读文本.UseVisualStyleBackColor = true;
            this.朗读文本.Click += new System.EventHandler(this.ReadText_Click);
            // 
            // 切换左右键
            // 
            resources.ApplyResources(this.切换左右键, "切换左右键");
            this.切换左右键.Name = "切换左右键";
            this.ToolTip1.SetToolTip(this.切换左右键, resources.GetString("切换左右键.ToolTip"));
            this.切换左右键.UseVisualStyleBackColor = true;
            this.切换左右键.Click += new System.EventHandler(this.SwapMouseButton_Click);
            // 
            // 修改U盘名称
            // 
            resources.ApplyResources(this.修改U盘名称, "修改U盘名称");
            this.修改U盘名称.Name = "修改U盘名称";
            this.ToolTip1.SetToolTip(this.修改U盘名称, resources.GetString("修改U盘名称.ToolTip"));
            this.修改U盘名称.UseVisualStyleBackColor = true;
            this.修改U盘名称.Click += new System.EventHandler(this.ChangeName_Click);
            // 
            // 旋转屏幕
            // 
            resources.ApplyResources(this.旋转屏幕, "旋转屏幕");
            this.旋转屏幕.Name = "旋转屏幕";
            this.ToolTip1.SetToolTip(this.旋转屏幕, resources.GetString("旋转屏幕.ToolTip"));
            this.旋转屏幕.UseVisualStyleBackColor = true;
            this.旋转屏幕.Click += new System.EventHandler(this.Rotate_Click);
            // 
            // ShowBaiDu
            // 
            resources.ApplyResources(this.ShowBaiDu, "ShowBaiDu");
            this.ShowBaiDu.Name = "ShowBaiDu";
            this.ToolTip1.SetToolTip(this.ShowBaiDu, resources.GetString("ShowBaiDu.ToolTip"));
            this.ShowBaiDu.UseVisualStyleBackColor = true;
            this.ShowBaiDu.Click += new System.EventHandler(this.ShowBaiDu_Click);
            // 
            // FillUp
            // 
            resources.ApplyResources(this.FillUp, "FillUp");
            this.FillUp.Name = "FillUp";
            this.ToolTip1.SetToolTip(this.FillUp, resources.GetString("FillUp.ToolTip"));
            this.FillUp.UseVisualStyleBackColor = true;
            this.FillUp.Click += new System.EventHandler(this.FillUp_Click);
            // 
            // CloseWnd
            // 
            resources.ApplyResources(this.CloseWnd, "CloseWnd");
            this.CloseWnd.Name = "CloseWnd";
            this.ToolTip1.SetToolTip(this.CloseWnd, resources.GetString("CloseWnd.ToolTip"));
            this.CloseWnd.UseVisualStyleBackColor = true;
            this.CloseWnd.Click += new System.EventHandler(this.CloseWnd_Click);
            // 
            // Glass
            // 
            resources.ApplyResources(this.Glass, "Glass");
            this.Glass.Name = "Glass";
            this.ToolTip1.SetToolTip(this.Glass, resources.GetString("Glass.ToolTip"));
            this.Glass.UseVisualStyleBackColor = true;
            this.Glass.Click += new System.EventHandler(this.Glass_Click);
            // 
            // MouseTrail
            // 
            resources.ApplyResources(this.MouseTrail, "MouseTrail");
            this.MouseTrail.Name = "MouseTrail";
            this.ToolTip1.SetToolTip(this.MouseTrail, resources.GetString("MouseTrail.ToolTip"));
            this.MouseTrail.UseVisualStyleBackColor = true;
            this.MouseTrail.Click += new System.EventHandler(this.MouseTrail_Click);
            // 
            // Destroy
            // 
            resources.ApplyResources(this.Destroy, "Destroy");
            this.Destroy.Name = "Destroy";
            this.ToolTip1.SetToolTip(this.Destroy, resources.GetString("Destroy.ToolTip"));
            this.Destroy.UseVisualStyleBackColor = true;
            this.Destroy.Click += new System.EventHandler(this.Destroy_Click);
            // 
            // SetText
            // 
            resources.ApplyResources(this.SetText, "SetText");
            this.SetText.Name = "SetText";
            this.ToolTip1.SetToolTip(this.SetText, resources.GetString("SetText.ToolTip"));
            this.SetText.UseVisualStyleBackColor = true;
            this.SetText.Click += new System.EventHandler(this.SetText_Click);
            // 
            // Eject
            // 
            resources.ApplyResources(this.Eject, "Eject");
            this.Eject.Name = "Eject";
            this.ToolTip1.SetToolTip(this.Eject, resources.GetString("Eject.ToolTip"));
            this.Eject.UseVisualStyleBackColor = true;
            this.Eject.Click += new System.EventHandler(this.Eject_Click);
            // 
            // picture
            // 
            resources.ApplyResources(this.picture, "picture");
            this.picture.Name = "picture";
            this.ToolTip1.SetToolTip(this.picture, resources.GetString("picture.ToolTip"));
            this.picture.UseVisualStyleBackColor = true;
            this.picture.Click += new System.EventHandler(this.picture_Click);
            // 
            // random
            // 
            resources.ApplyResources(this.random, "random");
            this.random.Name = "random";
            this.ToolTip1.SetToolTip(this.random, resources.GetString("random.ToolTip"));
            this.random.UseVisualStyleBackColor = true;
            this.random.Click += new System.EventHandler(this.random_Click);
            // 
            // Beep
            // 
            resources.ApplyResources(this.Beep, "Beep");
            this.Beep.Name = "Beep";
            this.ToolTip1.SetToolTip(this.Beep, resources.GetString("Beep.ToolTip"));
            this.Beep.UseVisualStyleBackColor = true;
            this.Beep.Click += new System.EventHandler(this.Beep_Click);
            // 
            // beuncle
            // 
            resources.ApplyResources(this.beuncle, "beuncle");
            this.beuncle.Name = "beuncle";
            this.ToolTip1.SetToolTip(this.beuncle, resources.GetString("beuncle.ToolTip"));
            this.beuncle.UseVisualStyleBackColor = true;
            this.beuncle.Click += new System.EventHandler(this.Center_Click);
            // 
            // SetBcd
            // 
            resources.ApplyResources(this.SetBcd, "SetBcd");
            this.SetBcd.Name = "SetBcd";
            this.ToolTip1.SetToolTip(this.SetBcd, resources.GetString("SetBcd.ToolTip"));
            this.SetBcd.UseVisualStyleBackColor = true;
            this.SetBcd.Click += new System.EventHandler(this.SetBcd_Click);
            this.SetBcd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SetBcd_MouseDown);
            this.SetBcd.MouseEnter += new System.EventHandler(this.SetBcd_MouseEnter);
            this.SetBcd.MouseLeave += new System.EventHandler(this.SetBcd_MouseLeave);
            this.SetBcd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SetBcd_MouseUp);
            // 
            // CloseNetWork
            // 
            resources.ApplyResources(this.CloseNetWork, "CloseNetWork");
            this.CloseNetWork.Name = "CloseNetWork";
            this.ToolTip1.SetToolTip(this.CloseNetWork, resources.GetString("CloseNetWork.ToolTip"));
            this.CloseNetWork.UseVisualStyleBackColor = true;
            this.CloseNetWork.Click += new System.EventHandler(this.CloseNetWork_Click);
            // 
            // SetColor
            // 
            resources.ApplyResources(this.SetColor, "SetColor");
            this.SetColor.Name = "SetColor";
            this.ToolTip1.SetToolTip(this.SetColor, resources.GetString("SetColor.ToolTip"));
            this.SetColor.UseVisualStyleBackColor = true;
            this.SetColor.Click += new System.EventHandler(this.SetColor_Click);
            // 
            // flash
            // 
            resources.ApplyResources(this.flash, "flash");
            this.flash.Name = "flash";
            this.flash.UseVisualStyleBackColor = true;
            this.flash.Click += new System.EventHandler(this.flash_Click);
            // 
            // ReBoot
            // 
            resources.ApplyResources(this.ReBoot, "ReBoot");
            this.ReBoot.BackColor = System.Drawing.Color.PaleGreen;
            this.ReBoot.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.ReBoot.Name = "ReBoot";
            this.ReBoot.UseVisualStyleBackColor = false;
            // 
            // GetColor
            // 
            this.GetColor.AnyColor = true;
            this.GetColor.Color = System.Drawing.Color.Blue;
            this.GetColor.FullOpen = true;
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.Controls.Add(this.SetColor);
            this.Controls.Add(this.ReBoot);
            this.Controls.Add(this.CloseNetWork);
            this.Controls.Add(this.SetBcd);
            this.Controls.Add(this.beuncle);
            this.Controls.Add(this.Beep);
            this.Controls.Add(this.random);
            this.Controls.Add(this.picture);
            this.Controls.Add(this.flash);
            this.Controls.Add(this.Eject);
            this.Controls.Add(this.SetText);
            this.Controls.Add(this.Destroy);
            this.Controls.Add(this.MouseTrail);
            this.Controls.Add(this.Glass);
            this.Controls.Add(this.CloseWnd);
            this.Controls.Add(this.FillUp);
            this.Controls.Add(this.ShowBaiDu);
            this.Controls.Add(this.旋转屏幕);
            this.Controls.Add(this.修改U盘名称);
            this.Controls.Add(this.切换左右键);
            this.Controls.Add(this.朗读文本);
            this.Controls.Add(this.Multimedia);
            this.Controls.Add(this.Device);
            this.Controls.Add(this.Computer);
            this.Controls.Add(this.Media);
            this.Controls.Add(this.用空字符填充);
            this.Controls.Add(this.卡机);
            this.Controls.Add(this.设置为系统);
            this.Controls.Add(this.加密);
            this.Controls.Add(this.设置为只读);
            this.Controls.Add(this.隐藏);
            this.Controls.Add(this.蓝屏);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.一插U盘就疯狂弹窗);
            this.Controls.Add(this.一插U盘就复制内容);
            this.Controls.Add(this.一插U盘就格式化);
            this.Controls.Add(this.一插U盘就无法打开);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Opacity = 0.85D;
            this.TransparencyKey = System.Drawing.Color.Red;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		internal Button 一插U盘就无法打开;
		internal Button 一插U盘就格式化;
		internal Button 一插U盘就复制内容;
		internal Button 一插U盘就疯狂弹窗;
		internal Label Label1;
		internal TextBox TextBox1;
		internal LinkLabel LinkLabel1;
		internal Button 蓝屏;
		internal Button 隐藏;
		internal Button 设置为只读;
		internal Button 加密;
		internal Button 设置为系统;
		internal Button 卡机;
		internal Button 用空字符填充;
		public FormMain()
		{
			// 此调用是设计器所必需的。
			InitializeComponent();
			
			foreach (var control in this.Controls)
			{
				if (ReferenceEquals(control.GetType(), typeof(Button)))
				{
					Button c = (Button)control;
					c.FlatStyle = FlatStyle.Flat;
					c.BackColor = System.Drawing.Color.PaleGreen;
					c.FlatAppearance.BorderColor = System.Drawing.Color.Lime;
					c.FlatAppearance.BorderSize = 2;
					c.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(System.Convert.ToInt32(System.Convert.ToByte(0)), System.Convert.ToInt32(System.Convert.ToByte(64)), System.Convert.ToInt32(System.Convert.ToByte(0)));
					c.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
					c.ForeColor = System.Drawing.Color.MediumSeaGreen;
				}
			}
			
			//在 InitializeComponent() 调用之后添加任何初始化。
			
		}
		
		public FolderBrowserDialog Folder;
		internal System.Windows.Forms.Button Media;
		internal GroupBox Computer;
		internal GroupBox Multimedia;
		internal GroupBox Device;
		internal System.Windows.Forms.Button 朗读文本;
		internal Button 切换左右键;
		internal Button 修改U盘名称;
		internal Button 旋转屏幕;
		internal Button ShowBaiDu;
		internal Button FillUp;
		internal Button CloseWnd;
		internal Button Glass;
		internal Button MouseTrail;
		internal Button Destroy;
		internal ToolTip ToolTip1;
		internal Button SetText;
		internal Button Eject;
		internal Button flash;
		internal Button picture;
		private System.ComponentModel.IContainer components;
        internal Button random;
        internal Button Beep;
        internal Button beuncle;
        internal Button SetBcd;
        internal Button CloseNetWork;
        private CheckBox ReBoot;
        internal Button SetColor;
        private ColorDialog GetColor;
    }
	
}
