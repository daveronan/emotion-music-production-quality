using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video.VFW;
using CsvHelper;
using ShimmerAPI;
using ShimmerLibrary;

namespace GEMS.net
{
    public partial class Control : Form
    {
        public ShimmerSDBT ShimmerDevice1 = new ShimmerSDBT("Shimmer1", "");
        private string _comPort1;

        private bool _writeData = false;
        private bool _writeBaseLineData = false;

        private List<String> _audioFiles = new List<string>(); 
        private string _currentUser = String.Empty;

        public ShimmerSDBT ShimmerDevice2 = new ShimmerSDBT("Shimmer2", "");
        private string _comPort2;

        private System.IO.Ports.SerialPort _serialPort = new SerialPort();
        private Configuration _configure;
        private Experiment _experiment;
        public static readonly string ApplicationName = Application.ProductName.ToString().Replace("_", " ");

        private delegate void ShowChannelTextBoxesCallback(int i, string shimmernumber);
        private delegate void ShowChannelLabelsCallback(List<String> s, string shimmernumber);
        private delegate void UpdateChannelsCallback(List<Double> d, string shimmernumber);

        //Create webcam object
        private VideoCaptureDevice _videoSource;
        // instantiate AVI writer, use WMV3 codec
        AVIWriter _writer = new AVIWriter("wmv3");
 
        List<System.Windows.Forms.Label> ListofLabelsShimmer1 = new List<System.Windows.Forms.Label>();
        List<TextBox> ListofTextBoxShimmer1 = new List<TextBox>();

        List<System.Windows.Forms.Label> ListofLabelsShimmer2 = new List<System.Windows.Forms.Label>();
        List<TextBox> ListofTextBoxShimmer2 = new List<TextBox>();

        private List<String> ShimmerIdSetup = new List<String>();

        private int ShowTBcountShimmer1 = 0;
        private int ShowTBcountShimmer2 = 0;
        //Write to file
        private String _delimeter = ",";

        //ECG-HR
        private ECGToHR ECGtoHR;
        private Boolean _enableECGtoHRConversion = false;
        private int TrainingPeriodECG = 10; //5 second buffer

        //PPG-HR
        private PPGToHRAlgorithm PPGtoHeartRateCalculation;
        private Boolean EnablePPGtoHRConversion = false;
        private int NumberOfHeartBeatsToAverage = 5;
        private int NumberOfHeartBeatsToAverageECG = 1;
        private int TrainingPeriodPPG = 10; //5 second buffer

        //ExG
        public String ECGSignalName = "ECG LL-RA"; //This is used to identify which signal to feed into the ECG to HR algorithm
        private int ExGLeadOffCounter = 0;
        private int ExGLeadOffCounterSize = 0;

        public String PPGSignalName = "Internal ADC A13"; //This is used to identify which signal to feed into the PPF to HR algorithm

        //Filters
        Filter NQF_Exg1Ch1;
        Filter NQF_Exg1Ch2;
        Filter NQF_Exg2Ch1;
        Filter NQF_Exg2Ch2;
        Filter LPF_PPG;
        Filter HPF_PPG;
        Filter HPF_Exg1Ch1;
        Filter HPF_Exg1Ch2;
        Filter HPF_Exg2Ch1;
        Filter HPF_Exg2Ch2;
        Filter BSF_Exg1Ch1;
        Filter BSF_Exg1Ch2;
        Filter BSF_Exg2Ch1;
        Filter BSF_Exg2Ch2;
        public bool EnableHPF_0_05HZ = false;
        public bool EnableNQF = false;
        public bool EnableHPF_0_5HZ = false;
        public bool EnableHPF_5HZ = false;
        public bool EnableBSF_49_51HZ = false;
        public bool EnableBSF_59_61HZ = false;

        private bool _firstTimeShimmer1 = false;
        private bool _firstTimeShimmer2 = false;

        private Logging _writeToFileShimmer1;
        private Logging _writeToFileShimmer2;

        private Logging _writeToFileGEMS;


        
        public Control()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            _comPort1 = comboBoxComPorts1.Text;
            _comPort2 = comboBoxComPorts2.Text;
            // btsd changes1
            ShimmerDevice1 = new ShimmerSDBT("Shimmer1", _comPort1);
            ShimmerDevice1.UICallback += this.HandleShimmerEvent;
            String[] names1 = SerialPort.GetPortNames();
            foreach (String s in names1)
            {
                comboBoxComPorts1.Items.Add(s);
            }
            comboBoxComPorts1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxComPorts1.AutoCompleteSource = AutoCompleteSource.ListItems;

