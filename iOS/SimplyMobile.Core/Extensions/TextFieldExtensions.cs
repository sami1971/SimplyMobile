using System;
using MonoTouch.UIKit;
using System.Reflection;
using System.ComponentModel;

namespace SimplyMobile.Core
{
	public static class TextFieldExtensions
	{
		public static void SetText(this UITextField textField, object source, PropertyInfo property)
		{
			var text = property.GetValue(source).ToString();

			if (textField.Text != text)
			{
				textField.Text = text;
			}
		}

		public static PropertyChangedEventHandler Bind(this UITextField textField, INotifyPropertyChanged source, string propertyName)
		{
			var property = source.GetProperty(propertyName);

			textField.SetText(source, property);
			var handler = new PropertyChangedEventHandler((s, e) =>
				{
					if (e.PropertyName == propertyName)
					{
						textField.SetText(source, property);
					}
				});

			source.PropertyChanged += handler;
			textField.EditingChanged += (sender, e) => property.GetSetMethod().Invoke(source, new []{textField.Text});

			return handler;
		}
	}
}

