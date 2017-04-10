
using Microsoft.Speech.Recognition;

namespace VoiceLogger.Interfaces
{
    public interface ISpeech
    {
        Choices GetCommands(Choices words);
        void HandleWord(string word);
    }
}