            // btsd changes1
            ShimmerDevice2 = new ShimmerSDBT("Shimmer2", _comPort2);
            ShimmerDevice2.UICallback += this.HandleShimmerEvent;
            String[] names2 = SerialPort.GetPortNames();
            foreach (String s in names2)
            {
                comboBoxComPorts2.Items.Add(s);
            }
            comboBoxComPorts2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxComPorts2.AutoCompleteSource = AutoCompleteSource.ListItems;

            //List all available video sources. (That can be webcams as well as tv cards, etc)
            FilterInfoCollection videosources = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //Check if atleast one video source is available
            if (videosources != null)
            {
                //For example use first video device. You may check if this is your webcam.
                _videoSource = new VideoCaptureDevice(videosources[0].MonikerString);

                try
                {
                    //Check if the video device provides a list of supported resolutions
                    if (_videoSource.VideoCapabilities.Length > 0)
                    {
                        string highestSolution = "0;0";
                        //Search for the highest resolution
                        for (int i = 0; i < _videoSource.VideoCapabilities.Length; i++)
                        {
                            if (_videoSource.VideoCapabilities[i].FrameSize.Width >
                                Convert.ToInt32(highestSolution.Split(';')[0]))
                                highestSolution = _videoSource.VideoCapabilities[i].FrameSize.Width.ToString() + ";" +
                                                  i.ToString();
                        }
                        //Set the highest resolution as active
                        _videoSource.VideoResolution =
                            _videoSource.VideoCapabilities[Convert.ToInt32(highestSolution.Split(';')[1])];
                    }
                }
                catch (Exception exp)
                {
                    throw;
                }

                //Create NewFrame event handler
                //(This one triggers every time a new frame/image is captured
                _videoSource.NewFrame += _videoSource_NewFrame;
         

                //Start recording
                _videoSource.Start();
            }
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _configure = new Configuration(this);
            if (_configure.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseVideoSource();

            if (ShimmerDevice1 != null)
            {
                if (ShimmerDevice1.GetState() == (int)Shimmer.SHIMMER_STATE_STREAMING)
                {
                    ShimmerDevice1.StopStreaming();
                }

                ShimmerDevice1.Disconnect();
            }

            if (ShimmerDevice2 != null)
            {
                if (ShimmerDevice2.GetState() == (int)Shimmer.SHIMMER_STATE_STREAMING)
                {
                    ShimmerDevice2.StopStreaming();
                }

                ShimmerDevice2.Disconnect();
            }

            try
            {
                if (_writeToFileShimmer1 != null)
                {
                    _writeToFileShimmer1.CloseFile();
                }

                if (_writeToFileShimmer2 != null)
                {
                    _writeToFileShimmer2.CloseFile();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Control.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }

            Application.Exit();
        }

        private void comboBoxComPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }

