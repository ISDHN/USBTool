using System.Collections.Generic;
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
using System.Windows;

namespace USBTool
{
		public partial class Mediashow : System.Windows.Forms.Form
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
		
		//Windows 窗体设计器所必需的
		private System.ComponentModel.Container components = null;
		
		//注意: 以下过程是 Windows 窗体设计器所必需的
		//可以使用 Windows 窗体设计器修改它。
		//不要使用代码编辑器修改它。
		[System.Diagnostics.DebuggerStepThrough()]
			private void InitializeComponent()
			{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mediashow));
            this.Host = new System.Windows.Forms.Integration.ElementHost();
            this.mel = new System.Windows.Controls.MediaElement();
            this.SuspendLayout();
            // 
            // Host
            // 
            this.Host.BackColor = System.Drawing.Color.Transparent;
            this.Host.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Host.Location = new System.Drawing.Point(0, 0);
            this.Host.Name = "Host";
            this.Host.Size = new System.Drawing.Size(800, 450);
            this.Host.TabIndex = 0;
            this.Host.Text = "Host";
            this.Host.Child = this.mel;
            // 
            // Mediashow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.Controls.Add(this.Host);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Mediashow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Mediashow_Shown);
            this.ResumeLayout(false);

			}
		
		internal System.Windows.Forms.Integration.ElementHost Host;
		internal System.Windows.Controls.MediaElement mel;
		public Mediashow()
		{
			
			// 此调用是设计器所必需的。
			InitializeComponent();
			mel.MediaEnded += new System.Windows.RoutedEventHandler(mel_MediaEnded);
			Host.Child = mel;
			mel.LoadedBehavior = System.Windows.Controls.MediaState.Manual;
			this.Width = System.Convert.ToInt32(SystemParameters.PrimaryScreenWidth / 2);
			this.Height = System.Convert.ToInt32(SystemParameters.PrimaryScreenHeight / 2);
			// 在 InitializeComponent() 调用之后添加任何初始化。
			
		}
	}
	
}
