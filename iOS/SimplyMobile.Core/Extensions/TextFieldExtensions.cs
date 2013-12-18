using System;
using MonoTouch.UIKit;
using System.Reflection;
using System.ComponentModel;

namespace SimplyMobile.Core
{
	public static class TextFieldExtensions
	{
		public static void Bind(this UITextField textField, INotifyPropertyChanged source, string propertyName)
		{
			var property = source.GetProperty(propertyName);

			source.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == propertyName)
				{
					textField.Text = property.GetValue(source) as String;
				}
			};

			textField.EditingChanged += (sender, e) => property.GetSetMethod().Invoke(source, new []{textField.Text});
		}
	}
}

