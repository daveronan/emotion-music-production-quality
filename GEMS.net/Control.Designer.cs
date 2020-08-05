namespace GEMS.net
{
    partial class Control
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readExperimentalDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxComPorts1 = new System.Windows.Forms.ComboBox();
            this.textBoxShimmer1State = new System.Windows.Forms.TextBox();
            this.textBoxShimmer2State = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonStreamShimmer1 = new System.Windows.Forms.Button();
            this.buttonStopShimmer1 = new System.Windows.Forms.Button();
            this.comboBoxComPorts2 = new System.Windows.Forms.ComboBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelShimmer1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelShimmer2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonStreamShimmer2 = new System.Windows.Forms.Button();
            this.buttonStopShimmer2 = new System.Windows.Forms.Button();
            this.labelPRR = new System.Windows.Forms.Label();
            this.openDialog = new System.Windows.Forms.OpenFileDialog();
            this.pictureBoxVideo = new System.Windows.Forms.PictureBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideo)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configureToolStripMenuItem,
            this.readExperimentalDataToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(976, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Enabled = false;
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.configureToolStripMenuItem.Text = "Configuration";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.configurationToolStripMenuItem_Click);
            // 
            // readExperimentalDataToolStripMenuItem
            // 
            this.readExperimentalDataToolStripMenuItem.Name = "readExperimentalDataToolStripMenuItem";
            this.readExperimentalDataToolStripMenuItem.Size = new System.Drawing.Size(143, 20);
            this.readExperimentalDataToolStripMenuItem.Text = "Read Experimental Data";
            this.readExperimentalDataToolStripMenuItem.Click += new System.EventHandler(this.readExperimentalDataToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // comboBoxComPorts1
            // 
            this.comboBoxComPorts1.FormattingEnabled = true;
            this.comboBoxComPorts1.Location = new System.Drawing.Point(77, 70);
            this.comboBoxComPorts1.Name = "comboBoxComPorts1";
            this.comboBoxComPorts1.Size = new System.Drawing.Size(121, 21);
            this.comboBoxComPorts1.TabIndex = 1;
            this.comboBoxComPorts1.SelectedIndexChanged += new System.EventHandler(this.comboBoxComPorts_SelectedIndexChanged);
            // 
            // textBoxShimmer1State
            // 
            this.textBoxShimmer1State.Location = new System.Drawing.Point(306, 73);
            this.textBoxShimmer1State.Name = "textBoxShimmer1State";
            this.textBoxShimmer1State.Size = new System.Drawing.Size(121, 20);
            this.textBoxShimmer1State.TabIndex = 2;
            // 
            // textBoxShimmer2State
            // 
            this.textBoxShimmer2State.Location = new System.Drawing.Point(306, 99);
            this.textBoxShimmer2State.Name = "textBoxShimmer2State";
            this.textBoxShimmer2State.Size = new System.Drawing.Size(121, 20);
            this.textBoxShimmer2State.TabIndex = 2;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(15, 41);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 3;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(96, 41);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonDisconnect.TabIndex = 3;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // buttonStreamShimmer1
            // 
            this.buttonStreamShimmer1.Enabled = false;
            this.buttonStreamShimmer1.Location = new System.Drawing.Point(440, 68);
            this.buttonStreamShimmer1.Name = "buttonStreamShimmer1";
            this.buttonStreamShimmer1.Size = new System.Drawing.Size(75, 23);
            this.buttonStreamShimmer1.TabIndex = 3;
            this.buttonStreamShimmer1.Text = "Stream";
            this.buttonStreamShimmer1.UseVisualStyleBackColor = true;
            this.buttonStreamShimmer1.Click += new System.EventHandler(this.buttonStreamShimmer1_Click);
            // 
            // buttonStopShimmer1
            // 
            this.buttonStopShimmer1.Enabled = false;
            this.buttonStopShimmer1.Location = new System.Drawing.Point(521, 68);
            this.buttonStopShimmer1.Name = "buttonStopShimmer1";
            this.buttonStopShimmer1.Size = new System.Drawing.Size(75, 23);
            this.buttonStopShimmer1.TabIndex = 3;
            this.buttonStopShimmer1.Text = "Stop";
            this.buttonStopShimmer1.UseVisualStyleBackColor = true;
            this.buttonStopShimmer1.Click += new System.EventHandler(this.buttonStopShimmer1_Click);
            // 
            // comboBoxComPorts2
            // 
            this.comboBoxComPorts2.FormattingEnabled = true;
            this.comboBoxComPorts2.Location = new System.Drawing.Point(77, 97);
            this.comboBoxComPorts2.Name = "comboBoxComPorts2";
            this.comboBoxComPorts2.Size = new System.Drawing.Size(121, 21);
            this.comboBoxComPorts2.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelShimmer1,
            this.toolStripStatusLabelShimmer2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 705);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(976, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelShimmer1
            // 
            this.toolStripStatusLabelShimmer1.Name = "toolStripStatusLabelShimmer1";
            this.toolStripStatusLabelShimmer1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelShimmer2
            // 
            this.toolStripStatusLabelShimmer2.Name = "toolStripStatusLabelShimmer2";
            this.toolStripStatusLabelShimmer2.Size = new System.Drawing.Size(0, 17);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Shimmer 1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Shimmer 2";
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Shimmer 1 Status";
            this.label3.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(211, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Shimmer 2 Status";
            this.label4.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonStreamShimmer2
            // 
            this.buttonStreamShimmer2.Enabled = false;
            this.buttonStreamShimmer2.Location = new System.Drawing.Point(440, 98);
            this.buttonStreamShimmer2.Name = "buttonStreamShimmer2";
            this.buttonStreamShimmer2.Size = new System.Drawing.Size(75, 23);
            this.buttonStreamShimmer2.TabIndex = 3;
            this.buttonStreamShimmer2.Text = "Stream";
            this.buttonStreamShimmer2.UseVisualStyleBackColor = true;
            this.buttonStreamShimmer2.Click += new System.EventHandler(this.buttonStreamShimmer2_Click);
            // 
            // buttonStopShimmer2
            // 
            this.buttonStopShimmer2.Enabled = false;
            this.buttonStopShimmer2.Location = new System.Drawing.Point(521, 98);
            this.buttonStopShimmer2.Name = "buttonStopShimmer2";
            this.buttonStopShimmer2.Size = new System.Drawing.Size(75, 23);
            this.buttonStopShimmer2.TabIndex = 3;
            this.buttonStopShimmer2.Text = "Stop";
            this.buttonStopShimmer2.UseVisualStyleBackColor = true;
            this.buttonStopShimmer2.Click += new System.EventHandler(this.buttonStopShimmer2_Click);
            // 
            // labelPRR
            // 
            this.labelPRR.AutoSize = true;
            this.labelPRR.Location = new System.Drawing.Point(303, 80);
            this.labelPRR.Name = "labelPRR";
            this.labelPRR.Size = new System.Drawing.Size(0, 13);
            this.labelPRR.TabIndex = 184;
            // 
            // openDialog
            // 
            this.openDialog.FileName = "SensorData";
            // 
            // pictureBoxVideo
            // 
            this.pictureBoxVideo.Location = new System.Drawing.Point(15, 449);
            this.pictureBoxVideo.Name = "pictureBoxVideo";
            this.pictureBoxVideo.Size = new System.Drawing.Size(400, 253);
            this.pictureBoxVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxVideo.TabIndex = 185;
            this.pictureBoxVideo.TabStop = false;
            this.pictureBoxVideo.Click += new System.EventHandler(this.pictureBoxVideo_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Enabled = false;
            this.buttonStart.Location = new System.Drawing.Point(833, 664);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(131, 38);
            this.buttonStart.TabIndex = 187;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 727);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.pictureBoxVideo);
            this.Controls.Add(this.labelPRR);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.comboBoxComPorts2);
            this.Controls.Add(this.buttonStopShimmer2);
            this.Controls.Add(this.buttonStopShimmer1);
            this.Controls.Add(this.buttonStreamShimmer2);
            this.Controls.Add(this.buttonStreamShimmer1);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxShimmer2State);
            this.Controls.Add(this.textBoxShimmer1State);
            this.Controls.Add(this.comboBoxComPorts1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Control";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GEMS Experiment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxComPorts1;
        private System.Windows.Forms.TextBox textBoxShimmer1State;
        private System.Windows.Forms.TextBox textBoxShimmer2State;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonStreamShimmer1;
        private System.Windows.Forms.Button buttonStopShimmer1;
        private System.Windows.Forms.ComboBox comboBoxComPorts2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelShimmer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelShimmer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonStreamShimmer2;
        private System.Windows.Forms.Button buttonStopShimmer2;
        private System.Windows.Forms.Label labelPRR;
        private System.Windows.Forms.OpenFileDialog openDialog;
        private System.Windows.Forms.PictureBox pictureBoxVideo;
        private System.Windows.Forms.ToolStripMenuItem readExperimentalDataToolStripMenuItem;
        private System.Windows.Forms.Button buttonStart;
    }
}

