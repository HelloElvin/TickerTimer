/*
 * Created by SharpDevelop.
 * User: Elvin
 * Date: 2018/2/3
 * Time: 15:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Ticker
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tasklistBox = new System.Windows.Forms.ComboBox();
			this.lList = new System.Windows.Forms.Label();
			this.csCbx = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AllowDrop = true;
			this.label1.AutoEllipsis = true;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 12);
			this.label1.TabIndex = 0;
			this.label1.SizeChanged += new System.EventHandler(this.csChanged);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 227);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(260, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Show Tasks";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(12, 32);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(260, 189);
			this.panel1.TabIndex = 2;
			// 
			// tasklistBox
			// 
			this.tasklistBox.FormattingEnabled = true;
			this.tasklistBox.Location = new System.Drawing.Point(151, 6);
			this.tasklistBox.Name = "tasklistBox";
			this.tasklistBox.Size = new System.Drawing.Size(121, 20);
			this.tasklistBox.TabIndex = 3;
			this.tasklistBox.SelectedValueChanged += new System.EventHandler(this.tasklistBoxChanged);
			// 
			// lList
			// 
			this.lList.Location = new System.Drawing.Point(13, 6);
			this.lList.Name = "lList";
			this.lList.Size = new System.Drawing.Size(132, 23);
			this.lList.TabIndex = 4;
			// 
			// csCbx
			// 
			this.csCbx.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.csCbx.Checked = true;
			this.csCbx.CheckState = System.Windows.Forms.CheckState.Checked;
			this.csCbx.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.csCbx.Location = new System.Drawing.Point(12, 248);
			this.csCbx.Name = "csCbx";
			this.csCbx.Size = new System.Drawing.Size(154, 22);
			this.csCbx.TabIndex = 5;
			this.csCbx.Text = "到期自动切换任务列表";
			this.csCbx.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			this.csCbx.UseVisualStyleBackColor = true;
			this.csCbx.CheckedChanged += new System.EventHandler(this.csCbxCheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 269);
			this.Controls.Add(this.csCbx);
			this.Controls.Add(this.lList);
			this.Controls.Add(this.tasklistBox);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.button1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Ticker";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox tasklistBox;
		private System.Windows.Forms.Label lList;
		private System.Windows.Forms.CheckBox csCbx;
	}
}
