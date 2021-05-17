
namespace Testcam
{
    partial class Camera_setting_Form
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Cam1 = new System.Windows.Forms.Button();
            this.Cambox1 = new System.Windows.Forms.ComboBox();
            this.Camera1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Index";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(555, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 32);
            this.button1.TabIndex = 29;
            this.button1.Text = "SAVE Setting";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(379, 8);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(162, 21);
            this.comboBox1.TabIndex = 26;
            // 
            // Cam1
            // 
            this.Cam1.Location = new System.Drawing.Point(305, 11);
            this.Cam1.Margin = new System.Windows.Forms.Padding(2);
            this.Cam1.Name = "Cam1";
            this.Cam1.Size = new System.Drawing.Size(56, 19);
            this.Cam1.TabIndex = 28;
            this.Cam1.Text = "Preview";
            this.Cam1.UseVisualStyleBackColor = true;
            this.Cam1.Click += new System.EventHandler(this.Cam1_Click);
            // 
            // Cambox1
            // 
            this.Cambox1.FormattingEnabled = true;
            this.Cambox1.Location = new System.Drawing.Point(84, 11);
            this.Cambox1.Margin = new System.Windows.Forms.Padding(2);
            this.Cambox1.Name = "Cambox1";
            this.Cambox1.Size = new System.Drawing.Size(165, 21);
            this.Cambox1.TabIndex = 27;
            this.Cambox1.SelectedIndexChanged += new System.EventHandler(this.Cambox1_SelectedIndexChanged);
            this.Cambox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Cambox1_MouseDown);
            // 
            // Camera1
            // 
            this.Camera1.AutoSize = true;
            this.Camera1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Camera1.Location = new System.Drawing.Point(7, 11);
            this.Camera1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Camera1.Name = "Camera1";
            this.Camera1.Size = new System.Drawing.Size(49, 13);
            this.Camera1.TabIndex = 25;
            this.Camera1.Text = "Camera1";
            // 
            // Camera_setting_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 43);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Cam1);
            this.Controls.Add(this.Cambox1);
            this.Controls.Add(this.Camera1);
            this.Name = "Camera_setting_Form";
            this.Text = "Camera_setting_Form";
            this.Load += new System.EventHandler(this.Camera_setting_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Cam1;
        private System.Windows.Forms.ComboBox Cambox1;
        private System.Windows.Forms.Label Camera1;
    }
}