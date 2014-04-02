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

        public async void Toggle(object sender, EventArgs e)
        {
            if (this.tokenSource == null)
            {
                this.tokenSource = new CancellationTokenSource ();
                this.ButtonText = ButtonCancelText;

                try
                {
                    await Update(this.tokenSource.Token, new Progress<string>(progress => this.Label = progress));
                }
                catch (OperationCanceledException)
                {
                    this.Label = "Update cancelled";
                }
                finally
                {
                    this.ButtonText = ButtonStartText;
                    this.tokenSource = null;
                }
            }
            else
            {
                Finish();
            }
        }

        public void Finish()
        {
            if (this.tokenSource != null)
            {
                this.tokenSource.Cancel ();
            }
        }

        private static async Task Update(CancellationToken token, IProgress<string> progress)
        {
            var seconds = 0d;

            for (seconds = 0; seconds < 50; seconds++)
            {
                token.ThrowIfCancellationRequested();
                if (progress != null)
                {
                    progress.Report(string.Format("Updating {0:0.0}s...", seconds / 10));
                }
                
                for (var n = 0; n < 10; n++)
                {
                    token.ThrowIfCancellationRequested();
                    await Task.Delay(10);
                }
            }
        }
    }
}

