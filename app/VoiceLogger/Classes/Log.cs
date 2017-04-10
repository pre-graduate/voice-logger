using CodeLog.Interfaces;
using NAudio.Wave;
using System;
using System.IO;

namespace CodeLog.Classes
{
    public class Log : ILog
    {
        public WaveFileWriter WaveWriter { get; set; }
        public WavFile LogFile { get; set; } = new WavFile();
        public WaveIn WaveIn { get; set; } = new WaveIn();

        public int KinectDeviceIndex { get; set; }
        public string Filename { get; set; }

        public Log()
        {
            Filename = $"Data/Logs/{DateTime.Now.ToString("yy-MM-dd - HH-mm-ss")}.wav";

            for (var i = 0; i < WaveIn.DeviceCount; ++i)
            {          
                if (!WaveIn.GetCapabilities(i).ProductName.Contains("kinect"))
                {
                    continue;
                }

                KinectDeviceIndex = i;
                break;
            }
        }

        public void RecordLog()
        {
            WaveIn.DeviceNumber = KinectDeviceIndex;
            WaveIn.WaveFormat = new WaveFormat(22100, WaveIn.GetCapabilities(KinectDeviceIndex).Channels);

            WaveIn.DataAvailable += GetData;
            WaveWriter = new WaveFileWriter(Filename, WaveIn.WaveFormat);
            WaveIn.StartRecording();
        }

        public void PlayLog()
        {
            if (File.Exists(Filename))
            {
                LogFile.OpenFile(Filename);
                LogFile.Play();
            }
        }

        public void StopLog()
        {
            LogFile.CloseFile();
        }

        public void StopRecording()
        {
            WaveIn?.StopRecording();
            WaveIn?.Dispose();
            WaveIn = null;

            WaveWriter?.Dispose();
            WaveWriter = null;
        }

        private void GetData(object sender, WaveInEventArgs e)
        {
            WaveWriter?.Write(e.Buffer, 0, e.BytesRecorded);
            WaveWriter?.Flush();
        }
    }
}
