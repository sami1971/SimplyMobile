using System;
using Android.Webkit;
using System.Collections.Generic;
using SimplyMobile.Text;


namespace SimplyMobile.Web
{
	public partial class WebHybrid
	{
		private WebView webView;

		public WebHybrid(WebView webView)
		{
			this.webView = webView;
			Initialize();
		}

		public WebHybrid(WebView webView, IJsonSerializer serializer) : this(webView)
		{
			this.Serializer = serializer;
		}

		private void Initialize()
		{
			this.registeredActions = new Dictionary<string, Action<string>>();
			this.webView.Settings.JavaScriptEnabled = true;
			this.InjectNativeFunctionScript ();

			this.webView.SetWebViewClient (new Client (this));
			this.webView.SetWebChromeClient(new ChromeClient());
		}

		partial void Inject(string script)
		{
			this.webView.LoadUrl(string.Format("javascript: {0}", script));
		}

		private class Client : WebViewClient
		{
			private WebHybrid webHybrid;

			public Client(WebHybrid webHybrid)
			{
				this.webHybrid = webHybrid;
			}

			public override bool ShouldOverrideUrlLoading (WebView view, string url)
			{
				if (this.webHybrid == null || !this.webHybrid.CheckRequest(url))
				{
					view.LoadUrl(url);
				}
				return true;
			}
		}

		private class ChromeClient : WebChromeClient 
		{
            public override bool OnJsAlert(WebView view, string url, string message, JsResult result)
            {
                // the built-in alert is pretty ugly, you could do something different here if you wanted to
                return base.OnJsAlert(view, url, message, result);
            }
        }
	}
}

