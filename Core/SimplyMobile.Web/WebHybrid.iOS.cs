using System;
using MonoTouch.UIKit;
using System.Collections.Generic;
using MonoTouch.Foundation;
using SimplyMobile.Text;

namespace SimplyMobile.Web
{
    public partial class WebHybrid
    {
        protected UIWebView WebView;

        public WebHybrid (UIWebView webView, IJsonSerializer serializer)
        {
            this.Serializer = serializer;
            this.WebView = webView;
            this.Initialize();
        }

        public void Dispose()
        {
            this.WebView.ShouldStartLoad -= HandleStartLoad;
        }

        partial void Inject(string script)
        {
            this.WebView.EvaluateJavascript (script);
        }

        partial void LoadFile(string fileName)
        {
            this.WebView.LoadRequest (new NSUrlRequest (new NSUrl (fileName, false)));
        }

        private void Initialize()
        {
            this.registeredActions = new Dictionary<string, Action<string>> ();
            this.WebView.ShouldStartLoad += HandleStartLoad;
            this.InjectNativeFunctionScript ();
        }

        private bool HandleStartLoad (UIWebView webView, NSUrlRequest request, 
            UIWebViewNavigationType navigationType)
        {
            return !this.CheckRequest (request.Url.RelativeString);
        }
    }
}

