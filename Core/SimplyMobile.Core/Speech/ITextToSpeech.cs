using System;
using System.Threading.Tasks;

namespace SimplyMobile.Speech
{
    public interface ITextToSpeech
    {
        Task SpeakText(string content);
        Task SpeakSsml(string content);
        Task SpeakSsmlFromUri(Uri content);
    }
}

