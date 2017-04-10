using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

using Microsoft.Kinect;

namespace VoiceLogger.Classes
{
    public class Kinect
    {    
        public WriteableBitmap Image { get; private set; }
        public KinectSensor Sensor { get; private set; }
        public byte[] Buffer { get; private set; }

        public WriteableBitmap Frame => Image;
        public KinectSensor Device => Sensor;

        public bool InitialiseDevice()
        {
            foreach(var sensor in KinectSensor.KinectSensors)
            {
                if (sensor.Status == KinectStatus.Connected)
                {
                    Sensor = sensor;
                }
            }

            if(Sensor != null)
            {
                Sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);
                Buffer = new byte[Sensor.ColorStream.FramePixelDataLength];
                Image = new WriteableBitmap(Sensor.ColorStream.FrameWidth, Sensor.ColorStream.FrameHeight, 96.0, 96.0, PixelFormats.Bgr32, null);

                Sensor.ColorFrameReady += Write;
                Sensor.Start();
                return true;
            }
           
            return false;
        }

        public void Write(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (var frame = e.OpenColorImageFrame())
            {
                if (frame == null)
                {
                    return;
                }

                frame.CopyPixelDataTo(Buffer);
                var rect = new Int32Rect(0, 0, Image.PixelWidth, Image.PixelHeight);
                Image.WritePixels(rect, Buffer, Image.PixelWidth * frame.BytesPerPixel, 0);
            }
        }

    }
}
