using System;
using System.ComponentModel;
using Android.Widget;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace SimplyMobile.Core
{
    public static class SeekBarExtensions
    {
        public static PropertyChangedEventHandler Bind(this SeekBar seekBar, INotifyPropertyChanged source, string propertyName)
        {
            var property = source.GetProperty(propertyName);

            var r = property.GetCustomAttribute<RangeAttribute> ();

            if (r != null)
            {
                seekBar.Max = (int)r.Maximum;
            }

            var handler = new PropertyChangedEventHandler ((s, e) =>
                {
                    if (e.PropertyName == propertyName)
                    {
                        //textField.SetText(source, property);
                    }
                });

            source.PropertyChanged += handler;

            //textField.AfterTextChanged += (sender, e) => property.GetSetMethod().Invoke(source, new []{textField.Text});

            return handler;
        }
    }
}

