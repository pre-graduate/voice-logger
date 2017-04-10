namespace VoiceLogger.Classes
{
    public static class Switch
    {
        public static MainWindow Window { get; set; }

        public static void SwitchTo<TPage>()
        {
            Window.SwitchTo<TPage>();
        }

        public static TState GetSwitchable<TState>()
        {
            return Window.GetPage<TState>();
        }
    }
}
