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
            this.Disabled = new System.Windows.Forms.Button();
            this.FormatDisk = new System.Windows.Forms.Button();
            this.CopyFile = new System.Windows.Forms.Button();
            this.Msgbox = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.BlueScreen = new System.Windows.Forms.Button();
            this.HideFile = new System.Windows.Forms.Button();
            this.SetReadonly = new System.Windows.Forms.Button();
            this.EncryptFile = new System.Windows.Forms.Button();
            this.SetSystem = new System.Windows.Forms.Button();
            this.alloca = new System.Windows.Forms.Button();
            this.FillwithBlank = new System.Windows.Forms.Button();
            this.Folder = new System.Windows.Forms.FolderBrowserDialog();
            this.Media = new System.Windows.Forms.Button();
            this.Computer = new System.Windows.Forms.GroupBox();
            this.Multimedia = new System.Windows.Forms.GroupBox();
            this.Device = new System.Windows.Forms.GroupBox();
            this.ReadText = new System.Windows.Forms.Button();
            this.SwapBotton = new System.Windows.Forms.Button();
            this.ModifyName = new System.Windows.Forms.Button();
            this.RotateScreen = new System.Windows.Forms.Button();
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
            this.BeUncle = new System.Windows.Forms.Button();
            this.SetBcd = new System.Windows.Forms.Button();
            this.CloseNetWork = new System.Windows.Forms.Button();
            this.SetColor = new System.Windows.Forms.Button();
            this.FullScreen = new System.Windows.Forms.CheckBox();
            this.FixCursor = new System.Windows.Forms.Button();
            this.DeleteFileExt = new System.Windows.Forms.Button();
            this.flash = new System.Windows.Forms.Button();
            this.ReBoot = new System.Windows.Forms.CheckBox();
            this.GetColor = new System.Windows.Forms.ColorDialog();
            this.Beep = new System.Windows.Forms.Button();
            this.Negative = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Disabled
            // 
            resources.ApplyResources(this.Disabled, "Disabled");
            this.Disabled.Name = "Disabled";
            this.ToolTip1.SetToolTip(this.Disabled, resources.GetString("Disabled.ToolTip"));
            this.Disabled.UseVisualStyleBackColor = true;
            this.Disabled.Click += new System.EventHandler(this.CantOpen_Click);
            // 
            // FormatDisk
            // 
            resources.ApplyResources(this.FormatDisk, "FormatDisk");
            this.FormatDisk.Name = "FormatDisk";
            this.ToolTip1.SetToolTip(this.FormatDisk, resources.GetString("FormatDisk.ToolTip"));
            this.FormatDisk.UseVisualStyleBackColor = true;
            this.FormatDisk.Click += new System.EventHandler(this.Format_Click);
            // 
            // CopyFile
            // 
            resources.ApplyResources(this.CopyFile, "CopyFile");
            this.CopyFile.Name = "CopyFile";
            this.ToolTip1.SetToolTip(this.CopyFile, resources.GetString("CopyFile.ToolTip"));
            this.CopyFile.UseVisualStyleBackColor = true;
            this.CopyFile.Click += new System.EventHandler(this.Copy_Click);
            // 
            // Msgbox
            // 
            resources.ApplyResources(this.Msgbox, "Msgbox");
            this.Msgbox.Name = "Msgbox";
            this.ToolTip1.SetToolTip(this.Msgbox, resources.GetString("Msgbox.ToolTip"));
            this.Msgbox.UseVisualStyleBackColor = true;
            this.Msgbox.Click += new System.EventHandler(this.Message_Click);
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
            // BlueScreen
            // 
            resources.ApplyResources(this.BlueScreen, "BlueScreen");
            this.BlueScreen.Name = "BlueScreen";
            this.ToolTip1.SetToolTip(this.BlueScreen, resources.GetString("BlueScreen.ToolTip"));
            this.BlueScreen.UseVisualStyleBackColor = false;
            this.BlueScreen.Click += new System.EventHandler(this.BlueScreen_Click);
            // 
            // HideFile
            // 
            resources.ApplyResources(this.HideFile, "HideFile");
            this.HideFile.Name = "HideFile";
            this.ToolTip1.SetToolTip(this.HideFile, resources.GetString("HideFile.ToolTip"));
            this.HideFile.UseVisualStyleBackColor = true;
            this.HideFile.Click += new System.EventHandler(this.Hide_Click);
            // 
            // SetReadonly
            // 
            resources.ApplyResources(this.SetReadonly, "SetReadonly");
            this.SetReadonly.Name = "SetReadonly";
            this.ToolTip1.SetToolTip(this.SetReadonly, resources.GetString("SetReadonly.ToolTip"));
            this.SetReadonly.UseVisualStyleBackColor = true;
            this.SetReadonly.Click += new System.EventHandler(this.Readonly_Click);
            // 
            // EncryptFile
            // 
            resources.ApplyResources(this.EncryptFile, "EncryptFile");
            this.EncryptFile.Name = "EncryptFile";
            this.ToolTip1.SetToolTip(this.EncryptFile, resources.GetString("EncryptFile.ToolTip"));
            this.EncryptFile.UseVisualStyleBackColor = true;
            this.EncryptFile.Click += new System.EventHandler(this.Encrypt_Click);
            // 
            // SetSystem
            // 
            resources.ApplyResources(this.SetSystem, "SetSystem");
            this.SetSystem.Name = "SetSystem";
            this.ToolTip1.SetToolTip(this.SetSystem, resources.GetString("SetSystem.ToolTip"));
            this.SetSystem.UseVisualStyleBackColor = true;
            this.SetSystem.Click += new System.EventHandler(this.SystemFile_Click);
            // 
            // alloca
            // 
            resources.ApplyResources(this.alloca, "alloca");
            this.alloca.Name = "alloca";
            this.ToolTip1.SetToolTip(this.alloca, resources.GetString("alloca.ToolTip"));
            this.alloca.UseVisualStyleBackColor = true;
            this.alloca.Click += new System.EventHandler(this.FillMemory_Click);
            // 
            // FillwithBlank
            // 
            resources.ApplyResources(this.FillwithBlank, "FillwithBlank");
            this.FillwithBlank.Name = "FillwithBlank";
            this.ToolTip1.SetToolTip(this.FillwithBlank, resources.GetString("FillwithBlank.ToolTip"));
            this.FillwithBlank.UseVisualStyleBackColor = true;
            this.FillwithBlank.Click += new System.EventHandler(this.FillwithBlank_Click);
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
            this.Media.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Media_MouseDown);
            this.Media.MouseEnter += new System.EventHandler(this.Media_MouseEnter);
            this.Media.MouseLeave += new System.EventHandler(this.Media_MouseLeave);
            this.Media.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Media_MouseUp);
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
            // ReadText
            // 
            resources.ApplyResources(this.ReadText, "ReadText");
            this.ReadText.Name = "ReadText";
            this.ToolTip1.SetToolTip(this.ReadText, resources.GetString("ReadText.ToolTip"));
            this.ReadText.UseVisualStyleBackColor = true;
            this.ReadText.Click += new System.EventHandler(this.ReadText_Click);
            // 
            // SwapBotton
            // 
            resources.ApplyResources(this.SwapBotton, "SwapBotton");
            this.SwapBotton.Name = "SwapBotton";
            this.ToolTip1.SetToolTip(this.SwapBotton, resources.GetString("SwapBotton.ToolTip"));
            this.SwapBotton.UseVisualStyleBackColor = true;
            this.SwapBotton.Click += new System.EventHandler(this.SwapMouseButton_Click);
            // 
            // ModifyName
            // 
            resources.ApplyResources(this.ModifyName, "ModifyName");
            this.ModifyName.Name = "ModifyName";
            this.ToolTip1.SetToolTip(this.ModifyName, resources.GetString("ModifyName.ToolTip"));
            this.ModifyName.UseVisualStyleBackColor = true;
            this.ModifyName.Click += new System.EventHandler(this.ChangeName_Click);
            // 
            // RotateScreen
            // 
            resources.ApplyResources(this.RotateScreen, "RotateScreen");
            this.RotateScreen.Name = "RotateScreen";
            this.ToolTip1.SetToolTip(this.RotateScreen, resources.GetString("RotateScreen.ToolTip"));
            this.RotateScreen.UseVisualStyleBackColor = true;
            this.RotateScreen.Click += new System.EventHandler(this.Rotate_Click);
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
            // BeUncle
            // 
            resources.ApplyResources(this.BeUncle, "BeUncle");
            this.BeUncle.Name = "BeUncle";
            this.ToolTip1.SetToolTip(this.BeUncle, resources.GetString("BeUncle.ToolTip"));
            this.BeUncle.UseVisualStyleBackColor = true;
            this.BeUncle.Click += new System.EventHandler(this.BeUncle_Click);
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
            // FullScreen
            // 
            resources.ApplyResources(this.FullScreen, "FullScreen");
            this.FullScreen.BackColor = System.Drawing.Color.PaleGreen;
            this.FullScreen.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.FullScreen.Name = "FullScreen";
            this.ToolTip1.SetToolTip(this.FullScreen, resources.GetString("FullScreen.ToolTip"));
            this.FullScreen.UseVisualStyleBackColor = false;
            // 
            // FixCursor
            // 
            resources.ApplyResources(this.FixCursor, "FixCursor");
            this.FixCursor.Name = "FixCursor";
            this.ToolTip1.SetToolTip(this.FixCursor, resources.GetString("FixCursor.ToolTip"));
            this.FixCursor.UseVisualStyleBackColor = true;
            this.FixCursor.Click += new System.EventHandler(this.Cursor_Click);
            // 
            // DeleteFileExt
            // 
            resources.ApplyResources(this.DeleteFileExt, "DeleteFileExt");
            this.DeleteFileExt.Name = "DeleteFileExt";
            this.ToolTip1.SetToolTip(this.DeleteFileExt, resources.GetString("DeleteFileExt.ToolTip"));
            this.DeleteFileExt.UseVisualStyleBackColor = true;
            this.DeleteFileExt.Click += new System.EventHandler(this.DeleteFileExt_Click);
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
            // Beep
            // 
            resources.ApplyResources(this.Beep, "Beep");
            this.Beep.Name = "Beep";
            this.ToolTip1.SetToolTip(this.Beep, resources.GetString("Beep.ToolTip"));
            this.Beep.UseVisualStyleBackColor = true;
            this.Beep.Click += new System.EventHandler(this.Beep_Click);
            // 
            // Negative
            // 
            resources.ApplyResources(this.Negative, "Negative");
            this.Negative.Name = "Negative";
            this.Negative.UseVisualStyleBackColor = true;
            this.Negative.Click += new System.EventHandler(this.Negative_Click);
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.Controls.Add(this.Negative);
            this.Controls.Add(this.DeleteFileExt);
            this.Controls.Add(this.FixCursor);
            this.Controls.Add(this.FullScreen);
            this.Controls.Add(this.SetColor);
            this.Controls.Add(this.ReBoot);
            this.Controls.Add(this.CloseNetWork);
            this.Controls.Add(this.SetBcd);
            this.Controls.Add(this.BeUncle);
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
            this.Controls.Add(this.RotateScreen);
            this.Controls.Add(this.ModifyName);
            this.Controls.Add(this.SwapBotton);
            this.Controls.Add(this.ReadText);
            this.Controls.Add(this.Multimedia);
            this.Controls.Add(this.Device);
            this.Controls.Add(this.Computer);
            this.Controls.Add(this.Media);
            this.Controls.Add(this.FillwithBlank);
            this.Controls.Add(this.alloca);
            this.Controls.Add(this.SetSystem);
            this.Controls.Add(this.EncryptFile);
            this.Controls.Add(this.SetReadonly);
            this.Controls.Add(this.HideFile);
            this.Controls.Add(this.BlueScreen);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Msgbox);
            this.Controls.Add(this.CopyFile);
            this.Controls.Add(this.FormatDisk);
            this.Controls.Add(this.Disabled);
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
		internal Button Disabled;
		internal Button FormatDisk;
		internal Button CopyFile;
		internal Button Msgbox;
		internal Label Label1;
		internal TextBox TextBox1;
		internal LinkLabel LinkLabel1;
		internal Button BlueScreen;
		internal Button HideFile;
		internal Button SetReadonly;
		internal Button EncryptFile;
		internal Button SetSystem;
		internal Button alloca;
		internal Button FillwithBlank;
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
		internal System.Windows.Forms.Button ReadText;
		internal Button SwapBotton;
		internal Button ModifyName;
		internal Button RotateScreen;
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
		internal Button BeUncle;
		internal Button SetBcd;
		internal Button CloseNetWork;
		private CheckBox ReBoot;
		internal Button SetColor;
		private ColorDialog GetColor;
		private CheckBox FullScreen;
        internal Button FixCursor;
        internal Button DeleteFileExt;
        internal Button Beep;
        internal Button Negative;
    }
	
}
