using System;
using Android.Webkit;
using Android.Content;
using Android.Runtime;
using Android.Util;

namespace SimplyMobile.Web
{
    [Register("HybridWebView")]
    public partial class HybridWebView : WebView
    {
        public HybridWebView (Context context) : base(context)
        {
            Initialize ();
        }

        public HybridWebView(Context context, IAttributeSet attrs) :  base(context, attrs) 
        {
            Initialize ();
        }

        public HybridWebView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) 
        {
            Initialize ();
        }

        public HybridWebView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize ();
        }

        private void Initialize()
        {
            this.Settings.JavaScriptEnabled = true;
            this.InjectNativeFunctionScript ();

            this.SetWebViewClient (new Client ());
        }

        partial void Inject(string script)
        {
            this.LoadUrl(string.Format("javascript: {0}", script));
        }

        private class Client : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading (WebView view, string url)
            {
                var hybridView = view as HybridWebView;

                if (hybridView == null || !hybridView.CheckRequest(url))
                {
                    view.LoadUrl(url);
                }
                return true;
            }
        }
    }
}

