using System;
using Android.Widget;
using System.ComponentModel;
using System.Reflection;

namespace SimplyMobile.Core
{
	public static class EditTextExtensions
	{
		public static void SetText(this EditText textField, object source, PropertyInfo property)
		{
			var text = property.GetValue(source).ToString();

			if (textField.Text != text)
			{
				textField.Text = text;
			}
		}

		public static void Bind(this EditText textField, INotifyPropertyChanged source, string propertyName)
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

			textField.AfterTextChanged += (sender, e) => property.GetSetMethod().Invoke(source, new []{textField.Text});
		}
	}
}

