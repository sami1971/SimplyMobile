// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace ObservableCollectionTest
{
    [Register ("EditableTextViewController")]
    partial class EditableTextViewController
    {
        [Outlet]
        MonoTouch.UIKit.UIButton buttonAdd { get; set; }

        [Outlet]
        ObservableCollectionTest.EditableTextCollectionView collectionView { get; set; }

        [Outlet]
        MonoTouch.UIKit.UILabel labelLastChecked { get; set; }

        [Outlet]
        MonoTouch.UIKit.UILabel labelLastText { get; set; }

        [Outlet]
        ObservableCollectionTest.EditableTextTable tableView { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (buttonAdd != null) {
                buttonAdd.Dispose ();
                buttonAdd = null;
            }

            if (collectionView != null) {
                collectionView.Dispose ();
                collectionView = null;
            }

            if (labelLastChecked != null) {
                labelLastChecked.Dispose ();
                labelLastChecked = null;
            }

            if (labelLastText != null) {
                labelLastText.Dispose ();
                labelLastText = null;
            }

            if (tableView != null) {
                tableView.Dispose ();
                tableView = null;
            }
        }
    }
}
