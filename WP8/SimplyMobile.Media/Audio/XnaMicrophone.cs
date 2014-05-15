using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;

namespace SimplyMobile.Media
{
    using Core;

    public class XnaMicrophone : IAudioStream
    {
        private Microphone microphone;

        public XnaMicrophone()
        {
            this.microphone = Microphone.Default;
            this.microphone.BufferReady += microphone_BufferReady;
        }

        public event EventHandler<Core.EventArgs<byte[]>> OnBroadcast;

        public int SampleRate
        {
            get { return this.microphone.SampleRate; }
        }

        public int ChannelCount
        {
            get { return 1; }
        }

        public int BitsPerSample
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<Core.EventArgs<bool>> OnActiveChanged;

        public event EventHandler<Core.EventArgs<Exception>> OnException;

        public bool Active
        {
            get { return this.microphone.State == MicrophoneState.Started; }
        }

        public bool Start()
        {
            try
            {
                this.microphone.Start();
            }
            catch (Exception ex)
            {
                if (this.OnException != null)
                {
                    this.OnException.Invoke<Exception>(this, ex);
                }
                return false;
            }

            return this.Active;
        }

        public void Stop()
        {
            this.microphone.Stop();
        }

        void microphone_BufferReady(object sender, EventArgs e)
        {
            var buffer = new byte[4096];
            int read;

            do
            {
                read = this.microphone.GetData(buffer, 0, buffer.Length);
                if (this.OnBroadcast != null)
                {
                    this.OnBroadcast.Invoke<byte[]>(this, buffer);
                }
            } 
            while (read > 0);
        }

        #region IAudioStream Members


        public int AverageBytesPerSecond
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}
