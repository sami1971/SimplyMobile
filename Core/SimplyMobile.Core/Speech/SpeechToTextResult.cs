using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Speech
{
    public class SpeechToTextResult
    {
        public SpeechToTextResult(string text, Confidence confidenceLevel, object source)
        {
            this.Text = text;
            this.ConfidenceLevel = confidenceLevel;
            this.Source = source;
        }

        public Confidence ConfidenceLevel { get; private set; }

        public string Text { get; private set; }

        public object Source { get; private set; }
    }
}
