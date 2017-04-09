using System.Collections.Generic;
using CodeLog.Interfaces;

namespace CodeLog.Classes
{
    public class LogManager
    {
        public List<ILog> AudioLogs { get; private set; } = new List<ILog>();
        public bool Recording { get; private set; }

        public void AddLog()
        {
            var previous = AudioLogs.Count;

            AudioLogs.Add(new Log());
            AudioLogs[previous].RecordLog();

            Recording = true;
        }

        public void PlayLastLog()
        {
            if (!Recording && AudioLogs.Count > 0)
            {
                AudioLogs[AudioLogs.Count - 1].PlayLog();
            }
        }

        public void StopPlayingLogs()
        {
            foreach (var log in AudioLogs)
            {
                log.StopLog();
            }
        }

        public void StopRecordingLastLog()
        {
            if (Recording && AudioLogs.Count > 0)
            {
                AudioLogs[AudioLogs.Count - 1].StopRecording();
                Recording = false;
            }
        }
    }
}
