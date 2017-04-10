using System.Collections.Generic;
using System.Windows;
using System;

using MahApps.Metro.Controls;
using VoiceLogger.Interfaces;
using VoiceLogger.Classes;
using VoiceLogger.Pages;

namespace VoiceLogger
{
    public partial class MainWindow : MetroWindow
    {
        const string GitHubLink = "https://github.com/william-taylor/voice-logger";

        public List<ISwitchable> Pages { get; }

        public MainWindow()
        {
            try
            { 
                InitializeComponent();
                
                Pages = new List<ISwitchable>
                {
                    new VersionPage(),
                    new OutputPage(),
                    new AboutPage(),
                    new MainPage()
                };

                Switch.Window = this;
                Switch.SwitchTo<MainPage>();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }
        }

        public void SwitchTo<TState>()
        {
            foreach (var page in Pages)
            {
                if (page is TState)
                {
                    Content = page;
                    page.OnEnter();
                }
            }
        }

        public TState GetPage<TState>()
        {
            foreach (var switchable in Pages)
            {
                if (switchable is TState)
                {
                    return (TState)switchable;
                }
            }

            return default(TState);
        }

        private void OpenGitHub(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(GitHubLink);
        }
    }
}
