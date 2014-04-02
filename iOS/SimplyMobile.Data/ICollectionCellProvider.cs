using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace SimplyMobile.Data
{
    public interface ICollectionCellProvider<T>
    {
        UICollectionViewCell GetCell (T item, NSIndexPath indexPath);
    }
}

