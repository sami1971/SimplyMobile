SimplyMobile
============

Collection of abstracted mobile functionalities. Xamarin.iOS, Xamarin.Android &amp; WP8.

Simple to use components and usage examples suitable also for new Xamarin developers. Device abstraction examples
help in understanding the differences between the platforms. Usage of the components are designed to be extremely
simple.

Example use of the static Battery class is the same from iOS, Android and WP8:

	var currentBatteryLevel = Battery.Level; // returns a unified 0-100 based on the battery level

	Battery.OnLevelChange += (sender, level) => {...subscribes to level change events...};

One of the most helpful classes is a data source designed to work with the base data observers (UITableView, ListView etc).
On Windows platforms many controls already use ObservableCollection as their data source so it was the natural base for
the class. Basic use (displaying textbox with the object value) does not require more than the below code. To display 
custom views only a few lines of code is needed (more in the wiki-pages).

	// create a new datasource from existing collection (optional)
	var dataSource = new ObservableDataSource<EditableText> (<insert enumerable list from f.e. SQLite database>);
	
	// bind a consumer, f.e. UITableView
	datasource.Bind(<data consumer>);
	
	// add new item, will automatically refresh the consumer
	dataSource.Add(new EditableText());
	
Project also includes some plugins using third party OSS libraries. For example IJsonSerializer has both JSON.Net and
ServiceStack.Text plugins allowing easy swap from one (de)serializer to another and of course the so important performance
tests. Recent addition is ServiceStack.OrmLite.SQLite libraries for both iOS and Android allowing easy access to SQLite 
databases (ServiceStack.OrmLite also supports many major databases on the desktop/server side).
