Core Plot is a plotting framework for OS X and iOS that allows you to create
simple or complex graphs quickly and easily. Core Plot is tightly integrated
with Apple technologies like Core Animation, Core Data, and Cocoa Bindings.

Here's an example of creating a simple graph with an XY scatter plot:

```csharp
using CorePlot;
using System.Drawing;
using MonoTouch.CoreGraphics;
...
public override void ViewDidLoad ()
{
	base.ViewDidLoad ();

	var linear = CPTScaleType.Linear;
	var graph = new CPTXYGraph (View.Frame, linear, linear) {
		Title = "App Sales",
		BackgroundColor = new CGColor (0.982f, 0.988f, 0.890f)
	};

	graph.AddPlot (new CPTScatterPlot { DataSource = new MyDataSource () });
	graph.DefaultPlotSpace.Scale (0.1f, PointF.Empty);

	View.AddSubview (new CPTGraphHostingView (View.Frame) {
		HostedGraph = graph
	});
}

// A simple data source for the plot
class MyDataSource : CPTScatterPlotDataSource
{
	static float [] Sales = { 0, 1, 1, 1, 3, 6, 4, 5, 8, 12 };

	public override int NumberOfRecordsForPlot (CPTPlot plot)
	{
		return Sales.Length;
	}

	public override NSNumber NumberForPlot (CPTPlot plot, CPTPlotField field, int index)
	{
		return field == CPTPlotField.ScatterPlotFieldX ? index : Sales [index];
	}
}
```

*Some component screenshots assembled with [PlaceIt](http://placeit.breezi.com/).*
