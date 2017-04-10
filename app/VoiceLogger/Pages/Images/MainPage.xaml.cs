
using System.Collections.Generic;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System;

using CodeLog.Interfaces;
using CodeLog.Classes;

namespace CodeLog.Pages
{  
    public partial class MainPage : Page, ISwitchable
    {
        private Recogniser m_RecognitionEngine;
        private Speech m_Speech;
        private Kinect m_Kinect;

        public MainPage()
        {
            m_Speech = new Speech();
            m_Kinect = new Kinect();

            if (m_Kinect.InitialiseDevice())
            {
                InitializeComponent();

                m_RecognitionEngine = new Recogniser(m_Kinect);
                m_RecognitionEngine.Initialise(m_Speech);
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        public void DirButtonClick(object sender, EventArgs e)
        {
            ZipFolder ZipFile = new ZipFolder(@"\Data\log");
            ZipFile.ChooseDirectory();
            
            var success = false;
            BackgroundWorker Worker = new BackgroundWorker();
          
            Worker.DoWork += (Sender, args) =>
            {
                success = ZipFile.Create();
            };

            Worker.RunWorkerCompleted += (Sender, args) =>
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

            Switch.GetSwitchable<OutputPage>().Zip = ZipFile;
            Switch.Window.Title = "Creating File ...";
            Switch.SwitchTo<OutputPage>();

            Worker.RunWorkerAsync(); 
        }

        private void HandleMenu(object sender, RoutedEventArgs e)
        {
            switch ((sender as MenuItem).Header.ToString())
            {
                case "Play Last Log": m_Speech.HandleWord("PLAY"); break;
                case "Start": m_Speech.HandleWord("RECORD"); break;
                case "Stop": m_Speech.HandleWord("STOP"); break;
                
                case "Version": Switch.SwitchTo<VersionPage>(); break;
                case "About": Switch.SwitchTo<AboutPage>(); break;
                case "Exit": Switch.Window.Close(); break;
                
                    default: 
                        break;
            }
        }

        public void OnEnter()
        {
        }
    }
}
