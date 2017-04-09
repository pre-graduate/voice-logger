using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;
using System;

namespace CodeLog.Classes
{
    using Interfaces;

    public class Recogniser
    {
        public const double ConfidenceLevel = 0.3;
        public const string WakeUpKey = "WAKEUP";
        public const string SleepKey = "SLEEP";

        public SpeechRecognitionEngine SpeechEngine { get; set; }
        public RecognizerInfo RecogniserInfo { get; set; }
        public ISpeech WordsToWatch { get; set; }
        public Kinect WindowKinect { get; set; }
        public bool Active { get; set; }

        public Recogniser(Kinect kinect)
        {
            RecogniserInfo = GetRecogniser();
            WindowKinect = kinect;
        }

        public void Initialise(Speech words)
        {
            WordsToWatch = words;

            var commands = words.GetCommands(new Choices());
            commands.Add(new SemanticResultValue("sleep", SleepKey));
            commands.Add(new SemanticResultValue("wake", WakeUpKey));

            var speechAudioFormat = new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null);
            var grammerConstructor = new GrammarBuilder();
            grammerConstructor.Culture = RecogniserInfo.Culture;
            grammerConstructor.Append(commands);

            SpeechEngine = new SpeechRecognitionEngine(RecogniserInfo.Id);
            SpeechEngine.LoadGrammar(new Grammar(grammerConstructor));
            SpeechEngine.SpeechRecognized += SpeeckRecognized;
            SpeechEngine.UpdateRecognizerSetting("AdaptationOn", 0);
            SpeechEngine.SetInputToAudioStream(WindowKinect.Device.AudioSource.Start(), speechAudioFormat);
            SpeechEngine.RecognizeAsync(RecognizeMode.Multiple);

            Active = WindowKinect.Device.ElevationAngle > 0;
        }

        private void SpeeckRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence < ConfidenceLevel)
            {
                return;
            }

            var word = e.Result.Semantics.Value.ToString();

            switch (word)
            {
                case WakeUpKey:
                {
                    WindowKinect.Device.ElevationAngle = WindowKinect.Device.MaxElevationAngle;
                    Active = true;
                    break;
                }

                case SleepKey:
                {
                    WindowKinect.Device.ElevationAngle = WindowKinect.Device.MinElevationAngle;
                    Active = false;
                    break;
                }

                default:
                {
                    if (Active)
                    {
                        WordsToWatch.HandleWord(word);
                    }

                    break;
                }
            }
        }
    
        private static RecognizerInfo GetRecogniser()
        {
            foreach (var recognizer in SpeechRecognitionEngine.InstalledRecognizers())
            {
                var country = recognizer.Culture.Name;
                var value = string.Empty;
               
                recognizer.AdditionalInfo.TryGetValue("Kinect", out value);

                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                if (Compare(value, "True") && Compare(country, "en-US"))
                {
                    return recognizer;
                }

                if(Compare(value, "True") && Compare(country, "en-UK"))
                {
                    return recognizer;
                }
            }

            return null;
        }

        private static bool Compare(string value, string equalsTo)
        {
            return value.Equals(equalsTo, StringComparison.OrdinalIgnoreCase);
        }
    }
}
