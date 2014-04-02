using Android.Content;
using Android.Views;
using Android.Widget;
using SimplyMobile.Data;
using Android.Util;

namespace ObservableCollectionTest
{
    /// <summary>
    /// Editable text table.
    /// </summary>
    public class EditableTextTable : ListView, ITableCellProvider<EditableText>
    {
        #region Android AXML required constructors
        public EditableTextTable (Context context) : base(context)
        {
        }

        public EditableTextTable (Context context, IAttributeSet attrs) :  base(context, attrs)
        {

        }

        public EditableTextTable (Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {

        }
        #endregion

        #region ITableCellProvider implementation
        /// <summary>
        /// Gets the Android View cell to display in EditableTextTable.
        /// </summary>
        /// <returns>The view.</returns>
        /// <param name="item">Item to bind with.</param>
        /// <param name="convertView">Convert view.</param>
        public View GetView (EditableText item, View convertView)
        {
            var editableCell = convertView as EditableTextCell ?? new EditableTextCell(this.Context);

            editableCell.Bind (item);

            return editableCell;
        }
        #endregion
    }
}

