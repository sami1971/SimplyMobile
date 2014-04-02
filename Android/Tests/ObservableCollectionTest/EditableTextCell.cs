using System;
using Android.Content;
using Android.Util;
using Android.Widget;

namespace ObservableCollectionTest
{
    /// <summary>
    /// Editable text cell for Android view.
    /// </summary>
    public class EditableTextCell : LinearLayout
    {
        private Switch switchCheck;
        private EditText textField;
        private EditableText editableText;

        public EditableTextCell (Context context) :
            base (context)
        {
            Initialize ();
        }

        public EditableTextCell (Context context, IAttributeSet attrs) :
            base (context, attrs)
        {
            Initialize ();
        }

        public EditableTextCell (Context context, IAttributeSet attrs, int defStyle) :
            base (context, attrs, defStyle)
        {
            Initialize ();
        }

        /// <summary>
        /// Bind the specified editableText.
        /// </summary>
        /// <param name="editableText">Editable text.</param>
        public void Bind(EditableText editableText)
        {
            if (this.editableText != null)
            {
                this.editableText.PropertyChanged -= HandlePropertyChanged;
                this.switchCheck.CheckedChange -= HandleValueChanged;
                this.textField.AfterTextChanged -= HandleEditingChanged;
            }

            this.textField.Text = editableText.Text;
            this.switchCheck.Checked = this.textField.Enabled =  editableText.Checked;

            this.editableText = editableText;
            this.editableText.PropertyChanged += HandlePropertyChanged;

            this.switchCheck.CheckedChange += HandleValueChanged;
            this.textField.AfterTextChanged += HandleEditingChanged;
        }

        private void HandleEditingChanged (object sender, EventArgs e)
        {
            this.editableText.Text = this.textField.Text;
        }

        private void HandleValueChanged (object sender, EventArgs e)
        {
            this.editableText.Checked = this.textField.Enabled = this.switchCheck.Checked;
        }

        private void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text" && this.textField.Text != this.editableText.Text)
            {
                this.textField.Text = this.editableText.Text;
            }
            else if (e.PropertyName == "Checked" && this.switchCheck.Checked != this.editableText.Checked)
            {
                this.switchCheck.Checked = this.editableText.Checked;
            }
        }

        private void Initialize ()
        {
            this.LayoutParameters = new LayoutParams (LayoutParams.FillParent, 75);
            this.switchCheck = new Switch (this.Context);
            this.AddView (this.switchCheck);

            this.textField = new EditText (this.Context) 
            { 
                Enabled = false,
                LayoutParameters = new LayoutParams(LayoutParams.FillParent, LayoutParams.FillParent)
            };

            this.AddView (this.textField);
        }
    }
}

