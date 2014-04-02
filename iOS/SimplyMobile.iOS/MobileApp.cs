using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonoTouch.UIKit;

namespace SimplyMobile.Core
{
    public partial class MobileApp : UIApplicationDelegate
    {
        public static bool IsPhone 
        {
            get 
            {
                return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone;
            }
        }
    }
}
