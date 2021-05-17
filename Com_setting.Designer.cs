
namespace Testcam
{
    partial class Com_setting
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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Baudrate_box = new System.Windows.Forms.ComboBox();
            this.Com_setting_box = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.Save_btn = new System.Windows.Forms.Button();
            this.Set_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "BAUD";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "COM";
            // 
            // Baudrate_box
            // 
            this.Baudrate_box.FormattingEnabled = true;
            this.Baudrate_box.Location = new System.Drawing.Point(60, 35);
            this.Baudrate_box.Name = "Baudrate_box";
            this.Baudrate_box.Size = new System.Drawing.Size(121, 21);
            this.Baudrate_box.TabIndex = 3;
            // 
            // Com_setting_box
            // 
            this.Com_setting_box.FormattingEnabled = true;
            this.Com_setting_box.Location = new System.Drawing.Point(60, 8);
            this.Com_setting_box.Name = "Com_setting_box";
            this.Com_setting_box.Size = new System.Drawing.Size(121, 21);
            this.Com_setting_box.TabIndex = 4;
            this.Com_setting_box.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Com_setting_box_MouseDown);
            // 
            // Save_btn
            // 
            this.Save_btn.Location = new System.Drawing.Point(218, 33);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Size = new System.Drawing.Size(75, 23);
            this.Save_btn.TabIndex = 7;
            this.Save_btn.Text = "Save";
            this.Save_btn.UseVisualStyleBackColor = true;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // Set_btn
            // 
            this.Set_btn.Location = new System.Drawing.Point(218, 5);
            this.Set_btn.Name = "Set_btn";
            this.Set_btn.Size = new System.Drawing.Size(75, 23);
            this.Set_btn.TabIndex = 8;
            this.Set_btn.Text = "TEST";
            this.Set_btn.UseVisualStyleBackColor = true;
            this.Set_btn.Click += new System.EventHandler(this.Set_btn_Click);
            // 
            // Com_setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 61);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Baudrate_box);
            this.Controls.Add(this.Com_setting_box);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.Set_btn);
            this.Name = "Com_setting";
            this.Text = "Com_setting";
            this.Load += new System.EventHandler(this.Com_setting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Baudrate_box;
        private System.Windows.Forms.ComboBox Com_setting_box;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Button Set_btn;
    }
}