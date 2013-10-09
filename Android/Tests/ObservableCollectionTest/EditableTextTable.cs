using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SimplyMobile.Data;
using Android.Util;

namespace ObservableCollectionTest
{
	public class EditableTextTable : ListView, ITableCellProvider<EditableText>
	{
		public EditableTextTable(Context context) : base(context)
		{
		}

		public EditableTextTable(Context context, IAttributeSet attrs) :  base(context, attrs) 
		{

		}

		public EditableTextTable(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle) 
		{

		}

		#region ITableCellProvider implementation
		public View GetView (EditableText item, View convertView)
		{
			var editableCell = convertView as EditableTextCell ?? new EditableTextCell(this.Context);

			editableCell.Bind (item);

			return editableCell;
		}
		#endregion
	}
}

