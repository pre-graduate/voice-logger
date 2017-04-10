using System.Windows;

namespace CodeLog.Classes
{
    public static class Alerts
    {
        public static void ShowError(string title, string body)
        {
            MessageBox.Show(body, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void ShowInfo(string title, string body)
        {
            MessageBox.Show(body, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ShowSuccess(string title, string body)
        {
            MessageBox.Show(body, title, MessageBoxButton.OK, MessageBoxImage.None);
        }
    }
}
