using System;
using MonoTouch.UIKit;

namespace DeviceTests
{
	public class TabBarController : UITabBarController
	{
		UIViewController mainController;

		public TabBarController ()
		{


		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();

			this.mainController = new MainViewController () 
			{
				TabBarItem = new UITabBarItem (UITabBarSystemItem.Featured, 0)
			};

			this.ViewControllers = new UIViewController[] 
			{
				mainController
			};
		}
	}
}

