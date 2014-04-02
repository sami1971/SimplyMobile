using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SimplyMobile.Web
{
    [Register("HybridWebView")]
    public partial class HybridWebView : UIWebView
    {
        public HybridWebView ()
        {
            Initialize ();
        }

        public HybridWebView (IntPtr handle) : base (handle)
        {
            Initialize ();
        }



        partial void Inject(string script)
        {
            this.EvaluateJavascript (script);
        }

        private void Initialize()
        {
            this.registeredActions = new Dictionary<string, Action<string>> ();
            this.ShouldStartLoad += HandleStartLoad;
            this.InjectNativeFunctionScript ();
        }

        private bool HandleStartLoad (UIWebView webView, NSUrlRequest request, 
            UIWebViewNavigationType navigationType)
        {
            return this.CheckRequest (request.Url.RelativeString);
        }

    }
}

