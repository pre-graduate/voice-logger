using Microsoft.Speech.Recognition;
using CodeLog.Interfaces;
using CodeLog.Pages;

namespace CodeLog.Classes
{
    public class Speech : ISpeech
    {
        private const string StartSound = "Data/Sounds/beep.wav";
        private const string StopSound = "Data/Sounds/beep2.wav";

        public WavFile BeepStartSound { get; set; } = new WavFile();
        public WavFile BeepStopSound { get; set; } = new WavFile();

        public LogManager LogManager { get; set; } = new LogManager();
        public bool Recording { get; set; }

        public Speech()
        {
            BeepStartSound.OpenFile(StartSound);
            BeepStopSound.OpenFile(StopSound);
        }

        public Choices GetCommands(Choices words)
        {
            words.Add(new SemanticResultValue("hide interface", "CLOSE"));
            words.Add(new SemanticResultValue("show interface", "SHOW"));
            words.Add(new SemanticResultValue("extract", "EXTRACT"));
            words.Add(new SemanticResultValue("initiate", "RECORD"));
            words.Add(new SemanticResultValue("export", "EXTRACT"));
            words.Add(new SemanticResultValue("terminate", "EXIT"));
            words.Add(new SemanticResultValue("play log", "PLAY"));
            words.Add(new SemanticResultValue("cease", "STOP"));
        
            return words;
        }

        public void HandleWord(string word)
        {
            if (word == "STOP" && Recording)
            {
                LogManager.StopRecordingLastLog();
                BeepStopSound.Play();
                Recording = false;
            }

            if (Recording)
            {
                return;
            }
            
            switch (word)
            {
                case "EXTRACT":
                {
                    Switch.Window.Show();
                    Switch.GetSwitchable<MainPage>().ExportLogs(null, null); 
                    break;
                }

                case "PLAY": LogManager.PlayLastLog(); break;
                case "CLOSE": Switch.Window.Hide(); break;
                case "EXIT": Switch.Window.Close(); break;
                case "SHOW": Switch.Window.Show(); break;
                case "RECORD":
                {
                    if (Recording == false)
                    {
                        Recording = true;
                        BeepStartSound.Play();
                        LogManager.AddLog();
                    }

                    break;
                }
            }
        }
    }
}
