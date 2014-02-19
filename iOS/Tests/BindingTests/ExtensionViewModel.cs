using System;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using SimplyMobile.Core;

namespace BindingTests
{
	public class ExtensionViewModel : ViewModel
	{
		private long count;
		private bool toggleOn;
		private double sliderValue;
		private string textField = "Empty string";
		private string buttonTitle = "Click me";
		private string sliderValueText = string.Empty;

		//Command command;
		private ICommand command;

		public string TextField 
		{
			get 
			{
				return textField;
			}
			set 
			{
				if (textField != value) 
				{
					textField = value;
					NotifyPropertyChanged ();
				}
			}
		}

		public string ButtonTitle 
		{
			get
			{
				return buttonTitle;
			}
			set 
			{
				if (buttonTitle != value) 
				{
					buttonTitle = value;
					NotifyPropertyChanged ();
				}
			}
		}


//		[Range (0, 1000)]
//		public double SliderValue 
//		{
//			get
//			{
//				return sliderValue;
//			}
//			set
//			{
//				if (sliderValue != value && this.IsValid (value))
//				{
//					sliderValue = value;
//					this.NotifyPropertyChanged ();
//					this.SliderValueText = string.Format ("Slider value is {0}", sliderValue);
//				}
//			}
//		}

		public string SliderValueText 
		{
			get
			{
				return sliderValueText;
			}
			private set
			{
				sliderValueText = value;
				NotifyPropertyChanged ();
			}
		}

		public bool ToggleOn 
		{
			get
			{
				return toggleOn;
			}
			set
			{
				if (toggleOn != value)
				{
					toggleOn = value;
					this.NotifyPropertyChanged ();
				}
			}
		}

		public void OnButtonClick(object sender, EventArgs e)
		{
			this.ButtonTitle = string.Format ("Button pressed {0} times.", ++count);
		}
	}
}

