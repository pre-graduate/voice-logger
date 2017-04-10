using System.Windows.Controls;
using System;

using VoiceLogger.Interfaces;
using VoiceLogger.Classes;

namespace VoiceLogger.Pages
{
    public partial class VersionPage : Page, ISwitchable
    {
        public VersionPage()
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
