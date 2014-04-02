using System;
using Android.Widget;
using System.Reflection;
using System.ComponentModel;
using Android.App;

namespace SimplyMobile.Core
{
    public static class CompoundButtonExtensions
    {
        public static void SetValue(this CompoundButton toggle, object source, PropertyInfo property)
        {
            var value = (bool)property.GetValue(source);

            if (toggle.Checked != value)
            {
                toggle.Checked = value;
            }
        }

        public static PropertyChangedEventHandler Bind(this CompoundButton toggle, INotifyPropertyChanged source, string propertyName)
        {
            var property = source.GetProperty(propertyName);

            if (property.PropertyType == typeof(bool))
            {
                toggle.SetValue(source, property);
                var handler = new PropertyChangedEventHandler ((s, e) =>
                    {
                        if (e.PropertyName == propertyName)
                        {
                            var activity = toggle.Context as Activity;

                            if (activity != null)
                            {
                                activity.RunOnUiThread(() => toggle.SetValue(source, property));
                            }
                        }
                    });

                source.PropertyChanged += handler;
                toggle.CheckedChange += (sender, e) => property.GetSetMethod().Invoke (source, new object[]{ toggle.Checked });

                return handler;
            } 
            else
            {
                throw new InvalidCastException ("Binding property is not boolean");
            }
        }

    }
}

