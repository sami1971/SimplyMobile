using System;
using MonoTouch.UIKit;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace SimplyMobile.Core
{
	public static class LabelExtensions
	{
		public static void Bind(this UILabel label, INotifyPropertyChanged source, string propertyName)
		{
			var property = source.GetProperty(propertyName);

			source.PropertyChanged += (sender, e) =>
			{
				if (e.PropertyName == propertyName)
				{
					label.Text = property.GetValue(source) as String;
				}
			};
		}
	}
}

