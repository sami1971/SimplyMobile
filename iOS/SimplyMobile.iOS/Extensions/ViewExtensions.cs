using System;
using MonoTouch.UIKit;

namespace SimplyMobile
{
    public static class ViewExtensions
    {
        public static UIViewController GetController (this UIView view)
        {
            UIResponder responder = view;

            while (responder != null && !(responder is UIViewController))
            {
                responder = responder.NextResponder;
            }

            return responder as UIViewController;
        }
    }
}

