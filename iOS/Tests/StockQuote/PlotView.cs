using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.CoreAnimation;

using CorePlot;
using MonoTouch.CoreGraphics;

namespace StockQuote
{
    [Register("PlotView")]
    public class PlotView : UIView
    {
        public PlotView ()
        {
            Initialize ();
        }

        public PlotView (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        private void Initialize()
        {
            var graph = new CPTXYGraph (this.Frame, CPTScaleType.Linear, CPTScaleType.Linear) {
                Title = "Stock History",
                BackgroundColor = new CGColor(0.982f, 0.982f, 0.890f)
            };

            var host = new CPTGraphHostingView (this.Frame) {
                HostedGraph = graph,
                BackgroundColor = UIColor.Red
            };

            this.AddSubview (host);
            this.BackgroundColor = UIColor.Red;
        }
    }
}

