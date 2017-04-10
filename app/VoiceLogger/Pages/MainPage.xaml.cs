using System.Windows.Controls;
using System.ComponentModel;
using System.Windows;
using System.Text;
using System;

using static VoiceLogger.Classes.Alerts;
using VoiceLogger.Interfaces;
using VoiceLogger.Classes;

namespace VoiceLogger.Pages
{  
    public partial class MainPage : Page, ISwitchable
    {
        public ZipFolder ZipFolder { get; set; } = new ZipFolder(@"\Data\Logs");
        public Kinect WindowsKinect { get; set; } = new Kinect();
        public Speech SpeechDetector { get; set; } = new Speech();

        public Recogniser RecognitionEngine { get; set; }

        public MainPage()
        {
            InitializeComponent();

            if (!WindowsKinect.InitialiseDevice())
            {
                MessageBox.Show("Couldn't find an available Kinect device!", "No Kinect Found!");
                Application.Current.Shutdown();
            }

            RecognitionEngine = new Recogniser(WindowsKinect);
            RecognitionEngine.Initialise(SpeechDetector);
        }

        public void ExportLogs(object sender, EventArgs e)
        {
            SpeechDetector.LogManager.StopPlayingLogs();

            if (!ZipFolder.FilesToCopy)
            {
                ShowError("No Logs!", "There are no logs to export");
                return;
            }

            if (ZipFolder.ChooseDirectory())
            {
                var success = false;
                var worker = new BackgroundWorker();

                worker.DoWork += (sen, args) => success = ZipFolder.Create();
                worker.RunWorkerCompleted += (sen, args) =>
                {
                    if (success)
                    {
                        Switch.GetSwitchable<OutputPage>().Finished();
                        Switch.Window.Title = "Export Finished";
                    }
                    else
                    {
                        Switch.Window.Title = "Kinect Log";
                        Switch.SwitchTo<MainPage>();
                    }
                };

                Switch.GetSwitchable<OutputPage>().Zip = ZipFolder;
                Switch.Window.Title = "Creating File ...";
                Switch.SwitchTo<OutputPage>();

                worker.RunWorkerAsync();
            }
        }

        private void HandleCommands(object sender, RoutedEventArgs e)
        {
            var builder = new StringBuilder();

            builder.Append("Hide Interface, ");
            builder.Append("Show Interface, ");
            builder.Append("Extract, ");
            builder.Append("Initate, ");
            builder.Append("Terminate, ");
            builder.Append("Play Log, ");
            builder.Append("Cease, ");
            builder.Append("Sleep, ");
            builder.Append("Wake");

            ShowInfo("Commands", builder.ToString());
        }

        private void HandleMenu(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;

            switch (menuItem?.Header.ToString())
            {
                case "Play Last Log": SpeechDetector.HandleWord("PLAY"); break;
                case "Start": SpeechDetector.HandleWord("RECORD"); break;
                case "Stop": SpeechDetector.HandleWord("STOP"); break;
                
                case "Version": Switch.SwitchTo<VersionPage>(); break;
                case "About": Switch.SwitchTo<AboutPage>(); break;
                case "Exit": Switch.Window.Close(); break;
            }
        }

        public void OnEnter()
        {
        }
    }
}
