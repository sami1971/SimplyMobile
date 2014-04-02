using System;
using Android.Widget;
using System.ComponentModel;
using System.Reflection;
using Android.App;

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

        public static PropertyChangedEventHandler Bind(this EditText textField, INotifyPropertyChanged source, string propertyName)
        {
            var property = source.GetProperty(propertyName);

            textField.SetText(source, property);

            var handler = new PropertyChangedEventHandler ((s, e) =>
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

            textField.AfterTextChanged += (sender, e) => property.GetSetMethod().Invoke(source, new []{textField.Text});

            return handler;
        }
    }
}

