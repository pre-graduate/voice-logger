
#region Using Statements

using System.Windows.Controls;
using System.Windows;
using System;

using CodeLog.Interfaces;
using CodeLog.Classes;

#endregion

namespace CodeLog.Pages
{
    public partial class OutputPage : Page, ISwitchable
    {
        private string m_PreviousLocation;
        private string m_PreviousSize;
        private string m_Previous;
        private ZipFolder m_Folder;

        public OutputPage()
        {
            InitializeComponent();
        }

        public void OnEnter()
        {
            Status.Text = "Export In Progress";

            m_PreviousLocation = Export.Text;
            m_PreviousSize = FolderSize.Text;
            m_Previous = Number.Text; 
        }

        public void Finished()
        {
            FolderSize.Text += m_Folder.GetDirectorySize().ToString() + "MB";
            Export.Text += m_Folder.FolderDirectory;
            Number.Text += m_Folder.NumberOfLogs;
            Status.Text = "Export Finished";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderSize.Text = m_PreviousSize;
            Export.Text = m_PreviousLocation;
            Number.Text = m_Previous;

            Switch.Window.Title = "Kinect Log";
            Switch.SwitchTo<MainPage>();
        }

        public ZipFolder Zip
        {
            get
            {
                return m_Folder;
            }
            set
            {
                m_Folder = value;
            }
        }
    }
}
