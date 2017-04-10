namespace VoiceLogger.Interfaces
{
    public interface ILog
    {
        void StopRecording();
        void RecordLog();
        void PlayLog();
        void StopLog();
    }
}
