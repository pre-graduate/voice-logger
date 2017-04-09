using NAudio.Wave;

namespace CodeLog.Classes
{
    public class WavFile
    {
        public WaveFileReader FileReader { get; set; }
        public DirectSoundOut SoundOut { get; set; }
        public float Volume { get; set; } = 1.0f;

        public void OpenFile(string filename)
        {
            FileReader = new WaveFileReader(filename);
            SoundOut = new DirectSoundOut();
            SoundOut.Init(new WaveChannel32(FileReader));
        }

        public void CloseFile()
        {
            FileReader?.Close();
        }

        public void Play()
        {
            if (SoundOut == null)
            {
                return;
            }
           
            SoundOut.Volume = Volume;
            SoundOut.Play();

            FileReader.Position = 0;
        }
    }
}
