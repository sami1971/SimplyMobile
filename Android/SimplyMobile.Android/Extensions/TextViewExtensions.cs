using System;
using Android.Widget;
using System.ComponentModel;
using System.Reflection;
using Android.App;

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

        public static PropertyChangedEventHandler Bind(this TextView textField, INotifyPropertyChanged source, string propertyName)
        {
            var property = source.GetProperty(propertyName);

            textField.SetText(source, property);

            var handler = new PropertyChangedEventHandler( (sender, e) =>
            {
                if (e.PropertyName == propertyName)
                {
                    var activity = textField.Context as Activity;

                    if (activity != null)
                    {
                        activity.RunOnUiThread(() => textField.SetText(source, property));
                    }
                }
                });

            source.PropertyChanged += handler;

            return handler;
            //textField.AfterTextChanged += (sender, e) => property.GetSetMethod().Invoke(source, new []{textField.Text});
        }
    }
}

