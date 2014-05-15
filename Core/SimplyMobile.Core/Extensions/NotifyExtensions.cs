using System;
using System.Reflection;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace SimplyMobile.Core
{
    public static class NotifyExtensions
    {
        public static void RemoveHandlers(this INotifyPropertyChanged source, IEnumerable<PropertyChangedEventHandler> handlers)
        {
            foreach (var handler in handlers)
            {
                source.PropertyChanged -= handler;
            }
        }

        public static PropertyInfo GetProperty(this INotifyPropertyChanged source, string propertyName)
        {
            var t = source.GetType ();

            var prop = t.GetTypeInfo().DeclaredProperties.FirstOrDefault (a => a.Name == propertyName);

            if (prop == null)
            {
                prop = t.GetRuntimeProperties().FirstOrDefault(a => a.Name == propertyName);
//              prop = t.GetProperties ().FirstOrDefault (a => a.Name == propertyName);
            }

            if (prop == null)
            {
                throw new Exception(
                    string.Format("Property {0} not found in {1}.", propertyName, t));
            }

            return prop;
        }
    }
}

