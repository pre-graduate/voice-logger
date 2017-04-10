using VoiceLogger.Interfaces;
using VoiceLogger.Classes;

using System.Windows.Controls;
using System;

namespace VoiceLogger.Pages
{
    public partial class AboutPage : Page, ISwitchable
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void GoBackClick(object sender, EventArgs e)
        {
            Switch.SwitchTo<MainPage>();
        }

        public void OnEnter()
        {
        }
    }
}
