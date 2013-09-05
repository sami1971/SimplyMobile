using System;
using MonoTouch.AudioToolbox;

namespace SimplyMobile.Media
{
	using Core;

	public class AudioStream : IAudioStream
	{
		private InputAudioQueue audioQueue;

		private int bufferSize;

		#region IAudioStream implementation

		public event EventHandler<EventArgs<byte[]>> OnBroadcast;

		public int SampleRate
		{
			get;
			private set;
		}

		public int ChannelCount
		{
			get
			{
				return 1;
			}
		}

		public int BitsPerSample
		{
			get
			{
				return 16;
			}
		}

		#endregion

		#region IMonitor implementation

		public event EventHandler<EventArgs<bool>> OnActiveChanged;

		public event EventHandler<EventArgs<Exception>> OnException;

		public bool Start ()
		{
			return (this.Active = this.audioQueue.Start() == AudioQueueStatus.Ok);
		}

		public void Stop ()
		{
			this.audioQueue.Stop (true);
		}

		public bool Active
		{
			get
			{
				this.audioQueue.IsRunning;
			}
		}

		#endregion

		public AudioStream(int sampleRate, int bufferSize)
		{
			this.SampleRate = sampleRate;
			this.bufferSize = bufferSize;
			this.Init ();
		}

		private void Init()
		{
			var audioFormat = new AudioStreamBasicDescription()
			{
				SampleRate = this.SampleRate,
				Format = AudioFormatType.LinearPCM,
				FormatFlags = AudioFormatFlags.LinearPCMIsSignedInteger | AudioFormatFlags.LinearPCMIsPacked,
				FramesPerPacket = 1,
				ChannelsPerFrame = 1,
				BitsPerChannel = this.BitsPerSample,
				BytesPerPacket = 2,
				BytesPerFrame = 2,
				Reserved = 0
			};

			audioQueue = new InputAudioQueue(audioFormat);
			audioQueue.InputCompleted += new EventHandler<InputCompletedEventArgs>(QueueInputCompleted);

			var bufferByteSize = this.bufferSize * audioFormat.BytesPerPacket;

			IntPtr bufferPtr;
			for (int index = 0; index < 3; index++)
			{
				audioQueue.AllocateBufferWithPacketDescriptors(bufferByteSize, this.bufferSize, out bufferPtr);
				audioQueue.EnqueueBuffer(bufferPtr, bufferByteSize, null);
			}
		}

		/// <summary>
		/// Handles iOS audio buffer queue completed message.
		/// </summary>
		/// <param name='sender'>Sender object</param>
		/// <param name='e'> Input completed parameters.</param>
		private void QueueInputCompleted(object sender, InputCompletedEventArgs e)
		{
			// return if we aren't actively monitoring audio packets
			if (!this.Active)
			{
				return;
			}

			var buffer = (AudioQueueBuffer)System.Runtime.InteropServices.Marshal.PtrToStructure(e.IntPtrBuffer, typeof(AudioQueueBuffer));
			if (this.OnBroadcast != null)
			{
				byte[] send = new byte[buffer.AudioDataByteSize];
				System.Runtime.InteropServices.Marshal.Copy(buffer.AudioData, send, 0, (int)buffer.AudioDataByteSize);

				this.OnBroadcast(this, new EventArgs<byte[]>(send));
			}
			                   
			AudioQueueStatus status = audioQueue.EnqueueBuffer(e.IntPtrBuffer, this.bufferSize, e.PacketDescriptions);  

			if (status != AudioQueueStatus.Ok)
			{
				// todo: 
			}
		}       

	}
}

