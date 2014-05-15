using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Phone.Speech.Synthesis;

namespace SimplyMobile.Speech
{
    public class TextToSpeech : ITextToSpeech
    {
        private readonly SpeechSynthesizer synthesizer;

        public TextToSpeech()
        {
            this.synthesizer = new SpeechSynthesizer();
        }

        public Task SpeakText(string content)
        {
            return this.synthesizer.SpeakTextAsync(content).AsTask();
        }

        public Task SpeakSsml(string content)
        {
            return this.synthesizer.SpeakSsmlAsync(content).AsTask();
        }

        public Task SpeakSsmlFromUri(Uri content)
        {
            return this.synthesizer.SpeakSsmlFromUriAsync(content).AsTask();
        }
    }
}
