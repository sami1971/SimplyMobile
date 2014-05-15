using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Speech
{
    public interface ISpeechToText
    {
        Task<SpeechToTextResult> Recognize();
    }
}
