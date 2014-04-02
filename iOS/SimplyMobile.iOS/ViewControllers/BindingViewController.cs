using System;
using System.Drawing;

using MonoTouch.CoreFoundation;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Collections.Generic;

namespace SimplyMobile.iOS
{
    [Register("BindingViewController")]
    public class BindingViewController : UIViewController
    {
        public BindingViewController() { }

        public BindingViewController(string nibName, NSBundle bundle) : base(nibName, bundle) { }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.eventReleaseActions = new List<Action>();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            this.eventReleaseActions.ForEach(a => a.Invoke());
            this.eventReleaseActions.Clear();
        }

        protected void AddDisappearAction(Action action)
        {
            this.eventReleaseActions.Add(action);
        }

        protected List<Action> eventReleaseActions;
    }
}