using System;
using MonoTouch.UIKit;
using SimplyMobile.Text;
using SimplyMobile.Web;
using System.IO;
using MonoTouch.Foundation;

namespace SimplyMobile.Web.CanvasJs
{
    public partial class CanvasView : WebHybrid
    {
        public CanvasView (UIWebView webView) : this(webView, null)
        {
        }

        public CanvasView(UIWebView webView, IJsonSerializer serializer)
            : base(webView, serializer)
        {
            
        }

        public void Load()
        {
            var fileName = "chart.html"; // remember case-sensitive
            string localHtmlUrl = Path.Combine(NSBundle.MainBundle.BundlePath, fileName);

            if (File.Exists (localHtmlUrl)) 
            {
                this.WebView.LoadRequest(new NSUrlRequest(new NSUrl(localHtmlUrl, false)));
            }
        }
    }
}

