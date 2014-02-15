using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SimplyMobile.Core
{
	public abstract class ViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation
		/// <summary>
		/// Occurs when property is changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion

		/// <summary>
		/// Unbind all handlers from property changed event.
		/// </summary>
		public void Unbind()
		{
			this.PropertyChanged = null;
		}

		/// <summary>
		/// Changes the property if the value is different and invokes PropertyChangedEventHandler.
		/// </summary>
		/// <param name="property">Property.</param>
		/// <param name="value">Value.</param>
		/// <param name="propertyName">Property name.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		protected void ChangeAndNotify<T>(ref T property, T value,  [CallerMemberName] string propertyName = "")
		{
			if (!EqualityComparer<T>.Default.Equals(property, value))
			{
				property = value;
				NotifyPropertyChanged (propertyName);
			}
		}

		/// <summary>
		/// Notifies the property changed.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		/// <summary>
		/// Validate the specified value for a property.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="propertyName">Property name.</param>
		/// <exception cref=""></exception>
		protected void Validate(object value, [CallerMemberName] string propertyName = "")
		{
			var validationContext = new ValidationContext( this )
			{
				MemberName = propertyName
			};
			Validator.ValidateProperty( value, validationContext );
		}

		/// <summary>
		/// Determines whether this value is valid for the property.
		/// </summary>
		/// <returns><c>true</c> if this value is valid; otherwise, <c>false</c>.</returns>
		/// <param name="value">Value to be set.</param>
		/// <param name="propertyName">Property name.</param>
		protected bool IsValid(object value, [CallerMemberName] string propertyName = "")
		{
			var validationContext = new ValidationContext( this )
			{
				MemberName = propertyName
			};

			try
			{
				Validate(value, propertyName);
			}
			catch
			{
				return false;
			}
			return true;
		}

		protected string GetMemberName([CallerMemberName] string propertyName = "")
		{
			return propertyName;
		}
	}
}

