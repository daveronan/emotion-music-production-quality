using ShimmerAPI;

namespace GEMS.net
{
    partial class Configuration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public ShimmerSDBT ShimmerDevice1 = new ShimmerSDBT("Shimmer1", "");
        public ShimmerSDBT ShimmerDevice2 = new ShimmerSDBT("Shimmer2", "");

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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonApplyAll = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.configurationGeneral2 = new GEMS.net.ConfigurationGeneral();
            this.configurationGeneral1 = new GEMS.net.ConfigurationGeneral();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(774, 485);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabIndexChanged += new System.EventHandler(this.tabControl1_TabIndexChanged);
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.configurationGeneral2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(766, 459);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Shimmer 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonApplyAll
            // 
            this.buttonApplyAll.Location = new System.Drawing.Point(711, 525);
            this.buttonApplyAll.Name = "buttonApplyAll";
            this.buttonApplyAll.Size = new System.Drawing.Size(75, 23);
            this.buttonApplyAll.TabIndex = 1;
            this.buttonApplyAll.Text = "Apply All";
            this.buttonApplyAll.UseVisualStyleBackColor = true;
            this.buttonApplyAll.Click += new System.EventHandler(this.buttonApplyAll_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(630, 525);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 23);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // configurationGeneral2
            // 
            this.configurationGeneral2.Location = new System.Drawing.Point(0, 0);
            this.configurationGeneral2.Name = "configurationGeneral2";
            this.configurationGeneral2.Size = new System.Drawing.Size(763, 459);
            this.configurationGeneral2.TabIndex = 0;
            // 
            // configurationGeneral1
            // 
            this.configurationGeneral1.Location = new System.Drawing.Point(0, 0);
            this.configurationGeneral1.Name = "configurationGeneral1";
            this.configurationGeneral1.Size = new System.Drawing.Size(766, 459);
            this.configurationGeneral1.TabIndex = 0;
            this.configurationGeneral1.Load += new System.EventHandler(this.configurationGeneral1_Load);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.configurationGeneral1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(766, 459);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Shimmer 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // Configuration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 561);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonApplyAll);
            this.Controls.Add(this.tabControl1);
            this.Name = "Configuration";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Configuration_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button buttonApplyAll;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.TabPage tabPage2;
        protected internal ConfigurationGeneral configurationGeneral2;
        private System.Windows.Forms.TabPage tabPage1;
        protected internal ConfigurationGeneral configurationGeneral1;
    }
}