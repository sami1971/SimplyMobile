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
	public partial class ObservableDataSource<T> : NSObject
    {
        private float defaultRowHeight = 22;

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
            foreach (var tableView in this.observers.OfType<UITableView>())
            {
				tableView.InvokeOnMainThread(tableView.ReloadData);
            }
			foreach (var collectionView in this.observers.OfType<UICollectionView>())
			{
				collectionView.InvokeOnMainThread(collectionView.ReloadData);
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
                foreach (var tableView in notifyCollectionChangedEventArgs.NewItems.OfType<UITableView>())
                {
					tableView.WeakDataSource = this;
					tableView.WeakDelegate = this;
					tableView.InvokeOnMainThread (tableView.ReloadData);
                }

				foreach (var collectionView in notifyCollectionChangedEventArgs.NewItems.OfType<UICollectionView>().Where(a => a is ICollectionCellProvider<T>))
				{
					collectionView.WeakDataSource = this;
					collectionView.WeakDelegate = this;
					collectionView.InvokeOnMainThread(collectionView.ReloadData);
				}
            }
            else if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var tableView in notifyCollectionChangedEventArgs.OldItems.OfType<UITableView>())
                {
                    tableView.DataSource = null;
                    tableView.Delegate = null;
					tableView.InvokeOnMainThread (tableView.ReloadData);
                }

				foreach (var collectionView in notifyCollectionChangedEventArgs.NewItems.OfType<UICollectionView>())
				{
					collectionView.DataSource = null;
					collectionView.Delegate = null;
					collectionView.InvokeOnMainThread(collectionView.ReloadData);
				}
            }
        }

		#region UITableView weak delegate
		[Export("tableView:cellForRowAtIndexPath:")]
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

			var cellProvider = tableView as ITableCellProvider<T>;

			if (cellProvider != null)
			{
				return cellProvider.GetCell (item);
			}

			var cell = tableView.DequeueReusableCell(this.CellId);

			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Value1, this.CellId);
			}

			cell.TextLabel.Text = item.ToString();
			return cell;
		}

		[Export("tableView:numberOfRowsInSection:")]
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

		[Export("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			this.InvokeItemSelectedEvent(tableView, this.Data[indexPath.Item]);
		}

		[Export("tableView:heightForRowAtIndexPath:")]
		public float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			var cellProvider = tableView as ITableCellProvider<T>;

			if (cellProvider != null)
			{
				return cellProvider.GetHeightForRow(indexPath);
			}

			return this.DefaultRowHeight;
		}
		#endregion

		#region UICollectionView weak delegate
		[Export("collectionView:didSelectItemAtIndexPath:")]
		public void ItemSelected (UICollectionView collectionView, NSIndexPath indexPath)
		{
			this.InvokeItemSelectedEvent(collectionView, this.Data[indexPath.Item]);
		}

		[Export("collectionView:numberOfItemsInSection:")]
		public int RowsInSection(UICollectionView tableView, int section)
		{
			return this.Data.Count;
		}

		[Export("collectionView:cellForItemAtIndexPath:")]
		public UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cellProvider = collectionView as ICollectionCellProvider<T>;

			return cellProvider.GetCell (this.Data[indexPath.Row], indexPath);
		}
		#endregion
    }
}
