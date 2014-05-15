using System;
using Android.Media;

namespace SimplyMobile.Media
{
    using Core;

    public class AudioStream : IAudioStream
    {
        private readonly int bufferSize;

        /// <summary>
        /// The audio source.
        /// </summary>
        private readonly AudioRecord audioSource;

        /// <summary>
        /// Occurs when new audio has been streamed.
        /// </summary>
        public event EventHandler<EventArgs<byte[]>> OnBroadcast;

        /// <summary>
        /// The default device.
        /// </summary>
        public static AudioSource DefaultDevice = AudioSource.Mic;

        /// <summary>
        /// Gets the sample rate.
        /// </summary>
        /// <value>
        /// The sample rate.
        /// </value>
        public int SampleRate
        {
            get 
            {
                return this.audioSource.SampleRate;
            }
        }

        /// <summary>
        /// Gets bits per sample.
        /// </summary>
        public int BitsPerSample 
        { 
            get
            {
                return (this.audioSource.AudioFormat == Encoding.Pcm16bit) ? 16 : 8;
            }
        }

        /// <summary>
        /// Gets the channel count.
        /// </summary>
        /// <value>
        /// The channel count.
        /// </value>        
        public int ChannelCount
        {
            get
            {
                return this.audioSource.ChannelCount;
            }
        }

        /// <summary>
        /// Gets the average data transfer rate
        /// </summary>
        /// <value>The average data transfer rate in bytes per second.</value>
        public int AverageBytesPerSecond 
        { 
            get
            {
                return this.SampleRate * this.BitsPerSample / 8 * this.ChannelCount;
            }
        }

        public bool Active
        {
            get
            {
                return (this.audioSource.RecordingState == RecordState.Recording);
            }
        }

        /// <summary>
        /// Start recording from the hardware audio source.
        /// </summary>
        public bool Start()
        {
            Android.OS.Process.SetThreadPriority (Android.OS.ThreadPriority.UrgentAudio);

            if (this.Active)
            {
                return this.Active;
            }

            this.audioSource.StartRecording();

            Record ();

            return this.Active;
        }

        /// <summary>
        /// Stops recording.
        /// </summary>
        public void Stop ()
        {
            this.audioSource.Stop();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimplyMobile.Media.AudioStream"/> class.
        /// </summary>
        /// <param name="sampleRate">Sample rate.</param>
        /// <param name="bufferSize">Buffer size.</param>
        public AudioStream(int sampleRate, int bufferSize)
        {
            this.bufferSize = bufferSize;
            this.audioSource = new AudioRecord(
                AudioStream.DefaultDevice, 
                sampleRate, 
                ChannelIn.Mono,
                Encoding.Pcm16bit,
                this.bufferSize);
        }

        /// <summary>
        /// Record from the microphone and broadcast the buffer.
        /// </summary>
        private async void Record()
        {
            //var buffer = new byte[this.bufferSize];

            //var task = this.audioSource.ReadAsync (buffer, 0, this.bufferSize).ContinueWith (
            //    () =>
            //{
            //    if (this.OnBroadcast != null)
            //    {
            //        this.OnBroadcast(this, new EventArgs<byte[]>(buffer));
            //    }
            //    if (this.Active)
            //    {
            //        Record();
            //    }
            //});
        }

        public event EventHandler<EventArgs<bool>> OnActiveChanged;

        public event EventHandler<EventArgs<Exception>> OnException;
    }
}

