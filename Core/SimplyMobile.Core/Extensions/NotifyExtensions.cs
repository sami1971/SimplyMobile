using System;
using System.Reflection;
using System.ComponentModel;
using System.Linq;

namespace SimplyMobile.Core
{
	public static class NotifyExtensions
	{
		public static PropertyInfo GetProperty(this INotifyPropertyChanged source, string propertyName)
		{
			var t = source.GetType ();

			var prop = t.GetTypeInfo().DeclaredProperties.FirstOrDefault (a => a.Name == propertyName);

			if (prop == null)
			{
				throw new Exception(
					string.Format("Property {0} not found in {1}.", propertyName, t));
			}

			return prop;
		}
	}
}

