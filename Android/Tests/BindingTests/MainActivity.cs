using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SimplyMobile.Core;

namespace BindingTests
{
	[Activity (Label = "BindingTests", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private ExtensionViewModel model;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			model = new ExtensionViewModel ();

			var button = FindViewById<Button> (Resource.Id.myButton);
			var label = FindViewById<TextView> (Resource.Id.label);
			var textView = FindViewById<EditText> (Resource.Id.textView);

			button.OnClick (model.OnButtonClick);
			button.BindTitle (model, "ButtonTitle");
			label.Bind(model, "TextField");
			textView.Bind(model, "TextField");
		}
	}
}


