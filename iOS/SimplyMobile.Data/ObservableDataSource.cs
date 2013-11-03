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
	public partial class ObservableDataSource<T>
    {
        private float defaultRowHeight = 22;

		private TableViewDelegate tableDelegate;

		private CollectionViewDelegate collectionDelegate;

		private TableDataSource tableSource;

		private CollectionDataSource collectionDataSource;

		/// <summary>
		/// Gets the table delegate.
		/// </summary>
		/// <value>The table delegate.</value>
		private TableViewDelegate TableDelegate
        {
            get
            {
				return this.tableDelegate ?? (this.tableDelegate = new TableViewDelegate(this.RowSelected, defaultRowHeight));
            }
        }

		/// <summary>
		/// Gets the collection delegate.
		/// </summary>
		/// <value>The collection delegate.</value>
		private CollectionViewDelegate CollectionDelegate
		{
			get
			{
				return this.collectionDelegate ?? (this.collectionDelegate = new CollectionViewDelegate(this.RowSelected));
			}
		}

		/// <summary>
		/// Gets the table source.
		/// </summary>
		/// <value>The table source.</value>
		private TableDataSource TableSource
		{
			get
			{
				return this.tableSource ?? (this.tableSource = new TableDataSource(this));
			}
		}

		/// <summary>
		/// Gets the collection source.
		/// </summary>
		/// <value>The collection source.</value>
		private CollectionDataSource CollectionSource
		{
			get
			{
				return this.collectionDataSource ?? (this.collectionDataSource = new CollectionDataSource(this));
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
        /// Row selected from delegate.
        /// </summary>
        /// <param name="sender">Sender view.</param>
		/// <param name="args">Event arguments with index path.</param>
        private void RowSelected(object sender, EventArgs<int> args)
        {
			this.InvokeItemSelectedEvent (sender, this.Data[args.Value]);
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
                    tableView.DataSource = this.TableSource;
                    tableView.Delegate = this.TableDelegate;
					tableView.InvokeOnMainThread (tableView.ReloadData);
                }

				foreach (var collectionView in notifyCollectionChangedEventArgs.NewItems.OfType<UICollectionView>().Where(a=>a is ICollectionCellProvider<T>))
				{
					collectionView.DataSource = this.CollectionSource;
					collectionView.Delegate = this.CollectionDelegate;
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

		private class CollectionDataSource : UICollectionViewDataSource
		{
			private readonly ObservableDataSource<T> source;

			internal CollectionDataSource(ObservableDataSource<T> source)
			{
				this.source = source;
			}

			#region implemented abstract members of UICollectionViewDataSource

			public override int GetItemsCount (UICollectionView collectionView, int section)
			{
				return this.source.Data.Count;
			}

			public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
			{
				var cellProvider = collectionView as ICollectionCellProvider<T>;

				return cellProvider.GetCell (this.source.Data.ElementAt(indexPath.Row), indexPath);
			}

			#endregion
		}

		private class TableDataSource : UITableViewDataSource
		{
			private readonly ObservableDataSource<T> source;

			internal TableDataSource(ObservableDataSource<T> source)
			{
				this.source = source;
			}

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
			public override int RowsInSection(UITableView tableView, int section)
			{
				return this.source.Data.Count;
			}

			/// <summary>
			/// The get cell.
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
			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				this.source.InvokeItemRequestedEvent (tableView, indexPath.Row);

				var item = this.source.Data[indexPath.Row];

				var cellProvider = tableView as ITableCellProvider<T>;

				if (cellProvider != null)
				{
					return cellProvider.GetCell (item);
				}

				var cell = tableView.DequeueReusableCell(this.source.CellId);

				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Value1, this.source.CellId);
				}

				cell.TextLabel.Text = item.ToString();
				return cell;
			}
		}

        /// <summary>Private UITableViewDelegate to capture row selected events</summary>
        private class TableViewDelegate : UITableViewDelegate
        {
            /// <summary>
            /// Occurs when on selection.
            /// </summary>
            private EventHandler<EventArgs<int>> OnSelected;

            private float defaultRowHeight;

            public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                var cellProvider = tableView as ITableCellProvider<T>;

                if (cellProvider != null)
                {
                    return cellProvider.GetHeightForRow(indexPath);
                }

				return defaultRowHeight;
            }

            /// <summary>
            /// Rows the selected.
            /// </summary>
            /// <param name="tableView">Table view.</param>
            /// <param name="indexPath">Index path.</param>
            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                this.OnSelected(tableView, new EventArgs<int>(indexPath.Item));
            }

			public override void RowDeselected (UITableView tableView, NSIndexPath indexPath)
			{

			}

			public override void RowHighlighted (UITableView tableView, NSIndexPath rowIndexPath)
			{

			}

			public override void RowUnhighlighted (UITableView tableView, NSIndexPath rowIndexPath)
			{

			}

            /// <summary>
            /// Initializes a new instance of the <see cref="WK.ComplyTrack.Mobile.UI.DataItemSelected"/> class.
            /// </summary>
            /// <param name="itemDelegate">Item delegate.</param>
			public TableViewDelegate(EventHandler<EventArgs<int>> itemDelegate, float defaultRowHeight = 22)
            {
                this.OnSelected = itemDelegate;
                this.defaultRowHeight = defaultRowHeight;
            }
        }

		private class CollectionViewDelegate : UICollectionViewDelegate
		{
			/// <summary>
			/// Occurs when on selection.
			/// </summary>
			private EventHandler<EventArgs<int>> OnSelected;

			public CollectionViewDelegate(EventHandler<EventArgs<int>> itemDelegate)
			{
				this.OnSelected = itemDelegate;
			}

			public override void ItemSelected (UICollectionView collectionView, NSIndexPath indexPath)
			{
				this.OnSelected (this, new EventArgs<int> (indexPath.Row));
			}

			public override void ItemDeselected (UICollectionView collectionView, NSIndexPath indexPath)
			{

			}

			public override void ItemHighlighted (UICollectionView collectionView, NSIndexPath indexPath)
			{

			}

			public override void ItemUnhighlighted (UICollectionView collectionView, NSIndexPath indexPath)
			{

			}
		}
    }
}
