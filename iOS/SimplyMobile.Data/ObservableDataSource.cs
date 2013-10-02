using System;
using System.Collections.Specialized;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using SimplyMobile.Core;

namespace SimplyMobile.Data
{
	/// <summary>
	/// Observable data source iOS portion. Implements <see cref="UITableViewDataSource"/>
	/// </summary>
	public partial class ObservableDataSource<T>
		// todo: investigate UITableViewSource as an altenative to UITableViewDataSource
    {
        private float defaultRowHeight = 22;

        private BaseDelegate eventDelegate;

		private TableDataSource tableSource;

		/// <summary>
		/// Gets the table delegate.
		/// </summary>
		/// <value>The table delegate.</value>
        private BaseDelegate TableDelegate
        {
            get
            {
                return this.eventDelegate ?? (this.eventDelegate = new BaseDelegate(this.RowSelected, defaultRowHeight));
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
        /// Rows the selected.
        /// </summary>
        /// <param name="tableView">Table view.</param>
        /// <param name="indexPath">Index path.</param>
        private void RowSelected(object sender, EventArgs<int> args)
        {
            if (this.OnSelected != null)
            {
                this.OnSelected(sender, new EventArgs<object>(this.Data[args.Value]));
            }
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
            }
            else if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var tableView in notifyCollectionChangedEventArgs.OldItems.OfType<UITableView>())
                {
                    tableView.DataSource = null;
                    tableView.Delegate = null;
					tableView.InvokeOnMainThread (tableView.ReloadData);
                }
            }
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
				var item = this.source.Data[indexPath.Row];

				var cellProvider = tableView as ITableCellProvider;

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
        private class BaseDelegate : UITableViewDelegate
        {
            /// <summary>
            /// Occurs when on selection.
            /// </summary>
            private EventHandler<EventArgs<int>> OnSelected;

            private float defaultRowHeight;

            public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
            {
                var cellProvider = tableView as ITableCellProvider;

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

            /// <summary>
            /// Initializes a new instance of the <see cref="WK.ComplyTrack.Mobile.UI.DataItemSelected"/> class.
            /// </summary>
            /// <param name="itemDelegate">Item delegate.</param>
            public BaseDelegate(EventHandler<EventArgs<int>> itemDelegate, float defaultRowHeight = 22)
            {
                this.OnSelected = itemDelegate;
                this.defaultRowHeight = defaultRowHeight;
            }
        }
    }
}
