using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using SimplyMobile.Text;

namespace SimplyMobile.Web
{
	public partial class WebHybrid
	{
		private UIWebView webView;

		public WebHybrid (UIWebView webView)
		{
			this.webView = webView;
			this.Initialize();
		}

		public WebHybrid (UIWebView webView, IJsonSerializer serializer) : this(webView)
		{
			this.Serializer = serializer;
		}

		partial void Inject(string script)
		{
			this.webView.EvaluateJavascript (script);
		}

		private void Initialize()
		{
			this.registeredActions = new Dictionary<string, Action<string>> ();
			this.webView.ShouldStartLoad += HandleStartLoad;
			this.InjectNativeFunctionScript ();
		}

		private bool HandleStartLoad (UIWebView webView, NSUrlRequest request, 
			UIWebViewNavigationType navigationType)
		{
			return !this.CheckRequest (request.Url.RelativeString);
		}
	}
}

