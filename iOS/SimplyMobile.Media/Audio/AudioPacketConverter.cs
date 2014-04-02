using System;
using MonoTouch.AudioToolbox;

namespace SimplyMobile.Media
{
    public class AudioPacketConverter
    {
        private IAudioStream stream;
        private AudioStreamBasicDescription description;

        public bool Start(IAudioStream stream)
        {
            this.stream = stream;
            this.stream.OnBroadcast += HandleOnBroadcast;
            this.description = new AudioStreamBasicDescription (AudioFormatType.LinearPCM) 
            {
                BitsPerChannel = stream.BitsPerSample / stream.ChannelCount,
            };
        }

        void HandleOnBroadcast (object sender, SimplyMobile.Core.EventArgs<byte[]> e)
        {
            var desc = new AudioStreamBasicDescription (AudioFormatType.LinearPCM);
        }
    }
}

