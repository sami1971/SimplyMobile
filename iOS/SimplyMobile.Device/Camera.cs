using System;
using MonoTouch.UIKit;

namespace SimplyMobile.Device
{
    public class Camera
    {
        public bool OpenCameraView(object sender)
        {
            var controller = sender as UIViewController;

            if (controller == null)
            {
                return false;
            }

            var cameraView = new UIImagePickerController () 
            {
                SourceType = UIImagePickerControllerSourceType.Camera
            };

            controller.PresentViewController (cameraView, true, null);

            return true;
        }
    }
}

