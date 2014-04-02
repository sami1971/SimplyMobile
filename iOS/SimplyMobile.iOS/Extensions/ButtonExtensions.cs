using System;
using MonoTouch.UIKit;
using System.Reflection;
using System.ComponentModel;

namespace SimplyMobile.Core
{
    public static class ButtonExtensions
    {
        public static void OnClick(this UIButton button, EventHandler handler)
        {
            button.TouchUpInside += handler;
        }

        public static void SetTitle(this UIButton button, object source, PropertyInfo property,
            UIControlState state = UIControlState.Normal)
        {
            var text = property.GetValue(source).ToString();

            if (button.Title(state) != text)
            {
                button.InvokeOnMainThread(() => button.SetTitle(text, state));
            }
        }

        public static Action BindTitle(this UIButton button, 
                    INotifyPropertyChanged source, 
                    string propertyName,
                    UIControlState state = UIControlState.Normal)
        {
            var property = source.GetProperty(propertyName);

            button.SetTitle (source, property, state);

            var handler = new PropertyChangedEventHandler(
                (s, e) =>
                {
                    if (e.PropertyName == propertyName)
                    {
                        button.SetTitle (source, property, state);
                    }
                }
            );

            source.PropertyChanged += handler;

            return new Action(() => source.PropertyChanged -= handler);
        }
    }
}

