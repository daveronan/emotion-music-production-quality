using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX.AudioVideoPlayback;

namespace GEMS.net
{
    public partial class VideoPlayer : Form
    {
        private Video _ourVideo;

        public VideoPlayer()
        {
            InitializeComponent();
        }

        public VideoPlayer(string fileName, double position)
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            _ourVideo = new Video(fileName);

            if (position < _ourVideo.Duration)
            {
                _ourVideo.CurrentPosition = position;
            }
            else
            {
                _ourVideo.CurrentPosition = 0;
            }

            if (Math.Abs(position) > 0)
            {
                VideoTimer.Enabled = true;
                VideoTimer.Start();
            }

            _ourVideo.Ending += _ourVideo_Ending;
            _ourVideo.Owner = this;
            _ourVideo.Size = this.Size;
            _ourVideo.Fullscreen = true;
            _ourVideo.Play();
        }

        void _ourVideo_Ending(object sender, EventArgs e)
        {
            this.Close();
        }

        private void VideoPlayer_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _ourVideo.Stop();
            this.Close();
        }
    }
}
