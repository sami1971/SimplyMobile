using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Speech;
using System.Globalization;
using Android.App;
using SimplyMobile.Core;
using Android.Util;

namespace SimplyMobile.Speech
{
    public class SpeechToText : ISpeechToText
    {
        private const int ResultSpeech = 1;

        public SpeechToText ()
        {
        }

        #region ISpeechToText implementation

        public async Task<SpeechToTextResult> Recognize()
        {
            return await Task.Factory.StartNew (() =>
            {
                var intent = this.RegisterReceiver (null, new IntentFilter (RecognizerIntent.ActionRecognizeSpeech));

                if (intent != null)
                {
//                    intent.PutExtra (RecognizerIntent.ExtraLanguageModel, CultureInfo.CurrentCulture.Name);
                    var texts = intent.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    return new SpeechToTextResult(texts[0], Confidence.High, intent);
                }
                else
                {
                    Log.Error("SpeakText", "Your device doesn't support Speech to Text");
                    return new SpeechToTextResult("Your device doesn't support Speech to Text", Confidence.Rejected, null);
                }


            });
        }

        #endregion

        #region ISpeechToText implementation

        public Task SpeakText(String content)
        {
            return Task.Factory.StartNew (() =>
            {
                var intent = this.RegisterReceiver (null, new IntentFilter (RecognizerIntent.ActionRecognizeSpeech));

                if (intent != null)
                {
                    intent.PutExtra (RecognizerIntent.ExtraLanguageModel, CultureInfo.CurrentCulture.Name);
                }
                else
                {
                    Log.Error("SpeakText", "Your device doesn't support Speech to Text");
                }
            });
        }

        public Task SpeakSsml(String content)
        {
            throw new System.NotImplementedException ();
        }

        public Task SpeakSsmlFromUri(Uri content)
        {
            throw new System.NotImplementedException ();
        }

        #endregion
    }
}

