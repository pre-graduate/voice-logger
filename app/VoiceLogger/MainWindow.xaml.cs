using System.Collections.Generic;
using System.Windows;
using System;

using CodeLog.Interfaces;
using CodeLog.Classes;
using CodeLog.Pages;

namespace CodeLog
{
    public partial class MainWindow : Window
    {
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
    }
}
