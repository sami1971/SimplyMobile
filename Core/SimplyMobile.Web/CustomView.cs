using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Webkit;

namespace SimplyMobile.Web
{
    [Register("CustomView")]
    public class CustomView : WebView
    {
        public CustomView (Context context) :
            base (context)
        {
            Initialize ();
        }

        public CustomView (Context context, IAttributeSet attrs) :
            base (context, attrs)
        {
            Initialize ();
        }

        public CustomView (Context context, IAttributeSet attrs, int defStyle) :
            base (context, attrs, defStyle)
        {
            Initialize ();
        }

        public CustomView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            Initialize ();
        }

        void Initialize ()
        {
        }
    }
}

