using System;
using Android.Views;

namespace SimplyMobile.Data
{
    public interface IDropDownCellProvider<T>
	{
        View GetDropDownView(T item, View convertView);
	}
}

