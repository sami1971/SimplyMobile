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
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace SimplyMobile.Data
{
    /// <summary>
    /// Table cell provider interface.
    /// </summary>
    /// <description>
    /// Implement this interface in your UITableView class to override
    /// the default table cell view in your application.
    /// 
    /// NOTE: Implementing this in other than the table view bind to the
    /// data source will not have any effect.
    /// </description>
    public interface ITableCellProvider<T>
    {
        /// <summary>
        /// Gets the custom cell.
        /// </summary>
        /// <returns><see cref="MonoTouch.UIKit.UITableViewCell"/></returns>
        /// <param name="item">Item.</param>
        UITableViewCell GetCell(T item);

        /// <summary>
        /// Gets the height for row.
        /// </summary>
        /// <returns><see cref="float"/></see></returns>
        /// <param name="indexPath">Index path.</param>
        float GetHeightForRow (NSIndexPath indexPath, T item);
    }
}

