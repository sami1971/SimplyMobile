using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BindingTests
{
	public class ExtensionViewModel : INotifyPropertyChanged
	{
		private long count;

		private string textField = string.Empty;

		public string TextField 
		{
			get 
			{
				return textField;
			}
			set 
			{
				if (textField != value) 
				{
					textField = value;
					NotifyPropertyChanged ();
				}
			}
		}

		public void OnButtonClick(object sender, EventArgs e)
		{
			this.TextField = string.Format ("Button pressed {0} times.", ++count);
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

