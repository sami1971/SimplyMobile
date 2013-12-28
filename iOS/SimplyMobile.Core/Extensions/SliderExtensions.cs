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

		public static void SetMinimum(this UISlider slider, object source, PropertyInfo property)
		{
			var value = (double)property.GetValue(source);
			float f = (float)value;

			if (slider.MinValue != f)
			{
				slider.MinValue = f;
			}
		}

		public static void SetMaximum(this UISlider slider, object source, PropertyInfo property)
		{
			var value = (double)property.GetValue(source);
			float f = (float)value;

			if (slider.MaxValue != f)
			{
				slider.MaxValue = f;
			}
		}


		public static PropertyChangedEventHandler Bind(this UISlider slider, INotifyPropertyChanged source, string propertyName)
		{
			var property = GetProperty(source, propertyName);

			var r = property.GetCustomAttribute<RangeAttribute> ();

			if (r != null)
			{
				slider.SetRange(r);
			}

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

		public static Tuple<PropertyChangedEventHandler,EventHandler> Bind(
			this UISlider slider, 
			INotifyPropertyChanged source, 
			string valueName,
			string minName,
			string maxName)
		{
			var propertyValue = GetProperty(source, valueName);
			var propertyMin = GetProperty(source, minName);
			var propertyMax = GetProperty(source, maxName);

			slider.SetValue(source, propertyValue);
			slider.SetMinimum(source, propertyMin);
			slider.SetMaximum(source, propertyMax);

			var handler = new PropertyChangedEventHandler ((s, e) =>
				{
					if (e.PropertyName == valueName)
					{
						slider.SetValue(source, propertyValue);
					}
					else if (e.PropertyName == minName)
					{
						slider.SetMinimum(source, propertyMin);
					}
					else if (e.PropertyName == maxName)
					{
						slider.SetMinimum(source, propertyMax);
					}
				});

			source.PropertyChanged += handler;

			var sliderHandler = new EventHandler ((s, e) => propertyValue.GetSetMethod().Invoke (source, new object[]{ slider.Value }));
			slider.ValueChanged += sliderHandler;

			return new Tuple<PropertyChangedEventHandler, EventHandler>(handler, sliderHandler);
		}

		private static PropertyInfo GetProperty(INotifyPropertyChanged source, string propertyName)
		{
			var property = source.GetProperty(propertyName);

			if (property.PropertyType == typeof(int) || property.PropertyType == typeof(double)
			    || property.PropertyType == typeof(float))
			{
				return property;
			}

			throw new InvalidCastException ("Binding property is not valid");
		}
	}
}

