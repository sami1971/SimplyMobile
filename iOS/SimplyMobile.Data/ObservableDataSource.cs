using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace SimplyMobile.Data
{
    public partial class ObservableDataSource : UITableViewDataSource
		// todo: investigate UITableViewSource as an altenative to UITableViewDataSource
    {
        /// <summary>
        /// The cell identifier. 
        /// </summary>
        /// <remarks>Default value is "cid"</remarks>
        private string cellId = "cid";

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
            return this.Data.Count;
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
			var item = this.Data[indexPath.Row];

			var cellProvider = tableView as ITableCellProvider;

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
                    tableView.DataSource = this;
					tableView.InvokeOnMainThread (tableView.ReloadData);
                }
            }
            else if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var tableView in notifyCollectionChangedEventArgs.OldItems.OfType<UITableView>())
                {
                    tableView.DataSource = null;
					tableView.InvokeOnMainThread (tableView.ReloadData);
                }
            }
        }
    }
}
