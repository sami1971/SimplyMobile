using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Phone.Speech.Recognition;

namespace SimplyMobile.Speech
{
    public static class SpeechToTextExtensions
    {
        public static SpeechToTextResult AsResult(this SpeechRecognitionResult result)
        {
            return new SpeechToTextResult(result.Text, (Confidence)((long)result.TextConfidence), result);
        }
    }
}
