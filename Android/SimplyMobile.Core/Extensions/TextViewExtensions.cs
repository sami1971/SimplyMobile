using System;
using Android.Widget;
using System.ComponentModel;
using System.Reflection;

namespace SimplyMobile.Core
{
	public static class TextViewExtensions
	{
		public static void SetText(this TextView textField, object source, PropertyInfo property)
		{
			var text = property.GetValue(source).ToString();

			if (textField.Text != text)
			{
				textField.Text = text;
			}
		}

		public static void Bind(this TextView textField, INotifyPropertyChanged source, string propertyName)
		{
			var property = source.GetProperty(propertyName);

			textField.SetText(source, property);

			source.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == propertyName)
				{
					textField.SetText(source, property);
				}
			};

			//textField.AfterTextChanged += (sender, e) => property.GetSetMethod().Invoke(source, new []{textField.Text});
		}
	}
}

