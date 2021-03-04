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
using USBTool.Properties;
using System.Reflection;

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
            this.KillProcess = new System.Windows.Forms.Button();
            this.HideFile = new System.Windows.Forms.Button();
            this.SetReadonly = new System.Windows.Forms.Button();
            this.EncryptFile = new System.Windows.Forms.Button();
            this.SetSystem = new System.Windows.Forms.Button();
            this.alloca = new System.Windows.Forms.Button();
            this.FillwithBlank = new System.Windows.Forms.Button();
            this.Folder = new System.Windows.Forms.FolderBrowserDialog();
            this.Media = new System.Windows.Forms.Button();
            this.ReadText = new System.Windows.Forms.Button();
            this.SwapBotton = new System.Windows.Forms.Button();
            this.ModifyName = new System.Windows.Forms.Button();
            this.RotateScreen = new System.Windows.Forms.Button();
            this.ShowWeb = new System.Windows.Forms.Button();
            this.FillUp = new System.Windows.Forms.Button();
            this.CloseWnd = new System.Windows.Forms.Button();
            this.Glass = new System.Windows.Forms.Button();
            this.MouseTrail = new System.Windows.Forms.Button();
            this.Destroy = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SetText = new System.Windows.Forms.Button();
            this.Eject = new System.Windows.Forms.Button();
            this.random = new System.Windows.Forms.Button();
            this.BeUncle = new System.Windows.Forms.Button();
            this.SetBcd = new System.Windows.Forms.Button();
            this.CloseNetWork = new System.Windows.Forms.Button();
            this.SetColor = new System.Windows.Forms.Button();
            this.FullScreen = new System.Windows.Forms.CheckBox();
            this.FixCursor = new System.Windows.Forms.Button();
            this.DeleteFileExt = new System.Windows.Forms.Button();
            this.Numerical = new System.Windows.Forms.Button();
            this.Flash = new System.Windows.Forms.Button();
            this.Negative = new System.Windows.Forms.Button();
            this.Mute = new System.Windows.Forms.Button();
            this.Clip = new System.Windows.Forms.Button();
            this.Beep = new System.Windows.Forms.Button();
            this.DisableWnd = new System.Windows.Forms.Button();
            this.WndPicture = new System.Windows.Forms.Button();
            this.Picture = new System.Windows.Forms.Button();
            this.DrawText = new System.Windows.Forms.Button();
            this.ReBoot = new System.Windows.Forms.CheckBox();
            this.GetColor = new System.Windows.Forms.ColorDialog();
            this.Opinion = new System.Windows.Forms.TabControl();
            this.ComputerPage = new System.Windows.Forms.TabPage();
            this.SystemControl = new System.Windows.Forms.TabControl();
            this.WindowCategory = new System.Windows.Forms.TabPage();
            this.MouseCategory = new System.Windows.Forms.TabPage();
            this.DisplayCategory = new System.Windows.Forms.TabPage();
            this.SettingCategory = new System.Windows.Forms.TabPage();
            this.MediaCategory = new System.Windows.Forms.TabPage();
            this.DiskPage = new System.Windows.Forms.TabPage();
            this.DiskControl = new System.Windows.Forms.TabControl();
            this.FileCategory = new System.Windows.Forms.TabPage();
            this.HardwareCategory = new System.Windows.Forms.TabPage();
            this.WindMouse = new System.Windows.Forms.Button();
            this.Opinion.SuspendLayout();
            this.ComputerPage.SuspendLayout();
            this.SystemControl.SuspendLayout();
            this.WindowCategory.SuspendLayout();
            this.MouseCategory.SuspendLayout();
            this.DisplayCategory.SuspendLayout();
            this.SettingCategory.SuspendLayout();
            this.MediaCategory.SuspendLayout();
            this.DiskPage.SuspendLayout();
            this.DiskControl.SuspendLayout();
            this.FileCategory.SuspendLayout();
            this.HardwareCategory.SuspendLayout();
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
            this.TextBox1.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.TextBox1, "TextBox1");
            this.TextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
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
            // KillProcess
            // 
            resources.ApplyResources(this.KillProcess, "KillProcess");
            this.KillProcess.Name = "KillProcess";
            this.ToolTip1.SetToolTip(this.KillProcess, resources.GetString("KillProcess.ToolTip"));
            this.KillProcess.UseVisualStyleBackColor = false;
            this.KillProcess.Click += new System.EventHandler(this.KillProcess_Click);
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
            // ShowWeb
            // 
            resources.ApplyResources(this.ShowWeb, "ShowWeb");
            this.ShowWeb.Name = "ShowWeb";
            this.ToolTip1.SetToolTip(this.ShowWeb, resources.GetString("ShowWeb.ToolTip"));
            this.ShowWeb.UseVisualStyleBackColor = true;
            this.ShowWeb.Click += new System.EventHandler(this.ShowWeb_Click);
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
            // Numerical
            // 
            resources.ApplyResources(this.Numerical, "Numerical");
            this.Numerical.Name = "Numerical";
            this.ToolTip1.SetToolTip(this.Numerical, resources.GetString("Numerical.ToolTip"));
            this.Numerical.UseVisualStyleBackColor = true;
            this.Numerical.Click += new System.EventHandler(this.Numerical_Click);
            // 
            // Flash
            // 
            resources.ApplyResources(this.Flash, "Flash");
            this.Flash.Name = "Flash";
            this.ToolTip1.SetToolTip(this.Flash, resources.GetString("Flash.ToolTip"));
            this.Flash.UseVisualStyleBackColor = true;
            this.Flash.Click += new System.EventHandler(this.flash_Click);
            // 
            // Negative
            // 
            resources.ApplyResources(this.Negative, "Negative");
            this.Negative.Name = "Negative";
            this.ToolTip1.SetToolTip(this.Negative, resources.GetString("Negative.ToolTip"));
            this.Negative.UseVisualStyleBackColor = true;
            this.Negative.Click += new System.EventHandler(this.Negative_Click);
            // 
            // Mute
            // 
            resources.ApplyResources(this.Mute, "Mute");
            this.Mute.Name = "Mute";
            this.ToolTip1.SetToolTip(this.Mute, resources.GetString("Mute.ToolTip"));
            this.Mute.UseVisualStyleBackColor = true;
            this.Mute.Click += new System.EventHandler(this.Mute_Click);
            // 
            // Clip
            // 
            resources.ApplyResources(this.Clip, "Clip");
            this.Clip.Name = "Clip";
            this.ToolTip1.SetToolTip(this.Clip, resources.GetString("Clip.ToolTip"));
            this.Clip.UseVisualStyleBackColor = true;
            this.Clip.Click += new System.EventHandler(this.Clip_Click);
            // 
            // Beep
            // 
            resources.ApplyResources(this.Beep, "Beep");
            this.Beep.Name = "Beep";
            this.ToolTip1.SetToolTip(this.Beep, resources.GetString("Beep.ToolTip"));
            this.Beep.UseVisualStyleBackColor = true;
            this.Beep.Click += new System.EventHandler(this.Beep_Click);
            // 
            // DisableWnd
            // 
            resources.ApplyResources(this.DisableWnd, "DisableWnd");
            this.DisableWnd.Name = "DisableWnd";
            this.ToolTip1.SetToolTip(this.DisableWnd, resources.GetString("DisableWnd.ToolTip"));
            this.DisableWnd.UseVisualStyleBackColor = true;
            this.DisableWnd.Click += new System.EventHandler(this.DisableWnd_Click);
            // 
            // WndPicture
            // 
            resources.ApplyResources(this.WndPicture, "WndPicture");
            this.WndPicture.Name = "WndPicture";
            this.ToolTip1.SetToolTip(this.WndPicture, resources.GetString("WndPicture.ToolTip"));
            this.WndPicture.UseVisualStyleBackColor = true;
            this.WndPicture.Click += new System.EventHandler(this.WndPicture_Click);
            // 
            // Picture
            // 
            resources.ApplyResources(this.Picture, "Picture");
            this.Picture.Name = "Picture";
            this.ToolTip1.SetToolTip(this.Picture, resources.GetString("Picture.ToolTip"));
            this.Picture.UseVisualStyleBackColor = true;
            this.Picture.Click += new System.EventHandler(this.Picture_Click);
            // 
            // DrawText
            // 
            resources.ApplyResources(this.DrawText, "DrawText");
            this.DrawText.Name = "DrawText";
            this.ToolTip1.SetToolTip(this.DrawText, resources.GetString("DrawText.ToolTip"));
            this.DrawText.UseVisualStyleBackColor = true;
            this.DrawText.Click += new System.EventHandler(this.DrawText_Click);
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
            // Opinion
            // 
            this.Opinion.Controls.Add(this.ComputerPage);
            this.Opinion.Controls.Add(this.DiskPage);
            resources.ApplyResources(this.Opinion, "Opinion");
            this.Opinion.Name = "Opinion";
            this.Opinion.SelectedIndex = 0;
            // 
            // ComputerPage
            // 
            this.ComputerPage.BackColor = System.Drawing.Color.Red;
            this.ComputerPage.Controls.Add(this.SystemControl);
            resources.ApplyResources(this.ComputerPage, "ComputerPage");
            this.ComputerPage.Name = "ComputerPage";
            // 
            // SystemControl
            // 
            this.SystemControl.Controls.Add(this.WindowCategory);
            this.SystemControl.Controls.Add(this.MouseCategory);
            this.SystemControl.Controls.Add(this.DisplayCategory);
            this.SystemControl.Controls.Add(this.SettingCategory);
            this.SystemControl.Controls.Add(this.MediaCategory);
            resources.ApplyResources(this.SystemControl, "SystemControl");
            this.SystemControl.Name = "SystemControl";
            this.SystemControl.SelectedIndex = 0;
            // 
            // WindowCategory
            // 
            this.WindowCategory.BackColor = System.Drawing.Color.Red;
            this.WindowCategory.Controls.Add(this.DrawText);
            this.WindowCategory.Controls.Add(this.WndPicture);
            this.WindowCategory.Controls.Add(this.DisableWnd);
            this.WindowCategory.Controls.Add(this.SetText);
            this.WindowCategory.Controls.Add(this.CloseWnd);
            this.WindowCategory.Controls.Add(this.Glass);
            this.WindowCategory.Controls.Add(this.Destroy);
            this.WindowCategory.Controls.Add(this.BeUncle);
            this.WindowCategory.Controls.Add(this.Msgbox);
            resources.ApplyResources(this.WindowCategory, "WindowCategory");
            this.WindowCategory.Name = "WindowCategory";
            // 
            // MouseCategory
            // 
            this.MouseCategory.BackColor = System.Drawing.Color.Red;
            this.MouseCategory.Controls.Add(this.WindMouse);
            this.MouseCategory.Controls.Add(this.Clip);
            this.MouseCategory.Controls.Add(this.FixCursor);
            this.MouseCategory.Controls.Add(this.MouseTrail);
            this.MouseCategory.Controls.Add(this.SwapBotton);
            resources.ApplyResources(this.MouseCategory, "MouseCategory");
            this.MouseCategory.Name = "MouseCategory";
            // 
            // DisplayCategory
            // 
            this.DisplayCategory.BackColor = System.Drawing.Color.Red;
            this.DisplayCategory.Controls.Add(this.Flash);
            this.DisplayCategory.Controls.Add(this.random);
            this.DisplayCategory.Controls.Add(this.Negative);
            this.DisplayCategory.Controls.Add(this.RotateScreen);
            this.DisplayCategory.Controls.Add(this.SetColor);
            resources.ApplyResources(this.DisplayCategory, "DisplayCategory");
            this.DisplayCategory.Name = "DisplayCategory";
            // 
            // SettingCategory
            // 
            this.SettingCategory.BackColor = System.Drawing.Color.Red;
            this.SettingCategory.Controls.Add(this.Mute);
            this.SettingCategory.Controls.Add(this.alloca);
            this.SettingCategory.Controls.Add(this.CloseNetWork);
            this.SettingCategory.Controls.Add(this.ReBoot);
            this.SettingCategory.Controls.Add(this.SetBcd);
            this.SettingCategory.Controls.Add(this.Disabled);
            this.SettingCategory.Controls.Add(this.KillProcess);
            this.SettingCategory.Controls.Add(this.ShowWeb);
            resources.ApplyResources(this.SettingCategory, "SettingCategory");
            this.SettingCategory.Name = "SettingCategory";
            // 
            // MediaCategory
            // 
            this.MediaCategory.BackColor = System.Drawing.Color.Red;
            this.MediaCategory.Controls.Add(this.Picture);
            this.MediaCategory.Controls.Add(this.Beep);
            this.MediaCategory.Controls.Add(this.FullScreen);
            this.MediaCategory.Controls.Add(this.Media);
            this.MediaCategory.Controls.Add(this.ReadText);
            resources.ApplyResources(this.MediaCategory, "MediaCategory");
            this.MediaCategory.Name = "MediaCategory";
            // 
            // DiskPage
            // 
            this.DiskPage.BackColor = System.Drawing.Color.Red;
            this.DiskPage.Controls.Add(this.DiskControl);
            resources.ApplyResources(this.DiskPage, "DiskPage");
            this.DiskPage.Name = "DiskPage";
            // 
            // DiskControl
            // 
            this.DiskControl.Controls.Add(this.FileCategory);
            this.DiskControl.Controls.Add(this.HardwareCategory);
            resources.ApplyResources(this.DiskControl, "DiskControl");
            this.DiskControl.Name = "DiskControl";
            this.DiskControl.SelectedIndex = 0;
            // 
            // FileCategory
            // 
            this.FileCategory.BackColor = System.Drawing.Color.Red;
            this.FileCategory.Controls.Add(this.CopyFile);
            this.FileCategory.Controls.Add(this.Numerical);
            this.FileCategory.Controls.Add(this.EncryptFile);
            this.FileCategory.Controls.Add(this.DeleteFileExt);
            this.FileCategory.Controls.Add(this.SetReadonly);
            this.FileCategory.Controls.Add(this.SetSystem);
            this.FileCategory.Controls.Add(this.HideFile);
            this.FileCategory.Controls.Add(this.FillwithBlank);
            resources.ApplyResources(this.FileCategory, "FileCategory");
            this.FileCategory.Name = "FileCategory";
            // 
            // HardwareCategory
            // 
            this.HardwareCategory.BackColor = System.Drawing.Color.Red;
            this.HardwareCategory.Controls.Add(this.FormatDisk);
            this.HardwareCategory.Controls.Add(this.ModifyName);
            this.HardwareCategory.Controls.Add(this.FillUp);
            this.HardwareCategory.Controls.Add(this.Eject);
            resources.ApplyResources(this.HardwareCategory, "HardwareCategory");
            this.HardwareCategory.Name = "HardwareCategory";
            // 
            // WindMouse
            // 
            resources.ApplyResources(this.WindMouse, "WindMouse");
            this.WindMouse.Name = "WindMouse";
            this.WindMouse.UseVisualStyleBackColor = true;
            this.WindMouse.Click += new System.EventHandler(this.WindMouse_Click);
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.Controls.Add(this.Opinion);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::USBTool.Properties.Resources.scp_funny;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMain";
            this.Opacity = 0.85D;
            this.TransparencyKey = System.Drawing.Color.Red;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Opinion.ResumeLayout(false);
            this.ComputerPage.ResumeLayout(false);
            this.SystemControl.ResumeLayout(false);
            this.WindowCategory.ResumeLayout(false);
            this.MouseCategory.ResumeLayout(false);
            this.DisplayCategory.ResumeLayout(false);
            this.SettingCategory.ResumeLayout(false);
            this.SettingCategory.PerformLayout();
            this.MediaCategory.ResumeLayout(false);
            this.MediaCategory.PerformLayout();
            this.DiskPage.ResumeLayout(false);
            this.DiskControl.ResumeLayout(false);
            this.FileCategory.ResumeLayout(false);
            this.HardwareCategory.ResumeLayout(false);
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
		internal Button KillProcess;
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
			foreach (var control in this.GetType().GetFields(BindingFlags.NonPublic |BindingFlags.Instance |BindingFlags.IgnoreCase))
			{
				if (ReferenceEquals(control.FieldType, typeof(Button)))
				{
					Button c = (Button)control.GetValue(this);
					c.FlatStyle = FlatStyle.Flat;
					c.BackColor = global::System.Drawing.Color.PaleGreen;
					c.FlatAppearance.BorderColor = global::System.Drawing.Color.Lime;
					c.FlatAppearance.BorderSize = 2;
					c.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.FromArgb(global::System.Convert.ToInt32(global::System.Convert.ToByte(0)), global::System.Convert.ToInt32(global::System.Convert.ToByte(64)), global::System.Convert.ToInt32(global::System.Convert.ToByte(0)));
					c.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Green;
					c.ForeColor = global::System.Drawing.Color.MediumSeaGreen;
				}
			}
		}
		
		public FolderBrowserDialog Folder;
		internal System.Windows.Forms.Button Media;
		internal System.Windows.Forms.Button ReadText;
		internal Button SwapBotton;
		internal Button ModifyName;
		internal Button RotateScreen;
		internal Button ShowWeb;
		internal Button FillUp;
		internal Button CloseWnd;
		internal Button Glass;
		internal Button MouseTrail;
		internal Button Destroy;
		internal ToolTip ToolTip1;
		internal Button SetText;
		internal Button Eject;
		internal Button Flash;
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
		internal Button Negative;
		internal Button Numerical;
		private TabControl Opinion;
		private TabPage DiskPage;
		private TabControl DiskControl;
		private TabPage FileCategory;
		private TabPage HardwareCategory;
		private TabPage ComputerPage;
		private TabControl SystemControl;
		private TabPage WindowCategory;
		private TabPage MouseCategory;
		private TabPage DisplayCategory;
		private TabPage SettingCategory;
		private TabPage MediaCategory;
        internal Button Mute;
        internal Button Clip;
        internal Button Beep;
        internal Button DisableWnd;
        internal Button WndPicture;
        internal Button Picture;
        internal Button DrawText;
        internal Button WindMouse;
    }
	
}
