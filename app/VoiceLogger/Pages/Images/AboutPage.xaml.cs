
using CodeLog.Interfaces;
using CodeLog.Classes;

using System.Windows.Controls;
using System.Windows;
using System;

namespace CodeLog.Pages
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
