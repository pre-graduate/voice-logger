
using Microsoft.Speech.Recognition;

namespace CodeLog.Interfaces
{
    public interface ISpeech
    {
        Choices GetCommands(Choices words);
        void HandleWord(string word);
    }
}
