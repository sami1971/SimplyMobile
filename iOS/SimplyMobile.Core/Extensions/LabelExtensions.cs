using System;
using MonoTouch.UIKit;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace SimplyMobile.Core
{
	public static class LabelExtensions
	{
		public static void SetText(this UILabel textField, object source, PropertyInfo property)
		{
			var text = property.GetValue(source).ToString();

			if (textField.Text != text)
			{
				textField.Text = text;
			}
		}

		public static PropertyChangedEventHandler Bind(this UILabel label, INotifyPropertyChanged source, string propertyName)
		{
			var property = source.GetProperty(propertyName);

			label.SetText(source, property);

			var handler = new PropertyChangedEventHandler((s, e) =>
			{
				if (e.PropertyName == propertyName)
				{
					label.SetText(source, property);
				}
			});

			source.PropertyChanged += handler;

			return handler;
		}
	}
}

