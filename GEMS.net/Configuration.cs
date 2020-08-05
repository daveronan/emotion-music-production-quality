using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShimmerAPI;

namespace GEMS.net
{
    public partial class Configuration : Form
    {

        public Control PControlForm;
        public byte[] ExgReg1UI = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public byte[] ExgReg2UI = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public int EnabledSensorsUI = 0;

        public Configuration()
        {
            InitializeComponent();
        }

        public Configuration(Control controlForm)
            : this()
        {
            PControlForm = controlForm;
            configurationGeneral1 = new ConfigurationGeneral(ShimmerDevice1);
            configurationGeneral2 = new ConfigurationGeneral(ShimmerDevice2);
        }

        private void Configuration_Load(object sender, EventArgs e)
        {
            if (PControlForm.ShimmerDevice1.GetState() == Shimmer.SHIMMER_STATE_STREAMING
                || PControlForm.ShimmerDevice1.GetState() == Shimmer.SHIMMER_STATE_NONE)
            {
                tabControl1.TabPages[0].Enabled = false;
                buttonApplyAll.Enabled = false;
            }
            else
            {
                buttonApplyAll.Enabled = true;
            }

            if (PControlForm.ShimmerDevice2.GetState() == Shimmer.SHIMMER_STATE_STREAMING
                || PControlForm.ShimmerDevice2.GetState() == Shimmer.SHIMMER_STATE_NONE)
            {
                tabControl1.TabPages[1].Enabled = false;
                buttonApplyAll.Enabled = false;
            }
            else
            {
                buttonApplyAll.Enabled = true;
            }

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonApplyAll_Click(object sender, EventArgs e)
        {
            buttonApplyAll.Text = "Configuring";
            buttonApplyAll.Enabled = false;
            buttonExit.Enabled = false;
            if (configurationGeneral1.comboBoxBaudRate.SelectedIndex != PControlForm.ShimmerDevice1.GetBaudRate() && PControlForm.ShimmerDevice1.GetShimmerVersion() == (int)Shimmer.ShimmerVersion.SHIMMER3)
            {
                configurationGeneral1.BaudRateChangeFlag = true;
            }
            else
            {
                configurationGeneral1.BaudRateChangeFlag = false;
            }
            configurationGeneral1.ApplyConfigurationChanges();
            if (!configurationGeneral1.BaudRateChangeFlag)
            {
                updateConfigurations();
            }


        }

        private void updateConfigurations()
        {
            buttonApplyAll.Text = "OK";
            buttonApplyAll.Enabled = true;
            buttonExit.Enabled = true;
            configurationGeneral1.BaudRateChangeFlag = false;
            PControlForm.ShimmerDevice1.ReadShimmerName();
            PControlForm.ShimmerDevice1.ReadExpID();
            PControlForm.ShimmerDevice1.ReadConfigTime();
            MessageBox.Show("Configurations changed.", ShimmerSDBT.AppNameCapture,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void configurationGeneral1_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            EnabledSensorsUI = configurationGeneral1.GetUIEnabledSensors();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
