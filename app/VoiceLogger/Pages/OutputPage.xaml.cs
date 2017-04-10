using System.Windows.Controls;
using System.Windows;

using VoiceLogger.Interfaces;
using VoiceLogger.Classes;

namespace VoiceLogger.Pages
{
    public partial class OutputPage : Page, ISwitchable
    {
        public string PreviousLocation { get; set; }
        public string PreviousSize { get; set; }
        public string Previous { get; set; }

        public ZipFolder Zip { get; set; }

        public OutputPage()
        {
            InitializeComponent();
        }

        public void OnEnter()
        {
            Status.Text = "Export In Progress";
            PreviousLocation = Export.Text;
            PreviousSize = FolderSize.Text;
            Previous = Number.Text; 
        }

        public void Finished()
        {
            FolderSize.Text += $"{Zip.GetDirectorySize()}MB";
            Export.Text += Zip.ExportedFilename;
            Number.Text += Zip.NumberOfLogs;
            Status.Text = "Export Finished";

            Zip.Cleanup();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderSize.Text = PreviousSize;
            Export.Text = PreviousLocation;
            Number.Text = Previous;

            Switch.Window.Title = "CodeLog";
            Switch.SwitchTo<MainPage>();
        }
    }
}
