using System;
using System.IO;
using System.Text;
using SimplyMobile.Core;

namespace SimplyMobile.Media.Audio
{
    public class WaveRecorder : IDisposable
    {
        private StreamWriter streamWriter;
        private BinaryWriter writer;
        private int byteCount;
        private IAudioStream stream;

        public bool StartRecorder(IAudioStream stream, string fileName)
        {
            if (this.stream != null || stream == null)
            {
                return false;
            }

            this.stream = stream;

            try
            {
                //this.streamWriter = new StreamWriter(fileName, false);
                this.writer = new BinaryWriter(this.streamWriter.BaseStream, Encoding.UTF8);
            }
            catch (Exception)
            {
                return false;
            }
            this.byteCount = 0;
            this.stream.OnBroadcast += OnStreamBroadcast;
            this.stream.OnActiveChanged += StreamActiveChanged;
            if (!this.stream.Active)
            {
                this.stream.Start();
            }

            return true;
        }

        void StreamActiveChanged(object sender, EventArgs<bool> e)
        {
            if (!e.Value)
            {
                StopRecorder();
            }
        }

        private void OnStreamBroadcast(object sender, EventArgs<byte[]> eventArgs)
        {
            this.writer.Write(eventArgs.Value);
            this.byteCount += eventArgs.Value.Length;
        }

        public void StopRecorder()
        {
            if (this.stream != null)
            {
                this.stream.OnBroadcast -= OnStreamBroadcast;
                this.stream.OnActiveChanged -= StreamActiveChanged;
            }

            if (this.streamWriter != null && this.streamWriter.BaseStream.CanWrite)
            {
                this.WriteHeader();
                this.streamWriter.Dispose();
                this.streamWriter = null;
            }

            this.stream = null;
        }

        public void Dispose()
        {
            StopRecorder();
        }

        private void WriteHeader()
        {
            this.writer.Seek(0, SeekOrigin.Begin);
            // chunk ID
            this.writer.Write('R');
            this.writer.Write('I');
            this.writer.Write('F');
            this.writer.Write('F');

            this.writer.Write(this.byteCount + 36);
            this.writer.Write('W');
            this.writer.Write('A');
            this.writer.Write('V');
            this.writer.Write('E');

            this.writer.Write('f');
            this.writer.Write('m');
            this.writer.Write('t');
            this.writer.Write(' ');

            this.writer.Write(16);
            this.writer.Write((short)1);

            this.writer.Write((short)this.stream.ChannelCount);
            this.writer.Write(this.stream.SampleRate);
            this.writer.Write(this.stream.SampleRate * 2);
            this.writer.Write((short)2);
            this.writer.Write((short)this.stream.BitsPerSample);
            this.writer.Write('d');
            this.writer.Write('a');
            this.writer.Write('t');
            this.writer.Write('a');
            this.writer.Write(this.byteCount);
        }
    }
}
