using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShimmerAPI;

namespace GEMS.net
{
    public partial class ConfigurationGeneral : UserControl
    {
         public Configuration PConfiguration;
        public static String[] SamplingRatesStringShimmer3 = new string[] { "1Hz", "10.2Hz", "51.2Hz", "102.4Hz", "204.8Hz", "256Hz", "512Hz", "1024Hz" };
        public static String[] SamplingRatesStringShimmer2 = { "0Hz (Off)", "10.2Hz", "51.2Hz", "102.4Hz", "128Hz", "170.7Hz", "204.8Hz", "256Hz", "512Hz", "1024Hz" };
        private int ReturnEnabledSensors = 0;
        private int previousShimmerState = -1;
        public Boolean BaudRateChangeFlag = false;
        public static String ExpansionBoard = "";
        public Boolean firstConnectFlag = false;
        private static Boolean lostConnectionFlag = true;
        private ShimmerSDBT ShimmerDevice;

        public static bool usingLinux
        {
            get
            {
                int p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        public ConfigurationGeneral()
        {
            InitializeComponent();
        }

        public ConfigurationGeneral(ShimmerSDBT shimmerDevice)
        {
            ShimmerDevice = shimmerDevice;
            InitializeComponent();
        }

        private void UserControlGeneralConfig_Load(object sender, EventArgs e)
        {
            if (usingLinux)
            {
                this.Width = 880;   //+85
                this.Height = 459;  //+20
            }

            try
            {
                PConfiguration = (Configuration)this.Parent.Parent.Parent;
                
       

                    checkBoxSensor1.Text = "Accelerometer";
                    checkBoxSensor2.Text = "Gyroscope";
                    checkBoxSensor3.Text = "Magnetometer";
                    checkBoxSensor4.Text = "Battery Monitor";
                    checkBoxSensor5.Text = "ECG";
                    checkBoxSensor6.Text = "EMG";
                    checkBoxSensor7.Text = "GSR";
                    checkBoxSensor8.Text = "Exp Board ADC0";
                    checkBoxSensor9.Text = "Exp Board ADC7";
                    checkBoxSensor10.Text = "Strain Gauge";
                    checkBoxSensor11.Text = "Heart Rate";
                    checkBoxSensor11.Visible = false;
                    checkBoxSensor12.Visible = false;
                    checkBoxSensor13.Visible = false;
                    checkBoxSensor14.Visible = false;
                    checkBoxSensor15.Visible = false;
                    checkBoxSensor16.Visible = false;
                    checkBoxSensor17.Visible = false;

                    checkBoxSensor19.Visible = false;

                    checkBoxSensor18.Visible = false;
                    checkBoxSensor18.Enabled = false;
                    checkBoxSensor20.Visible = false;
                    checkBoxSensor20.Enabled = false;
                    checkBoxSensor21.Visible = false;
                    checkBoxSensor21.Enabled = false;
                    checkBoxSensor22.Visible = false;
                    checkBoxSensor22.Enabled = false;

                    checkBoxSensor4.Enabled = false;
                    labelAccelRange.Text = "Accelerometer Range";
                    comboBoxBaudRate.Items.AddRange(Shimmer.LIST_OF_BAUD_RATE);
                    comboBoxBaudRate.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBoxBaudRate.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;

                    comboBoxMagRange.Items.AddRange(Shimmer.LIST_OF_MAG_RANGE_SHIMMER2);
                    comboBoxMagRange.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBoxMagRange.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxMagRange.DropDownStyle = ComboBoxStyle.DropDownList;

                    comboBoxSamplingRate.Items.AddRange(SamplingRatesStringShimmer2);
                    comboBoxSamplingRate.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBoxSamplingRate.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxSamplingRate.DropDownStyle = ComboBoxStyle.DropDownList;

                    comboBoxAccelRange.Items.AddRange(Shimmer.LIST_OF_ACCEL_RANGE_SHIMMER2);
                    comboBoxAccelRange.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBoxAccelRange.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxAccelRange.DropDownStyle = ComboBoxStyle.DropDownList;
                    
                    comboBoxGSRRange.Items.AddRange(Shimmer.LIST_OF_GSR_RANGE_SHIMMER2);
                    comboBoxGSRRange.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBoxGSRRange.AutoCompleteSource = AutoCompleteSource.ListItems;
                    comboBoxGSRRange.DropDownStyle = ComboBoxStyle.DropDownList;
                    
                    String[] ListofECGChannels = new String[2];
                    ListofECGChannels[0] = "ECG RA LL";
                    ListofECGChannels[1] = "ECG LA LL";
                    comboBoxSelectECGChannel.Items.Clear();
                    comboBoxSelectECGChannel.Items.AddRange(ListofECGChannels);
                    comboBoxSelectECGChannel.Enabled = true;
                    comboBoxPPGAdcChannel.Enabled = false;
                    comboBoxExGResolution.Enabled = false;
                    comboBoxExgGain.Enabled = false;
                    comboBoxBaudRate.Enabled = false;
                    buttonDetectExpansionBoard.Enabled = false;
                

                checkBox5VReg.Enabled = false;
                checkBoxVoltageMon.Enabled = false;
                checkBox3DOrientation.Enabled = false;
                checkBoxGyroOnTheFly.Enabled = false;
                checkBoxLowPowerMag.Enabled = false;
                checkBoxLowPowerAccel.Enabled = false;
                checkBoxLowPowerGyro.Enabled = false;
                checkBoxIntExpPower.Enabled = false;

                comboBoxMagRange.Enabled = false;
                comboBoxAccelRange.Enabled = false;
                comboBoxGSRRange.Enabled = false;
                comboBoxGyroRange.Enabled = false;
                comboBoxPressureRes.Enabled = false;

                if (PConfiguration.PControlForm.GetLoggingFormat() == ",")
                {
                    checkBoxComma.Checked = true;
                    checkBoxTab.Checked = false;
                }
                else if (PConfiguration.PControlForm.GetLoggingFormat() == "\t")
                {
                    checkBoxComma.Checked = false;
                    checkBoxTab.Checked = true;
                }

                ConfigSetup();
            }

            catch (InvalidCastException)
            {

            }
            catch (NullReferenceException)
            {

            }
        }
        
        

        private void ConfigSetup()
        {

            double samplingRate = ShimmerDevice.GetSamplingRate();
            {
                checkBoxVoltageMon.Enabled = true;
                checkBox3DOrientation.Enabled = true;
                checkBoxGyroOnTheFly.Enabled = true;
                checkBoxLowPowerAccel.Enabled = false;
                checkBoxLowPowerGyro.Enabled = false;
                checkBoxIntExpPower.Enabled = false;
                comboBoxBaudRate.Enabled = false;
                buttonDetectExpansionBoard.Enabled = false;

                textBoxExpansionBoard.Text = "";

                if (samplingRate > 1000)
                {
                    comboBoxSamplingRate.SelectedIndex = 9;
                }
                if (samplingRate > 500)
                {
                    comboBoxSamplingRate.SelectedIndex = 8;
                }
                else if (samplingRate > 250)
                {
                    comboBoxSamplingRate.SelectedIndex = 7;
                }
                else if (samplingRate > 200)
                {
                    comboBoxSamplingRate.SelectedIndex = 6;
                }
                else if (samplingRate > 150)
                {
                    comboBoxSamplingRate.SelectedIndex = 5;
                }
                else if (samplingRate > 120)
                {
                    comboBoxSamplingRate.SelectedIndex = 4;
                }
                else if (samplingRate > 100)
                {
                    comboBoxSamplingRate.SelectedIndex = 3;
                }
                else if (samplingRate > 50)
                {
                    comboBoxSamplingRate.SelectedIndex = 2;
                }
                else if (samplingRate > 10)
                {
                    comboBoxSamplingRate.SelectedIndex = 1;
                }
                else
                {
                    comboBoxSamplingRate.SelectedIndex = 0;
                }

                checkBoxEnableECGtoHR.Checked = PConfiguration.PControlForm.GetEnableECGtoHR();
                comboBoxMagRange.SelectedIndex = ShimmerDevice.GetMagRange();
            }

            //CheckBoxes
            checkBoxLowPowerMag.Checked = ShimmerDevice.LowPowerMagEnabled;
            checkBoxLowPowerAccel.Checked = ShimmerDevice.LowPowerAccelEnabled;
            checkBoxLowPowerGyro.Checked = ShimmerDevice.LowPowerGyroEnabled;
            checkBox5VReg.Checked = ShimmerDevice.GetVReg();
            if (ShimmerDevice.GetShimmerVersion() != (int)Shimmer.ShimmerVersion.SHIMMER3)
            {
                checkBoxVoltageMon.Checked = ShimmerDevice.GetPMux();
            }
            checkBox3DOrientation.Checked = ShimmerDevice.Is3DOrientationEnabled();
            checkBoxGyroOnTheFly.Checked = ShimmerDevice.IsGyroOnTheFlyCalEnabled();

            //ComboBoxes
            comboBoxAccelRange.SelectedIndex = ShimmerDevice.GetAccelRange();
            comboBoxGSRRange.SelectedIndex = ShimmerDevice.GetGSRRange();

            checkEnabledSensors();

            if (ShimmerDevice.GetShimmerVersion() == (int)Shimmer.ShimmerVersion.SHIMMER2R)
            {
                if (checkBoxSensor5.Checked)
                {
                    groupBoxECGtoHR.Enabled = true;
                }
                else
                {
                    groupBoxECGtoHR.Enabled = false;
                }
            }

            checkBoxEnablePPGtoHR.Checked = PConfiguration.PControlForm.GetEnablePPGtoHR(); // must be called after enabled sensors

            if (ShimmerDevice.GetInternalExpPower() == 1)
            {
                checkBoxIntExpPower.Checked = true;
            }
            else
            {
                checkBoxIntExpPower.Checked = false;
            }

            if (checkBoxIntExpPower.Checked)
            {
                groupBoxPPGtoHR.Enabled = true;
            }
            else
            {
                groupBoxPPGtoHR.Enabled = false;
                checkBoxEnablePPGtoHR.Checked = false;
            }

            checkBoxEnableECGtoHR.Checked = PConfiguration.PControlForm.GetEnableECGtoHR();

        }

        private void checkEnabledSensors()
        {
            int enabledSensors = ShimmerDevice.GetEnabledSensors();
            if (ShimmerDevice.GetShimmerVersion() != (int)Shimmer.ShimmerVersion.SHIMMER3)
            {
                if (((enabledSensors & 0xFF) & (int)Shimmer.SensorBitmapShimmer2.SENSOR_ACCEL) > 0)
                {
                    checkBoxSensor1.Checked = true;
                    comboBoxAccelRange.Enabled = true;
                }
                else
                {
                    checkBoxSensor1.Checked = false;
                    comboBoxAccelRange.Enabled = false;
                }
                if (((enabledSensors & 0xFF) & (int)Shimmer.SensorBitmapShimmer2.SENSOR_GYRO) > 0)
                {
                    checkBoxSensor2.Checked = true;
                }
                else
                {
                    checkBoxSensor2.Checked = false;
                }
                if (((enabledSensors & 0xFF) & (int)Shimmer.SensorBitmapShimmer2.SENSOR_MAG) > 0)
                {
                    checkBoxSensor3.Checked = true;
                    checkBoxLowPowerMag.Enabled = true;

                    if (!ShimmerDevice.GetFirmwareVersionFullName().Equals("BoilerPlate 0.1.0"))
                    {
                        checkBoxLowPowerMag.Enabled = true;
                        comboBoxMagRange.Enabled = true;
                        comboBoxMagRange.SelectedIndex = ShimmerDevice.GetMagRange();
                    }
                    else
                    {
                        checkBoxLowPowerMag.Enabled = false;
                        comboBoxMagRange.Enabled = false;
                    }
                }
                else
                {
                    checkBoxSensor3.Checked = false;
                    checkBoxLowPowerMag.Enabled = false;
                    comboBoxMagRange.Enabled = false;
                }
                if (((enabledSensors & 0xFF) & (int)Shimmer.SensorBitmapShimmer2.SENSOR_GSR) > 0)
                {
                    checkBoxSensor7.Checked = true;
                    comboBoxGSRRange.Enabled = true;
                }
                else
                {
                    checkBoxSensor7.Checked = false;
                    comboBoxGSRRange.Enabled = false;
                }
                if (((enabledSensors & 0xFF) & (int)Shimmer.SensorBitmapShimmer2.SENSOR_ECG) > 0)
                {
                    checkBoxSensor5.Checked = true;

                    if (PConfiguration.PControlForm.ECGSignalName.Equals("ECG RA LL"))
                    {
                        comboBoxSelectECGChannel.SelectedIndex = 0;
                    }
                    else if (PConfiguration.PControlForm.ECGSignalName.Equals("ECG LA LL"))
                    {
                        comboBoxSelectECGChannel.SelectedIndex = 1;
                    }
                    else
                    {
                        comboBoxSelectECGChannel.SelectedIndex = 0;
                    }
                }
                else
                {
                    checkBoxSensor5.Checked = false;
                }
                if (((enabledSensors & 0xFF) & (int)Shimmer.SensorBitmapShimmer2.SENSOR_EMG) > 0)
                {
                    checkBoxSensor6.Checked = true;
                }
                else
                {
                    checkBoxSensor6.Checked = false;
                }
                if (((enabledSensors & 0xFF00) & (int)Shimmer.SensorBitmapShimmer2.SENSOR_STRAIN_GAUGE) > 0)
                {
                    checkBoxSensor10.Checked = true;
                    checkBox5VReg.Enabled = false;
                }
                else
                {
                    checkBoxSensor10.Checked = false;
                    checkBox5VReg.Enabled = true;
                }
                if (((enabledSensors & 0xFF00) & (int)Shimmer.SensorBitmapShimmer2.SENSOR_HEART) > 0)
                {
                    checkBoxSensor11.Checked = true;
                }
                else
                {
                    checkBoxSensor11.Checked = false;
                }
                if ((((enabledSensors & 0xFF) & (int)Shimmer.SensorBitmapShimmer2.SENSOR_EXP_BOARD_A0) > 0))  //&& getPMux() == 0
                {
                    checkBoxSensor8.Checked = true;
                }
                else
                {
                    checkBoxSensor8.Checked = false;
                }
                if ((((enabledSensors & 0xFF) & (int)Shimmer.SensorBitmapShimmer2.SENSOR_EXP_BOARD_A7) > 0))  //&& getPMux() == 0)
                {
                    checkBoxSensor9.Checked = true;
                }
                else
                {
                    checkBoxSensor9.Checked = false;
                }
                if (((enabledSensors & 0xFFFF) & (int)Shimmer.SensorBitmapShimmer3.SENSOR_VBATT) > 0)
                {
                    checkBoxSensor4.Checked = true;
                }
                else
                {
                    checkBoxSensor4.Checked = false;
                }

            }
        }

        private void checkBoxSensor1_Click(object sender, EventArgs e)
        {
            {
                if (checkBoxSensor1.Checked)
                {
                    comboBoxAccelRange.Enabled = true;
                }
                else
                {
                    comboBoxAccelRange.Enabled = false;
                    checkBox3DOrientation.Checked = false;
                }
            }
        }

        private void checkBoxSensor2_Click(object sender, EventArgs e)
        {
            {
                if (checkBoxSensor2.Checked)
                {
                    checkBoxSensor5.Checked = false;
                    checkBoxSensor6.Checked = false;
                    checkBoxSensor7.Checked = false;
                    //checkBoxSensor9.Checked = false;
                    groupBoxECGtoHR.Enabled = false;
                    checkBoxEnableECGtoHR.Checked = false;
                }
                else
                {
                    checkBox3DOrientation.Checked = false;
                }
            }
        }

        private void checkBoxSensor3_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor3.Checked)
                {
                    checkBoxSensor5.Checked = false;
                    checkBoxSensor6.Checked = false;
                    checkBoxSensor7.Checked = false;
                    //checkBoxSensor9.Checked = false;
                    if (!ShimmerDevice.GetFirmwareVersionFullName().Equals("BoilerPlate 0.1.0"))
                    {
                        comboBoxMagRange.Enabled = true;
                        checkBoxLowPowerMag.Enabled = true;
                    }
                    groupBoxECGtoHR.Enabled = false;
                    checkBoxEnableECGtoHR.Checked = false;
                }
                else
                {
                    comboBoxMagRange.Enabled = false;
                    checkBoxLowPowerMag.Enabled = false;
                    checkBox3DOrientation.Checked = false;
                }
            }
        }

