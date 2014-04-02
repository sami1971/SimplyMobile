using System;
using MonoTouch.UIKit;
using System.Reflection;
using System.ComponentModel;

namespace SimplyMobile.Core
{
    public static class TextFieldExtensions
    {
        public static void SetText(this UITextField textField, object source, PropertyInfo property)
        {
            var text = property.GetValue(source).ToString();

            if (textField.Text != text)
            {
                textField.Text = text;
            }
        }

        public static Action Bind(this UITextField textField, INotifyPropertyChanged source, string propertyName)
        {
            var property = source.GetProperty(propertyName);

            textField.SetText(source, property);
            var handler = new PropertyChangedEventHandler((s, e) =>
                {
                    if (e.PropertyName == propertyName)
                    {
                        textField.InvokeOnMainThread(()=>
                            textField.SetText(source, property));
                    }
                });

            source.PropertyChanged += handler;

            var editHandler = new EventHandler((s, e) =>
                {
                    var sender = s as UITextField;
                    if (sender != null)
                    {
                        property.GetSetMethod().Invoke(source, new[] { sender.Text });
                    }
                });

            textField.EditingChanged += editHandler;

            // create an action to remove the event handlers
            return new Action(() =>
                {
                    textField.EditingChanged -= editHandler;
                    source.PropertyChanged -= handler;
                });
        }
    }
}

