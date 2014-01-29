using System;
using System.ComponentModel;
using MonoTouch.UIKit;
using System.Reflection;

namespace SimplyMobile.Core
{
	public static class SwitchExtensions
	{
		public static void SetValue(this UISwitch toggle, object source, PropertyInfo property)
		{
			var value = (bool)property.GetValue(source);

			if (toggle.On != value)
			{
				toggle.On = value;
			}
		}

		public static PropertyChangedEventHandler Bind(this UISwitch toggle, INotifyPropertyChanged source, string propertyName)
		{
			var property = source.GetProperty(propertyName);

			if (property.PropertyType == typeof(bool))
			{
				toggle.SetValue(source, property);
				var handler = new PropertyChangedEventHandler ((s, e) =>
				{
					if (e.PropertyName == propertyName)
					{
							toggle.InvokeOnMainThread(()=>
								toggle.SetValue(source, property));
					}
				});

				source.PropertyChanged += handler;
				toggle.ValueChanged += (sender, e) => property.GetSetMethod().Invoke (source, new object[]{ toggle.On });

				return handler;
			} 
			else
			{
				throw new InvalidCastException ("Binding property is not boolean");
			}
		}
	}
}

