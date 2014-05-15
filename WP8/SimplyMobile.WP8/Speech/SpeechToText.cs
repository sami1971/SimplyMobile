using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Phone.Speech.Recognition;
using Windows.Phone.Speech.Synthesis;

namespace SimplyMobile.Speech
{
    public class SpeechToText : ISpeechToText 
    {
        private readonly SpeechRecognizer recognizer;

        public SpeechToText()
        {
            this.recognizer = new SpeechRecognizer();
        }

        public async Task<SpeechToTextResult> Recognize()
        {
            var result = await this.recognizer.RecognizeAsync();

            return result.AsResult();
        }
    }
}
