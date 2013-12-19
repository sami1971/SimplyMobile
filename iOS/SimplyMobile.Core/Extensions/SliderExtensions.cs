using System;
using MonoTouch.UIKit;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SimplyMobile.Core
{
	public static class SliderExtensions
	{
		public static void SetRange(this UISlider slider, RangeAttribute range)
		{
			if (range.OperandType == typeof(int))
			{
				slider.SetRange ((int)range.Minimum, (int)range.Maximum);
			}
			else if (range.OperandType == typeof(long))
			{
				slider.SetRange ((long)range.Minimum, (long)range.Maximum);
			}
			else if (range.OperandType == typeof(decimal))
			{
				slider.SetRange ((decimal)range.Minimum, (decimal)range.Maximum);
			}
			else if (range.OperandType == typeof(float))
			{
				slider.SetRange ((float)range.Minimum, (float)range.Maximum);
			}
			else if (range.OperandType == typeof(double))
			{
				slider.SetRange ((double)range.Minimum, (double)range.Maximum);
			}
		}

		public static void SetRange(this UISlider slider, int min, int max)
		{
			slider.MinValue = (float)min;
			slider.MaxValue = (float)max;
		}

		public static void SetRange(this UISlider slider, float min, float max)
		{
			slider.MinValue = (float)min;
			slider.MaxValue = (float)max;
		}

		public static void SetRange(this UISlider slider, double min, double max)
		{
			slider.MinValue = (float)min;
			slider.MaxValue = (float)max;
		}

		public static void SetRange(this UISlider slider, long min, long max)
		{
			slider.MinValue = (float)min;
			slider.MaxValue = (float)max;
		}

		public static void SetRange(this UISlider slider, decimal min, decimal max)
		{
			slider.MinValue = (float)min;
			slider.MaxValue = (float)max;
		}

		public static void SetValue(this UISlider slider, object source, PropertyInfo property)
		{
			var value = (double)property.GetValue(source);
			float f = (float)value;

			if (slider.Value != f)
			{
				slider.Value = f;
			}
		}

		public static PropertyChangedEventHandler Bind(this UISlider slider, INotifyPropertyChanged source, string propertyName)
		{
			var property = source.GetProperty(propertyName);

			var r = property.GetCustomAttribute<RangeAttribute> ();

			if (r != null)
			{
				slider.SetRange(r);
			}

			if (property.PropertyType == typeof(int) || property.PropertyType == typeof(double))
			{
				slider.SetValue(source, property);
				var handler = new PropertyChangedEventHandler ((s, e) =>
					{
						if (e.PropertyName == propertyName)
						{
							slider.SetValue(source, property);
						}
					});

				source.PropertyChanged += handler;
				slider.ValueChanged += (sender, e) => property.GetSetMethod().Invoke (source, new object[]{ slider.Value });

				return handler;
			} 
			else
			{
				throw new InvalidCastException ("Binding property is not boolean");
			}
		}

	}
}

