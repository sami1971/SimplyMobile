using System;
using MonoTouch.UIKit;

namespace SimplyMobile.Device
{
    public static class CurrentDevice
    {
        public static DeviceType Type
        {
            get 
            { 
                return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone ?
                    DeviceType.iPhone :
                    DeviceType.iPad;
            }
        }
    }
}

