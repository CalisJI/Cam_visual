
namespace Testcam
{
    partial class Path_File
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextBox_PathFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Cancel_btn = new System.Windows.Forms.Button();
            this.Saving_btn = new System.Windows.Forms.Button();
            this.Open_Dialog_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBox_PathFile
            // 
            this.TextBox_PathFile.Location = new System.Drawing.Point(87, 12);
            this.TextBox_PathFile.Name = "TextBox_PathFile";
            this.TextBox_PathFile.Size = new System.Drawing.Size(454, 20);
            this.TextBox_PathFile.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Path File :";
            // 
            // Cancel_btn
            // 
            this.Cancel_btn.Location = new System.Drawing.Point(336, 47);
            this.Cancel_btn.Name = "Cancel_btn";
            this.Cancel_btn.Size = new System.Drawing.Size(122, 37);
            this.Cancel_btn.TabIndex = 7;
            this.Cancel_btn.Text = "CANCEL";
            this.Cancel_btn.UseVisualStyleBackColor = true;
            this.Cancel_btn.Click += new System.EventHandler(this.Cancel_btn_Click);
            // 
            // Saving_btn
            // 
            this.Saving_btn.Location = new System.Drawing.Point(149, 47);
            this.Saving_btn.Name = "Saving_btn";
            this.Saving_btn.Size = new System.Drawing.Size(122, 37);
            this.Saving_btn.TabIndex = 6;
            this.Saving_btn.Text = "SAVE";
            this.Saving_btn.UseVisualStyleBackColor = true;
            this.Saving_btn.Click += new System.EventHandler(this.Saving_btn_Click);
            // 
            // Open_Dialog_btn
            // 
            this.Open_Dialog_btn.Location = new System.Drawing.Point(560, 10);
            this.Open_Dialog_btn.Name = "Open_Dialog_btn";
            this.Open_Dialog_btn.Size = new System.Drawing.Size(40, 23);
            this.Open_Dialog_btn.TabIndex = 5;
            this.Open_Dialog_btn.Text = "...";
            this.Open_Dialog_btn.UseVisualStyleBackColor = true;
            this.Open_Dialog_btn.Click += new System.EventHandler(this.Open_Dialog_btn_Click);
            // 
            // Path_File
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 99);
            this.Controls.Add(this.TextBox_PathFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel_btn);
            this.Controls.Add(this.Saving_btn);
            this.Controls.Add(this.Open_Dialog_btn);
            this.Name = "Path_File";
            this.Text = "Path_File";
            this.Load += new System.EventHandler(this.Path_File_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_PathFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Cancel_btn;
        private System.Windows.Forms.Button Saving_btn;
        private System.Windows.Forms.Button Open_Dialog_btn;
    }
}