        private void checkBoxSensor4_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor4.Checked)
                {

                }
                else
                {

                }
            }
        }

        private void checkBoxSensor5_Click(object sender, EventArgs e)
        {

  
            {
                if (checkBoxSensor5.Checked)
                {
                    checkBoxSensor2.Checked = false;
                    checkBoxSensor3.Checked = false;
                    checkBoxSensor6.Checked = false;
                    checkBoxSensor7.Checked = false;
                    checkBoxSensor10.Checked = false;
                    groupBoxECGtoHR.Enabled = true;
                }
                else
                {
                    groupBoxECGtoHR.Enabled = false;
                    checkBoxEnableECGtoHR.Checked = false;
                }
            }
        }

        private void checkBoxSensor6_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor6.Checked)
                {
                    checkBoxSensor2.Checked = false;
                    checkBoxSensor3.Checked = false;
                    checkBoxSensor5.Checked = false;
                    checkBoxSensor7.Checked = false;
                    //checkBoxSensor9.Checked = false;
                    checkBoxSensor10.Checked = false;
                    groupBoxECGtoHR.Enabled = false;
                    checkBoxEnableECGtoHR.Checked = false;
                }
                else
                {

                }
            }
        }

        private void checkBoxSensor7_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor7.Checked)
                {
                    comboBoxGSRRange.Enabled = true;
                    checkBoxSensor2.Checked = false;
                    checkBoxSensor3.Checked = false;
                    checkBoxSensor5.Checked = false;
                    checkBoxSensor6.Checked = false;
                    //checkBoxSensor9.Checked = false;
                    checkBoxSensor10.Checked = false;
                    groupBoxECGtoHR.Enabled = false;
                    checkBoxEnableECGtoHR.Checked = false;
                }
                else
                {
                    comboBoxGSRRange.Enabled = false;
                }
            }
        }

        private void checkBoxSensor9_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor9.Checked)
                {

                }
                else
                {

                }
            }
        }

        private void checkBoxSensor10_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor10.Checked)
                {
                    checkBoxSensor2.Checked = false;
                    checkBoxSensor3.Checked = false;
                    checkBoxSensor5.Checked = false;
                    checkBoxSensor6.Checked = false;
                    checkBoxSensor7.Checked = false;
                    checkBox5VReg.Enabled = false;
                    groupBoxECGtoHR.Enabled = false;
                    checkBoxEnableECGtoHR.Checked = false;
                }
                else
                {
                    checkBox5VReg.Enabled = true;
                }
            }
        }

        private void checkBoxSensor11_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor11.Checked)
                {

                }
                else
                {

                }
            }
        }

        private void checkBoxSensor12_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor12.Checked)
                {

                }
                else
                {

                }
            }
        }

        private void checkBoxSensor13_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor13.Checked)
                {

                }
                else
                {

                }
            }
        }

        private void checkBoxSensor14_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor14.Checked)
                {

                }
                else
                {

                }
            }
        }

        private void checkBoxSensor15_Click(object sender, EventArgs e) //ECG
        {
        }

        private void checkBoxSensor16_Click(object sender, EventArgs e) //EMG
        {

        }

        private void checkBoxSensor17_Click(object sender, EventArgs e)
        {

        }



        private void checkBoxSensor19_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor19.Checked)
                {

                }
                else
                {

                }
            }
        }

        private void checkBox3DOrientation_Click(object sender, EventArgs e)
        {

            {
                if (checkBox3DOrientation.Checked)
                {
                    checkBoxSensor1.Checked = true;
                    checkBoxSensor2.Checked = true;
                    checkBoxSensor3.Checked = true;

                    comboBoxAccelRange.Enabled = true;
                    if (!ShimmerDevice.GetFirmwareVersionFullName().Equals("BoilerPlate 0.1.0"))
                    {
                        comboBoxMagRange.Enabled = true;
                    }
                    checkBoxLowPowerMag.Enabled = true;
                }
                else
                {

                }
            }
        }

        private void checkBoxGyroOnTheFly_Click(object sender, EventArgs e)
        {

            {
                if (checkBoxGyroOnTheFly.Checked)
                {
                    checkBoxSensor2.Checked = true;
                }
                else
                {

                }
            }
        }

        private void checkBox5VReg_Click(object sender, EventArgs e)
        {
            if (ShimmerDevice.GetShimmerVersion() != (int)Shimmer.ShimmerVersion.SHIMMER3)
            {
                if (checkBox5VReg.Checked)
                {
                    checkBoxSensor9.Checked = false;
                }
                else
                {

                }
            }
        }

        private void checkBoxIntExpPower_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxComma_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxComma.Checked)
            {
                checkBoxTab.Checked = false;
            }
        }

        private void checkBoxTab_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTab.Checked)
            {
                checkBoxComma.Checked = false;
            }
        }

        private void buttonToggleLED_Click(object sender, EventArgs e)
        {
            ShimmerDevice.ToggleLED();
        }

        private void buttonDetectExpansionBoard_Click(object sender, EventArgs e)
        {
            if (ShimmerDevice.GetShimmerVersion() == 3 && ShimmerDevice.GetCompatibilityCode() >= 4)
            {
                ShimmerDevice.ReadExpansionBoard();
                ExpansionBoard = ShimmerDevice.GetExpansionBoard();
                textBoxExpansionBoard.Text = ExpansionBoard;
            }
        }

        public void ApplyConfigurationChanges()
        {
            int selectedIndexSamplingRate = comboBoxSamplingRate.SelectedIndex;
            double samplingRate = -1;
            {
                switch (selectedIndexSamplingRate)
                {
                    case 0:
                        samplingRate = 0;
                        break;
                    case 1:
                        samplingRate = 10.2;
                        break;
                    case 2:
                        samplingRate = 51.2;
                        break;
                    case 3:
                        samplingRate = 102.4;
                        break;
                    case 4:
                        samplingRate = 128;
                        break;
                    case 5:
                        samplingRate = 170.7;
                        break;
                    case 6:
                        samplingRate = 204.8;
                        break;
                    case 7:
                        samplingRate = 256;
                        break;
                    case 8:
                        samplingRate = 512;
                        break;
                    case 9:
                        samplingRate = 1024;
                        break;
                }
                if (checkBox5VReg.Checked)
                {
                    ShimmerDevice.Write5VReg(1);
                }
                else
                {
                    ShimmerDevice.Write5VReg(0);
                }
                if (checkBoxVoltageMon.Checked)
                {
                    ShimmerDevice.WritePMux(1);
                }
                else
                {
                    ShimmerDevice.WritePMux(0);
                }
                if (!ShimmerDevice.GetFirmwareVersionFullName().Equals("BoilerPlate 0.1.0"))
                {
                    ShimmerDevice.WriteMagRange(comboBoxMagRange.SelectedIndex);
                }

                if (checkBoxSensor5.Checked)
                {
                    PConfiguration.PControlForm.ECGSignalName = comboBoxSelectECGChannel.Text;
                    PConfiguration.PControlForm.EnableECGtoHR(checkBoxEnableECGtoHR.Checked);
                }
                else
                {
                    PConfiguration.PControlForm.EnableECGtoHR(false);
                }
            }
            ShimmerDevice.WriteSamplingRate(samplingRate);
            ShimmerDevice.SetLowPowerMag(checkBoxLowPowerMag.Checked);
            ShimmerDevice.SetLowPowerAccel(checkBoxLowPowerAccel.Checked);
            ShimmerDevice.SetLowPowerGyro(checkBoxLowPowerGyro.Checked);
            ShimmerDevice.Set3DOrientation(checkBox3DOrientation.Checked);
            ShimmerDevice.SetGyroOnTheFlyCalibration(checkBoxGyroOnTheFly.Checked, 100, 1.2);
            ShimmerDevice.WriteAccelRange(comboBoxAccelRange.SelectedIndex);
            ShimmerDevice.WriteGSRRange(comboBoxGSRRange.SelectedIndex);


            if (checkBoxComma.Checked)
            {
                PConfiguration.PControlForm.SetLoggingFormat(",");
            }
            else
            {
                PConfiguration.PControlForm.SetLoggingFormat("\t");
            }


            EnableSensors();    //Called last

            if (BaudRateChangeFlag)
            { // to change baud rate, must disconnect then reconnect to the Shimmer
                ShimmerDevice.Disconnect();
                System.Threading.Thread.Sleep(200);
                ShimmerDevice.StartConnectThread();
            }
        }

        private void EnableSensors()
        {

            {
                if (checkBoxSensor1.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_ACCEL;
                }
                if (checkBoxSensor2.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_GYRO;
                }
                if (checkBoxSensor3.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_MAG;
                }
                if (checkBoxSensor4.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)Shimmer.SensorBitmapShimmer3.SENSOR_VBATT;
                }
                if (checkBoxSensor5.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_ECG;
                }
                if (checkBoxSensor6.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_EMG;
                }
                if (checkBoxSensor7.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_GSR;
                }
                if (checkBoxSensor8.Checked && !checkBoxVoltageMon.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)ShimmerBluetooth.SensorBitmapShimmer2.SENSOR_EXP_BOARD_A0;
                }

                if (checkBoxSensor9.Checked && !checkBoxVoltageMon.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)ShimmerBluetooth.SensorBitmapShimmer2.SENSOR_EXP_BOARD_A7;
                }

                if (checkBoxVoltageMon.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)ShimmerBluetooth.SensorBitmapShimmer2.SENSOR_VBATT;
                }
                if (checkBoxSensor10.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_STRAIN_GAUGE;
                }
                if (checkBoxSensor11.Checked)
                {
                    ReturnEnabledSensors = ReturnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_HEART;
                }
            }

            ShimmerDevice.WriteSensors(ReturnEnabledSensors);
            ReturnEnabledSensors = 0;
        }

        public int GetUIEnabledSensors()
        {
            int returnEnabledSensors = 0;
            {
                if (checkBoxSensor1.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_ACCEL;
                }
                if (checkBoxSensor2.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_GYRO;
                }
                if (checkBoxSensor3.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_MAG;
                }
                if (checkBoxSensor4.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)Shimmer.SensorBitmapShimmer3.SENSOR_VBATT;
                }
                if (checkBoxSensor5.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_ECG;
                }
                if (checkBoxSensor6.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_EMG;
                }
                if (checkBoxSensor7.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_GSR;
                }
                if (checkBoxSensor8.Checked && !checkBoxVoltageMon.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)ShimmerBluetooth.SensorBitmapShimmer2.SENSOR_EXP_BOARD_A0;
                }

                if (checkBoxSensor9.Checked && !checkBoxVoltageMon.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)ShimmerBluetooth.SensorBitmapShimmer2.SENSOR_EXP_BOARD_A7;
                }

                if (checkBoxVoltageMon.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)ShimmerBluetooth.SensorBitmapShimmer2.SENSOR_VBATT;
                }
                if (checkBoxSensor10.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_STRAIN_GAUGE;
                }
                if (checkBoxSensor11.Checked)
                {
                    returnEnabledSensors = returnEnabledSensors | (int)Shimmer.SensorBitmapShimmer2.SENSOR_HEART;
                }
            }

            return returnEnabledSensors;

        }

        private void numericUpDownBeatsToAve_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBoxSettings_Enter(object sender, EventArgs e)
        {

        }

        private void comboBoxSamplingRate_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            buttonApply.Text = "Applying";
            buttonApply.Enabled = false;

            {
                BaudRateChangeFlag = false;
            }
            ApplyConfigurationChanges();

            if (!BaudRateChangeFlag)
            {
                updateConfigurations();
            }
            else
            {
               firstConnectFlag = false; 
               this.ParentForm.Close();
            }
        }

        private void updateConfigurations()
        {
            if (ShimmerDevice.GetFirmwareIdentifier() == 3)
            {
                ShimmerDevice.SetConfigTime(Convert.ToInt32(ShimmerDevice.SystemTime2Config()));
                ShimmerDevice.WriteConfigTime();
                //ShimmerDevice.WriteSdConfigFile();
            }

            EnableButtons(Shimmer.SHIMMER_STATE_CONNECTED);

            //if (ShimmerDevice.Is3DOrientationEnabled())
            //{
            //    PConfiguration.PControlForm.ToolStripMenuItemShow3DOrientation.Enabled = true;
            //}
            //else
            //{
            //    PConfiguration.PControlForm.ToolStripMenuItemShow3DOrientation.Enabled = false;
            //}
            BaudRateChangeFlag = false;
            PConfiguration.PControlForm.AppendTextBox("Configuration done.", "ddd");
            MessageBox.Show("Configurations changed.", ShimmerSDBT.AppNameCapture,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EnableButtons(int state)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<int>(EnableButtons), new object[] { state });
                return;
            }

            if (state == (int)Shimmer.SHIMMER_STATE_CONNECTED)
            {
                buttonApply.Text = "Apply";
                buttonApply.Enabled = true;
            }

            else
            {
                
            }
        }

        private void checkBoxVoltageMon_CheckedChanged_1(object sender, EventArgs e)
        {

            if (ShimmerDevice.GetShimmerVersion() == (int)ShimmerBluetooth.ShimmerVersion.SHIMMER2R && checkBoxVoltageMon.Checked)
            {
                checkBoxSensor8.Text = "VSenseReg";
                checkBoxSensor9.Text = "VSenseBatt";
                checkBoxSensor8.Enabled = false;
                checkBoxSensor9.Enabled = false;
                checkBoxSensor8.Checked = true;
                checkBoxSensor9.Checked = true;
            }
            else
            {
                if (ShimmerDevice.GetShimmerVersion() == (int)ShimmerBluetooth.ShimmerVersion.SHIMMER2R)
                {
                    checkBoxSensor8.Text = "Exp Board ADC0";
                    checkBoxSensor9.Text = "Exp Board ADC7";
                    checkBoxSensor8.Enabled = true;
                    checkBoxSensor9.Enabled = true;

                }
            }
        }

        private void labelAccelRange_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxPPGAdcChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPPGAdcChannel.SelectedIndex == 0)
            {
                checkBoxSensor9.Checked = true;
            }
            else if (comboBoxPPGAdcChannel.SelectedIndex == 1)
            {
                checkBoxSensor10.Checked = true;
            }
            else if (comboBoxPPGAdcChannel.SelectedIndex == 2)
            {
                checkBoxSensor11.Checked = true;
            }
            else if (comboBoxPPGAdcChannel.SelectedIndex == 3)
            {
                checkBoxSensor12.Checked = true;
            }
        }

        private void checkBoxEnablePPGtoHR_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnablePPGtoHR.Checked)
            {
                if (checkBoxSensor9.Checked || checkBoxSensor10.Checked || checkBoxSensor11.Checked || checkBoxSensor12.Checked)
                {
                    checkBoxEnablePPGtoHR.Checked = true;
                }
                if (!checkBoxSensor9.Checked && !checkBoxSensor10.Checked && !checkBoxSensor11.Checked && !checkBoxSensor12.Checked)
                {
                    MessageBox.Show("Please enable either Sensor Int A1/A12/A13/A14", Control.ApplicationName,
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void checkBoxSensor9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxSensor10_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxSensor11_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxSensor12_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxEnableECGtoHR_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (!ShimmerDevice.IsDefaultECGConfigurationEnabled())
            {
                MessageBox.Show("Please set exg configuration to ECG", Control.ApplicationName,
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            */
        }

        private void checkBoxIntExpPower_CheckedChanged(object sender, EventArgs e)
        {
            if ((checkBoxSensor9.Checked == false) && (checkBoxSensor10.Checked == false) && (checkBoxSensor11.Checked == false) && (checkBoxSensor12.Checked == false) && (checkBoxIntExpPower.Checked == true))
            {
                MessageBox.Show("Internal Exp Power is enabled but no internal ADC has been enabled. Disable Internal Exp Power to conserve battery.", Control.ApplicationName,
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (checkBoxIntExpPower.Checked)
                {
                    groupBoxPPGtoHR.Enabled = true;
                }
                else
                {
                    groupBoxPPGtoHR.Enabled = false;
                    checkBoxEnablePPGtoHR.Checked = false;
                }
            }
        }

        private void checkBoxSensor14_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxSensor15_CheckedChanged(object sender, EventArgs e)
        {

            {
                if (checkBoxSensor15.Checked)
                {

                }
                else
                {

                }
            }
        }

        private void comboBoxExGResolution_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxExgGain_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] exg1Reg = PConfiguration.ExgReg1UI;
            byte[] exg2Reg = PConfiguration.ExgReg2UI;

            if (comboBoxExgGain.SelectedIndex != 7) // if not custom, adjust accordingly otherwise ignore
            {
                int gain = (int)Double.Parse(PConfiguration.configurationGeneral1.comboBoxExgGain.SelectedItem.ToString());
                int gainSetting = ConvertEXGGainValuetoSetting(gain);
                byte byte4exg1 = (byte)((exg1Reg[3] & 143) | (gainSetting << 4));
                byte byte5exg1 = (byte)((exg1Reg[4] & 143) | (gainSetting << 4));
                byte byte4exg2 = (byte)((exg2Reg[3] & 143) | (gainSetting << 4));
                byte byte5exg2 = (byte)((exg2Reg[4] & 143) | (gainSetting << 4));
                exg1Reg[3] = byte4exg1;
                exg1Reg[4] = byte5exg1;
                exg2Reg[3] = byte4exg2;
                exg2Reg[4] = byte5exg2;
                PConfiguration.ExgReg1UI = exg1Reg;
                PConfiguration.ExgReg2UI = exg2Reg;
            }

        }


        private void checkBoxSensor16_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (checkBoxSensor16.Checked)
                {

                }
                else
                {

                }
            }
        }

        private void checkBoxSensor17_CheckedChanged(object sender, EventArgs e)
        {


            {
                if (checkBoxSensor17.Checked)
                {

                }
                else
                {

                }
            }
        }

        protected int ConvertEXGGainSettingToValue(int setting)
        {
            if (setting == 0)
            {
                return 6;
            }
            else if (setting == 1)
            {
                return 1;
            }
            else if (setting == 2)
            {
                return 2;
            }
            else if (setting == 3)
            {
                return 3;
            }
            else if (setting == 4)
            {
                return 4;
            }
            else if (setting == 5)
            {
                return 8;
            }
            else if (setting == 6)
            {
                return 12;
            }
            else
            {
                return -1; // -1 means invalid value
            }
        }

        private void checkBoxSensor18_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSensor18.Checked)
            {

                checkBoxSensor21.Checked = false;
                checkBoxSensor22.Checked = false;


                byte[] exg1Reg = PConfiguration.ExgReg1UI;
                byte[] exg2Reg = PConfiguration.ExgReg2UI;
                string sr = PConfiguration.configurationGeneral1.comboBoxSamplingRate.SelectedItem.ToString();
                string subsr = sr.Substring(0, sr.Length - 2);
                double samplingRate = Double.Parse(subsr);

                int exgSR = getExGSamplingSetting(samplingRate);

                //adjust for sampling rate
                exg1Reg[0] = (byte)(((exg1Reg[0] >> 3) << 3) | exgSR);
                exg2Reg[0] = (byte)(((exg2Reg[0] >> 3) << 3) | exgSR);

                if (comboBoxExgGain.SelectedIndex != 7) // if not custom, adjust accordingly otherwise ignore
                {
                    int gain = (int)Double.Parse(PConfiguration.configurationGeneral1.comboBoxExgGain.SelectedItem.ToString());
                    int gainSetting = ConvertEXGGainValuetoSetting(gain);
                    byte byte4exg1 = (byte)((exg1Reg[3] & 143) | (gainSetting << 4));
                    byte byte5exg1 = (byte)((exg1Reg[4] & 143) | (gainSetting << 4));
                    byte byte4exg2 = (byte)((exg2Reg[3] & 143) | (gainSetting << 4));
                    byte byte5exg2 = (byte)((exg2Reg[4] & 143) | (gainSetting << 4));
                    exg1Reg[3] = byte4exg1;
                    exg1Reg[4] = byte5exg1;
                    exg2Reg[3] = byte4exg2;
                    exg2Reg[4] = byte5exg2;
                }
                else
                {
                    byte[] reg1 = PConfiguration.ExgReg1UI;
                    byte[] reg2 = PConfiguration.ExgReg2UI;
                    byte byte4exg1 = (byte)((exg1Reg[3] & 143) | (reg1[3] & 112));
                    byte byte5exg1 = (byte)((exg1Reg[4] & 143) | (reg1[4] & 112));
                    byte byte4exg2 = (byte)((exg2Reg[3] & 143) | (reg2[3] & 112));
                    byte byte5exg2 = (byte)((exg2Reg[4] & 143) | (reg2[4] & 112));
                    exg1Reg[3] = byte4exg1;
                    exg1Reg[4] = byte5exg1;
                    exg2Reg[3] = byte4exg2;
                    exg2Reg[4] = byte5exg2;
                }
                PConfiguration.ExgReg1UI = exg1Reg;
                PConfiguration.ExgReg2UI = exg2Reg;
            }
        }

        private void checkBoxSensor20_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSensor20.Checked)
            {

                checkBoxSensor21.Checked = false;
                checkBoxSensor22.Checked = false;

                byte[] exg1Reg = PConfiguration.ExgReg1UI;
                byte[] exg2Reg = PConfiguration.ExgReg2UI;
                string sr = PConfiguration.configurationGeneral1.comboBoxSamplingRate.SelectedItem.ToString();
                string subsr = sr.Substring(0, sr.Length - 2);
                double samplingRate = Double.Parse(subsr);

                int exgSR = getExGSamplingSetting(samplingRate);

                //adjust for sampling rate
                exg1Reg[0] = (byte)(((exg1Reg[0] >> 3) << 3) | exgSR);
                exg2Reg[0] = (byte)(((exg2Reg[0] >> 3) << 3) | exgSR);

                if (comboBoxExgGain.SelectedIndex != 7) // if not custom, adjust accordingly otherwise ignore
                {
                    int gain = (int)Double.Parse(PConfiguration.configurationGeneral1.comboBoxExgGain.SelectedItem.ToString());
                    int gainSetting = ConvertEXGGainValuetoSetting(gain);
                    byte byte4exg1 = (byte)((exg1Reg[3] & 143) | (gainSetting << 4));
                    byte byte5exg1 = (byte)((exg1Reg[4] & 143) | (gainSetting << 4));
                    byte byte4exg2 = (byte)((exg2Reg[3] & 143) | (gainSetting << 4));
                    byte byte5exg2 = (byte)((exg2Reg[4] & 143) | (gainSetting << 4));
                    exg1Reg[3] = byte4exg1;
                    exg1Reg[4] = byte5exg1;
                    exg2Reg[3] = byte4exg2;
                    exg2Reg[4] = byte5exg2;
                }
                else
                {
                    byte[] reg1 = PConfiguration.ExgReg1UI;
                    byte[] reg2 = PConfiguration.ExgReg2UI;
                    byte byte4exg1 = (byte)((exg1Reg[3] & 143) | (reg1[3] & 112));
                    byte byte5exg1 = (byte)((exg1Reg[4] & 143) | (reg1[4] & 112));
                    byte byte4exg2 = (byte)((exg2Reg[3] & 143) | (reg2[3] & 112));
                    byte byte5exg2 = (byte)((exg2Reg[4] & 143) | (reg2[4] & 112));
                    exg1Reg[3] = byte4exg1;
                    exg1Reg[4] = byte5exg1;
                    exg2Reg[3] = byte4exg2;
                    exg2Reg[4] = byte5exg2;
                }
                PConfiguration.ExgReg1UI = exg1Reg;
                PConfiguration.ExgReg2UI = exg2Reg;
            }
        }

        private void checkBoxSensor21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSensor21.Checked)
            {

                checkBoxSensor18.Checked = false;
                checkBoxSensor20.Checked = false;

                byte[] exg1Reg = PConfiguration.ExgReg1UI;
                byte[] exg2Reg = PConfiguration.ExgReg2UI;
                string sr = PConfiguration.configurationGeneral1.comboBoxSamplingRate.SelectedItem.ToString();
                string subsr = sr.Substring(0, sr.Length - 2);
                double samplingRate = Double.Parse(subsr);

                int exgSR = getExGSamplingSetting(samplingRate);

                //adjust for sampling rate
                exg1Reg[0] = (byte)(((exg1Reg[0] >> 3) << 3) | exgSR);
                exg2Reg[0] = (byte)(((exg2Reg[0] >> 3) << 3) | exgSR);

                if (comboBoxExgGain.SelectedIndex != 7) // if not custom, adjust accordingly otherwise ignore
                {
                    int gain = (int)Double.Parse(PConfiguration.configurationGeneral1.comboBoxExgGain.SelectedItem.ToString());
                    int gainSetting = ConvertEXGGainValuetoSetting(gain);
                    byte byte4exg1 = (byte)((exg1Reg[3] & 143) | (gainSetting << 4));
                    byte byte5exg1 = (byte)((exg1Reg[4] & 143) | (gainSetting << 4));
                    byte byte4exg2 = (byte)((exg2Reg[3] & 143) | (gainSetting << 4));
                    byte byte5exg2 = (byte)((exg2Reg[4] & 143) | (gainSetting << 4));
                    exg1Reg[3] = byte4exg1;
                    exg1Reg[4] = byte5exg1;
                    exg2Reg[3] = byte4exg2;
                    exg2Reg[4] = byte5exg2;
                }
                else
                {
                    byte[] reg1 = PConfiguration.ExgReg1UI;
                    byte[] reg2 = PConfiguration.ExgReg2UI;
                    byte byte4exg1 = (byte)((exg1Reg[3] & 143) | (reg1[3] & 112));
                    byte byte5exg1 = (byte)((exg1Reg[4] & 143) | (reg1[4] & 112));
                    byte byte4exg2 = (byte)((exg2Reg[3] & 143) | (reg2[3] & 112));
                    byte byte5exg2 = (byte)((exg2Reg[4] & 143) | (reg2[4] & 112));
                    exg1Reg[3] = byte4exg1;
                    exg1Reg[4] = byte5exg1;
                    exg2Reg[3] = byte4exg2;
                    exg2Reg[4] = byte5exg2;
                }
                PConfiguration.ExgReg1UI = exg1Reg;
                PConfiguration.ExgReg2UI = exg2Reg;
            }
        }

        private void checkBoxSensor22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSensor22.Checked)
            {

                checkBoxSensor18.Checked = false;
                checkBoxSensor20.Checked = false;

                byte[] exg1Reg = PConfiguration.ExgReg1UI;
                byte[] exg2Reg = PConfiguration.ExgReg2UI;
                string sr = PConfiguration.configurationGeneral1.comboBoxSamplingRate.SelectedItem.ToString();
                string subsr = sr.Substring(0, sr.Length - 2);
                double samplingRate = Double.Parse(subsr);

                int exgSR = getExGSamplingSetting(samplingRate);

                //adjust for sampling rate
                exg1Reg[0] = (byte)(((exg1Reg[0] >> 3) << 3) | exgSR);
                exg2Reg[0] = (byte)(((exg2Reg[0] >> 3) << 3) | exgSR);

                if (comboBoxExgGain.SelectedIndex != 7) // if not custom, adjust accordingly otherwise ignore
                {
                    int gain = (int)Double.Parse(PConfiguration.configurationGeneral1.comboBoxExgGain.SelectedItem.ToString());
                    int gainSetting = ConvertEXGGainValuetoSetting(gain);
                    byte byte4exg1 = (byte)((exg1Reg[3] & 143) | (gainSetting << 4));
                    byte byte5exg1 = (byte)((exg1Reg[4] & 143) | (gainSetting << 4));
                    byte byte4exg2 = (byte)((exg2Reg[3] & 143) | (gainSetting << 4));
                    byte byte5exg2 = (byte)((exg2Reg[4] & 143) | (gainSetting << 4));
                    exg1Reg[3] = byte4exg1;
                    exg1Reg[4] = byte5exg1;
                    exg2Reg[3] = byte4exg2;
                    exg2Reg[4] = byte5exg2;
                }
                PConfiguration.ExgReg1UI = exg1Reg;
                PConfiguration.ExgReg2UI = exg2Reg;
            }
        }

        /// <summary>
        /// This can be used to check the registers on the ExG Daughter board and determine whether it is using default ECG configurations
        /// </summary>
        /// <returns>Returns true if defaul ECG configurations is being used</returns>
        public bool IsDefaultExgTestSignalConfigurationEnabled(byte[] Exg1RegArray, byte[] Exg2RegArray)
        {
            bool isUsing = false;
            if (((Exg1RegArray[3] & 15) == 5) && ((Exg1RegArray[4] & 15) == 5) && ((Exg2RegArray[3] & 15) == 5) && ((Exg2RegArray[4] & 15) == 5))
            {
                isUsing = true;
            }
            return isUsing;
        }

        /// <summary>
        /// This can be used to check the registers on the ExG Daughter board and determine whether it is using default ECG configurations
        /// </summary>
        /// <returns>Returns true if defaul ECG configurations is being used</returns>
        public bool IsDefaultECGConfigurationEnabled(byte[] Exg1RegArray, byte[] Exg2RegArray)
        {
            bool isUsing = false;
            if (((Exg1RegArray[3] & 15) == 0) && ((Exg1RegArray[4] & 15) == 0) && ((Exg2RegArray[3] & 15) == 0) && ((Exg2RegArray[4] & 15) == 7))
            {
                isUsing = true;
            }
            return isUsing;
        }
        /// <summary>
        /// This can be used to check the registers on the ExG Daughter board and determine whether it is using default EMG configurations
        /// </summary>
        /// <returns>Returns true if defaul EMG configurations is being used</returns>
        public bool IsDefaultEMGConfigurationEnabled(byte[] Exg1RegArray, byte[] Exg2RegArray)
        {
            bool isUsing = false;
            if (((Exg1RegArray[3] & 15) == 9) && ((Exg1RegArray[4] & 15) == 0) && ((Exg2RegArray[3] & 15) == 1) && ((Exg2RegArray[4] & 15) == 1))
            {
                isUsing = true;
            }

            return isUsing;
        }

        public int ConvertEXGGainValuetoSetting(int value)
        {
            if (value == 6)
            {
                return 0;
            }
            else if (value == 1)
            {
                return 1;
            }
            else if (value == 2)
            {
                return 2;
            }
            else if (value == 3)
            {
                return 3;
            }
            else if (value == 4)
            {
                return 4;
            }
            else if (value == 8)
            {
                return 5;
            }
            else if (value == 12)
            {
                return 6;
            }
            else
            {
                return -1; // -1 means invalid value
            }
        }

        public int getExGSamplingSetting(double samplingRate)
        {
            if (samplingRate < 125)
            {
                return 0;
            }
            else if (samplingRate < 250)
            {
                return 1;
            }
            else if (samplingRate < 500)
            {
                return 2;
            }
            else if (samplingRate < 1000)
            {
                return 3;
            }
            else if (samplingRate < 2000)
            {
                return 4;
            }
            else if (samplingRate < 4000)
            {
                return 5;
            }
            else if (samplingRate < 8000)
            {
                return 6;
            }
            return -1;
        }

        //this just updates the UI and the UI click/checks shouldnt affect the settings
        public void ForceExGConfigurationUpdate(byte[] reg1, byte[] reg2)
        {
            checkBoxSensor18.Visible = false;
            checkBoxSensor18.Enabled = false;
            checkBoxSensor20.Visible = false;
            checkBoxSensor20.Enabled = false;
            checkBoxSensor21.Visible = false;
            checkBoxSensor21.Enabled = false;
            checkBoxSensor22.Visible = false;
            checkBoxSensor22.Enabled = false;
            int enabledSensors = PConfiguration.EnabledSensorsUI;
            if (IsDefaultExgTestSignalConfigurationEnabled(reg1, reg2) && (((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG1_16BIT) > 0) && (((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG2_16BIT) > 0)) || (((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG1_24BIT) > 0) && ((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG2_24BIT) > 0))))
            {
                checkBoxSensor17.Checked = true;
            }
            else if (IsDefaultEMGConfigurationEnabled(reg1, reg2) && (((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG1_16BIT) > 0) && (((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG2_16BIT) == 0)) || (((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG1_24BIT) > 0) && ((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG2_24BIT) == 0))))
            {
                checkBoxSensor16.Checked = true;
            }
            else if (IsDefaultECGConfigurationEnabled(reg1, reg2) && (((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG1_16BIT) > 0) && (((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG2_16BIT) > 0)) || (((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG1_24BIT) > 0) && ((enabledSensors & (int)Shimmer.SensorBitmapShimmer3.SENSOR_EXG2_24BIT) > 0))))
            {
                checkBoxSensor15.Checked = true;
            }
            else
            {

            }

            int gainexg1ch1 = ConvertEXGGainSettingToValue((reg1[3] >> 4) & 7);
            int gainexg1ch2 = ConvertEXGGainSettingToValue((reg1[4] >> 4) & 7);
            int gainexg2ch1 = ConvertEXGGainSettingToValue((reg2[3] >> 4) & 7);
            int gainexg2ch2 = ConvertEXGGainSettingToValue((reg2[4] >> 4) & 7);

            this.comboBoxExgGain.SelectedIndexChanged -= new System.EventHandler(this.comboBoxExgGain_SelectedIndexChanged);
            if (ShimmerDevice.GetShimmerVersion() == (int)Shimmer.ShimmerVersion.SHIMMER3)
            {
                //if the gain is the same
                if (gainexg1ch1 == gainexg1ch2 && gainexg2ch1 == gainexg2ch2 && gainexg1ch1 == gainexg2ch1)
                {
                    if (gainexg1ch1 == 1)
                    {
                        comboBoxExgGain.SelectedIndex = 0;
                    }
                    else if (gainexg1ch1 == 2)
                    {
                        comboBoxExgGain.SelectedIndex = 1;
                    }
                    else if (gainexg1ch1 == 3)
                    {
                        comboBoxExgGain.SelectedIndex = 2;
                    }
                    else if (gainexg1ch1 == 4)
                    {
                        comboBoxExgGain.SelectedIndex = 3;
                    }
                    else if (gainexg1ch1 == 6)
                    {
                        comboBoxExgGain.SelectedIndex = 4;
                    }
                    else if (gainexg1ch1 == 8)
                    {
                        comboBoxExgGain.SelectedIndex = 5;
                    }
                    else if (gainexg1ch1 == 12)
                    {
                        comboBoxExgGain.SelectedIndex = 6;
                    }
                    if (comboBoxExgGain.Items.Count == 8)
                    {
                        comboBoxExgGain.Items.RemoveAt(7);
                    }
                }
                else
                {

                }

            }
            this.comboBoxExgGain.SelectedIndexChanged += new System.EventHandler(this.comboBoxExgGain_SelectedIndexChanged);
        }

        private void comboBoxBaudRate_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (comboBoxBaudRate.SelectedIndex != ShimmerDevice.GetBaudRate())
            {
                BaudRateChangeFlag = true;
            }
            else
            {
                BaudRateChangeFlag = false;
            }

        }

        public void HandleEvent(object sender, EventArgs args) 
        {

            CustomEventArgs eventArgs = (CustomEventArgs)args;
            int indicator = eventArgs.getIndicator();
            switch (indicator)
            {
                case (int)Shimmer.ShimmerIdentifier.MSG_IDENTIFIER_STATE_CHANGE:
                    
                    int state = (int)eventArgs.getObject();
                    if (state == (int)Shimmer.SHIMMER_STATE_NONE)
                    {
                        if ((previousShimmerState == (int)Shimmer.SHIMMER_STATE_CONNECTING) && lostConnectionFlag && BaudRateChangeFlag)
                        {
                            lostConnectionFlag = false;                    
                            MessageBox.Show("Connection Lost with Shimmer while changing baud rate", ShimmerSDBT.AppNameCapture,
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            System.Threading.Thread.Sleep(200);
                        }
                        if (BaudRateChangeFlag)
                        {
                            firstConnectFlag = true;
                        }
                    }
                    else if (state == (int)Shimmer.SHIMMER_STATE_CONNECTING)
                    {
                        
                    }
                    else if (state == (int)Shimmer.SHIMMER_STATE_CONNECTED)
                    {
                        lostConnectionFlag = true;
                    }
                    else // streaming
                    {
                        
                    }

                    previousShimmerState = state;

                break;

            }

        }
    }
}
