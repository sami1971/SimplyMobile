using System;
using MonoTouch.UIKit;

namespace SimplyMobile.Core
{
	public static class ButtonExtensions
	{
		public static void OnClick(this UIButton button, EventHandler handler)
		{
			button.TouchUpInside += handler;
		}
	}
}

