//
//  Copyright 2013, Sami M. Kallio
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
using System;
using System.Collections.Specialized;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SimplyMobile.Data
{
    using Core;

    /// <summary>
    /// Observable data source iOS portion. Implements <see cref="UITableViewDataSource"/>
    /// </summary>
    public partial class ObservableDataSource<T> //: NSObject
    {
        private float defaultRowHeight = 22;

        private TableViewDelegate tableDelegate;
        private TableDataSource dataSource;

        private TableDataSource DataSource
        {
            get
            {
                return dataSource ??
                    (dataSource =
                    new TableDataSource(this.GetCell, this.RowsInSection));
            }
        }

        private TableViewDelegate TableDelegate
        {
            get
            {
                return tableDelegate ??
                    (tableDelegate =
                    new TableViewDelegate(this.RowSelected, this.GetHeightForRow));
            }
        }

        private CollectionDataSource collectionSource;

        private CollectionDataSource CollectionSource
        {
            get
            {
                return this.collectionSource ??
                    (this.collectionSource = new CollectionDataSource(this.RowsInSection, this.GetCell));
            }
        }

        private CollectionViewDelegate collectionDelegate;

        private CollectionViewDelegate CollectionDelegate
        {
            get
            {
                return this.collectionDelegate ??
                    (this.collectionDelegate = new CollectionViewDelegate(this.ItemSelected));
            }
        }

        private PickerDelegate pickerDelegate;

        private PickerDelegate PickerDelegate
        {
            get
            {
                return this.pickerDelegate ?? (this.pickerDelegate = new PickerDelegate ());
            }
        }
        /// <summary>
        /// The cell identifier. 
        /// </summary>
        /// <remarks>Default value is "cid"</remarks>
        private string cellId = "cid";

        /// <summary>
        /// Gets or sets the default height of the row.
        /// </summary>
        /// <value>The default height of the row.</value>
        public float DefaultRowHeight
        {
            get { return this.defaultRowHeight; }
            set { this.defaultRowHeight = value; }
        }

        /// <summary>
        /// Gets or sets the cell identifier.
        /// </summary>
        /// <value>The cell identifier.</value>
        public string CellId
        {
            get { return this.cellId; }
            set { this.cellId = value; }
        }

        /// <summary>
        /// The collection changed event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="notifyCollectionChangedEventArgs">
        /// The notify collection changed event args.
        /// </param>
        partial void CollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var refs = this.observers.Where(a=>a.IsAlive).Select(a=>a.Target).ToList();

            foreach (var tableView in refs.OfType<UITableView>())
            {
                tableView.InvokeOnMainThread(tableView.ReloadData);
            }
            foreach (var collectionView in refs.OfType<UICollectionView>())
            {
                collectionView.InvokeOnMainThread(collectionView.ReloadData);
            }
            foreach(var pickerView in refs.OfType<UIPickerView>())
            {
                pickerView.InvokeOnMainThread(pickerView.ReloadAllComponents);
            }
        }

        /// <summary>
        /// The observers changed event.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="notifyCollectionChangedEventArgs">
        /// The notify collection changed event args.
        /// </param>
        partial void ObserversChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                var refs = notifyCollectionChangedEventArgs.NewItems.OfType<WeakReference>().Where(a=>a.IsAlive).Select(a=>a.Target).ToList();

                foreach (var tableView in refs.OfType<UITableView>())
                {
                    tableView.DataSource = this.DataSource;
                    tableView.Delegate = this.TableDelegate;
                    //tableView.WeakDataSource = this;
                    //tableView.WeakDelegate = this;
                    tableView.InvokeOnMainThread (tableView.ReloadData);
                }

                // TODO: check why only ICollectionCellProvider
                foreach (var collectionView in refs.OfType<UICollectionView> ().Where(a => a is ICollectionCellProvider<T>))
                {
                    collectionView.DataSource = this.CollectionSource;
                    collectionView.Delegate = this.CollectionDelegate;
                    collectionView.InvokeOnMainThread (collectionView.ReloadData);
                }

                //foreach (var pickerView in notifyCollectionChangedEventArgs.NewItems.OfType<UIPickerView>())
                //{
                //    pickerView.DataSource = this;
                //    pickerView.WeakDelegate = this;
                //    pickerView.InvokeOnMainThread(pickerView.ReloadAllComponents);
                //}
            }
            else if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                var refs = notifyCollectionChangedEventArgs.OldItems.OfType<WeakReference>().Where(a=>a.IsAlive).Select(a=>a.Target).ToList();

                foreach (var tableView in refs.OfType<UITableView>())
                {
                    tableView.DataSource = null;
                    tableView.Delegate = null;
                    tableView.InvokeOnMainThread (tableView.ReloadData);
                }

                foreach (var collectionView in refs.OfType<UICollectionView>())
                {
                    collectionView.DataSource = null;
                    collectionView.Delegate = null;
                    collectionView.InvokeOnMainThread(collectionView.ReloadData);
                }

                //foreach (var pickerView in notifyCollectionChangedEventArgs.OldItems.OfType<UIPickerView>())
                //{
                //    pickerView.DataSource = null;
                //    pickerView.WeakDelegate = null;
                //    pickerView.InvokeOnMainThread(pickerView.ReloadAllComponents);
                //}
            }
        }

        #region UITableView weak delegate
        //[Export("tableView:cellForRowAtIndexPath:")]
        /// <summary>
        /// Gets cell for UITableView
        /// </summary>
        /// <param name="tableView">
        /// The table view.
        /// </param>
        /// <param name="indexPath">
        /// The index path.
        /// </param>
        /// <returns>
        /// The <see cref="UITableViewCell"/>.
        /// </returns>
        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            this.InvokeItemRequestedEvent (tableView, indexPath.Row);

            var item = this.Data[indexPath.Row];

            var cellProvider = tableView as ITableCellProvider<T> ?? this.FindProvider(tableView);

            if (cellProvider != null)
            {
                return cellProvider.GetCell (item);
            }

            var cell = tableView.DequeueReusableCell(this.CellId) ??
                new UITableViewCell(UITableViewCellStyle.Value1, this.CellId);

            cell.TextLabel.Text = item.ToString();
            return cell;
        }

        //[Export("tableView:numberOfRowsInSection:")]
        /// <summary>
        /// The rows in section.
        /// </summary>
        /// <param name="tableView">
        /// The table view.
        /// </param>
        /// <param name="section">
        /// The section.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int RowsInSection(UITableView tableView, int section)
        {
            return this.Data.Count;
        }

        //[Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            this.InvokeItemSelectedEvent(tableView, this.Data[indexPath.Item]);
        }

        //[Export("tableView:heightForRowAtIndexPath:")]
        public float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            var cellProvider = tableView as ITableCellProvider<T> ?? this.FindProvider(tableView);

            return cellProvider != null ? 
                cellProvider.GetHeightForRow (indexPath, this.Data [indexPath.Item]) : 
                this.DefaultRowHeight;

        }
        #endregion

        #region UICollectionView weak delegate
        //[Export("collectionView:didSelectItemAtIndexPath:")]
        public void ItemSelected (UICollectionView collectionView, NSIndexPath indexPath)
        {
            this.InvokeItemSelectedEvent(collectionView, this.Data[indexPath.Item]);
        }

        //[Export("collectionView:numberOfItemsInSection:")]
        public int RowsInSection(UICollectionView tableView, int section)
        {
            return this.Data.Count;
        }

        //[Export("collectionView:cellForItemAtIndexPath:")]
        public UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cellProvider = collectionView as ICollectionCellProvider<T>;

            return cellProvider.GetCell (this.Data[indexPath.Row], indexPath);
        }
        #endregion

        private class TableDataSource : UITableViewDataSource
        {
            public delegate UITableViewCell OnGetCell(UITableView tableView, NSIndexPath indexPath);
            public delegate int OnRowsInSection(UITableView tableView, int section);

            private readonly OnGetCell onGetCell;
            private readonly OnRowsInSection onRowsInSection;

            public TableDataSource(OnGetCell onGetCell, OnRowsInSection onRowsInSection)
            {
                this.onGetCell = onGetCell;
                this.onRowsInSection = onRowsInSection;
            }

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                return onGetCell(tableView, indexPath);
            }

            public override int RowsInSection(UITableView tableView, int section)
            {
                return onRowsInSection(tableView, section);
            }
        }

        private class TableViewDelegate : UITableViewDelegate
        {

            public delegate void OnRowSelected(UITableView tableView, NSIndexPath indexPath);
            public delegate float OnGetHeightForRow(UITableView tableView, NSIndexPath indexPath);


            private readonly OnRowSelected onRowSelected;
            private readonly OnGetHeightForRow onGetHeightForRow;

            public TableViewDelegate(OnRowSelected onRowSelected, OnGetHeightForRow onGetHeightForRow)
            {
                this.onRowSelected = onRowSelected;
                this.onGetHeightForRow = onGetHeightForRow;
            }

            public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                return onGetHeightForRow(tableView, indexPath);
            }

            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                onRowSelected(tableView, indexPath);
            }
        }

        private class CollectionDataSource : UICollectionViewDataSource
        {
            public delegate UICollectionViewCell OnGetCell (UICollectionView collectionView, NSIndexPath indexPath);
            public delegate int OnRowsInSection(UICollectionView tableView, int section);

            private readonly OnRowsInSection onRowsInSection;
            private readonly OnGetCell onGetCell;

            public CollectionDataSource(OnRowsInSection onRowsInSection, OnGetCell onGetCell)
            {
                this.onRowsInSection = onRowsInSection;
                this.onGetCell = onGetCell;
            }

            public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
            {
                return this.onGetCell(collectionView, indexPath);
            }

            public override int GetItemsCount(UICollectionView collectionView, int section)
            {
                return this.onRowsInSection(collectionView, section);
            }
        }

        private class CollectionViewDelegate : UICollectionViewDelegate
        {
            public delegate void OnItemSelected(UICollectionView collectionView, NSIndexPath indexPath);

            private readonly OnItemSelected onSelected;

            public CollectionViewDelegate(OnItemSelected onSelected)
            {
                this.onSelected = onSelected;
            }

            public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
            {
                 this.onSelected(collectionView, indexPath);
            }
        }
    }

    /// <summary>Private UITableViewDelegate to capture row selected events</summary>
    internal class PickerDelegate : UIPickerViewDelegate
    {

    }
}
