namespace GEMS.net
{
    partial class VideoPlayer
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
            this.VideoTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // VideoTimer
            // 
            this.VideoTimer.Interval = 30000;
            this.VideoTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // VideoPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 758);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "VideoPlayer";
            this.Text = "Video";
            this.Load += new System.EventHandler(this.VideoPlayer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer VideoTimer;
    }
}