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
    [Register ("DataCollectionViewController")]
    partial class DataCollectionViewController
    {
        [Outlet]
        MonoTouch.UIKit.UICollectionView collectionView { get; set; }

        [Outlet]
        MonoTouch.UIKit.UITableView tableBasic { get; set; }

        [Outlet]
        MonoTouch.UIKit.UITableView tableDetailed { get; set; }
        
        void ReleaseDesignerOutlets ()
        {
            if (collectionView != null) {
                collectionView.Dispose ();
                collectionView = null;
            }

            if (tableDetailed != null) {
                tableDetailed.Dispose ();
                tableDetailed = null;
            }

            if (tableBasic != null) {
                tableBasic.Dispose ();
                tableBasic = null;
            }
        }
    }
}
