using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace ObservableCollectionTest
{
    public partial class EditableTextViewController : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public EditableTextViewController ()
            : base (UserInterfaceIdiomIsPhone ? "EditableTextViewController_iPhone" : "EditableTextViewController_iPad", null)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            // Perform any additional setup after loading the view, typically from a nib.

            EditableTextViewModel.Instance.Items.Bind (this.tableView);
            EditableTextViewModel.Instance.Items.Bind (this.collectionView);

            this.buttonAdd.TouchUpInside += (sender, e) => 
            {
                EditableTextViewModel.Instance.AddItem(new EditableText());
            };

            EditableTextViewModel.Instance.PropertyChanged += HandlePropertyChanged;
        }

        void HandlePropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var model = sender as EditableTextViewModel;
            if (model == null)
            {
                return;
            }

            if (e.PropertyName == "LatestTextChange")
            {
                this.labelLastText.Text = model.LatestTextChange.Text;
            }
            else
            {
                this.labelLastChecked.Text = model.LatestCheckChange.Text;
            }
        }
    }
}

