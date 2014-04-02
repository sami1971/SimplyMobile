using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WebClientTests;

namespace CanvasDemo.iOS
{
    public partial class LabelCell : UITableViewCell
    {
        private DataPoint dataPoint;

        public static readonly NSString Key = new NSString ("LabelCell");
        public static readonly UINib Nib;

        static LabelCell ()
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
                Nib = UINib.FromName ("LabelCell_iPhone", NSBundle.MainBundle);
            else
                Nib = UINib.FromName ("LabelCell_iPad", NSBundle.MainBundle);
        }

        public LabelCell (IntPtr handle) : base (handle)
        {
        }

        public static LabelCell Create ()
        {
            return (LabelCell)Nib.Instantiate (null, null) [0];
        }

        public void Bind(DataPoint dataPoint)
        {
            this.dataPoint = dataPoint;

            this.textLabel.Text = dataPoint.Label;
            this.textLabel.EditingChanged += textLabel_EditingChanged;

            if (this.sliderY != null) 
            {
                this.sliderY.Value = dataPoint.Y;
//                this.sliderY.EditingChanged -= sliderY_EditingChanged;
//                this.sliderY.EditingChanged += sliderY_EditingChanged;
                this.sliderY.ValueChanged -= sliderY_EditingChanged;
                this.sliderY.ValueChanged += sliderY_EditingChanged;
            } 
            else 
            {
                this.textY.Text = dataPoint.Y.ToString();
                this.textY.EditingChanged -= textY_EditingChanged;
                this.textY.EditingChanged += textY_EditingChanged;
            }

            
        }

        void textLabel_EditingChanged(object sender, EventArgs e)
        {
            this.dataPoint.Label = this.textLabel.Text;
        }

        void textY_EditingChanged(object sender, EventArgs e)
        {
            long val;
            if (long.TryParse(this.textY.Text, out val))
            {
                this.dataPoint.Y = val;
            }
            else
            {
                this.textY.Text = this.dataPoint.Y.ToString();
            }
        }

        void sliderY_EditingChanged(object sender, EventArgs e)
        {
            this.dataPoint.Y = (long)this.sliderY.Value;
        }
    }
}

