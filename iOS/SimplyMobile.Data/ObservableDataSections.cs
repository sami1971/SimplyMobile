using System;
using MonoTouch.UIKit;
using System.Collections.Specialized;
using System.Linq;

namespace SimplyMobile.Data
{
    public partial class ObservableDataSections : UITableViewDataSource
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

        #region implemented abstract members of UITableViewDataSource

        public override int RowsInSection (UITableView tableView, int section)
        {
            return this.Sections[section].Values.Count;
        }

        public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
        {
            var item = this.Sections [indexPath.Section].Values [indexPath.Row];

            var cell = tableView.DequeueReusableCell(this.CellId);

            return this.SetCell(cell, item);
        }

        /// <summary>
        /// Sets the cell.
        /// </summary>
        /// <param name="cell">Cell to set.</param>
        /// <param name="item">Item object.</param>
        /// <remarks>Override for custom cell implementations.</remarks>                
        protected virtual UITableViewCell SetCell(UITableViewCell cell, object item)
        {
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Value1, this.CellId);
            }

            cell.TextLabel.Text = item.ToString();
            return cell;
        }

        public override int NumberOfSections (UITableView tableView)
        {
            return this.Sections.Count;
        }

        public override string TitleForHeader (UITableView tableView, int section)
        {
            return this.Sections [section].Section.ToString();
        }
        #endregion

        /// <summary>
        /// The collection changed partial method must be implemented in the OS specific code.
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
                tableView.InvokeOnMainThread (tableView.ReloadData);
            }
        }

        /// <summary>
        /// Partial class to be implemented in the OS specific code.
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

