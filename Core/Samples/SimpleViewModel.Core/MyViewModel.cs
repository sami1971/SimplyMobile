using System;
using System.Threading.Tasks;
using System.Threading;

namespace SimpleViewModel.Core
{
	public class MyViewModel : SimplyMobile.Core.ViewModel
	{
		private CancellationTokenSource tokenSource;

		private const string ButtonStartText = "Click to start updating";
		private const string ButtonCancelText = "Click to cancel updating";

		string label = string.Empty;
		public string Label
		{
			get
			{
				return label;
			}
			private set
			{
				this.ChangeAndNotify(ref label, value);
			}
		}

		string buttonText = ButtonStartText;

		public string ButtonText
		{
			get
			{
				return buttonText;
			}
			private set
			{
				this.ChangeAndNotify(ref buttonText, value);
			}
		}

		public void Toggle(object sender, EventArgs e)
		{
			if (this.tokenSource == null)
			{
				this.tokenSource = new CancellationTokenSource ();
				this.Update (this.tokenSource.Token).ContinueWith (t => this.tokenSource = null);
			}
			else
			{
				Finish();
			}
		}

		public async Task Update(CancellationToken token)
		{
			this.ButtonText = ButtonCancelText;
			var seconds = 0d;

			for (seconds = 0; seconds < 50 && !token.IsCancellationRequested; seconds++)
			{
				this.Label = string.Format("Updating {0:0.0}s...", seconds/10);
				for (var n = 0; n < 10 && !token.IsCancellationRequested; n++)
				{
					await Task.Delay (10);
				}
			}

			this.Label = token.IsCancellationRequested ? "Update cancelled" : "Update complete";
			this.ButtonText = ButtonStartText;
		}

		public void Finish()
		{
			if (this.tokenSource != null)
			{
				this.tokenSource.Cancel ();
			}
		}
	}
}

