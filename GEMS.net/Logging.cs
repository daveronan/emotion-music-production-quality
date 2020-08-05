using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using GEMS.net;

namespace ShimmerAPI
{
    class Logging
    {
        private StreamWriter PCsvFile = null;
        private String FileName;
        private String Delimeter = ",";
        private Boolean FirstWrite = true;

        public Logging(String fileName, String delimeter){
            Delimeter = delimeter;
            FileName = fileName;
            try
            {
                PCsvFile = new StreamWriter(FileName, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save to CSV",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
	    }

        public void WriteData(ObjectCluster obj)
        {
            if (FirstWrite)
            {
                WriteHeader(obj);
                FirstWrite = false;
            }
            Double[] data = obj.GetData().ToArray();

            String timeStamp = GetTimestamp(DateTime.Now);
            PCsvFile.Write(timeStamp + Delimeter);

            for (int i = 0; i < data.Length; i++)
            {
                
                PCsvFile.Write(data[i].ToString() + Delimeter);
            }
            PCsvFile.WriteLine();
        }

        public void WriteGemsAVData(SubmitEventArgs args)
        {
            //Write header
            PCsvFile.Write(
                "Wonder" + Delimeter
                + "Transcendence" + Delimeter
                + "Power" + Delimeter
                + "Tenderness" + Delimeter
                + "Nostalgia" + Delimeter
                + "Peacefulness" + Delimeter
                + "Joyful Activation" + Delimeter
                + "Sadness" + Delimeter
                + "Tension" + Delimeter
                + "Arousal" + Delimeter
                + "Valence" + Delimeter
                + "AVTension" + Delimeter
                + "Like" + Delimeter);
            PCsvFile.WriteLine();

            PCsvFile.Write(
                args.Wonder + Delimeter
                + args.Transcendence + Delimeter
                + args.Power + Delimeter
                + args.Tenderness + Delimeter
                + args.Nostalgia + Delimeter
                + args.Peacefulness + Delimeter
                + args.JoyfulActivation + Delimeter
                + args.Sadness + Delimeter
                + args.Tension + Delimeter
                + args.Arousal + Delimeter
                + args.Valence + Delimeter
                + args.Tense + Delimeter
                + args.Like + Delimeter);
            PCsvFile.WriteLine();
        }

        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        private void WriteHeader(ObjectCluster obj)
        {
            ObjectCluster objectCluster = new ObjectCluster(obj);
            List<String> names = objectCluster.GetNames();
            List<String> formats = objectCluster.GetFormats();
            List<String> units = objectCluster.GetUnits();
            List<Double> data = objectCluster.GetData();
            String deviceId = objectCluster.GetShimmerID();


            PCsvFile.Write("MSTimeStamp" + Delimeter);
            
            for (int i = 0; i < data.Count; i++)
            {
                PCsvFile.Write(deviceId + Delimeter);
            }
            PCsvFile.WriteLine();

            PCsvFile.Write("" + Delimeter);

            for (int i = 0; i < data.Count; i++)
            {
                PCsvFile.Write(names[i] + Delimeter);
            }

            PCsvFile.WriteLine();

            PCsvFile.Write("" + Delimeter);

            for (int i = 0; i < data.Count; i++)
            {
                PCsvFile.Write(formats[i] + Delimeter);
            }
            PCsvFile.WriteLine();

            PCsvFile.Write("" + Delimeter);

            for (int i = 0; i < data.Count; i++)
            {
                PCsvFile.Write(units[i] + Delimeter);
            }
            PCsvFile.WriteLine();
        }

        

        public void CloseFile()
        {
            if (PCsvFile != null)
            {
                PCsvFile.Close();
            }
            
        }
    }
}
