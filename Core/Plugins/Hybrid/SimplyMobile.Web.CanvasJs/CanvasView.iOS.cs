using System;
using MonoTouch.UIKit;
using SimplyMobile.Text;
using SimplyMobile.Web;

namespace SimlyMobile.Web.CanvasJs
{
	public partial class CanvasView : WebHybrid
	{
		public CanvasView (UIWebView webView) : this(webView, null)
		{
		}

		public CanvasView(UIWebView webView, IJsonSerializer serializer)
			: base(webView, serializer)
		{

			this.InjectJavaScript();
		}
	}
}

