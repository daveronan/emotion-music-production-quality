using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GEMS.net
{
    public delegate void NewTrackEventHandler(object sender, EventArgs e);

    public delegate void SubmitEventHandler(object sender, EventArgs e);

    public delegate void AudioStoppedEventHandler(object sender, EventArgs e);

    public delegate void BaselineReadingEventHandler(object sender, EventArgs e);


    public partial class Experiment : Form
    {

        private List<String> _audioFiles;
        private int _audioFileCount = 0;
        private double _videoLocation = 1.0;

        private bool GEMS = false;
        private bool VA = true;

        private bool _test = false;

        public event NewTrackEventHandler NewTrack;

        public event SubmitEventHandler Submit;

        public event AudioStoppedEventHandler AudioStopped;

        public event BaselineReadingEventHandler Baseline;

        private Control _pControl;

        private bool checkArousal = false;
        private bool checkValence = false;
        private bool checkTense = false;

        private bool checkWonder = false;
        private bool checkTranscendence = false;
        private bool checkPower = false;
        private bool checkTenderness = false;
        private bool checkNostalgia = false;
        private bool checkPeacefulness = false;
        private bool checkJoyfulActivation = false;
        private bool checkSadness = false;
        private bool checkTension = false;

        private bool checkLike = false;

        private SubmitEventArgs _submitEventArgs = new SubmitEventArgs();

        private BaselineEventArgs _baseLineEventArgs = new BaselineEventArgs();

        public Experiment()
        {
            InitializeComponent();
        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnNewTrack(EventArgs e)
        {
            if (NewTrack != null)
                NewTrack(this, e);
        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnSubmit(EventArgs e)
        {
            if (Submit != null)
                Submit(this, e);
        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnAudioStopped(EventArgs e)
        {
            if (AudioStopped != null)
                AudioStopped(this, e);
        }

        // Invoke the Changed event; called whenever list changes
        protected virtual void OnBaseline(EventArgs e)
        {
            if (Baseline != null)
                Baseline(this, e);
        }

        private void PlayAudio()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();

            player.SoundLocation = _audioFiles[_audioFileCount];
            AudioFileEventArgs eventArgs = new AudioFileEventArgs();
            eventArgs.AudioFile = _audioFiles[_audioFileCount]; 

            OnNewTrack(eventArgs);
            player.PlaySync();

            OnAudioStopped(null);
        }

        public Experiment(List<String> audioFiles, Control control)
        {
            InitializeComponent();
            _audioFiles = audioFiles;

            _pControl = control;

            List<String> testFiles = new List<string>();
            testFiles.Add(@"C:\Users\David\Desktop\test1.wav");
            testFiles.Add(@"C:\Users\David\Desktop\test2.wav");

            var rnd = new Random();
            var result = _audioFiles.OrderBy(item => rnd.Next());
            _audioFiles = new List<string>(result);

            testFiles.AddRange(_audioFiles);

            _audioFiles = testFiles;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            pictureBoxSetupCamera.Visible = false;
            labelWarning.Visible = false;
            buttonPlay.Enabled = false;
            buttonPlay.Text = "Playing...";

            backgroundWorkerAudio.RunWorkerAsync();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (!GEMS)
            {

                pictureBoxGEMS.Visible = false;

                groupBoxWonder.Visible = false;
                groupBoxTranscendence.Visible = false;
                groupBoxPower.Visible = false;
                groupBoxTenderness.Visible = false;
                groupBoxNostalgia.Visible = false;
                groupBoxPeacefulness.Visible = false;
                groupBoxJoyfulActivation.Visible = false;
                groupBoxSadness.Visible = false;
                groupBoxTenderness.Visible = false;
                groupBoxTension.Visible = false;

                textBoxWonder.Visible = false;
                textBoxTranscendence.Visible = false;
                textBoxPower.Visible = false;
                textBoxTenderness.Visible = false;
                textBoxNostalgia.Visible = false;
                textBoxPeacefulness.Visible = false;
                textBoxJoyfulActivation.Visible = false;
                textBoxSadness.Visible = false;
                textBoxTenderness.Visible = false;
                textBoxTension.Visible = false;

                pictureBoxAV.Visible = true;
                labelCalm.Visible = true;
                labelExciting.Visible = true;
                labelSad.Visible = true;
                labelHappy.Visible = true;
                labelNotTense.Visible = true;
                labelTense.Visible = true;
                trackBarArousal.Visible = true;
                trackBarValence.Visible = true;
                trackBarTension.Visible = true;
                labelZero1.Visible = true;
                labelZero2.Visible = true;
                labelZero3.Visible = true;
                groupBoxLike.Visible = true;
                

                radioButtonWonderVeryMuch.Checked = false;
                radioButtonWonderQuiteALot.Checked = false;
                radioButtonWonderModerately.Checked = false;
                radioButtonWonderSomewhat.Checked = false;
                radioButtonWonderNotAtAll.Checked = false;
                radioButtonTranscendenceVeryMuch.Checked = false;
                radioButtonTranscendenceQuiteALot.Checked = false;
                radioButtonTranscendenceModerately.Checked = false;
                radioButtonTranscendenceSomewhat.Checked = false;
                radioButtonTranscendenceNotAtAll.Checked = false;
                radioButtonPowerVeryMuch.Checked = false;
                radioButtonPowerQuiteALot.Checked = false;
                radioButtonPowerModerately.Checked = false;
                radioButtonPowerSomewhat.Checked = false;
                radioButtonPowerNotAtAll.Checked = false;
                radioButtonTendernessVeryMuch.Checked = false;
                radioButtonTendernessQuiteALot.Checked = false;
                radioButtonTendernessModerately.Checked = false;
                radioButtonTendernessSomewhat.Checked = false;
                radioButtonTendernessNotAtAll.Checked = false;
                radioButtonNostalgiaVeryMuch.Checked = false;
                radioButtonNostalgiaQuiteALot.Checked = false;
                radioButtonNostalgiaModerately.Checked = false;
                radioButtonNostalgiaSomewhat.Checked = false;
                radioButtonNostalgiaNotAtAll.Checked = false;
                radioButtonPeacefulnessVeryMuch.Checked = false;
                radioButtonPeacefulnessQuiteALot.Checked = false;
                radioButtonPeacefulnessModerately.Checked = false;
                radioButtonPeacefulnessSomewhat.Checked = false;
                radioButtonPeacefulnessNotAtAll.Checked = false;
                radioButtonJoyfulActivationVeryMuch.Checked = false;
                radioButtonJoyfulActivationQuiteALot.Checked = false;
                radioButtonJoyfulActivationModerately.Checked = false;
                radioButtonJoyfulActivationSomewhat.Checked = false;
                radioButtonJoyfulActivationNotAtAll.Checked = false;
                radioButtonSadnessVeryMuch.Checked = false;
                radioButtonSadnessQuiteALot.Checked = false;
                radioButtonSadnessModerately.Checked = false;
                radioButtonSadnessSomewhat.Checked = false;
                radioButtonSadnessNotAtAll.Checked = false;
                radioButtonTensionVeryMuch.Checked = false;
                radioButtonTensionQuiteALot.Checked = false;
                radioButtonTensionModerately.Checked = false;
                radioButtonTensionSomewhat.Checked = false;
                radioButtonTensionNotAtAll.Checked = false;

                checkWonder = false;
                checkTranscendence = false;
                checkPower = false;
                checkTenderness = false;
                checkNostalgia = false;
                checkPeacefulness = false;
                checkJoyfulActivation = false;
                checkSadness = false;
                checkTension = false;

                buttonSubmit.Enabled = false;

                GEMS = true;
                
            }

            if (!VA)
            {
                pictureBoxAV.Visible = false;
                labelCalm.Visible = false;
                labelExciting.Visible = false;
                labelSad.Visible = false;
                labelHappy.Visible = false;
                labelNotTense.Visible = false;
                labelTense.Visible = false;
                trackBarArousal.Visible = false;
                trackBarValence.Visible = false;
                trackBarTension.Visible = false;
                trackBarArousal.Value = 50;
                trackBarValence.Value = 50;
                trackBarTension.Value = 50;
                labelZero1.Visible = false;
                labelZero2.Visible = false;
                labelZero3.Visible = false;
                groupBoxLike.Visible = false;

                radioButtonLikeNotAtAll.Checked = false;
                radioButtonLikeSomewhat.Checked = false;
                radioButtonLikeModerately.Checked = false;
                radioButtonLikeQuiteALot.Checked = false;
                radioButtonLikeVeryMuch.Checked = false;

                checkValence = false;
                checkArousal = false;
                checkTense = false;
                checkLike = false;


                buttonSubmit.Visible = false;
                buttonSubmit.Enabled = false;

                buttonPlay.Visible = true;
                labelReady.Visible = true;

                GEMS = false;
                VA = true;

                _submitEventArgs.AudioFileName = _audioFiles[_audioFileCount];

                OnSubmit(_submitEventArgs);

                if (_audioFileCount == 1)
                {
                    labelPractice1.Visible = false;
                    labelPractice2.Visible = false;
                    VideoPlayer player = new VideoPlayer(@"C:\Users\David\Desktop\CalmSea.wmv", 0);
                    player.Show(this);
                    player.FormClosing += player_FormClosing;

                    _baseLineEventArgs.BaselineFile = "BaselineIntroduction";

                    OnBaseline(_baseLineEventArgs);

                }
                else if(_audioFileCount > 1)
                {
                    VideoPlayer player = new VideoPlayer(@"C:\Users\David\Desktop\CalmForest.wmv", _videoLocation);
                    player.Show(this);
                    player.FormClosing += player_FormClosing;
                    _baseLineEventArgs.BaselineFile = "Baseline" + _audioFiles[_audioFileCount];
                    _videoLocation += 30.0;

                    OnBaseline(_baseLineEventArgs);
                }
                

                _audioFileCount++;

                string songNumber = String.Format("Song {0}/22", (_audioFileCount + 1));
                labelSong.Text= songNumber;

                pictureBoxSetupCamera.Visible = true;
                labelWarning.Visible = true;

                if (_audioFileCount == _audioFiles.Count)
                {
                    MessageBox.Show("Thank you for doing the experiment!", Control.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }

            
        }

        void player_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnAudioStopped(null);
        }

        private void Experiment_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            this.Show();
        }

        private void WonderCheckChanged(object sender, EventArgs e)
        {
            checkWonder = true;

            if (sender == radioButtonWonderNotAtAll)
            {
                _submitEventArgs.Wonder = 1;
            }
            else if (sender == radioButtonWonderSomewhat)
            {
                _submitEventArgs.Wonder = 2;
            }
            else if (sender == radioButtonWonderModerately)
            {
                _submitEventArgs.Wonder = 3;
            }
            else if (sender == radioButtonWonderQuiteALot)
            {
                _submitEventArgs.Wonder = 4;
            }
            else if (sender == radioButtonWonderVeryMuch)
            {
                _submitEventArgs.Wonder = 5;
            }

            if (checkWonder && checkTranscendence && checkPower && checkTenderness && checkNostalgia &&
                checkPeacefulness && checkJoyfulActivation && checkSadness && checkTension)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void TranscendenceCheckChanged(object sender, EventArgs e)
        {
            if (sender == radioButtonTranscendenceNotAtAll)
            {
                _submitEventArgs.Transcendence = 1;
            }
            else if (sender == radioButtonTranscendenceSomewhat)
            {
                _submitEventArgs.Transcendence = 2;
            }
            else if (sender == radioButtonTranscendenceModerately)
            {
                _submitEventArgs.Transcendence = 3;
            }
            else if (sender == radioButtonTranscendenceQuiteALot)
            {
                _submitEventArgs.Transcendence = 4;
            }
            else if (sender == radioButtonTranscendenceVeryMuch)
            {
                _submitEventArgs.Transcendence = 5;
            }

            checkTranscendence = true;

            if (checkWonder && checkTranscendence && checkPower && checkTenderness && checkNostalgia &&
                checkPeacefulness && checkJoyfulActivation && checkSadness && checkTension)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void PowerCheckChanged(object sender, EventArgs e)
        {
            if (sender == radioButtonPowerNotAtAll)
            {
                _submitEventArgs.Power = 1;
            }
            else if (sender == radioButtonPowerSomewhat)
            {
                _submitEventArgs.Power = 2;
            }
            else if (sender == radioButtonPowerModerately)
            {
                _submitEventArgs.Power = 3;
            }
            else if (sender == radioButtonPowerQuiteALot)
            {
                _submitEventArgs.Power = 4;
            }
            else if (sender == radioButtonPowerVeryMuch)
            {
                _submitEventArgs.Power = 5;
            }

            checkPower = true;

            if (checkWonder && checkTranscendence && checkPower && checkTenderness && checkNostalgia &&
                checkPeacefulness && checkJoyfulActivation && checkSadness && checkTension)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void TendernessCheckChanged(object sender, EventArgs e)
        {
            if (sender == radioButtonTendernessNotAtAll)
            {
                _submitEventArgs.Tenderness = 1;
            }
            else if (sender == radioButtonTendernessSomewhat)
            {
                _submitEventArgs.Tenderness = 2;
            }
            else if (sender == radioButtonTendernessModerately)
            {
                _submitEventArgs.Tenderness = 3;
            }
            else if (sender == radioButtonTendernessQuiteALot)
            {
                _submitEventArgs.Tenderness = 4;
            }
            else if (sender == radioButtonTendernessVeryMuch)
            {
                _submitEventArgs.Tenderness = 5;
            }

            checkTenderness = true;

            if (checkWonder && checkTranscendence && checkPower && checkTenderness && checkNostalgia &&
                checkPeacefulness && checkJoyfulActivation && checkSadness && checkTension)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void NostalgiaCheckChanged(object sender, EventArgs e)
        {
            if (sender == radioButtonNostalgiaNotAtAll)
            {
                _submitEventArgs.Nostalgia = 1;
            }
            else if (sender == radioButtonNostalgiaSomewhat)
            {
                _submitEventArgs.Nostalgia = 2;
            }
            else if (sender == radioButtonNostalgiaModerately)
            {
                _submitEventArgs.Nostalgia = 3;
            }
            else if (sender == radioButtonNostalgiaQuiteALot)
            {
                _submitEventArgs.Nostalgia = 4;
            }
            else if (sender == radioButtonNostalgiaVeryMuch)
            {
                _submitEventArgs.Nostalgia = 5;
            }

            checkNostalgia = true;

            if (checkWonder && checkTranscendence && checkPower && checkTenderness && checkNostalgia &&
                checkPeacefulness && checkJoyfulActivation && checkSadness && checkTension)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void PeacefulnessCheckChanged(object sender, EventArgs e)
        {

            if (sender == radioButtonPeacefulnessNotAtAll)
            {
                _submitEventArgs.Peacefulness = 1;
            }
            else if (sender == radioButtonPeacefulnessSomewhat)
            {
                _submitEventArgs.Peacefulness = 2;
            }
            else if (sender == radioButtonPeacefulnessModerately)
            {
                _submitEventArgs.Peacefulness = 3;
            }
            else if (sender == radioButtonPeacefulnessQuiteALot)
            {
                _submitEventArgs.Peacefulness = 4;
            }
            else if (sender == radioButtonPeacefulnessVeryMuch)
            {
                _submitEventArgs.Peacefulness = 5;
            }

            checkPeacefulness = true;

            if (checkWonder && checkTranscendence && checkPower && checkTenderness && checkNostalgia &&
                checkPeacefulness && checkJoyfulActivation && checkSadness && checkTension)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void JoyfulActivationCheckChanged(object sender, EventArgs e)
        {
            if (sender == radioButtonJoyfulActivationNotAtAll)
            {
                _submitEventArgs.JoyfulActivation = 1;
            }
            else if (sender == radioButtonJoyfulActivationSomewhat)
            {
                _submitEventArgs.JoyfulActivation = 2;
            }
            else if (sender == radioButtonJoyfulActivationModerately)
            {
                _submitEventArgs.JoyfulActivation = 3;
            }
            else if (sender == radioButtonJoyfulActivationQuiteALot)
            {
                _submitEventArgs.JoyfulActivation = 4;
            }
            else if (sender == radioButtonJoyfulActivationVeryMuch)
            {
                _submitEventArgs.JoyfulActivation = 5;
            }

            checkJoyfulActivation = true;

            if (checkWonder && checkTranscendence && checkPower && checkTenderness && checkNostalgia &&
                checkPeacefulness && checkJoyfulActivation && checkSadness && checkTension)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void SadnessCheckChanged(object sender, EventArgs e)
        {
            if (sender == radioButtonSadnessNotAtAll)
            {
                _submitEventArgs.Sadness = 1;
            }
            else if (sender == radioButtonSadnessSomewhat)
            {
                _submitEventArgs.Sadness = 2;
            }
            else if (sender == radioButtonSadnessModerately)
            {
                _submitEventArgs.Sadness = 3;
            }
            else if (sender == radioButtonSadnessQuiteALot)
            {
                _submitEventArgs.Sadness = 4;
            }
            else if (sender == radioButtonSadnessVeryMuch)
            {
                _submitEventArgs.Sadness = 5;
            }

            checkSadness = true;

            if (checkWonder && checkTranscendence && checkPower && checkTenderness && checkNostalgia &&
                checkPeacefulness && checkJoyfulActivation && checkSadness && checkTension)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void TensionCheckChanged(object sender, EventArgs e)
        {
            if (sender == radioButtonTensionNotAtAll)
            {
                _submitEventArgs.Tension = 1;
            }
            else if (sender == radioButtonTensionSomewhat)
            {
                _submitEventArgs.Tension = 2;
            }
            else if (sender == radioButtonTensionModerately)
            {
                _submitEventArgs.Tension = 3;
            }
            else if (sender == radioButtonTensionQuiteALot)
            {
                _submitEventArgs.Tension = 4;
            }
            else if (sender == radioButtonTensionVeryMuch)
            {
                _submitEventArgs.Tension = 5;
            }

            checkTension = true;

            if (checkWonder && checkTranscendence && checkPower && checkTenderness && checkNostalgia &&
                checkPeacefulness && checkJoyfulActivation && checkSadness && checkTension)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void Experiment_FormClosing(object sender, FormClosingEventArgs e)
        {
            _pControl.WindowState = FormWindowState.Normal;
        }

        private void backgroundWorkerAudio_DoWork(object sender, DoWorkEventArgs e)
        {
            PlayAudio();
        }

        private void backgroundWorkerAudio_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonPlay.Visible = false;
            buttonPlay.Enabled = true;
            buttonPlay.Text = "Play";
            labelReady.Visible = false;

            pictureBoxGEMS.Visible = true;
            groupBoxWonder.Visible = true;
            groupBoxTranscendence.Visible = true;
            groupBoxPower.Visible = true;
            groupBoxTenderness.Visible = true;
            groupBoxNostalgia.Visible = true;
            groupBoxPeacefulness.Visible = true;
            groupBoxJoyfulActivation.Visible = true;
            groupBoxSadness.Visible = true;
            groupBoxTenderness.Visible = true;
            groupBoxTension.Visible = true;

            textBoxWonder.Visible = true;
            textBoxTranscendence.Visible = true;
            textBoxPower.Visible = true;
            textBoxTenderness.Visible = true;
            textBoxNostalgia.Visible = true;
            textBoxPeacefulness.Visible = true;
            textBoxJoyfulActivation.Visible = true;
            textBoxSadness.Visible = true;
            textBoxTenderness.Visible = true;
            textBoxTension.Visible = true;

            buttonSubmit.Visible = true;

            _submitEventArgs = new SubmitEventArgs();
        }

        private void trackBarArousal_Scroll(object sender, EventArgs e)
        {
            checkArousal = true;
            VA = false;

            _submitEventArgs.Arousal = trackBarArousal.Value;

            if (checkTense && checkArousal && checkValence && checkLike)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void trackBarValence_Scroll(object sender, EventArgs e)
        {
            checkValence = true;
            VA = false;

            _submitEventArgs.Valence = trackBarValence.Value;

            if (checkTense && checkArousal && checkValence && checkLike)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void trackBarTension_Scroll(object sender, EventArgs e)
        {
            checkTense = true;
            VA = false;

            _submitEventArgs.Tense = trackBarTension.Value;

            if (checkTense && checkArousal && checkValence && checkLike)
            {
                buttonSubmit.Enabled = true;
            }
        }

        private void Like_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == radioButtonLikeNotAtAll)
            {
                _submitEventArgs.Like = 1;
            }
            else if (sender == radioButtonLikeSomewhat)
            {
                _submitEventArgs.Like = 2;
            }
            else if (sender == radioButtonLikeModerately)
            {
                _submitEventArgs.Like = 3;
            }
            else if (sender == radioButtonLikeQuiteALot)
            {
                _submitEventArgs.Like = 4;
            }
            else if (sender == radioButtonLikeVeryMuch)
            {
                _submitEventArgs.Like = 5;
            }

            checkLike = true;
            VA = false;

            if (checkTense && checkArousal && checkValence && checkLike)
            {
                buttonSubmit.Enabled = true;
            }
        }
    }
}
