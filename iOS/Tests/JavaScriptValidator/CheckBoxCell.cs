using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.ComponentModel;
using SimplyMobile.Core;

namespace JavaScriptValidator
{
    public partial class CheckBoxCell : UITableViewCell, ITableCell<CheckBoxNode> 
    {
        private CheckBoxNode model;
        private List<PropertyChangedEventHandler> handlers;

        public static readonly NSString Key = new NSString ("CheckBoxCell");
        public static readonly UINib Nib;

        static CheckBoxCell ()
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
                Nib = UINib.FromName ("CheckBoxCell_iPhone", NSBundle.MainBundle);
            else
                Nib = UINib.FromName ("CheckBoxCell_iPad", NSBundle.MainBundle);
        }

        public CheckBoxCell (IntPtr handle) : base (handle)
        {
        }

        public static CheckBoxCell Create ()
        {
            return (CheckBoxCell)Nib.Instantiate (null, null) [0];
        }

        public void Bind(CheckBoxNode model)
        {
            Unbind ();
            this.model = model;
//          this.model.PropertyChanged += HandlePropertyChanged;
            this.handlers = new List<PropertyChangedEventHandler> ();
            this.handlers.Add(this.labelCaption.Bind (model, "Caption"));
            this.handlers.Add(this.switchChecked.Bind(model, "Checked"));
        }

//      void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
//      {
//
//      }

        public void Unbind()
        {
            if (this.handlers != null)
            {
                foreach (var handler in this.handlers)
                {
                    this.model.PropertyChanged -= handler;
                }
            }
//          if (this.model != null)
//          {
////                this.model.PropertyChanged -= HandlePropertyChanged;
//          }
            this.model = null;
        }

        protected override void Dispose (bool disposing)
        {
            Unbind ();
            base.Dispose (disposing);
        }
    }
}

