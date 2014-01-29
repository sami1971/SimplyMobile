using System;
using MonoTouch.UIKit;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Globalization;

namespace SimplyMobile.Core
{
	public static class LabelExtensions
	{
		public static void SetText(this UILabel textField, object source, PropertyInfo property, IFormatProvider formatProvider = null)
		{
			var text = string.Format (formatProvider ?? CultureInfo.CurrentCulture, "{0}", property.GetValue (source));

			if (textField.Text != text)
			{
				textField.Text = text;
			}
		}

		public static PropertyChangedEventHandler Bind(
			this UILabel label, 
			INotifyPropertyChanged source, 
			string propertyName, 
			IFormatProvider formatProvider = null)
		{
			var property = source.GetProperty(propertyName);

			label.SetText(source, property);

			var handler = new PropertyChangedEventHandler((s, e) =>
			{
				if (e.PropertyName == propertyName)
				{
					label.InvokeOnMainThread(()=> label.SetText(source, property));
				}
			});

			source.PropertyChanged += handler;

			return handler;
		}
	}
}

