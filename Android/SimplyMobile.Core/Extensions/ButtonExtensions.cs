using System;
using Android.Widget;
using System.ComponentModel;
using System.Reflection;

namespace SimplyMobile.Core
{
	public static class ButtonExtensions
	{
		public static void OnClick(this Button button, EventHandler handler)
		{
			button.Click += handler;
		}

		public static void SetTitle(this Button button, object source, PropertyInfo property)
		{
			var text = property.GetValue(source).ToString();

			if (button.Text != text)
			{
				button.Text = text;
			}
		}

		public static void BindTitle(this Button button, INotifyPropertyChanged source, string propertyName)
		{
			var property = source.GetProperty(propertyName);

			button.SetTitle (source, property);

			source.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == propertyName)
				{
					button.SetTitle (source, property);
				}
			};
		}
	}
}

