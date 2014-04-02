using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using SimplyMobile.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace JavaScriptValidator
{
    public partial class TextCell : UITableViewCell
    {
        private TextNode model;
        private List<PropertyChangedEventHandler> handlers;

        public static readonly NSString Key = new NSString ("TextCell");
        public static readonly UINib Nib;

        static TextCell ()
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
                Nib = UINib.FromName ("TextCell_iPhone", NSBundle.MainBundle);
            else
                Nib = UINib.FromName ("TextCell_iPad", NSBundle.MainBundle);
        }

        public TextCell (IntPtr handle) : base (handle)
        {
        }

        public static TextCell Create ()
        {
            return (TextCell)Nib.Instantiate (null, null) [0];
        }

        public void Bind(TextNode model)
        {
            Unbind ();
            this.model = model;
//          this.model.PropertyChanged += HandlePropertyChanged;
            this.handlers = new List<PropertyChangedEventHandler> ();
            this.handlers.Add(this.labelCaption.Bind (model, "Caption"));
            this.handlers.Add(this.textText.Bind(model, "Text"));

            this.model.PropertyChanged += HandlePropertyChanged;
            this.textText.Enabled = this.model.Enabled;
        }

        void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Enabled")
            {
                this.textText.Enabled = this.model.Enabled;
            }
        }

        public void Unbind()
        {
            if (this.handlers != null)
            {
                foreach (var handler in this.handlers)
                {
                    this.model.PropertyChanged -= handler;
                }
            }
            if (this.model != null)
            {
                this.model.PropertyChanged -= HandlePropertyChanged;
            }
            this.model = null;
        }

        protected override void Dispose (bool disposing)
        {
            Unbind ();
            base.Dispose (disposing);
        }
    }
}

