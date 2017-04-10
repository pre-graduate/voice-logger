using System.Windows.Controls;
using System.ComponentModel;
using System.Windows;
using System;

using CodeLog.Interfaces;
using CodeLog.Classes;

namespace CodeLog.Pages
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

            if (WindowsKinect.InitialiseDevice())
            {
               
            }
            else
            {
                #if false
                Application.Current.Shutdown();
                #endif
            }

            RecognitionEngine = new Recogniser(WindowsKinect);
            RecognitionEngine.Initialise(SpeechDetector);
        }

        public void ExportLogs(object sender, EventArgs e)
        {
            SpeechDetector.LogManager.StopPlayingLogs();

            if (!ZipFolder.FilesToCopy)
            {
                MessageBox.Show("There are no logs to export", "No Logs!", MessageBoxButton.OK, MessageBoxImage.Information);
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
            MessageBox.Show("hide interface, show interface, extract, initate, export, terminate, play log, cease");
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
