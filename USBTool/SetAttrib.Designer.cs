// VBConversions Note: VB project level imports
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
// End of VB project level imports

using USBTool;

namespace USBTool
{
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]public 
	partial class SetAttrib : System.Windows.Forms.Form
	{
		
		//Form 重写 Dispose，以清理组件列表。
		[System.Diagnostics.DebuggerNonUserCode()]protected override void Dispose(bool disposing)
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
		[System.Diagnostics.DebuggerStepThrough()]private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetAttrib));
            this.Choose = new System.Windows.Forms.ListBox();
            this.Volume = new System.Windows.Forms.TrackBar();
            this.Rate = new System.Windows.Forms.TrackBar();
            this.Open = new System.Windows.Forms.OpenFileDialog();
            this.Yes = new System.Windows.Forms.Button();
            this.Cancle = new System.Windows.Forms.Button();
            this.Browse = new System.Windows.Forms.Button();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SpeechVoice = new System.Windows.Forms.ComboBox();
            this.SpeechText = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rate)).BeginInit();
            this.SuspendLayout();
            // 
            // Choose
            // 
            this.Choose.FormattingEnabled = true;
            this.Choose.ItemHeight = 12;
            this.Choose.Location = new System.Drawing.Point(12, 12);
            this.Choose.Name = "Choose";
            this.Choose.Size = new System.Drawing.Size(150, 136);
            this.Choose.TabIndex = 0;
            // 
            // Volume
            // 
            this.Volume.Location = new System.Drawing.Point(9, 171);
            this.Volume.Maximum = 100;
            this.Volume.Name = "Volume";
            this.Volume.Size = new System.Drawing.Size(235, 45);
            this.Volume.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.Volume, "音量");
            this.Volume.Value = 50;
            this.Volume.ValueChanged += new System.EventHandler(this.Volume_ValueChanged);
            this.Volume.MouseLeave += new System.EventHandler(this.Volume_DragLeave);
            // 
            // Rate
            // 
            this.Rate.Location = new System.Drawing.Point(12, 222);
            this.Rate.Minimum = -10;
            this.Rate.Name = "Rate";
            this.Rate.Size = new System.Drawing.Size(232, 45);
            this.Rate.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.Rate, "速度");
            this.Rate.ValueChanged += new System.EventHandler(this.Rate_ValueChanged);
            this.Rate.MouseLeave += new System.EventHandler(this.Rate_MouseLeave);
            // 
            // Yes
            // 
            this.Yes.Location = new System.Drawing.Point(169, 12);
            this.Yes.Name = "Yes";
            this.Yes.Size = new System.Drawing.Size(75, 35);
            this.Yes.TabIndex = 3;
            this.Yes.Text = "确定";
            this.Yes.UseVisualStyleBackColor = true;
            this.Yes.Click += new System.EventHandler(this.Yes_Click);
            // 
            // Cancle
            // 
            this.Cancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancle.Location = new System.Drawing.Point(169, 64);
            this.Cancle.Name = "Cancle";
            this.Cancle.Size = new System.Drawing.Size(75, 35);
            this.Cancle.TabIndex = 4;
            this.Cancle.Text = "取消";
            this.Cancle.UseVisualStyleBackColor = true;
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(169, 113);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(75, 35);
            this.Browse.TabIndex = 5;
            this.Browse.Text = "浏览...";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // SpeechVoice
            // 
            this.SpeechVoice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SpeechVoice.FormattingEnabled = true;
            this.SpeechVoice.Location = new System.Drawing.Point(13, 128);
            this.SpeechVoice.Name = "SpeechVoice";
            this.SpeechVoice.Size = new System.Drawing.Size(150, 20);
            this.SpeechVoice.TabIndex = 6;
            // 
            // SpeechText
            // 
            this.SpeechText.Location = new System.Drawing.Point(12, 12);
            this.SpeechText.Multiline = true;
            this.SpeechText.Name = "SpeechText";
            this.SpeechText.Size = new System.Drawing.Size(150, 110);
            this.SpeechText.TabIndex = 7;
            // 
            // SetAttrib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(263, 285);
            this.Controls.Add(this.SpeechText);
            this.Controls.Add(this.SpeechVoice);
            this.Controls.Add(this.Browse);
            this.Controls.Add(this.Cancle);
            this.Controls.Add(this.Yes);
            this.Controls.Add(this.Rate);
            this.Controls.Add(this.Volume);
            this.Controls.Add(this.Choose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetAttrib";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "设置";
            ((System.ComponentModel.ISupportInitialize)(this.Volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Rate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		internal ToolTip ToolTip1;
		internal Button Yes;
		internal Button Cancle;
		internal Button Browse;
		public TrackBar Volume;
		public TrackBar Rate;
		public OpenFileDialog Open;
		public TextBox SpeechText;
		public ListBox Choose;
		public ComboBox SpeechVoice;
        private System.ComponentModel.IContainer components;
    }
	
}
