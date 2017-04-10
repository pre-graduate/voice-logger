using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;

namespace VoiceLogger.Classes
{
    public static class Alerts
    {
        public static async void ShowError(string title, string body)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);

            await metroWindow?.ShowMessageAsync(title, body, MessageDialogStyle.Affirmative);
        }

        public static async void ShowInfo(string title, string body)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);

            await metroWindow?.ShowMessageAsync(title, body, MessageDialogStyle.Affirmative);
        }

        public static async void ShowSuccess(string title, string body)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);

            await metroWindow?.ShowMessageAsync(title, body, MessageDialogStyle.Affirmative);
        }
    }
}