        public void AppendTextBox(string value, string shimmername)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, string>(AppendTextBox), new object[] { value, shimmername });
                return;
            }
            if (shimmername == "Shimmer1")
            {
                textBoxShimmer1State.Text = value;
            }
            else if(shimmername == "Shimmer2")
            {
                textBoxShimmer2State.Text = value;
            }
        }

        public void ChangeStatusLabel(string text, string shimmername)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string, string>(ChangeStatusLabel), new object[] { text, shimmername });
                return;
            }

            if (shimmername == "Shimmer1")
            {
                toolStripStatusLabelShimmer1.Text = text;
            }
            else if (shimmername == "Shimmer2")
            {
                toolStripStatusLabelShimmer2.Text = text;
            }

            
        }

        private void EnableButtons(int state, string shimmername)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int, string>(EnableButtons), new object[] { state, shimmername });
                return;
            }

            if (state == (int)Shimmer.SHIMMER_STATE_CONNECTED)
            {
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;
                configureToolStripMenuItem.Enabled = true;

                if (shimmername == "Shimmer1")
                {
                    buttonStreamShimmer1.Enabled = true;
                    buttonStopShimmer1.Enabled = false;
                }
                else if (shimmername == "Shimmer2")
                {
                    buttonStreamShimmer2.Enabled = true;
                    buttonStopShimmer2.Enabled = false;
                }


            }
            // btsd changes3 start
            else if (state == (int)Shimmer.SHIMMER_STATE_CONNECTING)
            {
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = false;
                
                configureToolStripMenuItem.Enabled = false;

                if (shimmername == "Shimmer1")
                {
                    buttonStreamShimmer1.Enabled = false;
                    buttonStopShimmer1.Enabled = false;
                }
                else if (shimmername == "Shimmer2")
                {
                    buttonStreamShimmer2.Enabled = false;
                    buttonStopShimmer2.Enabled = false;
                }

            }
            // btsd changes3 end
            else if (state == (int)Shimmer.SHIMMER_STATE_NONE)
            {
                buttonConnect.Enabled = true;
                buttonDisconnect.Enabled = false;
                configureToolStripMenuItem.Enabled = false;

                if (shimmername == "Shimmer1")
                {
                    buttonStreamShimmer1.Enabled = false;
                    buttonStopShimmer1.Enabled = false;
                }
                else if (shimmername == "Shimmer2")
                {
                    buttonStreamShimmer2.Enabled = false;
                    buttonStopShimmer2.Enabled = false;
                }

            }
            else if (state == (int)Shimmer.SHIMMER_STATE_STREAMING)
            {
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;
                // btsd changes1
                configureToolStripMenuItem.Enabled = true;

                if (shimmername == "Shimmer1")
                {
                    buttonStreamShimmer1.Enabled = false;
                    buttonStopShimmer1.Enabled = true;
                }
                else if (shimmername == "Shimmer2")
                {
                    buttonStreamShimmer2.Enabled = false;
                    buttonStopShimmer2.Enabled = true;
                }
            }
        }

        public string GetLoggingFormat()
        {
            return _delimeter;
        }

        public void SetLoggingFormat(string format)
        {
            _delimeter = format;
        }

        public void EnablePPGtoHR(bool enable)
        {
            EnablePPGtoHRConversion = enable;
        }

        public bool GetEnablePPGtoHR()
        {
            return EnablePPGtoHRConversion;
        }

        public void EnableECGtoHR(bool enable)
        {
            _enableECGtoHRConversion = enable;
        }

        public bool GetEnableECGtoHR()
        {
            return _enableECGtoHRConversion;
        }

        public void UpdateChannelTextBoxes(List<Double> listofdata, string shimmernumber)
        {
            if (this.buttonConnect.InvokeRequired)  // will be in the same thread as the controls to be added
            {
                UpdateChannelsCallback d = new UpdateChannelsCallback(UpdateChannelTextBoxes);
                this.Invoke(d, listofdata, shimmernumber);
            }
            else
            {
                if (shimmernumber == "Shimmer1")
                {

                    if (ListofTextBoxShimmer1.Count == listofdata.Count)
                    {
                        for (int i = 0; i < listofdata.Count; i++)
                        {
                            ListofTextBoxShimmer1[i].Text = (Math.Truncate(listofdata[i]*100)/100).ToString();
                        }
                    }
                }
                else if (shimmernumber == "Shimmer2")
                {
                    if (ListofTextBoxShimmer2.Count == listofdata.Count)
                    {
                        for (int i = 0; i < listofdata.Count; i++)
                        {
                            ListofTextBoxShimmer2[i].Text = (Math.Truncate(listofdata[i] * 100) / 100).ToString();
                        }
                    }
                }
            }
        }

        public void ShowChannelLabels(List<String> names, string shimmernumber)
        {
            if (this.buttonConnect.InvokeRequired)  // will be in the same thread as the controls to be added
            {
                ShowChannelLabelsCallback d = new ShowChannelLabelsCallback(ShowChannelLabels);
                this.Invoke(d, names, shimmernumber);
            }
            else
            {

                if (shimmernumber == "Shimmer1")
                {
                    for (int i = 0; i < names.Count; i++)
                    {
                        System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                        lbl.Text = names[i];
                        lbl.Size = new Size(250, 17);
                        lbl.Location = new Point(50, 150 + ((ListofLabelsShimmer1.Count - 1)*22));
                        ListofLabelsShimmer1.Add(lbl);
                        this.Controls.Add(lbl);
                    }
                }
                else if (shimmernumber == "Shimmer2")
                {
                    for (int i = 0; i < names.Count; i++)
                    {
                        System.Windows.Forms.Label lbl = new System.Windows.Forms.Label();
                        lbl.Text = names[i];
                        lbl.Size = new Size(250, 17);
                        lbl.Location = new Point(450, 150 + ((ListofLabelsShimmer2.Count - 1) * 22));
                        ListofLabelsShimmer2.Add(lbl);
                        this.Controls.Add(lbl);
                    }
                }
            }
        }

        public void ShowChannelTextBoxes(int count, string shimmernumber)
        {
            if (this.buttonConnect.InvokeRequired)  // will be in the same thread as the controls to be added
            {
                ShowChannelTextBoxesCallback d = new ShowChannelTextBoxesCallback(ShowChannelTextBoxes);
                this.Invoke(d, count, shimmernumber);
            }
            else
            {
                if (shimmernumber == "Shimmer1")
                {
                    for (int i = 0; i < count; i++)
                    {
                        TextBox txtBX = new TextBox();
                        txtBX.Text = ListofTextBoxShimmer1.Count.ToString();
                        txtBX.Location = new Point(300, 150 + ((ListofTextBoxShimmer1.Count - 1)*22));
                        ListofTextBoxShimmer1.Add(txtBX);
                        this.Controls.Add(txtBX);
                    }
                }
                else if (shimmernumber == "Shimmer2")
                {
                    for (int i = 0; i < count; i++)
                    {
                        TextBox txtBX = new TextBox();
                        txtBX.Text = ListofTextBoxShimmer2.Count.ToString();
                        txtBX.Location = new Point(700, 150 + ((ListofTextBoxShimmer2.Count - 1) * 22));
                        ListofTextBoxShimmer2.Add(txtBX);
                        this.Controls.Add(txtBX);
                    }
                }
            }
        }

        private void RemoveAllTextBox(string shimmername)
        {
            if (shimmername == "Shimmer1")
            {
                foreach (TextBox tx in ListofTextBoxShimmer1)
                {
                    this.Controls.Remove(tx);
                }

                foreach (System.Windows.Forms.Label l in ListofLabelsShimmer1)
                {
                    this.Controls.Remove(l);
                }

                ListofTextBoxShimmer1.Clear();
                ListofLabelsShimmer1.Clear();
            }
            else if (shimmername == "Shimmer2")
            {
                foreach (TextBox tx in ListofTextBoxShimmer2)
                {
                    this.Controls.Remove(tx);
                }

                foreach (System.Windows.Forms.Label l in ListofLabelsShimmer2)
                {
                    this.Controls.Remove(l);
                }

                ListofTextBoxShimmer2.Clear();
                ListofLabelsShimmer2.Clear();
            }

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            //for Shimmer and ShimmerSDBT
            ShimmerDevice1.SetComPort(comboBoxComPorts1.Text);
            ShimmerDevice2.SetComPort(comboBoxComPorts2.Text);
            

            //for Shimmer32Feet and ShimmerSDBT32Feet
            //shimmer.SetAddress("00066666940E");
            bool connect1 = true; // check to connect one at a time
            bool connect2 = true; // check to connect one at a time
            

            if (ShimmerDevice1.GetState() != Shimmer.SHIMMER_STATE_CONNECTED)
            {
                if (connect1)
                {
                    ShimmerDevice1.StartConnectThread();
                    connect1 = false;
                }
            }

            
            if (ShimmerDevice2.GetState() != Shimmer.SHIMMER_STATE_CONNECTED)
            {
                if (connect2)
                {
                    ShimmerDevice2.StartConnectThread();
                    connect2 = false;
                }
            }
            
        }

        //close the device safely
        private void CloseVideoSource()
        {
            if (_videoSource != null)
                if (_videoSource.IsRunning)
                {
                    _videoSource.SignalToStop();
                    _videoSource = null;
                }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            _writer.Close();
            CloseVideoSource();

            if (ShimmerDevice1 != null)
            {
                if (ShimmerDevice1.GetState() == (int)Shimmer.SHIMMER_STATE_STREAMING)
                {
                    ShimmerDevice1.StopStreaming();
                }
                ShimmerDevice1.Disconnect();
            }

            if (ShimmerDevice2 != null)
            {
                if (ShimmerDevice2.GetState() == (int)Shimmer.SHIMMER_STATE_STREAMING)
                {
                    ShimmerDevice2.StopStreaming();
                }
                ShimmerDevice2.Disconnect();
            }

            if (_writeToFileShimmer1 != null)
            {
                _writeToFileShimmer1.CloseFile();
            }

            if (_writeToFileShimmer2 != null)
            {
                _writeToFileShimmer2.CloseFile();
            }

            buttonStreamShimmer1.Enabled = false;
            buttonStopShimmer1.Enabled = false;
            RemoveAllTextBox("Shimmer1");
            labelPRR.Visible = false;

            buttonStreamShimmer2.Enabled = false;
            buttonStopShimmer2.Enabled = false;
            RemoveAllTextBox("Shimmer2");
            labelPRR.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            if (ShimmerDevice1 != null)
            {
                if (ShimmerDevice1.GetState() == (int)Shimmer.SHIMMER_STATE_STREAMING)
                {
                    if (ShimmerDevice1.GetFirmwareIdentifier() == 3)
                    {

                    }
                    else
                    {
                        ShimmerDevice1.StopStreaming();
                    }
                }
                ShimmerDevice1.Disconnect();

            }

            if (ShimmerDevice2 != null)
            {
                if (ShimmerDevice2.GetState() == (int)Shimmer.SHIMMER_STATE_STREAMING)
                {
                    if (ShimmerDevice2.GetFirmwareIdentifier() == 3)
                    {

                    }
                    else
                    {
                        ShimmerDevice2.StopStreaming();
                    }
                }
                ShimmerDevice2.Disconnect();
            }

            if (_writeToFileShimmer1 != null)
            {
                _writeToFileShimmer1.CloseFile();
            }

            if (_writeToFileShimmer2 != null)
            {
                _writeToFileShimmer2.CloseFile();
            }
        }

        private void buttonStreamShimmer1_Click(object sender, EventArgs e)
        {
            buttonStreamShimmer1.Enabled = false;
            buttonStopShimmer1.Enabled = true;
            _firstTimeShimmer1 = true;

            //SetupFilters();

            ////ECG-HR Conversion
            if (_enableECGtoHRConversion)
            {
                ECGtoHR = new ECGToHR(ShimmerDevice1.GetSamplingRate(), TrainingPeriodECG, NumberOfHeartBeatsToAverageECG);
            }
            ExGLeadOffCounter = 0;
            ExGLeadOffCounterSize = (int)ShimmerDevice1.GetSamplingRate();
            ShimmerIdSetup.Clear();

            ShimmerDevice1.StartStreaming();
        }

        private void buttonStreamShimmer2_Click(object sender, EventArgs e)
        {
            buttonStreamShimmer2.Enabled = false;
            buttonStopShimmer2.Enabled = true;
            _firstTimeShimmer2 = true;

            //SetupFilters();

            ////ECG-HR Conversion
            if (_enableECGtoHRConversion)
            {
                ECGtoHR = new ECGToHR(ShimmerDevice2.GetSamplingRate(), TrainingPeriodECG, NumberOfHeartBeatsToAverageECG);
            }
            ExGLeadOffCounter = 0;
            ExGLeadOffCounterSize = (int)ShimmerDevice2.GetSamplingRate();
            ShimmerIdSetup.Clear();
            ShimmerDevice2.StartStreaming();

        }

        private void buttonStopShimmer1_Click(object sender, EventArgs e)
        {
            buttonStreamShimmer1.Enabled = true;
            buttonStopShimmer1.Enabled = false;
            RemoveAllTextBox("Shimmer1");
            ShimmerDevice1.StopStreaming();
        }

        private void buttonStopShimmer2_Click(object sender, EventArgs e)
        {
            buttonStreamShimmer2.Enabled = true;
            buttonStopShimmer2.Enabled = false;
            RemoveAllTextBox("Shimmer2");
            ShimmerDevice2.StopStreaming();
        }

        private void readExperimentalDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openDialog.CheckFileExists = false;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string filename1 = openDialog.FileName;
                try
                {
                    using (var sr = new StreamReader(filename1))
                    {
                        var reader = new CsvReader(sr);

                        while (true)
                        {
                            var row = reader.Read();
                            if (reader.CurrentRecord == null)
                            {
                                break;
                            }
                            _audioFiles.Add(reader.CurrentRecord[0]);
                        }
                    }


                    buttonStart.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Control.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    throw;
                }
                
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Name nameDialog = new Name();

            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (nameDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                _currentUser = nameDialog.tbName.Text;
            }
            else
            {
                _currentUser = "Cancelled";
            }

            nameDialog.Dispose();

            if (_experiment != null)
                return;

            _experiment = new Experiment(_audioFiles, this);
            _experiment.Closed += delegate
            {
                WindowState = FormWindowState.Normal;
                _experiment = null;
            };
            _experiment.NewTrack += _experiment_NewTrack;
            _experiment.Submit += _experiment_Submit;
            _experiment.AudioStopped += _experiment_AudioStopped;
            _experiment.Baseline += _experiment_Baseline;

            _experiment.Show(this);
           // WindowState = FormWindowState.Minimized;
        }

        void _experiment_Baseline(object sender, EventArgs e)
        {
            BaselineEventArgs baseLine = (BaselineEventArgs)e;

            string filename1 = "..\\" +  _currentUser + Path.GetFileName (baseLine.BaselineFile) + "_Baseline_Shimmer1.csv";
            string filename2 = "..\\" + _currentUser + Path.GetFileName(baseLine.BaselineFile) + "_Baseline_Shimmer2.csv";
            try
            {
                _writeToFileShimmer1 = new Logging(filename1, _delimeter);
                _writeToFileShimmer2 = new Logging(filename2, _delimeter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Control.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }

            ////ECG-HR Conversion
            if (_enableECGtoHRConversion)
            {
                ECGtoHR = new ECGToHR(ShimmerDevice1.GetSamplingRate(), TrainingPeriodECG, NumberOfHeartBeatsToAverageECG);
            }
            ExGLeadOffCounter = 0;
            ExGLeadOffCounterSize = (int)ShimmerDevice1.GetSamplingRate();
            ShimmerIdSetup.Clear();

            ShimmerDevice1.StartStreaming();
            ShimmerDevice2.StartStreaming();

            _writeBaseLineData = true;
        }

        void _experiment_AudioStopped(object sender, EventArgs e)
        {

            if (!_writeBaseLineData)
            {
                _writeData = false;
                _writeBaseLineData = false;
                _writer.Close();
            }

            _writeData = false;
            _writeBaseLineData = false;

            _writeToFileShimmer1.CloseFile();
            _writeToFileShimmer2.CloseFile();
        }

        void _experiment_Submit(object sender, EventArgs e)
        {
            SubmitEventArgs submit = (SubmitEventArgs)e;

            string filename = "..\\" + _currentUser + Path.GetFileName(submit.AudioFileName) + "_GEMS_AV.csv";

            try
            {
                _writeToFileGEMS = new Logging(filename, _delimeter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Control.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }

            _writeToFileGEMS.WriteGemsAVData(submit);
            _writeToFileGEMS.CloseFile();
        }

        void _experiment_NewTrack(object sender, EventArgs e)
        {
            AudioFileEventArgs audioTrack = (AudioFileEventArgs) e;

            try
            {
                // create new AVI file and open it
                //string writerfile = "..\\" + _currentUser + ".avi"
                //_writer.FrameRate = 17;
                _writer.Open("..\\" + _currentUser + Path.GetFileName(audioTrack.AudioFile) + ".avi", _videoSource.VideoResolution.FrameSize.Width, _videoSource.VideoResolution.FrameSize.Height);
                
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, Control.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }

            string filename1 = "..\\" + _currentUser + Path.GetFileName(audioTrack.AudioFile) + "_Shimmer1.csv";
            string filename2 = "..\\" + _currentUser + Path.GetFileName(audioTrack.AudioFile) + "_Shimmer2.csv";
            try
            {
                _writeToFileShimmer1 = new Logging(filename1, _delimeter);
                _writeToFileShimmer2 = new Logging(filename2, _delimeter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Control.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                throw;
            }

            ////ECG-HR Conversion
            if (_enableECGtoHRConversion)
            {
                ECGtoHR = new ECGToHR(ShimmerDevice1.GetSamplingRate(), TrainingPeriodECG, NumberOfHeartBeatsToAverageECG);
            }
            ExGLeadOffCounter = 0;
            ExGLeadOffCounterSize = (int)ShimmerDevice1.GetSamplingRate();
            ShimmerIdSetup.Clear();

            ShimmerDevice1.StartStreaming();
            ShimmerDevice2.StartStreaming();

            _writeData = true;
        }

        private void _videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {

            try
            {
                if (_writeData && _writer != null)
                {
                   
                    //_writer.AddFrame((Bitmap)eventArgs.Frame.Clone());
                    _writer.AddFrame(eventArgs.Frame);
                }
            }
            catch (Exception r)
            {
                throw;
            }

            try
            {

                if (!_writeData)
                {

                    //if (pictureBoxVideo.BackgroundImage != null && _videoSource != null)
                    //    this.Invoke(new MethodInvoker(delegate() { pictureBoxVideo.BackgroundImage.Dispose(); }));

                    Bitmap bitmap = new Bitmap((Bitmap) eventArgs.Frame.Clone(), pictureBoxVideo.Width, pictureBoxVideo.Height);

                    if (!_writeData)
                    {
                        pictureBoxVideo.BackgroundImage = bitmap;
                    }

                    if (_experiment != null)
                    {
                        if (_experiment.pictureBoxSetupCamera != null)
                        {

                            if (!_writeData)
                            {
                                _experiment.pictureBoxSetupCamera.BackgroundImage = bitmap;
                            }
                        }
                    }
                }


            }
            catch (ObjectDisposedException e)
            {
                int j = 0;
            }
            catch (Exception r)
            {
                throw;
            }

        }

        public void HandleShimmerEvent(object sender, EventArgs args)
        {
            CustomEventArgs eventArgs = (CustomEventArgs)args;
            int indicator = eventArgs.getIndicator();

            switch (indicator)
            {
                case (int)Shimmer.ShimmerIdentifier.MSG_IDENTIFIER_STATE_CHANGE:

                    System.Diagnostics.Debug.Write(((Shimmer)sender).GetDeviceName() + " State = " + ((Shimmer)sender).GetStateString() + System.Environment.NewLine);
                    int state = (int)eventArgs.getObject();
                    if (state == (int)Shimmer.SHIMMER_STATE_CONNECTED)
                    {
                        AppendTextBox("Connected", ((Shimmer)sender).GetDeviceName());
                        ChangeStatusLabel("Connected to " + ((Shimmer)sender).GetComPort() + ". Firmware Version: " + ((Shimmer)sender).GetFirmwareVersionFullName(), ((Shimmer)sender).GetDeviceName());
                        EnableButtons((int)Shimmer.SHIMMER_STATE_CONNECTED, ((Shimmer)sender).GetDeviceName());
                    }
                    else if (state == (int)Shimmer.SHIMMER_STATE_CONNECTING)
                    {
                        AppendTextBox("Connecting", ((Shimmer)sender).GetDeviceName());
                        ChangeStatusLabel("Connecting", ((Shimmer)sender).GetDeviceName());
                        EnableButtons((int)Shimmer.SHIMMER_STATE_CONNECTING, ((Shimmer)sender).GetDeviceName());

                    }
                    else if (state == (int)Shimmer.SHIMMER_STATE_NONE)
                    {
                        AppendTextBox("Disconnected", ((Shimmer)sender).GetDeviceName());
                        ChangeStatusLabel("Disconnected", ((Shimmer)sender).GetDeviceName());
                        EnableButtons((int)Shimmer.SHIMMER_STATE_NONE, ((Shimmer)sender).GetDeviceName());
                    }
                    else if (state == (int)Shimmer.SHIMMER_STATE_STREAMING)
                    {
                        AppendTextBox("Streaming", ((Shimmer)sender).GetDeviceName());
                        ChangeStatusLabel("Streaming", ((Shimmer)sender).GetDeviceName());
                        EnableButtons((int)Shimmer.SHIMMER_STATE_STREAMING, ((Shimmer)sender).GetDeviceName());
                    }
                    break;
                case (int)Shimmer.ShimmerIdentifier.MSG_IDENTIFIER_NOTIFICATION_MESSAGE:
                    string message = (string)eventArgs.getObject();
                    System.Diagnostics.Debug.Write(((Shimmer)sender).GetDeviceName() + message + System.Environment.NewLine);
                    //Message BOX
                    int minorIdentifier = eventArgs.getMinorIndication();
                    if (minorIdentifier == (int)ShimmerSDBT.ShimmerSDBTMinorIdentifier.MSG_WARNING)
                    {
                        MessageBox.Show(message, Control.ApplicationName,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (minorIdentifier == (int)ShimmerSDBT.ShimmerSDBTMinorIdentifier.MSG_ERROR)
                    {
                        MessageBox.Show(message, Control.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (message.Equals("Connection lost"))
                    {
                        MessageBox.Show("Connection with device lost while streaming", Control.ApplicationName,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        ChangeStatusLabel(message, ((Shimmer)sender).GetDeviceName());
                    }
                    break;

                case (int)Shimmer.ShimmerIdentifier.MSG_IDENTIFIER_DATA_PACKET:
                    // this is essential to ensure the object is not a reference
                    ObjectCluster objectCluster = new ObjectCluster((ObjectCluster)eventArgs.getObject());
                    List<String> names = objectCluster.GetNames();
                    List<String> formats = objectCluster.GetFormats();
                    List<String> units = objectCluster.GetUnits();
                    List<Double> data = objectCluster.GetData();

                    if (EnablePPGtoHRConversion)
                    {
                        int index = objectCluster.GetIndex(PPGSignalName, "CAL");
                        int indexts = objectCluster.GetIndex("Timestamp", "CAL");
                        if (index != -1)
                        {
                            double dataFilteredLP = LPF_PPG.filterData(data[index]);
                            double dataFilteredHP = HPF_PPG.filterData(dataFilteredLP);
                            double[] dataTS = new double[] { data[indexts] };
                            int heartRate = (int)Math.Round(PPGtoHeartRateCalculation.ppgToHrConversion(dataFilteredHP, dataTS[0]));
                            names.Add("Heart Rate PPG");
                            formats.Add("");
                            units.Add("Beats/min");
                            data.Add(heartRate);
                        }
                    }


                    if (_enableECGtoHRConversion)
                    {
                        //ECG-HR Conversion
                        int index = -1;
                        {
                            index = objectCluster.GetIndex(ECGSignalName, "RAW");
                        }
                        if (index != -1)
                        {
                            int hr = -1;
                            double ecgData = -1;
                            ecgData = data[index];
                            double calTimestamp = objectCluster.GetData("Timestamp", "CAL").GetData();
                            hr = (int)ECGtoHR.ECGToHRConversion(ecgData, calTimestamp);
                            names.Add("Heart Rate ECG");
                            formats.Add("");
                            units.Add("Beats/min");
                            data.Add(hr);
                        }
                    }

                    if (_firstTimeShimmer1 && ((Shimmer)sender).GetDeviceName() == "Shimmer1")
                    {
                        List<String> fnames = new List<String>();
                        int icount = 0;
                        foreach (String s in names)
                        {
                            fnames.Add(s + " " + formats[icount] + " (" + units[icount] + ")");
                            icount++;
                        }
                        ShowChannelLabels(fnames, ((Shimmer)sender).GetDeviceName());
                        ShowChannelTextBoxes(fnames.Count, ((Shimmer)sender).GetDeviceName());

                        ShimmerIdSetup.Add(objectCluster.GetShimmerID());

                        _firstTimeShimmer1 = false;
                    }

                    if (_firstTimeShimmer2 && ((Shimmer)sender).GetDeviceName() == "Shimmer2")
                    {
                        List<String> fnames = new List<String>();
                        int icount = 0;
                        foreach (String s in names)
                        {
                            fnames.Add(s + " " + formats[icount] + " (" + units[icount] + ")");
                            icount++;
                        }

                        ShowChannelLabels(fnames, ((Shimmer)sender).GetDeviceName());
                        ShowChannelTextBoxes(fnames.Count, ((Shimmer)sender).GetDeviceName());

                        ShimmerIdSetup.Add(objectCluster.GetShimmerID());

                        _firstTimeShimmer2 = false;
                    }

                    ShowTBcountShimmer1++;
                    if (ShowTBcountShimmer1 % Math.Truncate(ShimmerDevice1.GetSamplingRate() / 5) == 0)
                    {
                        UpdateChannelTextBoxes(data, ((Shimmer)sender).GetDeviceName());
                    }

                    ShowTBcountShimmer2++;
                    if (ShowTBcountShimmer2 % Math.Truncate(ShimmerDevice2.GetSamplingRate() / 5) == 0)
                    {
                        UpdateChannelTextBoxes(data, ((Shimmer)sender).GetDeviceName());
                    }

                    //Write to file
                    if (_writeToFileShimmer1 != null && (_writeData || _writeBaseLineData) && ((Shimmer)sender).GetDeviceName() == "Shimmer1")
                    {
                        try
                        {
                            _writeToFileShimmer1.WriteData(objectCluster);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, Control.ApplicationName,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            throw;
                        }
                    }

                    if (_writeToFileShimmer2 != null && (_writeData || _writeBaseLineData) && ((Shimmer)sender).GetDeviceName() == "Shimmer2")
                    {
                        try
                        {
                            _writeToFileShimmer2.WriteData(objectCluster);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, Control.ApplicationName,
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            throw;
                        }

                    }
                    break;

            }
        }

        private void pictureBoxVideo_Click(object sender, EventArgs e)
        {

        }
    }

}
