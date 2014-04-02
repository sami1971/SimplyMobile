using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SimpleViewModel.Core;

namespace SimpleViewModel
{
    [Activity (Label = "SimpleViewModel", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button button;
        TextView label;
        MyViewModel viewModel = new MyViewModel();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            this.button = FindViewById<Button> (Resource.Id.myButton);
            this.label = FindViewById<TextView> (Resource.Id.textView1);

            this.button.Text = this.viewModel.ButtonText;
            this.label.Text = this.viewModel.Label;
        }

        protected override void OnResume()
        {
            base.OnResume ();
            this.button.Click += this.viewModel.Toggle;
            this.viewModel.PropertyChanged += HandlePropertyChanged;
        }

        protected override void OnPause()
        {
            base.OnPause ();
            this.button.Click -= this.viewModel.Toggle;
            this.viewModel.PropertyChanged -= HandlePropertyChanged;
            this.viewModel.Finish();
        }

        void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ButtonText")
            {
                this.button.Text = this.viewModel.ButtonText;
            } 
            else
            {
                this.label.Text = this.viewModel.Label;
            }
        }
    }
}


