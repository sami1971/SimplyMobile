using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ObservableCollectionTest
{
    public partial class EditableTextCollectionCell : UICollectionViewCell
    {
        public static readonly NSString Key = new NSString ("EditableTextCollectionCell");
        public static readonly UINib Nib;

        private EditableText editableText;

        static EditableTextCollectionCell ()
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
                Nib = UINib.FromName ("EditableTextCollectionCell_iPhone", NSBundle.MainBundle);
            else
                Nib = UINib.FromName ("EditableTextCollectionCell_iPad", NSBundle.MainBundle);
        }

        public EditableTextCollectionCell (IntPtr handle) : base (handle)
        {
        }

        public static EditableTextCollectionCell Create ()
        {
            return (EditableTextCollectionCell)Nib.Instantiate (null, null) [0];
        }

        public void Bind(EditableText editableText)
        {
            this.textField.Text = editableText.Text;
            this.switchCheck.On = editableText.Checked;

            if (this.editableText != null)
            {
                this.editableText.PropertyChanged -= HandlePropertyChanged;
                this.switchCheck.ValueChanged -= HandleValueChanged;
                this.textField.EditingChanged -= HandleEditingChanged;
            }

            this.editableText = editableText;
            this.editableText.PropertyChanged += HandlePropertyChanged;

            this.switchCheck.ValueChanged += HandleValueChanged;
            this.textField.EditingChanged += HandleEditingChanged;
        }

        private void HandleEditingChanged (object sender, EventArgs e)
        {
            this.editableText.Text = this.textField.Text;
        }

        private void HandleValueChanged (object sender, EventArgs e)
        {
            this.editableText.Checked = this.textField.Enabled = this.switchCheck.On;
        }

        private void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text" && this.textField.Text != this.editableText.Text)
            {
                this.textField.Text = this.editableText.Text;
            }
            else if (e.PropertyName == "Checked" && this.switchCheck.On != this.editableText.Checked)
            {
                this.textField.Enabled = this.switchCheck.On = this.editableText.Checked;
            }
        }
    }
}

