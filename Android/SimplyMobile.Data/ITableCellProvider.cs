using System;
using Android.Views;

namespace SimplyMobile.Data
{
	public interface ITableCellProvider
	{
		View GetView(object item, View convertView);
	}
}

