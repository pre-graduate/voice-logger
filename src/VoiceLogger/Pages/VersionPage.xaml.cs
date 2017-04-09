using System.Windows.Controls;
using System;

using CodeLog.Interfaces;
using CodeLog.Classes;

namespace CodeLog.Pages
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
