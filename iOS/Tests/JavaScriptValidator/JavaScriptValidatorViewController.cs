using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using SimplyMobile.Data;

namespace JavaScriptValidator
{
    public partial class JavaScriptValidatorViewController : UIViewController
    {
        public JavaScriptValidatorViewController () : base ("JavaScriptValidatorViewController", null)
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

            var doc = new Document () 
            {
                Nodes = new ObservableDataSource<DocumentNode>(
                    new DocumentNode[] 
                    {
                        new CheckBoxNode() { Name = "Checkbox", Caption = "Some checkbox", Checked = false},
                        new TextNode() { Name = "MyText", Caption = "Enabled only when checked"}
                    }),

            };

            doc.Nodes.Bind (this.tableDocument);
        }
    }
}

