// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace BindingTests
{
	[Register ("BindingTestsViewController")]
	partial class BindingTestsViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton button { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton buttonClose { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton buttonNewView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIDatePicker datePicker { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIDatePicker datePicker2 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel label { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel labelSliderValue { get; set; }

		[Outlet]
		MonoTouch.UIKit.UINavigationBar navBar { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISlider slider1 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISlider slider2 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField textField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch toggle1 { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch toggle2 { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (labelSliderValue != null) {
				labelSliderValue.Dispose ();
				labelSliderValue = null;
			}

			if (button != null) {
				button.Dispose ();
				button = null;
			}

			if (buttonClose != null) {
				buttonClose.Dispose ();
				buttonClose = null;
			}

			if (buttonNewView != null) {
				buttonNewView.Dispose ();
				buttonNewView = null;
			}

			if (datePicker != null) {
				datePicker.Dispose ();
				datePicker = null;
			}

			if (datePicker2 != null) {
				datePicker2.Dispose ();
				datePicker2 = null;
			}

			if (label != null) {
				label.Dispose ();
				label = null;
			}

			if (navBar != null) {
				navBar.Dispose ();
				navBar = null;
			}

			if (slider1 != null) {
				slider1.Dispose ();
				slider1 = null;
			}

			if (slider2 != null) {
				slider2.Dispose ();
				slider2 = null;
			}

			if (textField != null) {
				textField.Dispose ();
				textField = null;
			}

			if (toggle1 != null) {
				toggle1.Dispose ();
				toggle1 = null;
			}

			if (toggle2 != null) {
				toggle2.Dispose ();
				toggle2 = null;
			}
		}
	}
}
